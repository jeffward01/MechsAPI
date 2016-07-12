using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Amazon.S3.Model;
using UMPG.USL.API.Data;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LicenseTemplateModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.StaticDropdownsData;
using NLog;
using NLog.Config;
using NLog.Targets;
using UMPG.USL.Common;

namespace UMPG.USL.API.Business.Licenses
{
    
    public class LicenseProductManager : ILicenseProductManager
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly ILicenseSequenceRepository _licenseSequenceRepository;
        private readonly ILicenseProductRepository _licenseProductRepository;
        private readonly ILicenseProductRecordingRepository _licenseProductRecordingRepository;
        private readonly ILicensePRWriterRepository _licensePRWriterRepository;
        private readonly ILicensePRWriterRateRepository _licensePRWriterRateRepository;
        //        private readonly ILicensePRWriterRateNoteRepository _licensePRWriterRateNoteRepository;
        private readonly ILicensePRWriterNoteRepository _licensePRWriterNoteRepository;
        private readonly ILicensePRWriterRateStatusRepository _licensePRWriterRateStatusRepository;
        private readonly ILicenseProductConfigurationRepository _licenseProductConfigurationRepository;
        private readonly IRecsDataProvider _recsProvider;
        private readonly ILicenseNoteRepository _licenseNoteRepository;
        private readonly IAgreementStatutoryRateRepository _agreementStatutoryRateRepository;
        private readonly IStatRateRepository _statRateRepository;
        private readonly ILicenseSolrManager _licenseSolrManager;
        private readonly ILicenseAttachmentManager _licenseAttachmentManager;
        private readonly IRecordingMedleyRepository _medleyRepository;
        private readonly IRecs _recsRepository;

        private TimeSpan _ts5minutes = new TimeSpan(0, 5, 0);

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public LicenseProductManager(
            ILicenseRepository licenseRepository,
            ILicenseSequenceRepository licenseSequenceRepository,
            ILicenseProductRepository licenseProductRepository,
            ILicenseProductRecordingRepository licenseProductRecordingRepository,
            ILicensePRWriterRepository licensePRWriterRepository,
            ILicensePRWriterRateRepository licensePRWriterRateRepository,
            //ILicensePRWriterRateNoteRepository licensePRWriterRateNoteRepository,
            ILicensePRWriterNoteRepository licensePRWriterNoteRepository,
            ILicensePRWriterRateStatusRepository licensePRWriterRateStatusRepository,
            ILicenseProductConfigurationRepository licenseProductConfigurationRepository,
            IRecsDataProvider recsProvider,
            ILicenseNoteRepository licenseNoteRepository,
            IAgreementStatutoryRateRepository agreementStatutoryRateRepository,
            IStatRateRepository statRateRepository,
            ILicenseSolrManager licenseSolrManager,
            ILicenseAttachmentManager licenseAttachmentManager,
            IRecordingMedleyRepository medleyRepository,
            IRecs recsRepository)
        {
            _licenseRepository = licenseRepository;
            _licenseSequenceRepository = licenseSequenceRepository;
            _licenseProductRepository = licenseProductRepository;
            _licenseProductRecordingRepository = licenseProductRecordingRepository;
            _licensePRWriterRepository = licensePRWriterRepository;
            _licensePRWriterRateRepository = licensePRWriterRateRepository;
            //_licensePRWriterRateNoteRepository = licensePRWriterRateNoteRepository;
            _licensePRWriterNoteRepository = licensePRWriterNoteRepository;
            _licensePRWriterRateStatusRepository = licensePRWriterRateStatusRepository;
            _licenseProductConfigurationRepository = licenseProductConfigurationRepository;
            _recsProvider = recsProvider;
            _licenseNoteRepository = licenseNoteRepository;
            _agreementStatutoryRateRepository = agreementStatutoryRateRepository;
            _statRateRepository = statRateRepository;
            _licenseSolrManager = licenseSolrManager;
            _licenseAttachmentManager = licenseAttachmentManager;
            _medleyRepository = medleyRepository;
            _recsRepository = recsRepository;
        }



        public List<ProductConfiguration> GetProductConfigurationsAll(GetProductConfigurationsAllRequest request)
        {

            var productConfigurationsList = new List<ProductConfiguration>();
            var productIds = request.ProductIds;
            var licenseId = request.LicenseId;

            foreach (var productId in productIds)
            {
                //ProductConfiguration
                //todo: Steve talk
                var productConfigurations = new List<ProductConfiguration>();
                //_productRepository.GetRecsProductConfiguration(productId);
                var licenseProduct = _licenseProductRepository.GetLicenseProduct(licenseId, productId);
                if (licenseProduct != null)
                {
                    foreach (var productConfiguration in productConfigurations)
                    {
                        productConfiguration.LicenseProductConfiguration =
                            _licenseProductConfigurationRepository.GetLicenseProductConfiguration(
                                licenseProduct.LicenseProductId, (int)productConfiguration.product_configuration_id);
                    }
                }
                productConfigurationsList.AddRange(productConfigurations);
            }

            return productConfigurationsList;
        }


        public bool UpdateProductConfigurationsAll(List<UpdateProductConfigurationsAllRequest> requests)
        {
            var result = false;
            var i = 0;
            foreach (var request in requests)
            {
                i++;
                var licenseProduct = _licenseProductRepository.GetLicenseProduct(request.LicenseId, request.ProductId);

                if (licenseProduct == null)
                {
                    var newlicenseProduct = new LicenseProduct
                    {
                        LicenseId = request.LicenseId,
                        ProductId = request.ProductId,
                        CreatedDate = DateTime.Now,
                        ScheduleId = i,
                        CreatedBy = 1,
                    };
                    licenseProduct = _licenseProductRepository.Add(newlicenseProduct);
                }

                var newLicenseProductConfiguration = new LicenseProductConfiguration
                {
                    LicenseProductId = licenseProduct.LicenseProductId,
                    configuration_id = request.ConfigurationId,
                    configuration_name = request.ConfigurationName,
                    product_configuration_id = request.ProductConfigurationId,
                    PriorityReport = false,
                    StatusReport = false,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1,
                };

                var licenseProductConfiguration =
                    _licenseProductConfigurationRepository.Add(newLicenseProductConfiguration);

                /*
                var productRecordings = _recordingLinkRepository.GetProductRecordings(request.ProductId);
                foreach (var productRecording in productRecordings)
                {
                    var newLicenseRecording = new LicenseRecording

                }
                */

                // public List<ProductRecordingLink> GetProductRecordings(int productId)
                // tbd - add tracks, add writers
            }

            return result;
        }

        public bool DeleteLicenseProduct(int licenseId, int productId)
        {
            var rv = false;

            var modifiedDate = DateTime.Now;
            var modifiedBy = 1;

            var licenseProduct = _licenseProductRepository.GetLicenseProduct(licenseId, productId);
            if (licenseProduct != null)
            {
                var licenseProductIds = new List<int> { licenseProduct.LicenseProductId };
                var licenseProductRecordings =
                    _licenseProductRecordingRepository.GetLicenseRecordingsList(licenseProductIds);

                foreach (var licenseProductRecording in licenseProductRecordings)
                {
                    var licenseWriters =
                        _licensePRWriterRepository.GetLicenseWriters(licenseProductRecording.LicenseRecordingId);
                    //var licenseWriterIds = new List<int>();
                    var licenseWriterIds = licenseWriters.Select(w => w.LicenseWriterId).Distinct().ToList();

                    var licenseWriterRateIds = new List<int>();

                    foreach (var licenseWriterId in licenseWriterIds)
                    {
                        var licenseWriterRates =
                            _licensePRWriterRateRepository.GetLicenseProductRecordingWriterRates(licenseWriterId);
                        foreach (var licenseWriterRate in licenseWriterRates)
                        {
                            if (!licenseWriterRateIds.Contains(Convert.ToInt32(licenseWriterRate.LicenseWriterRateId)))
                            {
                                licenseWriterRateIds.Add(licenseWriterRate.LicenseWriterRateId);
                            }
                            // LicenseWriterRate
                            licenseWriterRate.Deleted = modifiedDate;
                            licenseWriterRate.ModifiedBy = modifiedBy;
                            licenseWriterRate.ModifiedDate = modifiedDate;
                            _licensePRWriterRateRepository.Update(licenseWriterRate);
                        }
                    }

                    var licenseWriterRateStatuses = _licensePRWriterRateStatusRepository.GetLicenseWriterRateStatus(licenseWriterRateIds);
                    foreach (var licenseWriterRateStatus in licenseWriterRateStatuses)
                    {
                        // LicenseWriterRateStatus
                        licenseWriterRateStatus.Deleted = modifiedDate;
                        licenseWriterRateStatus.ModifiedBy = modifiedBy;
                        licenseWriterRateStatus.ModifiedDate = modifiedDate;
                        _licensePRWriterRateStatusRepository.Update(licenseWriterRateStatus);

                    }

                    var licenseWriterNotes = _licensePRWriterNoteRepository.GetLicenseProductRecordingWriterNotes(licenseWriterIds);
                    foreach (var licenseWriterNote in licenseWriterNotes)
                    {
                        // LicenseWriterRateNote
                        licenseWriterNote.Deleted = modifiedDate;
                        licenseWriterNote.ModifiedBy = modifiedBy;
                        licenseWriterNote.ModifiedDate = modifiedDate;
                        _licensePRWriterNoteRepository.Update(licenseWriterNote);
                    }


                    foreach (var licenseWriter in licenseWriters)
                    {
                        //  LicenseWriter
                        licenseWriter.Deleted = modifiedDate;
                        licenseWriter.ModifiedBy = modifiedBy;
                        licenseWriter.ModifiedDate = modifiedDate;
                        _licensePRWriterRepository.Update(licenseWriter);

                    }

                    //  License Recording
                    licenseProductRecording.Deleted = modifiedDate;
                    licenseProductRecording.ModifiedBy = modifiedBy;
                    licenseProductRecording.ModifiedDate = modifiedDate;
                    _licenseProductRecordingRepository.Update(licenseProductRecording);

                }

                //LicenseProductConfiguration
                var licenseProductConfigurations =
                    _licenseProductConfigurationRepository.GetLicenseProductConfigurations(
                        licenseProduct.LicenseProductId);
                foreach (var licenseProductConfiguration in licenseProductConfigurations)
                {
                    licenseProductConfiguration.Deleted = modifiedDate;
                    licenseProductConfiguration.ModifiedBy = modifiedBy;
                    licenseProductConfiguration.ModifiedDate = modifiedDate;
                    _licenseProductConfigurationRepository.Update(licenseProductConfiguration);
                }

                // LicenseProduct
                licenseProduct.Deleted = modifiedDate;
                licenseProduct.ModifiedDate = modifiedDate;
                licenseProduct.ModifiedBy = modifiedBy;
                _licenseProductRepository.Update(licenseProduct);

                rv = true;
            }


            UpdateLicenseProductRollups(licenseId);

            return rv;


        }

        public void UpdateLicenseProductRollups(int licenseId)
        {
            var artistRollup = string.Empty;
            var configRollup = string.Empty;
            var artistsDone = false;
            var configsDone = false;
            var products = _licenseProductRepository.GetLicenseProducts(licenseId);

            foreach (var licenseProduct in products)
            {
                var productHeader = _recsProvider.RetrieveProductHeader(licenseProduct.ProductId);

                if (!artistsDone)
                {
                    if (artistRollup.Length == 0)
                    {
                        artistRollup = productHeader.Artist.name;
                    }
                    else if (artistRollup != productHeader.Artist.name)
                    {
                        artistRollup = "Various Artists";
                        artistsDone = true;
                    }
                }

                //return P=Phyisical, D=Digitial, B=Both
                if (!configsDone)
                {
                    foreach (var config in productHeader.Configurations)
                    {
                        // values can be ALL, DIGITAL, PHYSICAL, NONE
                        var c = config.Configuration.type.Substring(0, 1);
                        if (c == "A")
                        {
                            configRollup = "B";
                            configsDone = true;
                            break;
                        }
                        else if (c != "N")
                        {
                            if (configRollup.Length == 0)
                            {
                                configRollup = c;
                            }
                            else
                            {
                                if (configRollup != c)
                                {
                                    configRollup = "B";
                                    configsDone = true;
                                    break;
                                }
                            }
                        }

                    }
                }

                //if already at artistRollup=Various Artists and configRollup=B we can exit as they will not change
                if (artistsDone && configsDone)
                {
                    break;
                }
            }

            var license = _licenseRepository.GetLite(licenseId);
            var updateLicense = false;
            if (license.ArtistRollup != artistRollup)
            {
                license.ArtistRollup = artistRollup;
                updateLicense = true;
            }
            if (license.LicenseConfigurationRollup != configRollup)
            {
                license.LicenseConfigurationRollup = configRollup;
                updateLicense = true;
            }
            if (updateLicense)
            {
                _licenseRepository.UpdateLicense(license);
            }

        }


        public LicenseProduct GetSelectedProduct(int licenseId, int productId)
        {

            var licenseProduct = new LicenseProduct();

            if (licenseId > 0)
            {
                // first make sure the product is really in the license
                licenseProduct = _licenseProductRepository.GetLicenseProduct(licenseId, productId);
                if (licenseProduct == null)
                {
                    licenseProduct = new LicenseProduct();
                    licenseProduct.LicenseProductId = 0;
                    licenseProduct.LicenseId = 0;
                    licenseProduct.ProductId = productId;
                    licenseProduct.LicensePRecordingsNo = 0;

                    var productHeader = _recsProvider.RetrieveProductHeader(productId);
                    licenseProduct.ProductHeader = productHeader;
                    licenseProduct.title = productHeader.Title;
                }
                else
                {
                    licenseProduct.ProductHeader = _recsProvider.RetrieveProductHeader(licenseProduct.ProductId);

                    for (int i = 0; i < licenseProduct.ProductHeader.Configurations.Count; i++)
                    {
                        var licenseProductConfiguration =
                            _licenseProductConfigurationRepository.GetLicenseProductConfiguration(
                                (int)licenseProduct.LicenseProductId,
                                (int)licenseProduct.ProductHeader.Configurations[i].configuration_id);
                        if (licenseProductConfiguration != null)
                        {
                            licenseProductConfiguration.TotalAmount = 0;
                            licenseProductConfiguration.LicensedAmount = 0;
                            licenseProductConfiguration.NotLicensedAmount = 0;
                            licenseProduct.ProductHeader.Configurations[i].LicenseProductConfiguration =
                                licenseProductConfiguration;
                        }
                    }
                }

            }
            else
            {
                licenseProduct.LicenseProductId = 0;
                licenseProduct.LicenseId = 0;
                licenseProduct.ProductId = productId;
                licenseProduct.LicensePRecordingsNo = 0;

                var productHeader = _recsProvider.RetrieveProductHeader(productId);
                licenseProduct.ProductHeader = productHeader;
                licenseProduct.title = productHeader.Title;

            }

            return licenseProduct;

        }

        public List<LicenseProductConfigurationTotals> GetLicenseProductConfigurationIdTotals(int licenseProductId)
        {
            var configList = new List<LicenseProductConfigurationTotals>();
            var configIds = _licenseProductConfigurationRepository.GetLicenseProductConfigurationIds(licenseProductId);
            foreach (var configId in configIds)
            {
                configList.Add(new LicenseProductConfigurationTotals { configuration_id = configId, LicensedAmount = 0.0 });
            }
            return configList;
        }

        private int getLicensedPRWriterCount(List<WorksWriter> composers)
        {
            var licensedcnt = 0;
            foreach (var writer in composers)
            {
                if (writer.Controlled)
                {
                    licensedcnt = licensedcnt + 1;
                }
            }
            return licensedcnt;
        }

