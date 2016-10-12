using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Common.Mappers;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LicenseSearchModel;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.Common;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Business.Licenses
{
    public class LicenseProductSolrManager : ILicenseSolrManager
    {

        private readonly ILicenseRepository _licenseRepository;
        private readonly ILicenseProductRepository _licenseProductRepository;
        private readonly ILicenseNoteRepository _licenseNoteRepository;
        private readonly ILicenseSequenceRepository _licenseSequenceRepository;
        private readonly ISearchProvider _solrSearchProvider;
        private readonly ILicensePRWriterRateRepository _licensePRWriterRateRepository;
        private readonly ILicenseProductRecordingRepository _licenseProductRecordingRepository;
        private readonly ILicensePRWriterRepository _licensePRWriterRepository;
        private readonly ILicensePRWriterRateStatusRepository _licensePRWriterRateStatusRepository;
        private readonly ILicenseUploadPreviewLicenseRepository _licenseUploadPreviewLicenseRepository;
        private readonly ILicenseProductConfigurationRepository _licenseProductConfigurationRepository;
        private readonly IRecs _recs;
        private readonly ISolrUpdate _solrUpdate;
        private Dictionary<string, Dictionary<string, object>> methodsCache = new Dictionary<string, Dictionary<string, object>>();
        private Dictionary<int, List<LicenseProductRecording>> licenseProductRecordingsCache = new Dictionary<int, List<LicenseProductRecording>>();
        public LicenseProductSolrManager(
            ILicenseRepository licenseRepository,
            ILicenseProductRepository licenseProductRepository,
            ILicenseNoteRepository licenseNoteRepository,
            ILicenseSequenceRepository licenseSequenceRepository,
            ISearchProvider solrSearchProvider,
            ILicensePRWriterRateRepository licensePRWriterRateRepository,
            ILicenseProductRecordingRepository licenseProductRecordingRepository,
            ILicensePRWriterRepository licensePRWriterRepository,
            ILicensePRWriterRateStatusRepository licensePRWriterRateStatusRepository,
            ILicenseUploadPreviewLicenseRepository licenseUploadPreviewLicenseRepository,
            IRecs recs,
            ILicenseProductConfigurationRepository licenseProductConfigurationRepository,
            ISolrUpdate solrUpdate
            )
        {
            _licenseRepository = licenseRepository;
            _licenseProductRepository = licenseProductRepository;
            _licenseNoteRepository = licenseNoteRepository;
            _licenseSequenceRepository = licenseSequenceRepository;
            _solrSearchProvider = solrSearchProvider;
            _licensePRWriterRateRepository = licensePRWriterRateRepository;
            _licenseProductRecordingRepository = licenseProductRecordingRepository;
            _licensePRWriterRepository = licensePRWriterRepository;
            _licensePRWriterRateStatusRepository = licensePRWriterRateStatusRepository;
            _licenseUploadPreviewLicenseRepository = licenseUploadPreviewLicenseRepository;
            _recs = recs;
            _solrUpdate = solrUpdate;
            _licenseProductConfigurationRepository = licenseProductConfigurationRepository;
            createMethodsCacheKey();
        }

        public void ClearCache()
        {
            methodsCache.Clear();
            licenseProductRecordingsCache.Clear();
            createMethodsCacheKey();
        }
        public bool UpdateLicense(int licenseId, bool updateProducts = true)
        {
            var license = _licenseRepository.Get(licenseId);
            var licenseSolrRequest = new LicenseSOLRUpdateRequest();
            var products = _licenseProductRepository.GetLicenseProducts(licenseId);

            var productsConfiguration = _licenseProductRepository.GetLicenseProductConfigurations(licenseId);
            var productsConfigurationList = productsConfiguration.Select(c => c.configuration_id).Distinct().ToList();
            
            var containsSample = false;
            var recsTracks = new List<WorksRecording>();
            var licenseProductsIds = products.Select(p => p.LicenseProductId).ToList();
            var productIds = products.Select(p => (long)p.ProductId).ToList();
            var licenseRecordings = GetLicenseProductRecording(licenseProductsIds);
            var allRateStatuses = new List<LicenseProductRecordingWritersRateStatusDropdown>();
            var allWriterRates = new List<LicenseProductRecordingWriterRate>();
            var allPipsCodes = new List<string>();
            var allConfigurations = new List<Configuration>();
            var allUpcs = new List<string>();
            var allReleaseDates = new List<string>();
            var allConfigurationIds = new List<long>();
            var allLabelGroups = new List<long>();
            var allLabels = new List<long>();
            var allLabelNames = new List<string>();
            licenseSolrRequest.PipsCode = new List<string>();
            licenseSolrRequest.PublisherIpCode = new List<string>();
            licenseSolrRequest.TrackTitle = new List<string>();
            licenseSolrRequest.Writer = new List<string>();
            licenseSolrRequest.ArtistId = new List<long>();
            licenseSolrRequest.ArtistName = new List<string>();
            licenseSolrRequest.LocalClientCode = new List<string>();
            licenseSolrRequest.ReleaseDate = new List<string>();
            licenseSolrRequest.Upc = new List<string>();
            licenseSolrRequest.ProductConfigurationIds = new List<long>();
            var productsSummaries = new List<RecsProductSummary>();
            if (productIds != null && productIds.Count > 0) productsSummaries = _recs.ProductSummary(productIds);
            foreach (var product in productsSummaries)
            {
                if (product.Tracks != null)
                {
                    foreach (var productTrack in product.Tracks)
                    {
                        if (productTrack.Works != null && productTrack.Works.Count > 0)
                        {
                            licenseSolrRequest.PipsCode.AddRange(
                                productTrack.Works.Where(w => w.PipsCode != null).Select(w => w.PipsCode).ToList());
                            foreach (var work in productTrack.Works)
                            {
                                if (work.PublisherIpCodes != null && work.PublisherIpCodes.Count > 0)
                                {
                                    licenseSolrRequest.PublisherIpCode.AddRange(work.PublisherIpCodes);
                                }
                                if (work.WriterNames != null && work.WriterNames.Count > 0)
                                {
                                    licenseSolrRequest.Writer.AddRange(work.WriterNames);
                                }
                                if (work.ContainsSample) containsSample = true;
                                if (work.LocalClients != null && work.LocalClients.Count > 0)
                                    licenseSolrRequest.LocalClientCode.AddRange(work.LocalClients.Select(l => l.ClientCode).ToList());
                            }
 
                            if (productTrack.Artist != null)
                            {
                                licenseSolrRequest.ArtistId.Add((long)productTrack.Artist.Id.Value);
                                licenseSolrRequest.ArtistName.Add(productTrack.Artist.Name);
                            }
                        }
                        licenseSolrRequest.TrackTitle.Add(productTrack.Title);
                    }
                }
                if (product.ProductConfigurations != null && product.ProductConfigurations.Count > 0 )
                {
                    product.ProductConfigurations = product.ProductConfigurations.Where(p => p.ConfigurationId.HasValue && productsConfigurationList.Contains(p.ConfigurationId.Value)).ToList();
                }
                if (product.ProductConfigurations != null && product.ProductConfigurations.Count > 0)
                {
                    var upcs = product.ProductConfigurations.Where(pc => pc.Upc != null).Select(pc => pc.Upc)
                        .Distinct().ToList();
                    if (upcs.Count > 0) allUpcs.AddRange(upcs);

                    var releaseDates =
                        product.ProductConfigurations.Where(pc => pc.ReleaseDate.HasValue)
                            .Select(pc => StringHelper.ToSolrDate(pc.ReleaseDate.Value))
                            .Distinct()
                            .ToList();
                    if (releaseDates.Count > 0) allReleaseDates.AddRange(releaseDates);
                    var configurationIds = product.ProductConfigurations.Select(pc => (long)pc.ConfigurationId).ToList();
                    if (configurationIds.Count > 0) allConfigurationIds.AddRange(configurationIds);
                }

                if (product.Artist != null && product.Artist.Id.HasValue)
                {
                    licenseSolrRequest.ArtistName.Add(product.Artist.Name);
                    licenseSolrRequest.ArtistId.Add(product.Artist.Id.Value);
                }
                if (product.Label != null)
                {
                    allLabels.Add(product.Label.Id);
                    allLabelNames.Add(product.Label.Name);
                    var labelGroupIds = product.Label.LabelGroups != null
                                        ? product.Label.LabelGroups.Select(lg => lg.Id).Distinct().ToList()
                                        : new List<long>();
                    if (labelGroupIds.Count > 0) allLabelGroups.AddRange(labelGroupIds);
                }
               
            }
            foreach (var licenseProductRecording in licenseRecordings)
            {
                var writers =
                    GetLicenseWriters(
                        licenseProductRecording.LicenseRecordingId);
                foreach (var licenseProductRecordingWriter in writers)
                {
                    var rates = GetWriterRates(licenseProductRecordingWriter.LicenseWriterId);
                    if (rates != null && rates.Count > 0)
                    {
                        var ratesIds = rates.Select(r => r.LicenseWriterRateId).ToList();
                        var ratesStatuses = _licensePRWriterRateStatusRepository.GetLicenseWriterRateStatusList(ratesIds);
                        if (ratesStatuses != null && ratesStatuses.Count > 0)
                            allRateStatuses.AddRange(ratesStatuses);
                        allWriterRates.AddRange(rates);
                    }
                }
            }
            licenseSolrRequest.WritersConsentTypeId =
                allWriterRates.Where(r => r.writersConsentTypeId.HasValue)
                    .Select(r => r.writersConsentTypeId.Value).Distinct()
                    .ToList();
            licenseSolrRequest.LabelGroupId = allLabelGroups.Distinct().ToList();
            licenseSolrRequest.PipsCode = licenseSolrRequest.PipsCode.Distinct().ToList();
            licenseSolrRequest.PublisherIpCode = licenseSolrRequest.PublisherIpCode.Distinct().ToList();
            licenseSolrRequest.TrackTitle = licenseSolrRequest.TrackTitle.Distinct().ToList();
            licenseSolrRequest.Writer = licenseSolrRequest.Writer.Distinct().ToList();
            licenseSolrRequest.ArtistId = licenseSolrRequest.ArtistId.Distinct().ToList();
            licenseSolrRequest.ArtistName = licenseSolrRequest.ArtistName.Distinct().ToList();
            licenseSolrRequest.LocalClientCode = licenseSolrRequest.LocalClientCode.Distinct().ToList();          
            licenseSolrRequest.ProductTitle = productsSummaries.Select(ph => ph.Title).ToList();
            licenseSolrRequest.MechsPriority = productsSummaries.Select(p => p.MechsPriority).Distinct().ToList();
            licenseSolrRequest.StatusReport = license.StatusReport;
            licenseSolrRequest.LicenseNumber = license.LicenseNumber;

            var artistName = "Various Artists";
            if (productsSummaries.Count == 1)
            {
                artistName = productsSummaries.First().Artist != null ? productsSummaries.First().Artist.Name : "";
            }
            licenseSolrRequest.ContainsSample = containsSample;
            licenseSolrRequest.ArtistNameSortable = artistName;
            licenseSolrRequest.LabelName = allLabelNames.Distinct().ToList();
            licenseSolrRequest.LabelId = allLabels.Distinct().ToList();
            licenseSolrRequest.LicenseMethodId = license.LicenseMethodId;
            licenseSolrRequest.Id = license.LicenseId;
            licenseSolrRequest.LicenseTitle = license.LicenseName;
            licenseSolrRequest.LicenseMethodNameSortable = license.LicenseMethod != null
                ? license.LicenseMethod.LicenseMethod
                : "";
            licenseSolrRequest.LicenseNumberSortable = license.LicenseNumber;
            licenseSolrRequest.LicenseAssigneeSortable = license.Contact.FullName;
            licenseSolrRequest.LicenseLicenseeId = license.Licensee.LicenseeId;
            licenseSolrRequest.LicenseeNameSortable = license.Licensee.Name;
            licenseSolrRequest.LicenseAssigneeId = license.Contact.ContactId;
            licenseSolrRequest.LicenseCreatedDate = StringHelper.ToSolrDateTime(license.CreatedDate.Value);
            licenseSolrRequest.LicenseModifiedDate = StringHelper.ToSolrDateTime(license.ModifiedDate.Value);
            licenseSolrRequest.LicenseSignedDate = license.SignedDate.HasValue
                ? StringHelper.ToSolrDateTime(license.SignedDate.Value)
                : null;
            licenseSolrRequest.LicenseStatusSortable = license.LicenseStatus != null
                ? license.LicenseStatus.LicenseStatus
                : null;
            licenseSolrRequest.LicenseTypeId = license.LicenseTypeId;
            licenseSolrRequest.LicenseTypeNameSortable = license.LicenseType != null
                ? license.LicenseType.LicenseType
                : null;
            licenseSolrRequest.LicenseeNameSortable = license.Licensee.Name;
            licenseSolrRequest.LicenseCreatedBySortable = license.Contact.FullName;
            licenseSolrRequest.ProductCount = licenseProductsIds.Count;
            licenseSolrRequest.Data = createLicenseData(license, productsSummaries);
            licenseSolrRequest.WriterSpecialStatusId = allRateStatuses.Select(rs => (long)rs.SpecialStatusId).Distinct().ToList();
            licenseSolrRequest.WriterRateTypeId = allWriterRates.Where(wr => wr.RateTypeId.HasValue).Select(wr => (long)wr.RateTypeId.Value).Distinct().ToList();
            licenseSolrRequest.WriterRate = allWriterRates.Select(wr => wr.Rate).Distinct().ToList();
            licenseSolrRequest.Upc = allUpcs.Distinct().ToList();
            licenseSolrRequest.ProductConfigurationIds = allConfigurationIds.Distinct().ToList();
            licenseSolrRequest.RolledUpConfiguration = null;
            licenseSolrRequest.ReleaseDate = allReleaseDates.Distinct().ToList();
            licenseSolrRequest.WriterSpecialStatusId =
                allRateStatuses.Where(rs => rs != null).Select(rs => (long)rs.SpecialStatusId).Distinct().ToList();
            licenseSolrRequest.LicenseStatusId = license.LicenseStatusId;
            _solrUpdate.UpdateLicense(licenseSolrRequest);
            if (updateProducts)
            {
                var productRequests = new List<ProductSolrUpdateRequest>();
                foreach (var product in productsSummaries)
                {
                    var productLicenses = _licenseRepository.GetProductLicenses(product.Id);
                    var updateRequest = createProductUpdateRequest(product, productLicenses);
                    productRequests.Add(updateRequest);
                }
                _solrUpdate.UpdateProduct(productRequests);
            }
            return true;
        }

        public bool UpdateLicenseStatus(int licenseId)
        {
            var request = new List<object>();
            dynamic updateFieldsRequest = new ExpandoObject();
            var license = _licenseRepository.Get(licenseId);
            var licenseProducts = _licenseProductRepository.GetLicenseProducts(licenseId);
            var productIds = licenseProducts.Select(lp => (long)lp.ProductId).ToList();
            var productSummaries = new List<RecsProductSummary>();
            if (licenseProducts.Count > 0) productSummaries = _recs.ProductSummary(productIds);
            updateFieldsRequest.Id = licenseId;
            updateFieldsRequest.LicenseStatusSortable = new SolrUpdateField
            {
                Set = license.LicenseStatus != null
                        ? license.LicenseStatus.LicenseStatus : null
            };
            updateFieldsRequest.LicenseStatusId = new SolrUpdateField
            {
                Set = license.LicenseStatusId
            };
            updateFieldsRequest.Data = new SolrUpdateField
            {
                Set = createLicenseData(license, productSummaries)
            };
            updateFieldsRequest.LicenseModifiedDate = new SolrUpdateField
            {
                Set = StringHelper.ToSolrDateTime(license.ModifiedDate.Value)
            };

            request.Add(updateFieldsRequest);
            _solrUpdate.UpdateLicenseFields(request);
            if (productIds.Count > 0) UpdateRelatedProductsStatus(productIds);
            return true;
        }

        public bool UpdateLicenseAssignee(List<int> licenseIds)
        {
            var request = new List<object>();
            var allProductIds = new List<long>();
            foreach (var licenseId in licenseIds)
            {
                dynamic updateFieldsRequest = new ExpandoObject();
                var license = _licenseRepository.Get(licenseId);
                var licenseProducts = _licenseProductRepository.GetLicenseProducts(licenseId);
                var productIds = licenseProducts.Select(lp => (long)lp.ProductId).ToList();
                var productSummaries = new List<RecsProductSummary>();
                if (productIds.Count > 0) productSummaries = _recs.ProductSummary(productIds);
                updateFieldsRequest.Id = licenseId;
                updateFieldsRequest.LicenseAssigneeId = new SolrUpdateField();
                updateFieldsRequest.LicenseAssigneeId.Set = license.Contact.ContactId;
                updateFieldsRequest.LicenseAssigneeSortable = new SolrUpdateField();
                updateFieldsRequest.LicenseAssigneeSortable.Set = license.Contact.FullName;
                updateFieldsRequest.LicenseModifiedDate = new SolrUpdateField();
                updateFieldsRequest.LicenseModifiedDate.Set = StringHelper.ToSolrDateTime(license.ModifiedDate.Value);
                updateFieldsRequest.Data = new SolrUpdateField();
                updateFieldsRequest.Data.Set = createLicenseData(license, productSummaries);
                request.Add(updateFieldsRequest);
                _solrUpdate.UpdateLicenseFields(request);
                if (productIds.Count > 0) allProductIds.AddRange(productIds);
            }
            if (allProductIds.Count > 0)
                UpdateRelatedProductsAssignee(allProductIds.Distinct().ToList());
            return true;

        }

        private List<Configuration> getLicenseProductConfigurations(int licenseProductid, ProductHeader productHeader)
        {
            var licenseProductConfigurations =
                   _licenseProductConfigurationRepository.GetLicenseProductConfigurations(
                       licenseProductid);
            var licenseConfigIds = licenseProductConfigurations.Select(x => x.configuration_id).ToList();
            var configurations =
                productHeader.Configurations.Where(x => licenseConfigIds.Contains(x.Configuration.ConfigId)).Select(x => x.Configuration).ToList();
            return configurations;
        }
        private void UpdateRelatedProductsStatus(List<long> productsIds)
        {
            var request = new List<object>();
            foreach (var productId in productsIds)
            {
                dynamic updateFieldsRequest = new ExpandoObject();
                var licenses = _licenseRepository.GetProductLicenses(productId);
                var licensesStatusIds = new List<int>();

                foreach (var license in licenses)
                {
                    licensesStatusIds.Add(license.LicenseStatusId);
                }
                updateFieldsRequest.Id = productId;
                updateFieldsRequest.LicenseStatusId = new SolrUpdateField
                {
                    Set = licensesStatusIds
                };
                request.Add(updateFieldsRequest);
            }
            _solrUpdate.UpdateProductFields(request);
        }
        private void UpdateRelatedProductsAssignee(List<long> productsIds)
        {
            var request = new List<object>();
            foreach (var productId in productsIds)
            {
                dynamic updateFieldsRequest = new ExpandoObject();
                var licenses = _licenseRepository.GetProductLicenses(productId);
                var assigneesIds = new List<int>();
                var assigneesNames = new List<string>();
                foreach (var license in licenses)
                {
                    assigneesIds.Add(license.Contact.ContactId);
                }
                updateFieldsRequest.Id = productId;
                updateFieldsRequest.LicenseAssigneeId = new SolrUpdateField();
                updateFieldsRequest.LicenseAssigneeId.Set = assigneesIds;
                request.Add(updateFieldsRequest);
            }
            _solrUpdate.UpdateProductFields(request);
        }
        public bool UpdateProduct(long productId, bool updateRelatedLicenses = true)
        {
            var request = new List<ProductSolrUpdateRequest>();
            var licenses = _licenseRepository.GetProductLicenses(productId);
            var productSummary = _recs.ProductSummary(new List<long> { productId });
            if (productSummary != null && productSummary.Count == 1)
            {
                var productSolrUpdateRequest = createProductUpdateRequest(productSummary.First(), licenses);
                request.Add(productSolrUpdateRequest);
                _solrUpdate.UpdateProduct(request);
                if (updateRelatedLicenses)
                {
                    foreach (var license in licenses)
                    {
                        UpdateLicense(license.LicenseId, false);
                    }
                }
                return true;
            }

            return false;

        }
        private ProductSolrUpdateRequest createProductUpdateRequest(RecsProductSummary product, List<License> licenses)
        {
            var request = new ProductSolrUpdateRequest();
            var licenseProducts = _licenseProductRepository.GetAllLicenseProducts(product.Id);
            var licenseProductsIds = licenseProducts.Select(p => p.LicenseProductId).ToList();
            var licenseRecordings = GetLicenseProductRecording(licenseProductsIds);
            var licenseWriters = new List<LicenseProductRecordingWriter>();
            var allRateStatuses = new List<LicenseProductRecordingWritersRateStatusDropdown>();
            var allWriterRates = new List<LicenseProductRecordingWriterRate>();
            var containsSample = false;

            foreach (var licenseProductRecording in licenseRecordings)
            {
                var writers =
                    GetLicenseWriters(
                        licenseProductRecording.LicenseRecordingId);
                foreach (var licenseProductRecordingWriter in writers)
                {
                    var rates = GetWriterRates(licenseProductRecordingWriter.LicenseWriterId);
                    if (rates != null && rates.Count > 0)
                    {
                        allWriterRates.AddRange(rates);
                        var ratesIds = rates.Select(r => r.LicenseWriterRateId).ToList();
                        var ratesStatuses = _licensePRWriterRateStatusRepository.GetLicenseWriterRateStatusList(ratesIds);
                        if (ratesStatuses != null && ratesStatuses.Count > 0)
                            allRateStatuses.AddRange(ratesStatuses);
                    }

                }

            }
            if (product.Artist != null)
            {
                request.ArtistNameSortable = product.Artist.Name;
            }
            request.WritersConsentTypeId =
                allWriterRates.Where(r => r.writersConsentTypeId.HasValue)
                    .Select(r => r.writersConsentTypeId.Value)
                    .Distinct()
                    .ToList();
            request.PipsCode = new List<string>();
            request.PublisherIpCode = new List<string>();
            request.TrackTitle = new List<string>();
            request.Writer = new List<string>();
            request.ArtistId = new List<long>();
            request.ArtistName = new List<string>();
            request.LocalClientCode = new List<string>();
            request.MechsPriority = product.MechsPriority;
            var statusReport = licenses.Where(l => l.StatusReport).Count() > 0 ? true : false;
            request.StatusReport = statusReport;
            request.LicenseNumber = licenses.Select(l => l.LicenseNumber).Distinct().ToList();
            if (product.Tracks != null)
            {
                request.TrackCount = product.Tracks.Count;
                foreach (var productTrack in product.Tracks)
                {
                    if (productTrack.Works != null && productTrack.Works.Count > 0)
                    {
                        request.PipsCode.AddRange(
                            productTrack.Works.Where(w => w.PipsCode != null).Select(w => w.PipsCode).ToList());
                        var clientCodes = new List<string>();
                        foreach (var work in productTrack.Works)
                        {
                            if (work.PublisherIpCodes != null && work.PublisherIpCodes.Count > 0)
                            {
                                request.PublisherIpCode.AddRange(work.PublisherIpCodes);
                            }
                            if (work.WriterNames != null && work.WriterNames.Count > 0)
                            {
                                request.Writer.AddRange(work.WriterNames);
                            }
                            if (work.LocalClients != null && work.LocalClients.Count > 0)
                                request.LocalClientCode.AddRange(work.LocalClients.Select(l => l.ClientCode).ToList());
                            if (work.ContainsSample) containsSample = true;

                        }
                        if (productTrack.Artist != null)
                        {
                            request.ArtistId.Add((long)productTrack.Artist.Id.Value);
                            request.ArtistName.Add(productTrack.Artist.Name);
                        }
                    }
                    request.TrackTitle.Add(productTrack.Title);
                }
              
            }
            if (product.ProductConfigurations != null && product.ProductConfigurations.Count > 0)
            {
                request.Upc = product.ProductConfigurations.Where(pc => pc.Upc != null).Select(pc => pc.Upc)
                    .Distinct().ToList();
                request.ReleaseDate =
                    product.ProductConfigurations.Where(pc => pc.ReleaseDate.HasValue)
                        .Select(pc => StringHelper.ToSolrDate(pc.ReleaseDate.Value))
                        .Distinct()
                        .ToList();
                request.ProductConfigurationIds = product.ProductConfigurations.Select(pc => (long)pc.ConfigurationId).ToList();

            }
            if (product.Artist != null && product.Artist.Id.HasValue)
            {
                request.ArtistId.Add(product.Artist.Id.Value);
                request.ArtistName.Add(product.Artist.Name);
            }
            request.PipsCode = request.PipsCode.Distinct().ToList();
            request.PublisherIpCode = request.PublisherIpCode.Distinct().ToList();
            request.TrackTitle = request.TrackTitle.Distinct().ToList();
            request.Writer = request.Writer.Distinct().ToList();
            request.ArtistId = request.ArtistId.Distinct().ToList();
            request.ArtistName = request.ArtistName.Distinct().ToList();
            request.LocalClientCode = request.LocalClientCode.Distinct().ToList();
            request.ProductTitle = product.Title;
            request.HfaLicenseNumber =
                licenses.Where(l => l.HFARollupLicenseId != null).Select(l => l.HFARollupLicenseId).Distinct().ToList();
            request.Id = product.Id;
            var solrProduct = new LicenseProductSolr
            {
                Artist = new LicenseIdName
                {
                    Id = product.Artist != null ? product.Artist.Id : null,
                    Name = product.Artist != null ? product.Artist.Name : null
                },
                Id = product.Id,
                Label = product.Label != null ? new LicenseIdName
                {
                    Id = product.Label.Id,
                    Name = product.Label.Name
                } : null,
                LicenseCount = licenses.Count,
                Licenses = new List<object>(),
                ProductConfigurations = new List<LicenseProductConfigurationSolr>(),
                Title = product.Title,
                TrackCount = product.Tracks != null ? product.Tracks.Count : 0
            };
            request.LabelName = new List<string>();
            request.LabelId = new List<long>();
            if (product.Label != null)
            {
                request.LabelName.Add(product.Label.Name);
                request.LabelId.Add(product.Label.Id);
            }
            request.ContainsSample = containsSample;
            request.LicenseMethodId = licenses.Select(l => (long)l.LicenseMethodId).Distinct().ToList();
            request.LicenseTitle = licenses.Select(l => l.LicenseName).Distinct().ToList();
            request.LicenseAssigneeId = licenses.Select(l => (long)l.Contact.ContactId).Distinct().ToList();
            request.LicenseSignedDate = licenses.Where(l => l.SignedDate.HasValue).Select(l => StringHelper.ToSolrDate(l.SignedDate.Value)).Distinct().ToList();
            request.LicenseTypeId = licenses.Select(l => (long)l.LicenseTypeId).Distinct().ToList();
            request.LicenseCount = licenses.Count;
            request.LicenseLicenseeId = licenses.Select(l => l.LicenseeId).Distinct().ToList();
            request.LabelGroupId = product.Label != null && product.Label.LabelGroups != null
                ? product.Label.LabelGroups.Select(lg => lg.Id).Distinct().ToList()
                : new List<long>();
            request.WriterRateTypeId = allWriterRates.Where(wr => wr.RateTypeId.HasValue).Select(wr => (long)wr.RateTypeId.Value).Distinct().ToList();
            request.WriterRate = allWriterRates.Select(wr => wr.Rate).Distinct().ToList();
            request.LicenseStatusId = licenses.Select(l => l.LicenseStatusId).Distinct().ToList();
            request.Data = JsonConvert.SerializeObject(solrProduct);
            request.WriterSpecialStatusId =
                allRateStatuses.Where(rs => rs != null).Select(rs => (long)rs.SpecialStatusId).Distinct().ToList();
            return request;
        }
        private List<ProductHeader> getProductsHeaders(List<int> productIds)
        {
            var productHeaders = new List<ProductHeader>();
            foreach (var productId in productIds)
            {
                productHeaders.Add(GetProductHeader(productId));
            }
            return productHeaders;
        }
        private string createLicenseData(License license, List<RecsProductSummary> productSummaries)
        {
            var licenseSearchData = new LicenseSOLR();
            licenseSearchData.Assignee = new LicenseIdName
            {
                Id = license.Contact.ContactId,
                Name = license.Contact.FullName
            };
            if (license.Contact2 != null)
            {
                licenseSearchData.CreatedBy = new LicenseIdName
                {
                    Id = license.Contact2.ContactId,
                    Name = license.Contact2.FullName
                };
            }
            licenseSearchData.CreatedDate = license.CreatedDate.Value;
            licenseSearchData.Id = license.LicenseId;
            licenseSearchData.LicenseMethod = new LicenseIdName
            {
                Id = license.LicenseMethod.LicenseMethodId,
                Name = license.LicenseMethod.LicenseMethod
            };
            licenseSearchData.LicenseName = license.LicenseName;
            licenseSearchData.LicenseNumber = license.LicenseNumber;
            licenseSearchData.Licensee = new LicenseIdName
            {
                Id = license.Licensee.LicenseeId,
                Name = license.Licensee.Name
            };
            licenseSearchData.ModifiedDate = license.ModifiedDate.Value;
            licenseSearchData.Priority = new LicenseIdName
            {
                Id = license.LicensePriority.PriorityId,
                Name = license.LicensePriority.Priority
            };
            licenseSearchData.ProductCount = productSummaries.Count;
            licenseSearchData.SignedDate = license.SignedDate;
            licenseSearchData.Status = new LicenseIdName
            {
                Id = license.LicenseStatus.LicenseStatusId,
                Name = license.LicenseStatus.LicenseStatus
            };
            licenseSearchData.Type = new LicenseIdName
            {
                Id = license.LicenseType.LicenseTypeId,
                Name = license.LicenseType.LicenseType
            };
            var artistName = "Various Artists";
            if (productSummaries.Count == 1)
            {
                artistName = productSummaries.First().Artist != null ? productSummaries.First().Artist.Name : "";
            }
            var configuration = license.LicenseConfigurationRollup;
            licenseSearchData.RolledUpConfiguration = configuration;
            licenseSearchData.Artists = artistName;
            return JsonConvert.SerializeObject(licenseSearchData);
        }
        private void createMethodsCacheKey()
        {
            methodsCache.Add("GetLicenseWriters", new Dictionary<string, object>());
            methodsCache.Add("GetWriterRates", new Dictionary<string, object>());
            methodsCache.Add("GetRecsWriters", new Dictionary<string, object>());
            methodsCache.Add("GetProductHeader", new Dictionary<string, object>());
            methodsCache.Add("RetrieveTracks", new Dictionary<string, object>());
        }

        private ProductHeader GetProductHeader(int id)
        {
            if (!methodsCache["GetProductHeader"].ContainsKey(id.ToString()))
            {
                var result = _recs.RetrieveProductHeader(id);
                methodsCache["GetProductHeader"].Add(id.ToString(), result);
                return result;
            }
            else
            {
                return (ProductHeader)methodsCache["GetProductHeader"][id.ToString()];
            }
        }

        private List<LicenseProductRecordingWriter> GetLicenseWriters(int licenseRecordingId)
        {
            if (!methodsCache["GetLicenseWriters"].ContainsKey(licenseRecordingId.ToString()))
            {
                var result = _licensePRWriterRepository.GetLicenseProductRecordingWriters(licenseRecordingId);
                methodsCache["GetLicenseWriters"].Add(licenseRecordingId.ToString(), result);
                return result;
            }
            else
            {
                return (List<LicenseProductRecordingWriter>)methodsCache["GetLicenseWriters"][licenseRecordingId.ToString()];
            }
        }
        private List<LicenseProductRecordingWriterRate> GetWriterRates(int licenseWriterId)
        {
            if (!methodsCache["GetWriterRates"].ContainsKey(licenseWriterId.ToString()))
            {
                var result = _licensePRWriterRateRepository.GetLicenseProductRecordingWriterRates(licenseWriterId);
                methodsCache["GetWriterRates"].Add(licenseWriterId.ToString(), result);
                return result;
            }
            else
            {
                return (List<LicenseProductRecordingWriterRate>)methodsCache["GetWriterRates"][licenseWriterId.ToString()];
            }

        }
        private List<LicenseProductRecording> GetLicenseProductRecording(List<int> ids)
        {
            var notCached = ids.Where(x => !licenseProductRecordingsCache.ContainsKey(x)).ToList();
            if (notCached.Count > 0)
            {
                var results = _licenseProductRecordingRepository.GetLicenseRecordingsList(notCached);
                foreach (var licenseProductId in notCached)
                {
                    var licenseProducts = results.Where(x => x.LicenseProductId == licenseProductId).ToList();
                    licenseProductRecordingsCache.Add(licenseProductId, licenseProducts);
                }
            }
            var licenseProductRecordings = new List<LicenseProductRecording>();
            foreach (var id in ids)
            {
                licenseProductRecordings.AddRange(licenseProductRecordingsCache[id]);
            }
            return licenseProductRecordings;
        }
        private List<WorksRecording> RetrieveTracks(int id)
        {
            if (!methodsCache["RetrieveTracks"].ContainsKey(id.ToString()))
            {
                var result = _recs.RetrieveTracksWithWriters(id);
                methodsCache["RetrieveTracks"].Add(id.ToString(), result);
                return result;
            }
            else
            {
                return (List<WorksRecording>)methodsCache["RetrieveTracks"][id.ToString()];
            }
        }
    }
}