        public List<LicenseProduct> GetProductsNew(int licenseId)
        {

            // get license
            //Fix #3 pass this into Details routine to avoid another call
            var license = _licenseRepository.GetLite(licenseId);

            bool isExecuted = license.LicenseStatusId == 5 || license.LicenseStatusId == 7;
            
            var claimExceptionCount = 0;

            var begintime = DateTime.Now;
            var starttime = DateTime.Now;
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("--GetProductsNew (CURRENT) starts-------------");
            logger.Debug("----StartTime :" + starttime.ToString());

            var totalpercentage = 0.0;
            var licensedpercentage = 0.0;
            var actualContribution = 0.0;

            starttime = DateTime.Now;
            // Mechs LicenseProducts
            var products = _licenseProductRepository.GetLicenseProducts(licenseId);  //same hit
            logger.Debug(".End GetLicenseProducts (" + licenseId + ") : elapsed :" + (DateTime.Now - starttime).ToString());

            foreach (var licenseProduct in products)
            {

                starttime = DateTime.Now;
                //RECS get Product Header
                licenseProduct.ProductHeader = _recsProvider.RetrieveProductHeader(licenseProduct.ProductId); // same hit
                logger.Debug("..End RetrieveProductHeader (" + licenseProduct.ProductId + ") : elapsed :" + (DateTime.Now - starttime).ToString());

                // Mechs - get the count of all the Licenses tied to ProductId
                licenseProduct.RelatedLicensesNo =_licenseProductRepository.GetLicensesNo(licenseProduct.ProductId); //same hit
                licenseProduct.RelatedLicenseIds = _licenseProductRepository.GetLicenseIds(licenseProduct.ProductId);
                //add licenseProduct . relatedLicenseId


                //move this down a few lines and just return count
                //licenseProduct.LicensePRecordingsNo =_licenseProductRecordingRepository.GetLicenseProductRecordingsNo(licenseProduct.LicenseProductId); //THis was replaced with a count

                totalpercentage = 0.0;
                licensedpercentage = 0.0;
                actualContribution = 0.0;
                var total = 0.0;
                var licensed = 0.0;
                licenseProduct.Message = new List<string>();

                List<LicenseProductConfigurationTotals> licenseProductConfigIdTotals;

                starttime = DateTime.Now;
  //Call to Details              
                //fix #3 pass in the license object (details was calling it again)
                //fix #4 pass in the LicenseProduct so you dont need to call again
                licenseProduct.Recordings = this.GetLicenseProductRecordingsForLicenseDetailsNew(licenseProduct.LicenseProductId,license, licenseProduct, out licenseProductConfigIdTotals);

                //Fix #1 - Moved this to the details when LicenseRecordings are looked up
                //licenseProduct.LicensePRecordingsNo = licenseProduct.Recordings.Count;

                logger.Debug("***End GetLicenseProductRecordingsForLicenseDetails (" + licenseProduct.LicenseProductId + ") : elapsed :" + (DateTime.Now - starttime).ToString());

                // grab totals for percentages for each recording, note; this code already being done from Fix #2 below
                foreach (var rec in licenseProduct.Recordings)
                {
                    if (rec.Track.ClaimException)
                    {
                        licenseProduct.LicenseClaimException = "CLAIM EXCEPTION";
                        claimExceptionCount++;
                    }
                    totalpercentage = totalpercentage + rec.UmpgPercentageRollup;
                    total = total + rec.UmpgPercentageRollup;
                    licensed = licensed + rec.LicensedRollup;
                }
                licenseProduct.TotalLicenseConfigAmount = (licensed * 100) / total;
                for (int i = 0; i < licenseProduct.ProductHeader.Configurations.Count; i++)
                {
                    var licenseProductConfiguration =
                        _licenseProductConfigurationRepository.GetLicenseProductConfiguration(
                            (int)licenseProduct.LicenseProductId,
                            (int)licenseProduct.ProductHeader.Configurations[i].configuration_id);
                    if (licenseProductConfiguration != null)
                    {

                        // 20160526 - per Ryan display the total product config amount
                        //licenseProductConfiguration.TotalAmount = Convert.ToDecimal(totalpercentage / licenseProductConfigIdTotals.Count);
                        licenseProductConfiguration.TotalAmount = Convert.ToDecimal(totalpercentage);
                        //licenseProductConfiguration.LicensedAmount = Convert.ToDecimal(licensedpercentage);
                        //licenseProductConfiguration.NotLicensedAmount = Convert.ToDecimal(totalpercentage - licensedpercentage);
                        foreach (var licenseProductConfigIdTotal in licenseProductConfigIdTotals)
                        {
                            if (licenseProductConfigIdTotal.configuration_id == licenseProductConfiguration.configuration_id)
                            {
                                licenseProductConfiguration.LicensedAmount = Convert.ToDecimal(licenseProductConfigIdTotal.LicensedAmount / licenseProductConfigIdTotals.Count);
                                licenseProductConfiguration.NotLicensedAmount = Convert.ToDecimal((totalpercentage - licenseProductConfigIdTotal.LicensedAmount) / licenseProductConfigIdTotals.Count);
                            }
                        }

                        licenseProduct.ProductHeader.Configurations[i].LicenseProductConfiguration =
                            licenseProductConfiguration;
                    }
                }

            }

            //fix #2 this is already done from the Product loop above... comment this code out.
            //if (claimExceptionCount > 0)
            //{
            //    foreach (var licenseProduct in products)
            //    {
            //        licenseProduct.LicenseClaimException = "CLAIM EXCEPTION";
            //    }
            //}


            logger.Debug("GetProductsNew (__CURRENT__) total elapsed :" + (DateTime.Now - begintime).ToString());
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            return products;       




        }
        //http://spa.service/api/licenseProductCTRL/licenseproducts/GetLicenseProductOverview/2201 Test call
        public LicenseProductOverview GetLicenseProductOverview(int recProductId)
        {
            LicenseProductOverview licenseProductOverview = new LicenseProductOverview();
            LicenseProduct licenseProduct = _licenseProductRepository.GetLicenseProduct(recProductId);
            //if (licenseProduct == null)
            //{
            //    throw new Exception("recProduct Id: " + recProductId + " returned null.");
                
            //}
            licenseProductOverview.LicenseProductId = licenseProduct.LicenseProductId;
            licenseProductOverview.LicenseId = licenseProduct.LicenseId;
            licenseProductOverview.ProductId = licenseProduct.ProductId;
            licenseProductOverview.LicensePRecordingsNo = licenseProduct.LicensePRecordingsNo;
            licenseProductOverview.RelatedLicenseIds = licenseProduct.RelatedLicenseIds;
        licenseProductOverview.ProductHeader = _recsProvider.RetrieveProductHeader(licenseProduct.ProductId);
            licenseProductOverview.title = licenseProduct.title;

            //Fill LicenseProductConfigurations
            licenseProductOverview.LicenseProductConfigurations =
                _licenseProductConfigurationRepository.GetLicenseProductConfigurations(licenseProduct.LicenseProductId);

            foreach (var config in licenseProductOverview.LicenseProductConfigurations)
            {
                //config.LicenseProduct =  Not needed, already called
                //    _licenseProductRepository.GetLicenseProduct(licenseProductOverview.LicenseProductId);
                //config.RecsProductConfiguration =
                //    _licenseProductConfigurationRepository.Get(config.LicenseProductConfigurationId);
                //config.LicenseProductConfigurationInfo =
//_licenseProductConfigurationRepository.Get(config.LicenseProductConfigurationId);

                config.upc_code = GetMatchingUpcCode(licenseProductOverview.LicenseProductConfigurations,
                    licenseProductOverview.ProductHeader.Configurations);

            }


            licenseProductOverview.LicenseProductRecordings =
                _licenseProductRecordingRepository.GetLicenseRecordingsByLicenseProductId(
                    licenseProduct.LicenseProductId);


            //Fill LicenseProductRecordings
            foreach (var recording in licenseProductOverview.LicenseProductRecordings)
            {
                recording.LicensePRWriterNo =
                    _licensePRWriterRepository.GetLicenseWriters(recording.LicenseRecordingId).Count;
                recording.LicensePRLicensedWriterNo =
                    _licensePRWriterRepository.GetLicenseProductRecordingLicensedWritersNo(recording.LicenseRecordingId);
                recording.LicensePRUnLicensedWriterNo =
                    _licensePRWriterRepository.GetUnLicenseProductRecordingLicensedWritersNo(
                        recording.LicenseRecordingId);
                recording.LicensePRWriters =
                    _licensePRWriterRepository.GetLicenseProductRecordingWriters(recording.LicenseRecordingId);


            }

            licenseProductOverview.RelatedLicenseIds = _licenseRepository.GetAllRelatedLicenseIds(licenseProduct.ProductId);

            List<License> listOfLicenses = new List<License>();
            foreach (var licenseId in licenseProductOverview.RelatedLicenseIds)
            {
                License licnese = _licenseRepository.GetLicnese(licenseId);
                listOfLicenses.Add(licnese);
            }

            licenseProductOverview.Licenses = listOfLicenses;

            licenseProductOverview.PaidQuarter = licenseProduct.PaidQuarter;
            licenseProductOverview.ScheduleId = licenseProduct.ScheduleId;
            licenseProductOverview.Recordings = licenseProduct.Recordings;
            licenseProductOverview.Schedule = licenseProduct.Schedule;
            licenseProductOverview.RelatedLicensesNo = licenseProduct.RelatedLicensesNo;
            licenseProductOverview.Message = licenseProduct.Message;
            licenseProductOverview.LicenseClaimException = licenseProduct.LicenseClaimException;
            licenseProductOverview.TotalLicenseConfigAmount = licenseProduct.TotalLicenseConfigAmount;
                



            return licenseProductOverview;
        }

        public string GetMatchingUpcCode(List<LicenseProductConfiguration> licenseProductConfigurations,
            List<RecsConfiguration> configurations)
        {
            foreach (var config in licenseProductConfigurations)
            {
                foreach (var recConfig in configurations)
                {
                    if (recConfig.configuration_id == config.product_configuration_id)
                    {
                        return recConfig.UPC;
                    }
                }
            }
            return null;
        }
        public List<LicenseProduct> GetProducts(int licenseId)
        {
            // get license
            var license = _licenseRepository.GetLite(licenseId);
            bool isExecuted = license.LicenseStatusId == 5 || license.LicenseStatusId == 7;
            var claimExceptionCount = 0;

            var begintime = DateTime.Now;
            var starttime = DateTime.Now;
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("--GetProducts (CURRENT) starts-------------");
            logger.Debug("----StartTime :" + starttime.ToString());

            var totalpercentage = 0.0;
            var licensedpercentage = 0.0;
            var actualContribution = 0.0;

            starttime = DateTime.Now;
            var products = _licenseProductRepository.GetLicenseProducts(licenseId);  //same hit
            logger.Debug(".End GetLicenseProducts (" + licenseId + ") : elapsed :" + (DateTime.Now - starttime).ToString());

            foreach (var licenseProduct in products)
            {

                starttime = DateTime.Now;
                licenseProduct.ProductHeader =
                    _recsProvider.RetrieveProductHeader(licenseProduct.ProductId); // same hit
                logger.Debug("..End RetrieveProductHeader (" + licenseProduct.ProductId + ") : elapsed :" + (DateTime.Now - starttime).ToString());

                licenseProduct.RelatedLicensesNo =
                    _licenseProductRepository.GetLicensesNo(licenseProduct.ProductId); //same hit
                licenseProduct.LicensePRecordingsNo =
                    _licenseProductRecordingRepository.GetLicenseProductRecordingsNo(licenseProduct.LicenseProductId); //THis was replaced with a count

                totalpercentage = 0.0;
                licensedpercentage = 0.0;
                actualContribution = 0.0;
                var total = 0.0;
                var licensed = 0.0;
                licenseProduct.Message = new List<string>();

                List<LicenseProductConfigurationTotals> licenseProductConfigIdTotals;

                logger.Debug("***Start GetLicenseProductRecordingsForLicenseDetails called");
                starttime = DateTime.Now;
                licenseProduct.Recordings = this.GetLicenseProductRecordingsForLicenseDetails(licenseProduct.LicenseProductId, out licenseProductConfigIdTotals);
                logger.Debug("***End GetLicenseProductRecordingsForLicenseDetails (" + licenseProduct.LicenseProductId + ") : elapsed :" + (DateTime.Now - starttime).ToString());

                
                foreach (var rec in licenseProduct.Recordings)
                {
                    if (rec.Track.ClaimException)
                    {
                        licenseProduct.LicenseClaimException = "CLAIM EXCEPTION";
                        claimExceptionCount++;
                    }
                    totalpercentage = totalpercentage + rec.UmpgPercentageRollup;
                    total = total + rec.UmpgPercentageRollup;
                    licensed = licensed + rec.LicensedRollup;
                }
                licenseProduct.TotalLicenseConfigAmount = (licensed * 100) / total;

                for (int i = 0; i < licenseProduct.ProductHeader.Configurations.Count; i++)
                {
                    var licenseProductConfiguration =
                        _licenseProductConfigurationRepository.GetLicenseProductConfiguration(
                            (int)licenseProduct.LicenseProductId,
                            (int)licenseProduct.ProductHeader.Configurations[i].configuration_id);
                    if (licenseProductConfiguration != null)
                    {

                        // 20160526 - per Ryan display the total product config amount
                        //licenseProductConfiguration.TotalAmount = Convert.ToDecimal(totalpercentage / licenseProductConfigIdTotals.Count);
                        licenseProductConfiguration.TotalAmount = Convert.ToDecimal(totalpercentage);
                        //licenseProductConfiguration.LicensedAmount = Convert.ToDecimal(licensedpercentage);
                        //licenseProductConfiguration.NotLicensedAmount = Convert.ToDecimal(totalpercentage - licensedpercentage);
                        foreach (var licenseProductConfigIdTotal in licenseProductConfigIdTotals)
                        {
                            if (licenseProductConfigIdTotal.configuration_id == licenseProductConfiguration.configuration_id)
                            {
                                licenseProductConfiguration.LicensedAmount = Convert.ToDecimal(licenseProductConfigIdTotal.LicensedAmount / licenseProductConfigIdTotals.Count);
                                licenseProductConfiguration.NotLicensedAmount = Convert.ToDecimal((totalpercentage - licenseProductConfigIdTotal.LicensedAmount) / licenseProductConfigIdTotals.Count);
                            }
                        }

                        licenseProduct.ProductHeader.Configurations[i].LicenseProductConfiguration =
                            licenseProductConfiguration;
                    }
                }

            }
            if (claimExceptionCount > 0)
            {
                foreach (var licenseProduct in products)
                {
                    licenseProduct.LicenseClaimException = "CLAIM EXCEPTION";
                }
            }

            logger.Debug("GetProducts (__CURRENT__) total elapsed :" + (DateTime.Now - begintime).ToString());
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            logger.Debug("-------------------------------------");
            return products;
        }


        public List<LicenseProduct> GetProductsV2(int licenseId)
        {
            var products = _licenseProductRepository.GetLicenseProducts(licenseId);
            foreach (var licenseProduct in products)
            {
                licenseProduct.ProductHeader =
                    _recsProvider.RetrieveProductHeader(licenseProduct.ProductId);
                licenseProduct.RelatedLicensesNo =
                    _licenseProductRepository.GetLicensesNo(licenseProduct.ProductId);
                licenseProduct.LicensePRecordingsNo =
                    _licenseProductRecordingRepository.GetLicenseProductRecordingsNo(licenseProduct.LicenseProductId);
                for (int i = 0; i < licenseProduct.ProductHeader.Configurations.Count; i++)
                {
                    var licenseProductConfiguration =
                        _licenseProductConfigurationRepository.GetLicenseProductConfiguration(
                            (int)licenseProduct.LicenseProductId,
                            (int)licenseProduct.ProductHeader.Configurations[i].configuration_id);
                    if (licenseProductConfiguration != null)
                    {
                        licenseProduct.ProductHeader.Configurations[i].LicenseProductConfiguration =
                            licenseProductConfiguration;
                    }
                }

            }
            return products;
        }

        public GetWritersRatesRequest GetAllLicenseRelatedIds(int licenseid)
        {



            GetWritersRatesRequest test = new GetWritersRatesRequest();

            // Get LicenseProductIds
            test.LicenseProductIds = _licenseProductRepository.GetLicenseProducts(licenseid)
                .Select(x => x.LicenseProductId)
                .DefaultIfEmpty(0)
                .ToList();

            // Get LicenseConfigs
            test.LicenseConfigIds =
                _licenseProductConfigurationRepository.GetLicenseConfigurationList(test.LicenseProductIds)
                    .Select(x => x.LicenseProductConfigurationId)
                    .DefaultIfEmpty(0)
                    .ToList();

            // Get LicenseRecordingIds
            test.LicenseRecordingIds =
                _licenseProductRecordingRepository.GetLicenseProductRecordingsFromList(test.LicenseProductIds)
                    .Select(x => x.LicenseRecordingId)
                    .DefaultIfEmpty(0)
                    .ToList();

            // Get LicenseWriterIds
            test.LicenseWriterIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(test.LicenseRecordingIds)
                .ToList();



            return test;
        }


        public List<int> GetLicenseWriterRateIdsWithOutHolds(int licenseid)
        {

            List<string> test2;

            // Get LicenseProductIds
            var licenseProductIds = _licenseProductRepository.GetLicenseProducts(licenseid)
                .Select(x => x.LicenseProductId)
                .DefaultIfEmpty(0)
                .ToList();

            // Get LicenseRecordingIds
            var licenseRecordingIds =
                _licenseProductRecordingRepository.GetLicenseProductRecordingsFromList(licenseProductIds)
                    .Select(x => x.LicenseRecordingId)
                    .DefaultIfEmpty(0)
                    .ToList();

            // Get LicenseWriterIds
            var licenseWriterIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(licenseRecordingIds)
                .ToList();

            // Get LicenseWriterRateIds
            var licenseWriterRateIds = _licensePRWriterRateRepository.GetLicenseRecordingWriterRateIds(licenseWriterIds)
                .Select(x => x.LicenseWriterRateId)
                .ToList();


            var licenseWriterRateIdsWithoutHolds = _licensePRWriterRateStatusRepository.GetLicenseWriterRatesWithOutStatus(licenseWriterRateIds)
                .ToList();

            return licenseWriterRateIdsWithoutHolds;
        }

        public List<int> GetYearsForEditRates()
        {
            return _agreementStatutoryRateRepository.GetAll().OrderByDescending(x => x.Year).Select(x => x.Year).ToList();
        }

        public GetWritersRatesRequest GetAllLicenseRecordingRelatedIds(List<int> licenseProductids)
        {

            GetWritersRatesRequest test = new GetWritersRatesRequest();


            // Get LicenseConfigs
            test.LicenseConfigIds =
                _licenseProductConfigurationRepository.GetLicenseConfigurationList(licenseProductids)
                    .Select(x => x.LicenseProductConfigurationId)
                    .DefaultIfEmpty(0)
                    .ToList();

            // Get LicenseRecordingIds
            test.LicenseRecordingIds =
                _licenseProductRecordingRepository.GetLicenseProductRecordingsFromList(licenseProductids)
                    .Select(x => x.LicenseRecordingId)
                    .DefaultIfEmpty(0)
                    .ToList();

            // Get LicenseWriterIds
            test.LicenseWriterIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(test.LicenseRecordingIds)
                .ToList();



            return test;
        }



        public List<LicenseProductRecording> GetLicenseProductRecordings(int licenseproductId)
        {
            var licenseProduct = _licenseProductRepository.Get(licenseproductId);
            var recordings = _licenseProductRecordingRepository.GetLicenseProductRecordingsBrief(licenseproductId);
            var recsTracks = _recsProvider.RetrieveProductRecordings(licenseProduct.ProductId);

            for (int i = 0; i < recordings.Count; i++)
            {
                recordings[i].LicensePRWriterNo =
                _licensePRWriterRepository.GetLicenseProductRecordingWritersNo(recordings[i].LicenseRecordingId);


            }

            return recordings;
        }

        ////todo: Steve talk to be deleted
        public List<LicenseProductRecording> GetLicenseProductRecordingsBrief(int licenseproductId)
        {
            var recordings = _licenseProductRecordingRepository.GetLicenseProductRecordingsBrief(licenseproductId);
            var ids = recordings.Select(x => x.TrackId).ToList();
            //todo: Steve talk
            var recsTracks = new List<Recording>(); //_recordingRepository.GetRecordingsByIds(ids);
            for (int i = 0; i < recordings.Count; i++)
            {
                var test =
                    _licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(
                        (int)recordings[i].LicenseRecordingId);
                recordings[i].RecsRecording = recsTracks[i];
                recordings[i].LicensePRWriters = test.ToList();
            }

            return recordings;
        }



        public List<LicenseProductRecordingWriter> GetLicenseWriters(int licenseRecordingId)
        {

            // get Licensewriters Ids 

            var localWriters = _licensePRWriterRepository.GetLicenseWriters(licenseRecordingId);

            // then retrieve writer names from Recs

            //todo: need
            //foreach (var writer in localWriters)
            //{
            //    writer.Writer = _writerRepository.Get(writer.CAECode);
            //}

            return localWriters;
        }

        public List<LicenseProductRecordingWritersRateStatusDropdown> GetLicenseWriterRateStatusList(List<int> licenseWritersIds)
        {

            // get a list of license Writer names for dropdown purpose

            var localWriterRateStatuses = _licensePRWriterRateStatusRepository.GetLicenseWriterRateStatusList(licenseWritersIds);

            return localWriterRateStatuses;
        }


        public List<LicenseProductRecordingWriter> GetLicenseProductRecordingWriters(int licenseRecordingId,
            string worksCode)
        {

            var localWriters = _licensePRWriterRepository.GetLicenseProductRecordingWriters(licenseRecordingId);
            var recsWriters = _recsProvider.RetrieveTrackWriters(worksCode);
            foreach (var writer in localWriters)
            {
                //foreach (var recsWriter in recsWriters)
                //{
                //    if (writer.CAECode == recsWriter.cae)
                //    {
                //        writer.Writer = recsWriter;
                //    }
                //}

            }
            return localWriters;
        }

        public List<LicenseProductRecordingWriterRate> GetLicensePRWriterRates(int licenseWriterId)
        {

            return _licensePRWriterRateRepository.GetLicenseProductRecordingWriterRates(licenseWriterId);
        }

        public int GetLicenseProductRecordingWritersNo(int licenseRecordingId)
        {
            return _licensePRWriterRepository.GetLicenseProductRecordingWritersNo(licenseRecordingId);

        }


        // delete?
        public List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseId)
        {

            // returns licenseProduct, licenseProductConfiguration
            var licenseProductConfigurations = _licenseProductRepository.GetLicenseProductConfigurations(licenseId);

            var productIds =
                licenseProductConfigurations.Where(x => x.LicenseProduct.LicenseId == licenseId)
                    .Select(x => (int)x.LicenseProduct.ProductId)
                    .ToList();
            //todo: Steve talk
            var products = new List<Product>(); //_productRepository.GetProductsBrief(productIds);

            foreach (var licenseProductConfiguration in licenseProductConfigurations)
            {
                //todo: Steve talk
                var productConfiguration = new ProductConfiguration();
                //_productRepository.GetProductConfiguration((int)licenseProductConfiguration.LicenseProduct.ProductId, (int)licenseProductConfiguration.configuration_id);
                licenseProductConfiguration.RecsProductConfiguration = productConfiguration;
                //todo: Steve talk
                var licenseProductId =
                    products.Where(x => x.product_id == licenseProductConfiguration.LicenseProduct.ProductId)
                        .FirstOrDefault();
                //lambda more readabale code :)
                //(from p in products where p.product_id == licenseProductConfiguration.LicenseProduct.ProductId select p).FirstOrDefault();
                //licenseProductConfiguration.LicenseProduct.RecsProduct = licenseProductId;

                if (licenseProductConfiguration.configuration_id == 9 ||
                    licenseProductConfiguration.configuration_id == 10)
                {
                    licenseProductConfiguration.ConfigurationType = "DIGITAL";
                }
                else
                {
                    licenseProductConfiguration.ConfigurationType = "PHYSICAL";
                }

                var recordings = GetLicenseProductRecordingsBrief(licenseProductConfiguration.LicenseProductId);

                var total = recordings.Sum(r => r.LicensePRWriters.Sum(w => w.ExecutedSplit));
                var licensed = recordings.Sum(r => r.LicensePRWriters.Where(w => w.isLicensed == true).Sum(w => w.ExecutedSplit));
                var notlicensed =
                    recordings.Sum(r => r.LicensePRWriters.Where(w => w.isLicensed == false).Sum(w => w.ExecutedSplit));

                licenseProductConfiguration.TotalAmount = Convert.ToDecimal(total);
                licenseProductConfiguration.LicensedAmount = Convert.ToDecimal(licensed);
                licenseProductConfiguration.NotLicensedAmount = Convert.ToDecimal(notlicensed);

            }

            return licenseProductConfigurations;
        }


        public bool UpdateLicenseProductConfigurations(UpdateLicenseProductConfigurationsStatusRequest request)
        {
            var result = false;
            var licenseProductConfiguration =
                _licenseProductRepository.GetLicenseProductConfigurationById(request.LicenseProductConfigurationId);
            if (licenseProductConfiguration != null)
            {
                _licenseProductRepository.UpdateLicenseProductConfiguration(request.LicenseProductConfigurationId,
                    request.ReportField, request.FieldValue);
                result = true;
            }

            return result;
        }



        public bool UpdateLicenseProducts(UpdateLicenseProductsRequest request)
        {
            var result = false;
            var productList = request.LicenseProducts.Select(t => t.Key).ToList();
            var productRecordings = request.LicenseProducts;
            var existingProducts = _licenseProductRepository.GetLicenseProducts(request.LicenseId);
            var removedProducts = existingProducts.Where(lp => !productList.Contains(lp.ProductId));
            var addedProducts = new List<int>();
            foreach (var productId in productList)
            {
                if (existingProducts.FirstOrDefault(p => p.ProductId == productId) == null)
                    addedProducts.Add(productId);
            }
            productList.Where(t => !existingProducts.Exists(lp => lp.LicenseProductId == t));
            foreach (var licenseProduct in removedProducts)
            {
                licenseProduct.Deleted = DateTime.Now;
                licenseProduct.ModifiedDate = DateTime.Now;
                //licenseProduct.ModifiedBy = ;

                _licenseProductRepository.Update(licenseProduct);
                var existingLicenseRecordings =
                    _licenseProductRecordingRepository.GetLicenseProductRecordingsBrief(licenseProduct.LicenseProductId);
                foreach (var existingLicenseRecording in existingLicenseRecordings)
                {
                    existingLicenseRecording.Deleted = DateTime.Now;
                    existingLicenseRecording.ModifiedDate = DateTime.Now;

                    _licenseProductRecordingRepository.Update(existingLicenseRecording);
                    var existingWriters =
                        _licensePRWriterRepository.GetLicenseProductRecordingWriters(
                            existingLicenseRecording.LicenseRecordingId);
                    foreach (var licenseProductRecordingWriter in existingWriters)
                    {
                        licenseProductRecordingWriter.Deleted = DateTime.Now;
                        _licensePRWriterRepository.Update(licenseProductRecordingWriter);
                        var existingWriterRates =
                            _licensePRWriterRateRepository.GetLicenseProductRecordingWriterRates(
                                licenseProductRecordingWriter.LicenseWriterId);
                        foreach (var licenseProductRecordingWriterRate in existingWriterRates)
                        {
                            licenseProductRecordingWriterRate.Deleted = DateTime.Now;
                            //todo _licensePRWriterRateRepository update
                            // var existingWriterRateNotes = _licensePrWriterRateNoteRepository.
                        }
                    }
                }
                var existingConfigurations =
                    _licenseProductRepository.GetProductConfigurations(licenseProduct.LicenseProductId);
                foreach (var licenseProductConfiguration in existingConfigurations)
                {
                    licenseProductConfiguration.Deleted = DateTime.Now;
                    //todo update
                }

            }
            var i = 0;
            foreach (var addedProduct in addedProducts)
            {
                i++;
                var productLicenseResult = _licenseProductRepository.Add(new LicenseProduct
                {
                    ScheduleId = i,
                    ProductId = addedProduct,
                    LicenseId = request.LicenseId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now

                });
                // var recordings = productRecordings.SingleOrDefault(pr => pr.Key == addedProduct).Value;
                var recordings = _recsProvider.RetrieveProductRecordings(addedProduct);
                foreach (var recording in recordings)
                {
                    var addedRecording = _licenseProductRecordingRepository.Add(new LicenseProductRecording
                    {
                        TrackId = recording.Track.Id,
                        LicenseProductId = productLicenseResult.LicenseProductId,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                    var writers =
                        _recsProvider.RetrieveTrackWriters(recording.Track.Copyrights.FirstOrDefault().WorkCode);
                    foreach (var writer in writers)
                    {
                        var addedWriter = _licensePRWriterRepository.Add(new LicenseProductRecordingWriter
                        {
                            LicenseRecordingId = addedRecording.LicenseRecordingId,
                            CAECode = writer.CaeNumber,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                        });
                    }


                }


            }
            return result;
        }

        public List<LicenseProductConfigurations> GetLicenseProductConfigurationsAll(int licenseId)
        {

            var licenseProductConfigurationList = new List<LicenseProductConfigurations>();

            var licenseProducts = _licenseProductRepository.GetLicenseProducts(licenseId);
            var productIds = _licenseProductRepository.GetProductsIds(licenseId);
            //todo: Steve talk
            var products = new List<Product>(); //_productRepository.GetProducts(productIds);
            var lpcs = _licenseProductRepository.GetLicenseProductConfigurations(licenseId);

            foreach (var product in products)
            {
                //todo: Steve talk
                var productConfigurations = new List<ProductConfiguration>();
                //_productRepository.GetRecsProductConfiguration((int)product.product_id);
                var configurations = new List<RecsConfiguration>();

                foreach (var productConfiguration in productConfigurations)
                {
                    //todo: Steve talk
                    var configuration = new RecsConfiguration();
                    //_productRepository.GetRecsConfiguration((int)productConfiguration.configuration_Id);
                    configurations.Add(configuration);

                    var licenseProductConfiguration =
                        (from pc in lpcs
                         where pc.product_configuration_id == productConfiguration.product_configuration_id
                         select pc).FirstOrDefault();


                    productConfiguration.LicenseProductConfiguration = licenseProductConfiguration;
                }

                var licenseProductConfigurationItem = new LicenseProductConfigurations()
                {
                    RecsProduct = product,
                    RecsProductConfigurations = productConfigurations
                };

                licenseProductConfigurationList.Add(licenseProductConfigurationItem);
            }
            return licenseProductConfigurationList;
        }


        //todo: to be deleted... think is not used anymore
        //public List<LicenseProductDropdown> GetLicenseProductDropDown(int licenseId)
        //{
        //    var licenseProductLite = _licenseProductRepository.GetLicenseProductDropDown(licenseId);
        //    foreach (var item in licenseProductLite)
        //    {
        //        item.ProductTitle = _productRepository.Get(item.ProductId).Title;
        //    }
        //        return licenseProductLite;
        //}


        public bool UpdateLicenseProductConfigurationsAll(List<UpdateLicenseProductConfigurationsRequest> requests)
        {
            /*

            //
            // need to keep these grouped by licenseProductConfigurationGroupId
            //

            foreach (int licenseProductConfigurationGroupId in licenseProductConfigurationGroupIds)
            {
                //get all associated product configurations
                foreach (UpdateLicenseProductConfigurationsRequest config in configs)
                {

                    bool ConfigUpdates = config.originalLicenseProductConfigurationId != config.licenseProductConfigurationId;
                    //bool ProductUpdates = config.originalLicenseProductId != config.licenseProductId;
                    //LicenseProductConfiguration licenseProductConfiguration;
                    //LicenseProductConfigurationGrouping licenseProductConfigurationGrouping;

                    //  Config updates before Group updates
                    var licenseProductConfiguration = new LicenseProductConfiguration();

                    if (ConfigUpdates)
                    {
                        //add, update, delete LicenseProductConfiguration 


                        if (config.originalLicenseProductConfigurationId == 0)
                        {
                            // add 
                            licenseProductConfiguration = new LicenseProductConfiguration();
                            licenseProductConfiguration.LicenseProductId = config.licenseProductId;
                            licenseProductConfiguration.configuration_id = config.configuration_id;
                            licenseProductConfiguration.configuration_name = config.configuration_name;
                            licenseProductConfiguration.product_configuration_id = config.product_configuration_id;
                            licenseProductConfiguration.PriorityReport = false;
                            licenseProductConfiguration.StatusReport = false;

                            // temp
                            licenseProductConfiguration.CreatedDate = DateTime.Now;
                            licenseProductConfiguration.CreatedBy = 1;

                            var temp = _licenseProductConfigurationRepository.Add(licenseProductConfiguration);
                            licenseProductConfiguration = _licenseProductConfigurationRepository.Get(temp.LicenseProductConfigurationId);

                        }
                        else
                        {
                            if (config.licenseProductConfigurationId == 0)
                            {
                                //delete
                                var temp = _licenseProductConfigurationRepository.Get(config.originalLicenseProductConfigurationId);
                                temp.Deleted = DateTime.Now;
                                temp.ModifiedDate = DateTime.Now;
                                temp.ModifiedBy = 1;
                                _licenseProductConfigurationRepository.Update(temp);
                                licenseProductConfiguration = _licenseProductConfigurationRepository.Get(temp.LicenseProductConfigurationId);
                            }

                            //do we ever update?
                        }

                    }

                    
                }

            }
            */
            return true;
        }


        /// <summary>
        /// New method that gets recordings upside down
        /// </summary>
        /// <param name="licenseProductId"></param>
        /// <returns></returns>
        public List<WorksRecording> GetLicenseProductRecordingsV2(int licenseProductId, string safeId)
        {
            var licenseProduct = _licenseProductRepository.Get(licenseProductId);
            var license = _licenseRepository.GetLite(licenseProduct.LicenseId);

            var recordings = _licenseProductRecordingRepository.GetLicenseProductRecordingsBrief(licenseProductId);
            //medleys are not used yet
            //var medleysAndSamplesTrackIds = recordings.Where(x => x.TrackTypeId.HasValue).Select(x => x.LicenseRecordingId).ToList();
            //var medleysAndSamples = new List<RecordingMedley>();
            //medleysAndSamples.AddRange(_medleyRepository.GetByLicenseRecordingIds(medleysAndSamplesTrackIds));
            //var tracksForMedleys = _recsProvider.RetrieveTracks(medleysAndSamples.Select(x => x.LicenseRecordingMedley.MedleyTrackId).ToList(), safeId);
            bool isExecuted = license.LicenseStatusId == 5 || license.LicenseStatusId == 7;
            //this is old version without writers
            //var recsTracks = _recsProvider.RetrieveProductRecordings(licenseProduct.ProductId);
            //track with writers
            var recsTracks = _recsProvider.RetrieveTracksWithWriters(licenseProduct.ProductId);
            var licenseProductConfigIdTotals = GetLicenseProductConfigurationIdTotals(licenseProductId);
            foreach (var worksRecording in recsTracks)
            {

                var totalpercentage = 0.00;

                worksRecording.Message = string.Empty;

                foreach (var recording in recordings)
                {
                    if (recording.TrackId == worksRecording.Track.Id)
                    {
                        // get the licenserecording object
                        worksRecording.LicenseRecording = recording;
                        worksRecording.LicenseRecording.LicensePRWriterNo =
                             _licensePRWriterRepository.GetLicenseProductRecordingWritersNo(recording.LicenseRecordingId);
                        worksRecording.LicenseRecording.LicensePRLicensedWriterNo =
                             _licensePRWriterRepository.GetLicenseProductRecordingLicensedWritersNo(recording.LicenseRecordingId);
                        worksRecording.LicenseRecording.LicensePRUnLicensedWriterNo =
                           worksRecording.LicenseRecording.LicensePRWriterNo - worksRecording.LicenseRecording.LicensePRLicensedWriterNo;

                        var licenseProductRecordingWriters = _licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recording.LicenseRecordingId);
                        var licensedpercentage = 0.00;
                        if (worksRecording.Track.Copyrights != null)
                        {
                            recording.WorkCode = worksRecording.Track.Copyrights.FirstOrDefault().WorkCode;

                            //var writers = _recsProvider.RetrieveTrackWriters(recording.WorkCode);
                            var firstOrDefault = worksRecording.Track.Copyrights.FirstOrDefault();
                            if (firstOrDefault != null)
                            {
                                var writers = firstOrDefault.Composers;
                                foreach (var writer in writers)
                                {
                                    writer.Contribution = (float?) GetContribution(writer);
                                    //filter for controlled writers
                                    if (writer.Controlled)
                                    {
                                        var actualcontribution = (double) writer.Contribution;
                                        if (writer.Controlled)
                                        {
                                            var licenseProductRecordingWriter =
                                                licenseProductRecordingWriters.Where(
                                                    x => x.CAECode == writer.CaeNumber && x.isLicensed == true)
                                                    .FirstOrDefault();
                                            if (licenseProductRecordingWriter != null)
                                            {
                                                var writerLicensedConfigIds =
                                                    _licensePRWriterRateRepository.GetLicensedConfigIds(
                                                        licenseProductRecordingWriter.LicenseWriterId);

                                                double licensedConfigFactor = 1;
                                                if (licenseProductConfigIdTotals.Count > 0 &&
                                                    writerLicensedConfigIds.Count > 0)
                                                {
                                                    licensedConfigFactor =
                                                        Convert.ToDouble(
                                                            Convert.ToDecimal(writerLicensedConfigIds.Count)/
                                                            Convert.ToDecimal(licenseProductConfigIdTotals.Count));
                                                }

                                                if (isExecuted)
                                                {
                                                    if (licenseProductRecordingWriter.ExecutedSplit != null)
                                                    {
                                                        actualcontribution =
                                                            Convert.ToDouble(licenseProductRecordingWriter.ExecutedSplit);
                                                    }
                                                }
                                                else
                                                {
                                                    if (licenseProductRecordingWriter.SplitOverride != null)
                                                    {
                                                        actualcontribution =
                                                            Convert.ToDouble(licenseProductRecordingWriter.SplitOverride);
                                                    }
                                                    else if (licenseProductRecordingWriter.ClaimExceptionOverride != null)
                                                    {
                                                        actualcontribution =
                                                            Convert.ToDouble(
                                                                licenseProductRecordingWriter.ClaimExceptionOverride);
                                                    }
                                                }
                                                licensedpercentage += (actualcontribution*licensedConfigFactor);
                                               
                                            }
                                            totalpercentage = totalpercentage + actualcontribution;

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            recording.WorkCode = "N/A";
                            worksRecording.Message = "No WorkCode";
                        }
                        var listOfWriterIds = new List<int>();


                        listOfWriterIds.Add(worksRecording.LicenseRecording.LicenseRecordingId);
                        var licenseWriterIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(listOfWriterIds)
                            .ToList();
                        var licenseWriterRateIds = _licensePRWriterRateRepository.GetLicenseRecordingWriterRateIds(licenseWriterIds)
                            .Select(x => x.LicenseWriterRateId).ToList();
                        //populate key value with statuses and their occurances
                        if (worksRecording.LicenseRecording != null)
                            worksRecording.LicenseRecording.StatusRollup = GetStatusRollup(licenseWriterRateIds);


                        worksRecording.UmpgPercentageRollup = totalpercentage;
                        worksRecording.LicensedRollup = licensedpercentage;

                    }

                    if (worksRecording.LicenseRecording == null)
                    {
                        worksRecording.LicensedRollup = 0.00;

                    }
                }
            }
            //code for retrieve medleys
            //foreach (var licenseRecordingMedley in medleysAndSamples)
            //{
            //    foreach (var trackForMedley in tracksForMedleys.Values)
            //    {
            //        if (licenseRecordingMedley.LicenseRecordingMedley.MedleyTrackId == trackForMedley.Id)
            //        {
            //            LicenseRecordingMedley medley = licenseRecordingMedley.LicenseRecordingMedley;
            //            var trackToAdd = new WorksRecording()
            //            {
            //                Track = new WorksTrack()
            //                {
            //                    Duration = licenseRecordingMedley.LicenseRecordingMedley.MedleyDuration,
            //                    Title = trackForMedley.Title,
            //                    Id = (int)trackForMedley.Id,
            //                    Artists = new ArtistRecs()
            //                    {
            //                        name = licenseRecordingMedley.LicenseRecordingMedley.Artist
            //                    }

            //                },
            //                ProductId = licenseProduct.ProductId,
            //                LicenseRecording = licenseRecordingMedley.LicenseProductRecording,

            //            };
            //            trackToAdd.LicenseRecording.WorkCode = trackForMedley.PipsCode;
            //            trackToAdd.LicenseRecording.LicensePRWriterNo = trackForMedley.Writers.Count;
            //            trackToAdd.Track.ControledWritersNo = trackForMedley.Writers.Count(x => x.controlled == 1);
            //            trackToAdd.Track.Copyrights = new List<RecsCopyrights>
            //            {
            //                new RecsCopyrights()
            //                {
            //                    WorkCode = trackForMedley.PipsCode
            //                }
            //            };
            //            trackToAdd.Track.WritersNo = trackForMedley.Writers.Count;
            //            recsTracks.Add(trackToAdd);
            //        }
            //    }
            //}
            return recsTracks;
        }

        public List<WorksRecording> GetLicenseProductRecordingsV3(int licenseProductId, string safeId)
        {
            var licenseProduct = _licenseProductRepository.Get(licenseProductId);
            var license = _licenseRepository.GetLite(licenseProduct.LicenseId);

            var recordings = _licenseProductRecordingRepository.GetLicenseProductRecordingsBrief(licenseProductId);
            bool isExecuted = license.LicenseStatusId == 5 || license.LicenseStatusId == 7;

            var recsTracks = _recsProvider.RetrieveTracksWithWriters(licenseProduct.ProductId);

            var licenseProductConfigIdTotals = GetLicenseProductConfigurationIdTotals(licenseProduct.LicenseProductId);

            foreach (var worksRecording in recsTracks)
            {
                var totalpercentage = 0.00;

                worksRecording.Message = string.Empty;

                foreach (var recording in recordings)
                {
                    if (recording.TrackId == worksRecording.Track.Id)
                    {
                        worksRecording.LicenseRecording = recording;
                        worksRecording.LicenseRecording.LicensePRWriterNo =
                             _licensePRWriterRepository.GetLicenseProductRecordingWritersNo(recording.LicenseRecordingId);
                        worksRecording.LicenseRecording.LicensePRLicensedWriterNo =
                             _licensePRWriterRepository.GetLicenseProductRecordingLicensedWritersNo(recording.LicenseRecordingId);
                        worksRecording.LicenseRecording.LicensePRUnLicensedWriterNo =
                           worksRecording.LicenseRecording.LicensePRWriterNo - worksRecording.LicenseRecording.LicensePRLicensedWriterNo;

                        var licenseProductRecordingWriters = _licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recording.LicenseRecordingId);
                        var licensedpercentage = 0.00;
                        if (worksRecording.Track.Copyrights != null)
                        {
                            var firstOrDefault = worksRecording.Track.Copyrights.FirstOrDefault();
                            if (firstOrDefault != null)
                            {
                                
                                var worksWriters = firstOrDefault.Composers;
                                if (worksWriters != null)
                                {
                                    var writers = worksWriters;
                                    foreach (var writer in writers)
                                    {
                                        writer.Contribution = (float?) GetContribution(writer);
                                        if (writer.Controlled)
                                        {
                                            double actualcontribution = (double)writer.Contribution;
                                            if (writer.Controlled)
                                            {
                                                var licenseProductRecordingWriter = licenseProductRecordingWriters.Where(x => x.CAECode == writer.CaeNumber && x.isLicensed == true).FirstOrDefault();
                                                if (licenseProductRecordingWriter != null)
                                                {

                                                    var writerLicensedConfigIds = _licensePRWriterRateRepository.GetLicensedConfigIds(licenseProductRecordingWriter.LicenseWriterId);

                                                    double licensedConfigFactor = 1;
                                                    if (licenseProductConfigIdTotals.Count > 0 && writerLicensedConfigIds.Count > 0)
                                                    {
                                                        licensedConfigFactor = Convert.ToDouble(Convert.ToDecimal(writerLicensedConfigIds.Count) / Convert.ToDecimal(licenseProductConfigIdTotals.Count));
                                                    }

                                                    if (isExecuted)
                                                    {
                                                        if (licenseProductRecordingWriter.ExecutedSplit != null)
                                                        {
                                                            actualcontribution = Convert.ToDouble(licenseProductRecordingWriter.ExecutedSplit);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (licenseProductRecordingWriter.SplitOverride != null)
                                                        {
                                                            actualcontribution = Convert.ToDouble(licenseProductRecordingWriter.SplitOverride);
                                                        }
                                                        else if (licenseProductRecordingWriter.ClaimExceptionOverride != null)
                                                        {
                                                            actualcontribution = Convert.ToDouble(licenseProductRecordingWriter.ClaimExceptionOverride);
                                                        }
                                                    }
                                                    licensedpercentage += (actualcontribution * licensedConfigFactor);

                                                }
                                                totalpercentage = totalpercentage + actualcontribution;

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            recording.WorkCode = "N/A";
                            worksRecording.Message = "No WorkCode";
                        }
                        var listOfWriterIds = new List<int>();


                        listOfWriterIds.Add(worksRecording.LicenseRecording.LicenseRecordingId);
                        var licenseWriterIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(listOfWriterIds)
                            .ToList();
                        var licenseWriterRateIds = _licensePRWriterRateRepository.GetLicenseRecordingWriterRateIds(licenseWriterIds)
                            .Select(x => x.LicenseWriterRateId).ToList();
                        //populate key value with statuses and their occurances
                        if (worksRecording.LicenseRecording != null)
                            worksRecording.LicenseRecording.StatusRollup = GetStatusRollup(licenseWriterRateIds);


                        worksRecording.UmpgPercentageRollup = totalpercentage;
                        worksRecording.LicensedRollup = licensedpercentage;

                    }

                    if (worksRecording.LicenseRecording == null)
                    {
                        worksRecording.LicensedRollup = 0.00;

                    }
                }
            }
            return recsTracks;
        }

        /// <summary>
        /// New method that gets recordings upside down
        /// </summary>
        /// <param name="licenseProductId"></param>
        /// <returns></returns>
        public List<WorksWriter> GetLicenseWritersV2(int licenseRecordingId, string worksCode)
        {
            var localWriters = _licensePRWriterRepository.GetLicenseProductRecordingWriters(licenseRecordingId);
            var recsWriters = _recsProvider.RetrieveTrackWriters(worksCode);
            foreach (var writer in localWriters)
            {
                foreach (var recsWriter in recsWriters)
                {
                    //if (writer.CAECode == recsWriter.CaeNumber || (writer.CAECode == 0 && recsWriter.CaeNumber == null))
                    if (writer.CAECode == recsWriter.CaeNumber || (writer.CAECode == 0))
                    {
                        recsWriter.LicenseProductRecordingWriter = writer;
                    }
                }
            }
            return recsWriters;
        }

        public List<WorksWriter> GetLicenseWritersV3(int licenseRecordingId, List<WorksWriter> recsWriters)
        {
            var localWriters = _licensePRWriterRepository.GetLicenseProductRecordingWriters(licenseRecordingId);
            foreach (var writer in localWriters)
            {
                foreach (var recsWriter in recsWriters)
                {
                    if (writer.CAECode == recsWriter.CaeNumber || (writer.CAECode == 0 && recsWriter.CaeNumber == null))
                    {
                        recsWriter.LicenseProductRecordingWriter = writer;
                    }
                }
            }
            return recsWriters;
        }

        public List<LicenseProductRecordingsDropdown> GetLicenseProductRecordingsDropdown(int licenseId)
        {
            var licenseProducts = _licenseProductRepository.GetLicenseProducts(licenseId);
            var recordings = new List<LicenseProductRecording>();
            var recsRecordings = new List<WorksRecording>();
            foreach (var product in licenseProducts)
            {
                recsRecordings.AddRange(_recsProvider.RetrieveProductRecordings(product.ProductId));
                recordings.AddRange(
                    _licenseProductRecordingRepository.GetLicenseProductRecordingsBrief(product.LicenseProductId));
            }

            var lReturn = (from worksRecording in recsRecordings
                           from recording in recordings
                           where worksRecording.Track.Id == recording.TrackId
                           select new LicenseProductRecordingsDropdown()
                           {
                               LicenseProductId = recording.LicenseProductId,
                               RecordingName = worksRecording.Track.Title,
                               LicenseRecordingId = recording.LicenseRecordingId,
                               TrackId = worksRecording.Track.Id,
                               WorksCode = worksRecording.Track.Copyrights.FirstOrDefault().WorkCode
                           }).ToList();

            return lReturn.ToList();
        }

        public List<LicenseProductRecordingWritersDropdown> GetLicenseProductRecordingsWritersDropdown(
            GetWritersRatesRequest request)
        {
            var licenseProducts = _licenseProductRepository.GetLicenseProducts(request.LicenseId);

            var recsRecordings = new List<WorksRecording>();
            foreach (var product in licenseProducts)
            {
                recsRecordings.AddRange(_recsProvider.RetrieveProductRecordings(product.ProductId));
            }

            var localWriters = _licensePRWriterRepository.GetLicenseWriterList(request.LicenseRecordingIds);
            var worksWriters = new List<WorksWriter>();
            foreach (var recording in recsRecordings)
            {
                worksWriters.AddRange(
                    _recsProvider.RetrieveTrackWriters(recording.Track.Copyrights.FirstOrDefault().WorkCode));
            }
            var lReturn = (from worksWriter in worksWriters
                           from writer in localWriters
                           where worksWriter.CaeNumber == writer.CAECode
                           select new LicenseProductRecordingWritersDropdown()
                           {
                               Cae = worksWriter.CaeNumber,
                               LicenseWriterId = writer.LicenseWriterId,
                               WriterName = worksWriter.FullName
                           });
            return lReturn.GroupBy(x => x.Cae).Select(y => y.First()).ToList();

        }

        public List<int> GetWritersNoForEditRates(GetWritersRatesRequest request)
        {
            var lReturnList = new List<int>();
            var productIdsBasedOnConfig =
                _licenseProductConfigurationRepository.GetProductIdsWithConfiguration(request.AllLicenseProductIds,
                    request.LicenseConfigIds);
            var allProductIds = request.LicenseProductIds;
            allProductIds.AddRange(productIdsBasedOnConfig);
            var allrecordingIds = request.LicenseRecordingIds;
            allrecordingIds.AddRange(
                _licenseProductRecordingRepository.GetLicenseProductRecordingsFromList(allProductIds)
                    .Select(x => x.LicenseRecordingId)
                    .ToList());

            if (allrecordingIds.Count > 0)
            {
                lReturnList.AddRange(_licensePRWriterRepository.GetLicenseRecordingWriterIds(allrecordingIds)
                    .ToList());
            }
            lReturnList.AddRange(request.LicenseWriterIds);

            return lReturnList.Distinct().ToList();
        }

        public bool EditIndividualWriterRates(List<EditRatesSaveRequest> request)
        {
            var result = true;
            foreach (var editRatesSaveRequest in request)
            {
                var saveResult = EditRatesAndWriters(editRatesSaveRequest);
                if (!saveResult) result = false;
            }
            return result;
        }

        public bool EditRatesAndWriters(EditRatesSaveRequest request)
        {
            List<int> currentRequestSpecialStatusList = request.SelectedStatusesIds;
            List<LicenseProductRecordingWriterRate> deletedRates = new List<LicenseProductRecordingWriterRate>();
            //delete rates first
            foreach (var writerId in request.SelectedWritersIds)
            {
             
                foreach(var productConfigId in request.ProductConfigurationIds)
                {
                    var ratesToDelete =
                        _licensePRWriterRateRepository.GetLicenseProductRecordingWriterRatesByWriterIdConfig(writerId,
                            productConfigId).OrderBy(x => x.LicenseWriterRateId).ToList();
                    for (int i = 1; i < ratesToDelete.Count(); i++)
                    {
                        ratesToDelete[i].Deleted = DateTime.Now;
                        ratesToDelete[i].ModifiedBy = request.modifiedBy;
                        deletedRates.Add(ratesToDelete[i]);

                        _licensePRWriterRateRepository.Update(ratesToDelete[i]);
                    }
                
                }
                /*
                foreach (var configId in request.ConfigurationIds)
                {
                    var ratesToDelete =
                        _licensePRWriterRateRepository.GetLicenseProductRecordingWriterRatesByWriterIdConfig(writerId,
                            configId).OrderBy(x => x.LicenseWriterRateId).ToList();
                    for (int i = 1; i < ratesToDelete.Count(); i++)
                    {
                        ratesToDelete[i].Deleted = DateTime.Now;
                        ratesToDelete[i].ModifiedBy = request.modifiedBy;
                        deletedRates.Add(ratesToDelete[i]);

                        _licensePRWriterRateRepository.Update(ratesToDelete[i]);
                    }
                }
                */
            }
            var writers = _licensePRWriterRepository.GetLicensePrWritersFromIds(request.SelectedWritersIds);
            var trackIds = writers.Select(x => x.LicenseRecordingId).Distinct().ToList();
            /*
            var rates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                request.SelectedWritersIds, request.ConfigurationIds);
            */
            var rates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                request.SelectedWritersIds, request.ProductConfigurationIds);

            var rateToEditInfo = request.Rates.FirstOrDefault();
            foreach (var rate in rates)
            {
                bool ok = false;

                bool isLessThanOrEqualTo5Min;
                float statRate;
                decimal? writerSplit = 0;
                switch (rateToEditInfo.SelectedRateTypeId)
                {
                    case 1: //statutory (this should be a look up)
                    case 9:  //compulsory (this should be a look up)

                        isLessThanOrEqualTo5Min = IsLessThanOrEqualTo5Min(rate, request.Writers);
                        if (isLessThanOrEqualTo5Min)
                        {
                            statRate = (float)_agreementStatutoryRateRepository.Get(rateToEditInfo.StatYear).Rate;
                        }
                        else
                        {
                            var duration = GetSongDuration(rate, request.Writers);
                            statRate = _statRateRepository.GetStatRate(duration, new DateTime((int) rateToEditInfo.StatYear, 01, 01));

                        }
                        rateToEditInfo.Rate = (decimal)statRate;
                        decimal? lwriterSplit = request.SplitOverride;
                        if (lwriterSplit == null)
                        {
                            lwriterSplit = GetSplit(rate, request.Writers);
                        }
                        rateToEditInfo.SongRate = rateToEditInfo.Rate;
                        rateToEditInfo.ProRata = CalculateProRataRate(rateToEditInfo.SongRate, lwriterSplit);
                        break;

                    case 2: //controlled  fixed
                    case 5: //reduced fixed
                        writerSplit = request.SplitOverride;
                        if (writerSplit == null)
                        {
                            writerSplit = GetSplit(rate, request.Writers);
                        }
                        rateToEditInfo.SongRate = rateToEditInfo.Rate;
                        rateToEditInfo.ProRata = CalculateProRataRate(rateToEditInfo.SongRate, writerSplit);
                        rateToEditInfo.StatYear = null;
                        //escalted rate portion has been moved
                        break;
                    case 4: //Controlled: min Stat
                    case 7: //Reduced: min Stat
                        var statsRate = (float)_agreementStatutoryRateRepository.Get(rateToEditInfo.StatYear).Rate;

                        rateToEditInfo.Rate = CalculateRate(statsRate, rateToEditInfo.PercentOfStat);
                        rateToEditInfo.SongRate = CalculateSongRate(statsRate, rateToEditInfo.PercentOfStat);
                        decimal? lsplit = request.SplitOverride;
                        if (lsplit == null)
                        {
                            lsplit = GetSplit(rate, request.Writers);
                        }
                        rateToEditInfo.ProRata = CalculateProRataRate(rateToEditInfo.SongRate, lsplit);
                        break;
                    case 3: //Controlled: Stat
                    case 6: //Reduced:Stat
                    case 10:
                    case 11:
                        isLessThanOrEqualTo5Min = IsLessThanOrEqualTo5Min(rate, request.Writers);
                        if (isLessThanOrEqualTo5Min)
                        {
                            statRate = (float)_agreementStatutoryRateRepository.Get(rateToEditInfo.StatYear).Rate;
                        }
                        else
                        {
                            var duration = GetSongDuration(rate, request.Writers);
                            statRate = _statRateRepository.GetStatRate(duration, new DateTime((int) rateToEditInfo.StatYear, 01, 01));

                        }
                        rateToEditInfo.Rate = CalculateRate(statRate, rateToEditInfo.PercentOfStat);
                        rateToEditInfo.SongRate = CalculateSongRate(statRate, rateToEditInfo.PercentOfStat);
                        decimal? split = request.SplitOverride;
                        if (split == null)
                        {
                            split = GetSplit(rate, request.Writers);
                        }
                        rateToEditInfo.ProRata = CalculateProRataRate(rateToEditInfo.SongRate, split);
                        break;

                    case 8: //Gratis
                        rateToEditInfo.Rate = (decimal)0.000;
                        rateToEditInfo.ProRata = (decimal)0.000;
                        rateToEditInfo.SongRate = (decimal)0.000;
                        rateToEditInfo.StatYear = null;
                        break;

                    default:
                        rateToEditInfo.Rate = (decimal)0.000;
                        rateToEditInfo.ProRata = (decimal)0.000;
                        rateToEditInfo.SongRate = (decimal)0.000;
                        rateToEditInfo.modifiedBy = request.modifiedBy;
                        rateToEditInfo.StatYear = null;
                        break;

                }

                //check changes to rates
                ok = ok || rate.EscalatedRate != rateToEditInfo.EscalatedRate;
                ok = ok || rate.RateTypeId != rateToEditInfo.SelectedRateTypeId;
                //rate type (decimal(7,6))
                if (rateToEditInfo.Rate != 0)
                {
                    rateToEditInfo.Rate = Math.Truncate(rateToEditInfo.Rate * 1000000) / 1000000;
                }
                ok = ok || rate.Rate != rateToEditInfo.Rate;

                //proRata type (decimal(8,6))
                if (rateToEditInfo.ProRata != 0)
                {
                    rateToEditInfo.ProRata = Math.Truncate(rateToEditInfo.ProRata * 1000000) / 1000000;
                }
                ok = ok || rate.ProRataRate != rateToEditInfo.ProRata;

                //songRate type (decimal(8,6))
                if (rateToEditInfo.SongRate != 0)
                {
                    rateToEditInfo.SongRate = Math.Truncate(rateToEditInfo.SongRate * 1000000) / 1000000;
                }
                ok = ok || rate.PerSongRate != rateToEditInfo.SongRate;

                ok = ok || (rate.paidQuarter != request.PaidQtr && !(rate.paidQuarter == null && request.PaidQtr == "N/A"));

                //percentOfStat (decimal(8,4))
                if (rateToEditInfo.PercentOfStat != 0)
                {
                    rateToEditInfo.PercentOfStat = Math.Truncate(rateToEditInfo.PercentOfStat * 10000) / 10000;
                }
                ok = ok || (rate.PercentOfStat != rateToEditInfo.PercentOfStat && !(rate.PercentOfStat == null && rateToEditInfo.PercentOfStat == 0));

                rate.EscalatedRate = rateToEditInfo.EscalatedRate;
                rate.RateTypeId = rateToEditInfo.SelectedRateTypeId;
                rate.Rate = rateToEditInfo.Rate;
                rate.ProRataRate = rateToEditInfo.ProRata;
                rate.PerSongRate = rateToEditInfo.SongRate;
                rate.ModifiedDate = DateTime.Now;
                rate.ModifiedBy = request.modifiedBy;
                rate.paidQuarter = request.PaidQtr;
                rate.licenseDate = rateToEditInfo.LicenseDate;
                rate.PercentOfStat = rateToEditInfo.PercentOfStat;
                rate.StatYear = rateToEditInfo.StatYear;
                rate.StatYear = rateToEditInfo.StatYear;



                var allStatuses =
                    _licensePRWriterRateStatusRepository.GetLicenseWriterRateStatus(new List<int>()
                    {
                        rate.LicenseWriterRateId
                    });
                foreach (var status in allStatuses)
                {
                    status.Deleted = DateTime.Now;
                    status.ModifiedBy = 1; //until login is set,
                    status.ModifiedDate = DateTime.Now;
                    status.ModifiedBy = request.modifiedBy;
                    _licensePRWriterRateStatusRepository.Update(status);

                }
                foreach (var statusId in request.SelectedStatusesIds)
                {
                    _licensePRWriterRateStatusRepository.Add(new LicenseProductRecordingWriterRateStatus()
                    {
                        LicenseWriterRateId = rate.LicenseWriterRateId,
                        SpecialStatusId = statusId,
                        CreatedDate = DateTime.Now,
                        CreatedBy = request.modifiedBy
                    });
                }

                //check if the status has been changed
                int x = 0;
                int y = 0;
                bool statusChanged = false;

                if (allStatuses.Count == request.SelectedStatusesIds.Count)
                {
                    while (x < allStatuses.Count && !statusChanged)
                    {
                        bool found = false;
                        y = 0;
                        while (y < request.SelectedStatusesIds.Count && !statusChanged)
                        {
                            if (allStatuses[x].SpecialStatusId == request.SelectedStatusesIds[y])
                            {
                                found = true;
                            }
                            y++;
                        }
                        if (!found)
                        {
                            statusChanged = true;
                        }
                        x++;
                    }
                }
                else
                {
                    statusChanged = true;
                }

                ok = ok || statusChanged;

                //apply writerRateInclude logic

                var writerRateInclude = rate.WriterRateInclude;


                if (rateToEditInfo.SelectedRateTypeId == 2 || rateToEditInfo.SelectedRateTypeId == 5)
                {
                    if (request.Rates.Count > 1)
                    {

                        if (deletedRates.Count + 1 == request.Rates.Count)
                        {

                            for (int i = 1; i < request.Rates.Count; i++)
                            {

                                if (request.Rates[i].EscalatedRate != deletedRates[i - 1].EscalatedRate || request.Rates[i].Rate != deletedRates[i - 1].Rate)
                                {
                                    ok = true;
                                }

                            }

                        }

                    }
                }




                if (ok)
                {


                    writerRateInclude = true;

                    if (currentRequestSpecialStatusList.Count > 0)
                    {
                        foreach (var specialStatus in currentRequestSpecialStatusList)
                        {
                            if (specialStatus == 1 || specialStatus == 2)
                            {
                                writerRateInclude = false;
                            }
                        }
                    }
                    if (writerRateInclude)
                    {
                        //Gratis
                        if (rate.RateTypeId != 8 && (rate.Rate == 0 || rate.PerSongRate == 0))
                        {
                            writerRateInclude = false;
                        }
                    }


                    //if (writerRateInclude)
                    //{
                    //    List<int> currentWriters = new List<int>(rate.LicenseWriterId);
                    //    var currentWriter = _licensePRWriterRepository.GetLicensePrWritersFromIds(currentWriters);
                    //    if (currentWriter.FirstOrDefault().LicensedDate != null)
                    //    {
                    //        writerRateInclude = false;
                    //    }
                    //}

                    rate.WriterRateInclude = writerRateInclude;
                    rate.licenseDate = null;
                }

                //escaleted continue
                if (rateToEditInfo.SelectedRateTypeId == 2 || rateToEditInfo.SelectedRateTypeId == 5)
                {

                    //this is for escaladed rates
                    if (request.Rates.Count > 1)
                    {



                        for (int i = 1; i < request.Rates.Count; i++)
                        {
                            var escaladedRate = new LicenseProductRecordingWriterRate();
                            escaladedRate.LicenseWriterId = GetWriterId(rate, request.Writers);
                            escaladedRate.EscalatedRate = request.Rates[i].EscalatedRate;
                            escaladedRate.PerSongRate = request.Rates[i].Rate;
                            escaladedRate.Rate = request.Rates[i].Rate;
                            escaladedRate.ProRataRate = CalculateProRataRate(escaladedRate.PerSongRate, writerSplit);
                            escaladedRate.RateTypeId = rateToEditInfo.SelectedRateTypeId;
                            escaladedRate.configuration_id = rate.configuration_id;
                            escaladedRate.configuration_name = rate.configuration_name;
                            escaladedRate.CreatedDate = DateTime.Now;
                            escaladedRate.CreatedBy = request.modifiedBy;
                            escaladedRate.writersConsentTypeId = rate.writersConsentTypeId;
                            /* */
                            escaladedRate.product_configuration_id = rate.product_configuration_id;


                            escaladedRate.WriterRateInclude = writerRateInclude;

                            if (deletedRates.Count + 1 == request.Rates.Count && !ok)
                            {
                                escaladedRate.WriterRateInclude = deletedRates[i - 1].WriterRateInclude;
                                escaladedRate.licenseDate = deletedRates[i - 1].licenseDate;

                            }

                            _licensePRWriterRateRepository.Add(escaladedRate);

                        }
                    }

                }



                _licensePRWriterRateRepository.Update(rate);

            }
            foreach (var writer in writers)
            {
                writer.StatYear = rateToEditInfo.StatYear;
                writer.SplitOverride = request.SplitOverride;
                writer.ClaimExceptionOverride = request.ClaimExceptionOverride;
                foreach (var rate in rates)
                {
                    if (rate.LicenseWriterId == writer.LicenseWriterId)
                    {
                        writer.PaidQuarter = rate.paidQuarter;
                    }
                }
                if (request.Dt.Year > 1900) // only write if a reasonable date
                {
                    writer.LicensedDate = (DateTime)request.Dt;
                }
                writer.ModifiedDate = DateTime.Now;
                writer.ModifiedBy = request.modifiedBy;
                _licensePRWriterRepository.Update(writer);

            }
            UpdatePaidQrt(request.LicenseId);
            return true;
        }

        public LicenseTemplate GetLicenseTemplate(int licenseId)
        {
            //var licenseTemplate = new LicenseTemplate();
            //licenseTemplate.License = _licenseRepository.Get(licenseId);
            //licenseTemplate.LicenseProducts = this.GetProducts(licenseId);
            //foreach (var licenseProduct in licenseTemplate.LicenseProducts)
            //{
            //    licenseProduct.Recordings = this.GetLicenseProductRecordingsV2(licenseProduct.LicenseProductId,"");
            //    foreach (var worksRecording in licenseProduct.Recordings)
            //    {
            //        if (worksRecording.Track.Copyrights !=null)
            //        {
            //            var firstOrDefault = worksRecording.Track.Copyrights.FirstOrDefault();
            //            if (firstOrDefault != null && worksRecording.LicenseRecording != null)
            //                worksRecording.Writers =
            //                    this.GetLicenseWritersV2(worksRecording.LicenseRecording.LicenseRecordingId,
            //                        firstOrDefault.WorkCode);
            //        };
            //    }
            //}
            return GetLicenseTemplateV2(licenseId);

        }

        //this method uses Get track with writers for better performance
        public LicenseTemplate GetLicenseTemplateV2(int licenseId)
        {
            var licenseTemplate = new LicenseTemplate();
            licenseTemplate.License = _licenseRepository.Get(licenseId);
            licenseTemplate.LicenseProducts = this.GetProductsV2(licenseId);
            foreach (var licenseProduct in licenseTemplate.LicenseProducts)
            {
                licenseProduct.Recordings = this.GetLicenseProductRecordingsV3(licenseProduct.LicenseProductId, "");
                foreach (var worksRecording in licenseProduct.Recordings)
                {
                    if (worksRecording.Track.Copyrights != null)
                    {
                        var firstOrDefault = worksRecording.Track.Copyrights.FirstOrDefault();
                        if (firstOrDefault != null && worksRecording.LicenseRecording != null)
                            worksRecording.Writers =
                                this.GetLicenseWritersV3(worksRecording.LicenseRecording.LicenseRecordingId,
                                    firstOrDefault.Composers);
                    };
                }
            }
            return licenseTemplate;
        }
        //
        // This function for Cloning Entity Frame work entities
        //
        //Success
        //LicenseId
        //Message
        public CloneLicenseResult CloneLicense(int licenseId, string clonetype, int contactid)
        {
            // This method is to get the heirarchy of a license
            // and clone all the objects. Note we need to go about 5 levels deep.
            //todo:move this in repo... no context init in manager!
            // Create copy of license passed in and post for the id

            // Note: clonetype = Addendum or Copy ... determins the LicenseNumber

            var cloneLicenseResult = new CloneLicenseResult();
            cloneLicenseResult.success = false;
            cloneLicenseResult.errorMessage = string.Empty;
            cloneLicenseResult.licenseId = 0;



            try
            {

                var newLicenseId = 0;

                using (var context = new AuthContext())
                {
                    var sourceLicense = context.Licenses.AsNoTracking()
                        .Where(x => x.LicenseId == licenseId)
                        .FirstOrDefault();

                    var sourceLicenseId = sourceLicense.LicenseId;

                    var newLicense = context.Licenses.Add(sourceLicense);
                    newLicense.LicenseStatusId = 2; // verifying
                    newLicense.CreatedBy = contactid;
                    newLicense.ModifiedBy = contactid;
                    newLicense.CreatedDate = DateTime.Now.AddHours(-8);
                    newLicense.ModifiedDate = DateTime.Now.AddHours(-8);

                    // Increment the LicneseNumber with Dash and version 
                    // increment version if already present
                    // check if "-" in string, if not add -1 if so, increment
                    if (clonetype == "Addendum")
                    {
                        var dashposition = newLicense.LicenseNumber.IndexOf("-");
                        if (dashposition == -1)
                        {
                            // add the first version
                            newLicense.LicenseNumber = newLicense.LicenseNumber + "-1";
                            newLicense.LicenseName = newLicense.LicenseName + "_Rev1";
                        }
                        else
                        {
                            // parse number and increment
                            var versionnum = newLicense.LicenseNumber.Substring(dashposition + 1);
                            var newversionnum = Convert.ToInt16(versionnum) + 1;
                            newLicense.LicenseNumber = newLicense.LicenseNumber.Substring(0, dashposition) + "-" + newversionnum.ToString();
                            if (newversionnum <= 10)
                            {
                                newLicense.LicenseName = newLicense.LicenseName.Substring(0, newLicense.LicenseName.Length - 1) + newversionnum.ToString();
                            }
                            else
                            {
                                newLicense.LicenseName = newLicense.LicenseName.Substring(0, newLicense.LicenseName.Length - 2) + newversionnum.ToString();
                            }

                        }

                    }
                    else
                    {
                        // else assume it is a COPY and a new sequence number is created on licenseNumber

                        var sequence = _licenseSequenceRepository.Get();
                        sequence.LicenseNumber = sequence.LicenseNumber + 1;
                        _licenseSequenceRepository.Update(sequence);
                        newLicense.LicenseNumber = sequence.LicenseNumber.ToString();
                    }

                    //
                    // Add the RelatedLicenseId from the source record- per Michael, not sure what to do with it yet
                    //
                    newLicense.RelatedLicenseId = sourceLicense.LicenseId;
                    newLicense.CreatedBy = contactid;
                    newLicense.ModifiedBy = contactid;
                    if (clonetype == "Copy")
                    {
                        newLicense.LicenseName = newLicense.LicenseName + "_Copy" + newLicense.LicenseNumber.ToString();
                        newLicense.EffectiveDate = null;
                        newLicense.SignedDate = null;
                        newLicense.ReceivedDate = null;
                    };
                    context.SaveChanges();  //post so new License Id is generated
                    newLicenseId = newLicense.LicenseId;

                    if (clonetype == "Copy")
                    {
                        List<LicenseAttachment> sourceLicenseAttachments = _licenseAttachmentManager.GetAllAttachmentsByLicenseId(licenseId);
                        if (sourceLicenseAttachments.Count > 0)
                        {
                            foreach (var attach in sourceLicenseAttachments)
                            {
                                attach.licenseId = newLicense.LicenseId;
                                _licenseAttachmentManager.AddLicenseAttachment(attach);
                            }
                        }
                    }

                }



                // run thru the related tables and add records accordingly
                //  LicenseNotes
                //  LicenseProducts
                //      LicenseProductConfigurations
                //      LicenesRecordings
                //          LicenseWriters
                //              LicenseWriterNotes
                //              LicenseWriterRates
                //                  LicenseWriterRateStatus

                var oldLicenseProductId = 0;
                var oldLicenseProductRecordingId = 0;
                var oldTrackId = 0;

                //  LicenseNotes
                CloneLicenseNotes(licenseId, newLicenseId);

                //  LicenseProducts
                CloneLicenseProducts(licenseId, oldLicenseProductId, oldLicenseProductRecordingId, oldTrackId, clonetype,
                    contactid, newLicenseId);
                
                cloneLicenseResult.licenseId = newLicenseId;

                cloneLicenseResult.success = true;



                //return true;

            }
            catch (Exception ex)
            {
                cloneLicenseResult.errorMessage = ex.Message.ToString();
                cloneLicenseResult.licenseId = 0;
                cloneLicenseResult.success = false;
                //return false;
            }
            return cloneLicenseResult;


            /* using the LicenseTemplate approach was significantly slower
            if (type == "template")
            {
                var oldLicenseTemplate = GetLicenseTemplate(licenseId);

                //  need to add notes to Template if necessary
                //  LicenseNotes 

                var oldLicenseNotes = _licenseNoteRepository.GetLicenseNotes(licenseId);
                foreach (var licenseNote in oldLicenseNotes)
                {
                    licenseNote.licenseId = newLicenseId;
                    licenseNote.licenseNoteId = 0;
                    var newLicenseNote = _licenseNoteRepository.Add(licenseNote);
                }


                //  LicenseProduct
                foreach (var oldLicenseProduct in oldLicenseTemplate.LicenseProducts)
                {
                    var oldLicenseProductId = oldLicenseProduct.LicenseProductId;

                    var newLicenseProduct = oldLicenseProduct;
                    newLicenseProduct.LicenseId = newLicenseId;
                    newLicenseProduct = _licenseProductRepository.Add(newLicenseProduct);

                    //  LicenseProductConfiguration
                    foreach (var oldLicenseProductConfiguration in oldLicenseProduct.ProductHeader.Configurations)
                    {

                        var oldLicenseProductConfigurationId = oldLicenseProductConfiguration.LicenseProductConfiguration.LicenseProductConfigurationId;

                        var newLicenseProductConfiguration = oldLicenseProductConfiguration.LicenseProductConfiguration;
                        newLicenseProductConfiguration.LicenseProductConfigurationId = 0;
                        newLicenseProductConfiguration.LicenseProductId = newLicenseProduct.LicenseProductId;
                        newLicenseProductConfiguration = _licenseProductConfigurationRepository.Add(newLicenseProductConfiguration);
                    }

                    //  LicenseRecording
                    foreach (var oldLicenseProductRecording in oldLicenseProduct.Recordings)
                    {
                        var oldLicenseProductRecordingId = oldLicenseProductRecording.LicenseRecording.LicenseRecordingId;

                        var newLicenseProductRecording = oldLicenseProductRecording.LicenseRecording;
                        newLicenseProductRecording.LicenseProductId = newLicenseProduct.LicenseProductId;
                        newLicenseProductRecording.LicenseRecordingId = 0;
                        newLicenseProductRecording = _licenseProductRecordingRepository.Add(newLicenseProductRecording);

                        //  LicenseWriter
                        foreach (var oldWriter in oldLicenseProductRecording.Writers)
                        {
                            var oldWriterId = oldWriter.LicenseProductRecordingWriter.LicenseWriterId;
                            var newWriter = oldWriter.LicenseProductRecordingWriter;
                            newWriter.LicenseRecordingId = newLicenseProductRecording.LicenseRecordingId;
                            newWriter.LicenseWriterId = 0;
                            newWriter = _licensePRWriterRepository.Add(newWriter);

                            // LicenseWriterStatus
                            foreach (var oldWriterStatus in oldWriter.LicenseProductRecordingWriter.SpecialStatusList)
                            {
                                var newWriterStatus = oldWriterStatus;
                                newWriterStatus.LicenseWriterId = newWriter.LicenseWriterId;
                                newWriterStatus.LicenseWriterStatusId = 0;
                                newWriterStatus = _licensePRWriterStatusRepository.Add(newWriterStatus);
                            }

                            //  LicenseWriterRate
                            foreach (var oldWriterRate in oldWriter.LicenseProductRecordingWriter.RateList)
                            {
                                var oldWriterRateNotes = oldWriterRate.WriterRateNotes;
                                var newWriterRate = oldWriterRate;
                                newWriterRate.RateType = null;
                                newWriterRate.WriterRateNotes = null;
                                newWriterRate.LicenseWriterId = newWriter.LicenseWriterId;
                                newWriterRate.LicenseWriterRateId = 0;
                                    
                                //var newWriterRate = new LicenseProductRecordingWriterRate
                                //{
                                //    LicenseWriterId = newWriter.LicenseWriterId,
                                //    LicenseWriterRateId = 0,
                                //    configuration_id = oldWriterRate.configuration_id,
                                //    configuration_name = oldWriterRate.configuration_name,
                                //    CreatedBy = oldWriterRate.CreatedBy,
                                //    CreatedDate = oldWriterRate.CreatedDate,
                                //    Deleted = oldWriterRate.Deleted,
                                //    ModifiedBy = oldWriterRate.ModifiedBy,
                                //    ModifiedDate = oldWriterRate.ModifiedDate,
                                //    EscalatedRate = oldWriterRate.EscalatedRate,
                                //    RateTypeId = oldWriterRate.RateTypeId,
                                //    LongStatRate = oldWriterRate.LongStatRate,
                                //    MostRecentNote = oldWriterRate.MostRecentNote,
                                //    PerSongRate = oldWriterRate.PerSongRate,
                                //    ProRataRate = oldWriterRate.ProRataRate,
                                //    Rate = oldWriterRate.Rate,
                                //    RateNoteCount = oldWriterRate.RateNoteCount, 
                                //    RateType = oldWriterRate.RateType
                                //};
                                    
                                newWriterRate = _licensePRWriterRateRepository.Add(newWriterRate);

                                //  LicenseWriterRateNotes
                                foreach (var oldWriterRateNote in oldWriterRateNotes)
                                {
                                    var newWriterRateNote = oldWriterRateNote;
                                    newWriterRateNote.LicenseWriterRateId = newWriterRate.LicenseWriterRateId;
                                    newWriterRateNote.LicenseWriterRateNoteId = 0;
                                    newWriterRateNote = _licensePRWriterRateNoteRepository.Add(newWriterRateNote);
                                }


                            }
                        }

                    }

                }
            }
            */

        }



        public bool EditWriterIsIncluded(EditWriterIncludedSaveRequest request)
        {
            List<LicenseProductRecordingWriterRate> lwritersRates;
            List<int> writers;
            switch (request.SelectedConsentTypeId)
            {
                case 1:  // (Applies consent to only this track and this configuration)
                    var localList = new List<int>();
                    localList.Add(request.SelectedConfigId);
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    request.SaveWriterIds, localList);
                    foreach (var rate in lwritersRates)
                    {
                        rate.WriterRateInclude = request.IsIncluded;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);

                    }
                    break;
                case 2: // (Applies consent to the track, including all configurations )
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    request.SaveWriterIds, new List<int>());
                    foreach (var rate in lwritersRates)
                    {
                        rate.WriterRateInclude = request.IsIncluded;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);

                    }
                    break;
                case 3:  // (Applies consent to all like configurations for that writer)
                    writers = new List<int>();
                    foreach (var recId in request.SelectedRecordingIds)
                    {
                        writers.AddRange(_licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recId).Where(x => x.CAECode == request.SelecterWriterCae).Select(x => x.LicenseWriterId));
                    }
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    writers, new List<int>() { request.SelectedConfigId });
                    foreach (var rate in lwritersRates)
                    {
                        rate.WriterRateInclude = request.IsIncluded;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);

                    }
                    break;
                case 4:  // (Applies consent to that Writer across all tracks)
                    writers = new List<int>();
                    foreach (var recId in request.SelectedRecordingIds)
                    {
                        writers.AddRange(_licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recId).Where(x => x.CAECode == request.SelecterWriterCae).Select(x => x.LicenseWriterId));
                    }
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    writers, new List<int>());
                    foreach (var rate in lwritersRates)
                    {
                        rate.WriterRateInclude = request.IsIncluded;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);

                    }
                    break;
                case 5:  // (Clears All)
                    writers = new List<int>();
                    foreach (var recId in request.SelectedRecordingIds)
                    {
                        writers.AddRange(_licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recId).Where(x => x.CAECode == request.SelecterWriterCae).Select(x => x.LicenseWriterId));
                    }
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    writers, new List<int>());
                    foreach (var rate in lwritersRates)
                    {
                        rate.WriterRateInclude = request.IsIncluded;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);

                    }
                    break;
            };
            return true;
        }


        public bool EditWriterConsent(EditWriterConsentSaveRequest request)
        {
            List<LicenseProductRecordingWriterRate> lwritersRates;
            List<int> writers;
            switch (request.SelectedConsentTypeId)
            {
                case 1:  // (Applies consent to only this track and this configuration)
                    var localList = new List<int>();
                    localList.Add(request.SelectedConfigId);
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    request.SaveWriterIds, localList);
                    foreach (var rate in lwritersRates)
                    {
                        rate.writersConsentTypeId = request.SelectedConsentTypeId;
                        rate.writersConsentDate = DateTime.Now;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);
                    }
                    break;
                case 2: // (Applies consent to the track, including all configurations )
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    request.SaveWriterIds, new List<int>());
                    foreach (var rate in lwritersRates)
                    {
                        rate.writersConsentTypeId = request.SelectedConsentTypeId;
                        rate.writersConsentDate = DateTime.Now;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);
                    }
                    break;
                case 3:  // (Applies consent to all like configurations for that writer)
                    writers = new List<int>();
                    foreach (var recId in request.SelectedRecordingIds)
                    {
                        writers.AddRange(_licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recId).Where(x => x.CAECode == request.SelecterWriterCae).Select(x => x.LicenseWriterId));
                    }
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    writers, new List<int>() { request.SelectedConfigId });
                    foreach (var rate in lwritersRates)
                    {
                        rate.writersConsentTypeId = request.SelectedConsentTypeId;
                        rate.writersConsentDate = DateTime.Now;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);
                    }
                    break;
                case 4:  // (Applies consent to that Writer across all tracks)
                    writers = new List<int>();
                    foreach (var recId in request.SelectedRecordingIds)
                    {
                        writers.AddRange(_licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recId).Where(x => x.CAECode == request.SelecterWriterCae).Select(x => x.LicenseWriterId));
                    }
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    writers, new List<int>());
                    foreach (var rate in lwritersRates)
                    {
                        rate.writersConsentTypeId = request.SelectedConsentTypeId;
                        rate.writersConsentDate = DateTime.Now;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);
                    }
                    break;
                case 5:  // (Clears All)
                    writers = new List<int>();
                    foreach (var recId in request.SelectedRecordingIds)
                    {
                        writers.AddRange(_licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recId).Where(x => x.CAECode == request.SelecterWriterCae).Select(x => x.LicenseWriterId));
                    }
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    writers, new List<int>());
                    foreach (var rate in lwritersRates)
                    {
                        rate.writersConsentTypeId = request.SelectedConsentTypeId;
                        rate.writersConsentDate = DateTime.Now;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);
                    }
                    break;
            };
            return true;
        }

        public bool EditPaidQuarter(EditPaidQuarterSaveRequest request)
        {
            List<LicenseProductRecordingWriterRate> lwritersRates;
            List<int> writers;
            List<LicenseProductRecordingWriter> writersList;
            switch (request.SelectedConsentTypeId)
            {
                case 1:  // (Applies consent to only this track and this configuration)
                    var localList = new List<int>();
                    localList.Add(request.SelectedConfigId);
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    request.SaveWriterIds, localList);
                    foreach (var rate in lwritersRates)
                    {
                        rate.paidQuarter = request.PaidQuarter;
                        rate.writersConsentDate = DateTime.Now;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);
                    }
                    writersList = _licensePRWriterRepository.GetLicensePrWritersFromIds(request.SaveWriterIds);
                    foreach (var licenseProductRecordingWriter in writersList)
                    {
                        licenseProductRecordingWriter.PaidQuarter = request.PaidQuarter;
                        _licensePRWriterRepository.Update(licenseProductRecordingWriter);
                    }
                    break;
                case 2: // (Applies consent to the track, including all configurations )
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    request.SaveWriterIds, new List<int>());
                    foreach (var rate in lwritersRates)
                    {
                        rate.paidQuarter = request.PaidQuarter;
                        rate.writersConsentDate = DateTime.Now;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);
                    }
                    writersList = _licensePRWriterRepository.GetLicensePrWritersFromIds(request.SaveWriterIds);
                    foreach (var licenseProductRecordingWriter in writersList)
                    {
                        licenseProductRecordingWriter.PaidQuarter = request.PaidQuarter;
                        _licensePRWriterRepository.Update(licenseProductRecordingWriter);
                    }
                    break;
                case 3:  // (Applies consent to all like configurations for that writer)
                    writers = new List<int>();
                    foreach (var recId in request.SelectedRecordingIds)
                    {
                        writers.AddRange(_licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recId).Where(x => x.CAECode == request.SelecterWriterCae).Select(x => x.LicenseWriterId));
                    }
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    writers, new List<int>() { request.SelectedConfigId });
                    foreach (var rate in lwritersRates)
                    {
                        rate.paidQuarter = request.PaidQuarter;
                        rate.writersConsentDate = DateTime.Now;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);
                    }
                    writersList = _licensePRWriterRepository.GetLicensePrWritersFromIds(writers);
                    foreach (var licenseProductRecordingWriter in writersList)
                    {
                        licenseProductRecordingWriter.PaidQuarter = request.PaidQuarter;
                        licenseProductRecordingWriter.ModifiedDate = DateTime.Now;
                        _licensePRWriterRepository.Update(licenseProductRecordingWriter);
                    }
                    break;
                case 4:  // (Applies consent to that Writer across all tracks)
                    writers = new List<int>();
                    foreach (var recId in request.SelectedRecordingIds)
                    {
                        writers.AddRange(_licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recId).Where(x => x.CAECode == request.SelecterWriterCae).Select(x => x.LicenseWriterId));
                    }
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    writers, new List<int>());
                    foreach (var rate in lwritersRates)
                    {
                        rate.paidQuarter = request.PaidQuarter;
                        rate.writersConsentDate = DateTime.Now;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);
                    }
                    writersList = _licensePRWriterRepository.GetLicensePrWritersFromIds(writers);
                    foreach (var licenseProductRecordingWriter in writersList)
                    {
                        licenseProductRecordingWriter.PaidQuarter = request.PaidQuarter;
                        licenseProductRecordingWriter.ModifiedDate = DateTime.Now;

                        _licensePRWriterRepository.Update(licenseProductRecordingWriter);
                    }
                    break;
                case 5:  // (Clears All)
                    writers = new List<int>();
                    foreach (var recId in request.SelectedRecordingIds)
                    {
                        writers.AddRange(_licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recId).Where(x => x.CAECode == request.SelecterWriterCae).Select(x => x.LicenseWriterId));
                    }
                    lwritersRates = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(
                    writers, new List<int>());
                    foreach (var rate in lwritersRates)
                    {
                        rate.paidQuarter = request.PaidQuarter;
                        rate.writersConsentDate = DateTime.Now;
                        rate.ModifiedDate = DateTime.Now;
                        _licensePRWriterRateRepository.Update(rate);
                    }
                    writersList = _licensePRWriterRepository.GetLicensePrWritersFromIds(writers);
                    foreach (var licenseProductRecordingWriter in writersList)
                    {
                        licenseProductRecordingWriter.PaidQuarter = request.PaidQuarter;
                        licenseProductRecordingWriter.ModifiedDate = DateTime.Now;

                        _licensePRWriterRepository.Update(licenseProductRecordingWriter);
                    }
                    break;
            };
            UpdatePaidQrt(request.LicenseId);
            return true;
        }

        public void UpdateLicenseProductSchedule(List<LicenseProduct> licenseProducts)
        {
            foreach (var licenseProduct in licenseProducts)
            {
                var localProduct = _licenseProductRepository.Get(licenseProduct.LicenseProductId);
                localProduct.ScheduleId = licenseProduct.ScheduleId;
                licenseProduct.ModifiedDate = DateTime.Now;

                _licenseProductRepository.Update(licenseProduct);
            }
        }
       
        private bool IsLessThanOrEqualTo5Min(LicenseProductRecordingWriterRate rate, List<WorksWriter> writers)
        {
            var duration = "";
            var rv = false;

            foreach (var worksWriter in writers)
            {
                foreach (var lrate in worksWriter.LicenseProductRecordingWriter.RateList)
                {
                    if (lrate.LicenseWriterRateId == rate.LicenseWriterRateId)
                    {
                        duration = worksWriter.ParentSongDuration;
                        break;
                    }
                }
            }

            //Song length greater than 5 min and 0 seconds return FALSE
            //USL-1110 
            try
            {
                var minutes = int.Parse(duration.Substring(3, 2));
                var seconds = int.Parse(duration.Substring(6, 2));
                //If song length is less than or equal to 5 min in length returns TRUE.
                if (minutes < 5 || (minutes == 5 && seconds == 0))
                {
                    rv = true;
                }

            }
            catch (Exception e)
            {
                rv = false;
            }
            return rv;
        }

        private int GetSongDuration(LicenseProductRecordingWriterRate rate, List<WorksWriter> writers)
        {
            var duration = "";
            foreach (var worksWriter in writers)
            {
                foreach (var lrate in worksWriter.LicenseProductRecordingWriter.RateList)
                {
                    if (lrate.LicenseWriterRateId == rate.LicenseWriterRateId)
                    {
                        duration = worksWriter.ParentSongDuration;
                        break;
                    }
                }
            }
            var minutes = int.Parse(duration.Substring(3, 2));
            var hours = int.Parse(duration.Substring(0, 2));
            var seconds = int.Parse(duration.Substring(6, 2));
            if (seconds > 0)
            {
                minutes++;
            }
            return hours * 60 + minutes;
        }

        private decimal GetSplit(LicenseProductRecordingWriterRate rate, List<WorksWriter> writers)
        {
            decimal split = 0;
            foreach (var worksWriter in writers)
            {
                foreach (var lrate in worksWriter.LicenseProductRecordingWriter.RateList)
                {
                    if (lrate.LicenseWriterRateId == rate.LicenseWriterRateId)
                    {
                        split = (decimal)worksWriter.Contribution;
                        break;
                    }
                }
            }
            return split;
        }
        private int GetWriterId(LicenseProductRecordingWriterRate rate, List<WorksWriter> writers)
        {
            int writerId = 0;
            foreach (var worksWriter in writers)
            {
                foreach (var lrate in worksWriter.LicenseProductRecordingWriter.RateList)
                {
                    if (lrate.LicenseWriterRateId == rate.LicenseWriterRateId)
                    {
                        writerId = worksWriter.LicenseProductRecordingWriter.LicenseWriterId;
                        break;
                    }
                }
            }
            return writerId;
        }

        private decimal CalculateRate(float statRate, decimal statPercentage)
        {
            return (decimal)statRate * statPercentage / 100;
        }
        private decimal CalculateSongRate(float statRate, decimal statPercentage)
        {
            return (decimal)statRate * statPercentage / 100;
        }
        private decimal CalculateProRataRate(decimal perSongRate, decimal? split)
        {
            if (split != null) return perSongRate * (decimal)split / 100;
            return 0;
        }

        private void UpdatePaidQrt(int licenseId)
        {
            var products = _licenseProductRepository.GetLicenseProducts(licenseId);
            var tracks = _licenseProductRecordingRepository.GetLicenseProductRecordingsFromList(products.Select(x => x.LicenseProductId).ToList());
            var writers = _licensePRWriterRepository.GetLicenseWriterList(tracks.Select(x => x.LicenseRecordingId).ToList());
            foreach (var track in tracks)
            {
                var trackWriters = writers.Where(x => x.LicenseRecordingId == track.LicenseRecordingId).ToList();
                track.PaidQuarter = GetSmallesQrt(trackWriters.Select(x => x.PaidQuarter).ToList());
                track.ModifiedDate = DateTime.Now;

                _licenseProductRecordingRepository.Update(track);
            }
            foreach (var licenseProduct in products)
            {
                var prodTracks = tracks.Where(x => x.LicenseProductId == licenseProduct.LicenseProductId).ToList();

                //9/23/2015 filtered out paidQuarter = "N/A" to keep from blowing up.  may want to change this so "N/A" is the Smallest?
                //licenseProduct.PaidQuarter = GetSmallesQrt(prodTracks.Select(x => x.PaidQuarter).ToList());
                licenseProduct.PaidQuarter = GetSmallesQrt(prodTracks.Where(pt => pt.PaidQuarter != "N/A").Select(pq => pq.PaidQuarter).ToList());
                licenseProduct.ModifiedDate = DateTime.Now;

                _licenseProductRepository.Update(licenseProduct);
            }
        }

        private bool CompareQrt(string qrt1, string qrt2)
        {
            if (qrt1 == "N/A" && qrt2 != "N/A")
            {
                return false;
            }
            if (qrt1 != "N/A" && qrt2 == "N/A")
            {
                return true;
            }
            if (qrt1 == "N/A" && qrt2 == "N/A")
            {
                return true;
            }
            var year1 = int.Parse(qrt1.Substring(2, 2));
            var year2 = int.Parse(qrt2.Substring(2, 2));
            var qrta = int.Parse(qrt1.Substring(0, 1));
            var qrtb = int.Parse(qrt2.Substring(0, 1));
            if (year1 < year2)
            {
                return true;
            }
            if (year1 > year2)
            {
                return false;
            }
            if (qrta < qrtb)
            {
                return true;
            }
            return false;
        }

        private string GetSmallesQrt(List<string> paidQrts)
        {
            string lreturn = "";
            if (paidQrts.Count == 0)
            {
                return lreturn;
            }
            lreturn = paidQrts.FirstOrDefault(x => x != null);
            if (lreturn != null)
            {
                for (int i = 1; i < paidQrts.Count; i++)
                {
                    if (paidQrts[i] != null)
                    {
                        if (CompareQrt(paidQrts[i], lreturn))
                        {
                            lreturn = paidQrts[i];
                        }
                    }

                }
            }
            return lreturn;
        }
        private Dictionary<string, int> GetStatusRollup(List<int> writerRatesIds)
        {
            var lReturnlist = new Dictionary<string, int>();
            var statuses = _licensePRWriterRateStatusRepository.GetLicenseWriterRateStatus(writerRatesIds);

            foreach (var status in statuses)
            {

                if (lReturnlist.ContainsKey(status.LU_SpecialStatuses.SpecialStatus.ToLower()))
                {
                    lReturnlist[status.LU_SpecialStatuses.SpecialStatus.ToLower()]++;
                }
                else
                {
                    lReturnlist.Add(status.LU_SpecialStatuses.SpecialStatus.ToLower(), 1);
                }
            }
            return lReturnlist;
        }

        private List<WorksRecording> GetLicenseProductRecordingsForLicenseDetailsNew(int licenseProductId, License license, LicenseProduct licenseProduct, out List<LicenseProductConfigurationTotals> totals)
        {
            var starttime = DateTime.Now;

            // Mechs get license Product
            // fix #4 pass in from parent
            //var licenseProduct = _licenseProductRepository.Get(licenseProductId);

            // Mechs get license

            //Fix #3 passing in object, so not to call a 2nd time
            //var license = _licenseRepository.GetLite(licenseProduct.LicenseId);

            // get a lite version of recordings
            // Mechs LicenseProductRecording
            var mechsRecordings = _licenseProductRecordingRepository.GetLicenseProductRecordingsBrief(licenseProductId);

            //Fix #1 - have to get the Mechs recording number here
            licenseProduct.LicensePRecordingsNo = mechsRecordings.Count;

            bool isExecuted = license.LicenseStatusId == 5 || license.LicenseStatusId == 7;

            //get recording details

//            // RECs Recordings data 
            var recsTracks = _recsProvider.RetrieveTracksWithWriters(licenseProduct.ProductId);

            var licenseProductConfigIdTotals = GetLicenseProductConfigurationIdTotals(licenseProductId);

            // 20160525 - fix for recording/product rollup amounts for products with multiple configs of same type
            var licenseProductConfigIdUniqueCount =
                Convert.ToDecimal(licenseProductConfigIdTotals.Select(c => c.configuration_id).Distinct().Count());

            //iterate through the Recording details
            foreach (var worksRecording in recsTracks)
            {

                var totalpercentage = 0.00;

                worksRecording.Message = string.Empty;

                // here you we marry the Recs Recordings to Mechs Recordings
                foreach (var recording in mechsRecordings)
                {
                    if (recording.TrackId == worksRecording.Track.Id)
                    {
                        // get the licenserecording object
                        worksRecording.LicenseRecording = recording;
                       
                       // license Product Writers

                        starttime = DateTime.Now;
                        var licenseProductRecordingWriters = _licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recording.LicenseRecordingId);
                        //Fix #6 move this below call above, and just get the count
                        //worksRecording.LicenseRecording.LicensePRWriterNo = _licensePRWriterRepository.GetLicenseProductRecordingWritersNo(recording.LicenseRecordingId);
                        worksRecording.LicenseRecording.LicensePRWriterNo = licenseProductRecordingWriters.Count;

                        //Fix #7 query collection for writers with isLicensed = true
                        //worksRecording.LicenseRecording.LicensePRLicensedWriterNo = _licensePRWriterRepository.GetLicenseProductRecordingLicensedWritersNo(recording.LicenseRecordingId);
                        worksRecording.LicenseRecording.LicensePRLicensedWriterNo = licenseProductRecordingWriters.Where(x => x.isLicensed == true).Count();
           
                        worksRecording.LicenseRecording.LicensePRUnLicensedWriterNo = worksRecording.LicenseRecording.LicensePRWriterNo - worksRecording.LicenseRecording.LicensePRLicensedWriterNo;

                        var licensedpercentage = 0.00;
                        if (worksRecording.Track.Copyrights != null)
                        {
                            recording.WorkCode = worksRecording.Track.Copyrights.FirstOrDefault().WorkCode;

                            //var writers = _recsProvider.RetrieveTrackWriters(recording.WorkCode);
                            var firstOrDefault = worksRecording.Track.Copyrights.FirstOrDefault();
                            if (firstOrDefault != null)
                            {
                                starttime = DateTime.Now;
                                worksRecording.Writers = GetLicenseWritersV3(recording.LicenseRecordingId, firstOrDefault.Composers);

                                var writers = firstOrDefault.Composers;
                                foreach (var writer in writers)
                                {
                                    writer.Contribution = (float?)GetContribution(writer);
                                    //filter for controlled writers
                                    if (writer.Controlled)
                                    {
                                        var actualcontribution = (double)writer.Contribution;
                                        if (writer.Controlled)
                                        {
                                            var licenseProductRecordingWriter =
                                                licenseProductRecordingWriters.Where(
                                                    x => x.CAECode == writer.CaeNumber && x.isLicensed == true)
                                                    .FirstOrDefault();
                                            if (licenseProductRecordingWriter != null)
                                            {
                                                var writerLicensedConfigIds =
                                                    _licensePRWriterRateRepository.GetLicensedConfigIds(
                                                        licenseProductRecordingWriter.LicenseWriterId);

                                                double licensedConfigFactor = 1;
                                                if (licenseProductConfigIdTotals.Count > 0 &&
                                                    writerLicensedConfigIds.Count > 0)
                                                {
                                                    // 20160525 - fix for recording/product rollup amounts for products with multiple configs of same type
                                                    licensedConfigFactor =
                                                        Convert.ToDouble(
                                                            Convert.ToDecimal(writerLicensedConfigIds.Count) /
                                                            licenseProductConfigIdUniqueCount);
                                                    /*
                                                    licensedConfigFactor =
                                                        Convert.ToDouble(
                                                            Convert.ToDecimal(writerLicensedConfigIds.Count) /
                                                            Convert.ToDecimal(licenseProductConfigIdTotals.Count));
                                                    */
                                                }

                                                if (isExecuted)
                                                {
                                                    if (licenseProductRecordingWriter.ExecutedSplit != null)
                                                    {
                                                        actualcontribution =
                                                            Convert.ToDouble(licenseProductRecordingWriter.ExecutedSplit);
                                                    }
                                                }
                                                else
                                                {
                                                    if (licenseProductRecordingWriter.SplitOverride != null)
                                                    {
                                                        actualcontribution =
                                                            Convert.ToDouble(licenseProductRecordingWriter.SplitOverride);
                                                    }
                                                    else if (licenseProductRecordingWriter.ClaimExceptionOverride != null)
                                                    {
                                                        actualcontribution =
                                                            Convert.ToDouble(
                                                                licenseProductRecordingWriter.ClaimExceptionOverride);
                                                    }
                                                }
                                                licensedpercentage += (actualcontribution * licensedConfigFactor);
                                                foreach (var writerLicenseConfigId in writerLicensedConfigIds)
                                                {
                                                    foreach (var licenseProductConfigId in licenseProductConfigIdTotals)
                                                    {
                                                        if (writerLicenseConfigId == licenseProductConfigId.configuration_id)
                                                        {
                                                            licenseProductConfigId.LicensedAmount = licenseProductConfigId.LicensedAmount + actualcontribution;
                                                        }
                                                    }
                                                }
                                            }
                                            totalpercentage = totalpercentage + actualcontribution;

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            recording.WorkCode = "N/A";
                            worksRecording.Message = "No WorkCode";
                        }
                        var listOfWriterIds = new List<int>();
                        listOfWriterIds.Add(worksRecording.LicenseRecording.LicenseRecordingId);

                        var licenseWriterIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(listOfWriterIds).ToList();

                        var licenseWriterRateIds = _licensePRWriterRateRepository.GetLicenseRecordingWriterRateIds(licenseWriterIds)
                            .Select(x => x.LicenseWriterRateId).ToList();

                        //populate key value with statuses and their occurances
                        if (worksRecording.LicenseRecording != null)
                            worksRecording.LicenseRecording.StatusRollup = GetStatusRollup(licenseWriterRateIds);


                        worksRecording.UmpgPercentageRollup = totalpercentage;
                        worksRecording.LicensedRollup = licensedpercentage;

                        // fix #5 break out when matching tracks
                        break;

                    }

                    if (worksRecording.LicenseRecording == null)
                    {
                        worksRecording.LicensedRollup = 0.00;

                    }
                }
            }
            totals = licenseProductConfigIdTotals;
            return recsTracks;
        }


        private List<WorksRecording> GetLicenseProductRecordingsForLicenseDetails(int licenseProductId, out List<LicenseProductConfigurationTotals> totals)
        {
            // get license Product
            var licenseProduct = _licenseProductRepository.Get(licenseProductId);

            // get license
            var license = _licenseRepository.GetLite(licenseProduct.LicenseId);

            // get a lite version of recordings
            
            var recordings = _licenseProductRecordingRepository.GetLicenseProductRecordingsBrief(licenseProductId);

            bool isExecuted = license.LicenseStatusId == 5 || license.LicenseStatusId == 7;
            
            //get recording details
                        
            var recsTracks = _recsProvider.RetrieveTracksWithWriters(licenseProduct.ProductId);

            var licenseProductConfigIdTotals = GetLicenseProductConfigurationIdTotals(licenseProductId);

            // 20160525 - fix for recording/product rollup amounts for products with multiple configs of same type
            var licenseProductConfigIdUniqueCount =
                Convert.ToDecimal(licenseProductConfigIdTotals.Select(c => c.configuration_id).Distinct().Count());
            
            //iterate through the Recording details
            foreach (var worksRecording in recsTracks)
            {

                var totalpercentage = 0.00;

                worksRecording.Message = string.Empty;

                // here you have another list of ProductRecordings and your loop through to match up
                foreach (var recording in recordings)
                {
                    if (recording.TrackId == worksRecording.Track.Id)
                    {
                        // get the licenserecording object
                        worksRecording.LicenseRecording = recording;
                        worksRecording.LicenseRecording.LicensePRWriterNo =
                             _licensePRWriterRepository.GetLicenseProductRecordingWritersNo(recording.LicenseRecordingId);
                        worksRecording.LicenseRecording.LicensePRLicensedWriterNo =
                             _licensePRWriterRepository.GetLicenseProductRecordingLicensedWritersNo(recording.LicenseRecordingId);
                        worksRecording.LicenseRecording.LicensePRUnLicensedWriterNo =
                           worksRecording.LicenseRecording.LicensePRWriterNo - worksRecording.LicenseRecording.LicensePRLicensedWriterNo;

                        // license Product Writers

                        var licenseProductRecordingWriters = _licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(recording.LicenseRecordingId);

                        var licensedpercentage = 0.00;
                        if (worksRecording.Track.Copyrights != null)
                        {
                            recording.WorkCode = worksRecording.Track.Copyrights.FirstOrDefault().WorkCode;

                            //var writers = _recsProvider.RetrieveTrackWriters(recording.WorkCode);
                            var firstOrDefault = worksRecording.Track.Copyrights.FirstOrDefault();
                            if (firstOrDefault != null)
                            {
                                worksRecording.Writers = GetLicenseWritersV3(recording.LicenseRecordingId,firstOrDefault.Composers);

                                var writers = firstOrDefault.Composers;
                                foreach (var writer in writers)
                                {
                                    writer.Contribution = (float?) GetContribution(writer);
                                    //filter for controlled writers
                                    if (writer.Controlled)
                                    {
                                        var actualcontribution = (double)writer.Contribution;
                                        if (writer.Controlled)
                                        {
                                            var licenseProductRecordingWriter =
                                                licenseProductRecordingWriters.Where(
                                                    x => x.CAECode == writer.CaeNumber && x.isLicensed == true)
                                                    .FirstOrDefault();
                                            if (licenseProductRecordingWriter != null)
                                            {
                                                var writerLicensedConfigIds =
                                                    _licensePRWriterRateRepository.GetLicensedConfigIds(
                                                        licenseProductRecordingWriter.LicenseWriterId);

                                                double licensedConfigFactor = 1;
                                                if (licenseProductConfigIdTotals.Count > 0 &&
                                                    writerLicensedConfigIds.Count > 0)
                                                {
                                                    // 20160525 - fix for recording/product rollup amounts for products with multiple configs of same type
                                                    licensedConfigFactor =
                                                        Convert.ToDouble(
                                                            Convert.ToDecimal(writerLicensedConfigIds.Count)/
                                                            licenseProductConfigIdUniqueCount);
                                                    /*
                                                    licensedConfigFactor =
                                                        Convert.ToDouble(
                                                            Convert.ToDecimal(writerLicensedConfigIds.Count) /
                                                            Convert.ToDecimal(licenseProductConfigIdTotals.Count));
                                                    */
                                                }

                                                if (isExecuted)
                                                {
                                                    if (licenseProductRecordingWriter.ExecutedSplit != null)
                                                    {
                                                        actualcontribution =
                                                            Convert.ToDouble(licenseProductRecordingWriter.ExecutedSplit);
                                                    }
                                                }
                                                else
                                                {
                                                    if (licenseProductRecordingWriter.SplitOverride != null)
                                                    {
                                                        actualcontribution =
                                                            Convert.ToDouble(licenseProductRecordingWriter.SplitOverride);
                                                    }
                                                    else if (licenseProductRecordingWriter.ClaimExceptionOverride != null)
                                                    {
                                                        actualcontribution =
                                                            Convert.ToDouble(
                                                                licenseProductRecordingWriter.ClaimExceptionOverride);
                                                    }
                                                }
                                                licensedpercentage += (actualcontribution * licensedConfigFactor);
                                                 foreach (var writerLicenseConfigId in writerLicensedConfigIds)
                                                 {
                                                    foreach (var licenseProductConfigId in licenseProductConfigIdTotals)
                                                    {
                                                        if (writerLicenseConfigId == licenseProductConfigId.configuration_id)
                                                        {
                                                            licenseProductConfigId.LicensedAmount = licenseProductConfigId.LicensedAmount + actualcontribution;
                                                        }
                                                    }
                                                }
                                            }
                                            totalpercentage = totalpercentage + actualcontribution;

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            recording.WorkCode = "N/A";
                            worksRecording.Message = "No WorkCode";
                        }
                        var listOfWriterIds = new List<int>();
                        listOfWriterIds.Add(worksRecording.LicenseRecording.LicenseRecordingId);

                        var licenseWriterIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(listOfWriterIds).ToList();

                        var licenseWriterRateIds = _licensePRWriterRateRepository.GetLicenseRecordingWriterRateIds(licenseWriterIds)
                            .Select(x => x.LicenseWriterRateId).ToList();

                        //populate key value with statuses and their occurances
                        if (worksRecording.LicenseRecording != null)
                            worksRecording.LicenseRecording.StatusRollup = GetStatusRollup(licenseWriterRateIds);


                        worksRecording.UmpgPercentageRollup = totalpercentage;
                        worksRecording.LicensedRollup = licensedpercentage;

                        
                    }

                    if (worksRecording.LicenseRecording == null)
                    {
                        worksRecording.LicensedRollup = 0.00;

                    }
                }
            }
            totals = licenseProductConfigIdTotals;
            return recsTracks;
        }
        private double GetContribution(WorksWriter writer)
        {
            double contribution = (from originalPublisher in writer.OriginalPublishers from writerBase in originalPublisher.Administrator where writerBase.Controlled select writerBase).Aggregate<WriterBase, double>(0, (current, writerBase) => current + (double)writerBase.MechanicalCollectablePercentage);
            return writer.OriginalPublishers.Where(originalPublisher => originalPublisher.Controlled).Aggregate(contribution, (current, originalPublisher) => current + (double)originalPublisher.MechanicalCollectablePercentage);
        }


        private void getProductConfigs(LicenseProduct product, List<LicenseProductConfigurationTotals> licenseProductConfigIdTotals, double totalpercentage)
        {
            for (int i = 0; i < product.ProductHeader.Configurations.Count; i++)
            {
                var starttime = DateTime.Now;
                logger.Debug("----Start GetLicenseProductConfiguration called");
                var licenseProductConfiguration =
                    _licenseProductConfigurationRepository.GetLicenseProductConfiguration(
                        (int)product.LicenseProductId,
                        (int)product.ProductHeader.Configurations[i].configuration_id); //Hit needed
                logger.Debug("----Finish GetLicenseProductConfiguration called");
                logger.Debug("----End GetLicenseProductConfiguration : elapsed :" + (DateTime.Now - starttime).ToString());
                if (licenseProductConfiguration != null)
                {
                    licenseProductConfiguration.TotalAmount = Convert.ToDecimal(totalpercentage / licenseProductConfigIdTotals.Count);

                    foreach (var licenseProductConfigIdTotal in licenseProductConfigIdTotals)
                    {
                        if (licenseProductConfigIdTotal.configuration_id == licenseProductConfiguration.configuration_id)
                        {
                            licenseProductConfiguration.LicensedAmount = Convert.ToDecimal(licenseProductConfigIdTotal.LicensedAmount / licenseProductConfigIdTotals.Count);
                            licenseProductConfiguration.NotLicensedAmount = Convert.ToDecimal((totalpercentage - licenseProductConfigIdTotal.LicensedAmount) / licenseProductConfigIdTotals.Count);
                        }
                    }
                    product.ProductHeader.Configurations[i].LicenseProductConfiguration =
                        licenseProductConfiguration;
                }
            }
        }

        private void setUpNLogLogging()
        {
            //NLOG Step 1. Create configuration object
            LoggingConfiguration config = new LoggingConfiguration();

            //NLOG  Step 2. Create targets and add them to the configuration
            ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);

            FileTarget fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            //NLOG Step 3. Set target properties
            consoleTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
            fileTarget.FileName = "${basedir}/logs/USL_API_New_Log_${shortdate}.txt";
            fileTarget.Layout = "${message}";

            //NLOG  Step 4. Define rules
            LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);
            LoggingRule rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule2);

            //NLOG  Step 5. Activate the configuration
            LogManager.Configuration = config;

        }
        private List<LicenseProduct> setLicenseClaimException(List<LicenseProduct> products)
        {
            foreach (var licenseProduct in products)
            {
                licenseProduct.LicenseClaimException = "CLAIM EXCEPTION";
            }
            return products;
        }

        private List<int> getWriterIds(List<LicenseProductRecordingWriter> licenseProductRecordingWriters)
        {
            var licenseWriterIds = new List<int>();
            foreach (var writerId in licenseProductRecordingWriters)
            {
                licenseWriterIds.Add(writerId.LicenseWriterId);
            }
            return licenseWriterIds.ToList();
        }

        private void CloneLicenseNotes(int licenseId, int newLicenseId)
        {
            var oldLicenseNotes = _licenseNoteRepository.GetLicenseNotes(licenseId);
            foreach (var licenseNote in oldLicenseNotes)
            {
                licenseNote.licenseId = newLicenseId;
                licenseNote.NoteType = null;
                licenseNote.Contact = null;
                var newLicenseNote = _licenseNoteRepository.Add(licenseNote);
            }
        }

        private void CloneLicenseProducts(int licenseId, int oldLicenseProductId, int oldLicenseProductRecordingId, int oldTrackId, string clonetype, int contactid, int newLicenseId)
        {
            var oldLicenseProducts = _licenseProductRepository.GetLicenseProducts(licenseId);
            foreach (var licenseProduct in oldLicenseProducts)
            {
                oldLicenseProductId = licenseProduct.LicenseProductId;

                // change the old LicenseId to the new LicenseId, then add the record
                licenseProduct.LicenseId = newLicenseId;
                licenseProduct.Schedule = null;
                var newLicenseProduct = _licenseProductRepository.Add(licenseProduct);
                //  LicenseProductConfigurations

                var oldLicenseProductConfigurations = _licenseProductConfigurationRepository.GetLicenseProductConfigurations(oldLicenseProductId);
                foreach (var licenseProductConfiguration in oldLicenseProductConfigurations)
                {
                    //  change the old licenseProductId to the new licenseProductId, then add the record
                    licenseProductConfiguration.LicenseProductId = newLicenseProduct.LicenseProductId;
                    licenseProductConfiguration.LicenseProductConfigurationId = 0;
                    licenseProductConfiguration.CreatedBy = contactid;
                    licenseProductConfiguration.ModifiedBy = contactid;
                    var newLicenseProductConfiguration = _licenseProductConfigurationRepository.Add(licenseProductConfiguration);
                }

                //  LicenseRecordings
                //  new method
                var oldLicenseProductRecordings = _licenseProductRecordingRepository.GetLicenseRecordingsByLicenseProductId(oldLicenseProductId);
                foreach (var licenseProductRecording in oldLicenseProductRecordings)
                {
                    oldLicenseProductRecordingId = licenseProductRecording.LicenseRecordingId;
                    oldTrackId = licenseProductRecording.TrackId;


                    //  change old LicenseProductId to new LicenseProductId, add the record
                    licenseProductRecording.LicenseProductId = newLicenseProduct.LicenseProductId;
                    licenseProductRecording.LicenseRecordingId = 0;
                    licenseProductRecording.CreatedBy = contactid;
                    licenseProductRecording.ModifiedBy = contactid;
                    var newLicenseProductRecording = _licenseProductRecordingRepository.Add(licenseProductRecording);

                    //  LicenseWriters
                    var oldWriters = _licensePRWriterRepository.GetLicenseProductRecordingWriters(oldLicenseProductRecordingId);
                    foreach (var writer in oldWriters)
                    {

                        var oldLicenseWriterId = writer.LicenseWriterId;
                        List<int> writerIdList = new List<int> { oldLicenseWriterId };

                        writer.LicenseRecordingId = newLicenseProductRecording.LicenseRecordingId;
                        writer.LicenseWriterId = 0;
                        writer.CreatedBy = contactid;
                        writer.ModifiedBy = contactid;
                        writer.WriterNotes = new List<LicenseProductRecordingWriterNote>();
                        writer.ClaimExceptionOverride = null;
                        writer.SplitOverride = null;
                        var newWriter = _licensePRWriterRepository.Add(writer);


                        //  LicenseWriterNotes
                        var oldWriterNotes = _licensePRWriterNoteRepository.GetLicenseProductRecordingWriterNotes(writerIdList);
                        foreach (var writerNote in oldWriterNotes)
                        {
                            writerNote.LicenseWriterId = newWriter.LicenseWriterId;
                            writerNote.LicenseWriterNoteId = 0;
                            writerNote.CreatedBy = contactid;
                            writerNote.ModifiedBy = contactid;
                            //new method
                            var newWriterNote = _licensePRWriterNoteRepository.Add(writerNote);
                        }

                        //  LicenseWriterRates

                        var oldWriterRates = _licensePRWriterRateRepository.GetLicenseProductRecordingWriterRates(oldLicenseWriterId);


                        foreach (var writerRate in oldWriterRates)
                        {
                            List<int> writerRateIdList = new List<int> { writerRate.LicenseWriterRateId };

                            writerRate.LicenseWriterId = newWriter.LicenseWriterId;
                            writerRate.LicenseWriterRateId = 0;
                            writerRate.CreatedBy = contactid;
                            writerRate.ModifiedBy = contactid;
                            if (clonetype == "Addendum")
                            {
                                writerRate.WriterRateInclude = false;
                            }
                            var newWriterRate = _licensePRWriterRateRepository.Add(writerRate);


                            //  LicenseWriterRateStatus
                            var oldWriterStatuses = _licensePRWriterRateStatusRepository.GetLicenseWriterRateStatus(writerRateIdList);
                            foreach (var writerStatus in oldWriterStatuses)
                            {

                                writerStatus.LicenseWriterRateId = newWriterRate.LicenseWriterRateId;
                                writerStatus.LicenseWriterRateStatusId = 0;
                                writerStatus.CreatedBy = contactid;
                                writerStatus.ModifiedBy = contactid;
                                var newWriterStatus = _licensePRWriterRateStatusRepository.Add(writerStatus);
                            }

                        }

                    }
                }


            }
        }

     

    }
}
