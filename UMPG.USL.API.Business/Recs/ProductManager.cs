using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UMPG.USL.API.Business.DataHarmonization;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Recs
{
    public class ProductManager : IProductManager
    {
        private readonly ISearchProvider _recsSearchProvider;
        private readonly IRecsDataProvider _recsProvider;
        private readonly ILicenseProductRecordingRepository _licenseProductRecordingRepository;
        private readonly ILicenseProductRepository _licenseProductRepository;
        private readonly ILicenseProductManager _licenseProductManager;
        private readonly ISnapshotLicenseManager _snapshotLicenseManager;
        private readonly IRecCongruencyCheckService _recsProductChangeLogService;
        public ProductManager(ISearchProvider recSearchProvider, IRecsDataProvider recsProvider,
             IRecCongruencyCheckService recsProductChangeLogService,
            ILicenseProductRecordingRepository licenseProductRecordingRepository,
            ILicenseProductRepository licenseProductRepository, ILicenseProductManager licenseProductManager,
             ISnapshotLicenseManager snapshotLicenseManager
        ) //, IRecordingWorkLinkRepository recordingWorkLinkRepository
        {
            _recsProductChangeLogService = recsProductChangeLogService;
            _snapshotLicenseManager = snapshotLicenseManager;
            _licenseProductManager = licenseProductManager;
            _recsSearchProvider = recSearchProvider;
            _recsProvider = recsProvider;
            _licenseProductRecordingRepository = licenseProductRecordingRepository;
            _licenseProductRepository = licenseProductRepository;
        }

        public PagedResponse<Product> PagedSearch(ProductRequest request)
        {
            var result = _recsSearchProvider.SearchProducts(request, 1);

            //Get UPC codes here
            foreach (var product in result.Results)
            {
                var productId = (int)product.product_id;
                product.ProductHeader = _recsProvider.RetrieveProductHeader(productId);
            }

            return result;
        }

        /// <summary>
        /// New method to call recs for product info(header)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductHeader GetProductHeader(int productId)
        {
            var product = _recsProvider.RetrieveProductHeader(productId);
            product.RelatedLicensesNo =
                _licenseProductRepository.GetLicensesNo(productId);
            return product;
        }

        public bool UpdateProductPriority(UpdatePriorityRequest request)
        {
            return _recsProvider.UpdateProductPriority(request);
        }

        /// <summary>
        /// New method to call recs for tracks
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<WorksRecording> GetProductRecsRecordings(int productId)
        {
            var recs_recordings = _recsProvider.RetrieveProductRecordings(productId);

            // Now populate the UMPG % from adding the sum from call to composers work and adding up contributions on controlled writers.

            foreach (var track in recs_recordings)
            {
                track.Message = string.Empty;
                var umpgpercentage = 0.00;
                var workcode = string.Empty;

                try
                {
                    if (track.Track.Copyrights != null)
                    {
                        workcode = track.Track.Copyrights.FirstOrDefault().WorkCode;
                        //var workcode = track.Track.Copyrights.FirstOrDefault().WorkCode;
                        var recsWriters = _recsProvider.RetrieveTrackWriters(workcode);

                        foreach (var writer in recsWriters)
                        {
                            if (writer.Controlled)
                            {
                                umpgpercentage = (double)(umpgpercentage + writer.Contribution);
                            }
                        }
                    }
                    track.UmpgPercentageRollup = umpgpercentage;
                }
                catch (Exception ex)
                {
                    track.Message = string.Format("Error Retrieving writers info for work code {0} : {1}", workcode,
                        ex.Message.ToString());
                }
            }

            return recs_recordings;
        }

        public List<WorksWriter> GetWorksWriters(string worksCode)
        {
            return _recsProvider.RetrieveTrackWriters(worksCode);
        }

        //public List<License> GetLicenses(int productId)
        //{
        //    var ids = _productRepository.GetLicenseIds(productId);
        //    return _productRepository.GetLicenses(ids);
        //}

        /*
        public AddProductConfiguration(UpdateProductRequest updateProductRequest)
        {
            return _recsProvider.UpdateProduct(updateProductRequest);
        }
        */

        public ProductHeader UpdateProduct(object updateProductRequest)
        {
            return _recsProvider.UpdateProduct(updateProductRequest);
        }

        public List<WorksRecording> RetrieveTracks(int productId)
        {
            return _recsProvider.RetrieveTracks(productId);
        }

        public List<RecordLabel> RetrieveLabels()
        {
            return _recsProvider.RetrieveLabels();
        }

        public AddProductResult SaveProduct(ProductHeader request)
        {
            var addProductResult = new AddProductResult();
            addProductResult.success = false;
            addProductResult.productHeader = new ProductHeader();
            addProductResult.errorList = new List<Error>();

            try
            {
                //requires a dynamic json object, cannot post the existing product header using static models as will return 400 errors
                dynamic product, artist, label, config, configtype; //, newconfig, newconfigtype;

                artist = new ExpandoObject();
                //                artist.name = StringHelper.UrlEncode(request.Artist.name.ToString());
                artist.name = request.Artist.name.ToString().Trim();

                if (request.Artist.id > 0)
                {
                    artist.id = request.Artist.id;
                }

                product = new ExpandoObject();
                if (request.Id > 0)
                {
                    product.id = request.Id;
                }
                //                product.title = StringHelper.UrlEncode(request.Title.ToString());
                product.title = request.Title.ToString().Trim();
                product.artist = artist;

                if (request.Label != null)
                {
                    if (request.Label.name.Length > 0)
                    {
                        label = new ExpandoObject();
                        label.name = request.Label.name.ToString().Trim();
                        //label.name = StringHelper.UrlEncode(request.Label.name.ToString());
                        if (request.Label.label_id > 0)
                        {
                            label.id = request.Label.label_id;
                        }
                        product.label = label;
                    }
                }

                if (request.Configurations.Count > 0)
                {
                    var configs = new List<ExpandoObject>();
                    foreach (var configuration in request.Configurations)
                    {
                        config = new ExpandoObject();
                        configtype = new ExpandoObject();
                        if (configuration.configuration_id > 0)
                        {
                            config.id = configuration.configuration_id;
                        }
                        //config.id = configuration.id;
                        //config.name = configuration.name;
                        configtype.id = configuration.Configuration.ConfigId;
                        configtype.name = configuration.Configuration.name;
                        config.configuration = configtype;

                        //config.upc = configuration.UPC.ToString();
                        config.upc = configuration.UPC;

                        if (configuration.DatabaseVersion >= 0)
                        {
                            config.databaseVersion = configuration.DatabaseVersion;
                        }
                        if (configuration.ReleaseDate != null && configuration.ReleaseDate.HasValue)
                        {
                            config.releaseDate = Convert.ToDateTime(configuration.ReleaseDate).ToString("dd-MMM-yyyy");
                        }
                        configs.Add(config);
                    }
                    product.configurations = configs;
                }

                if (request.DatabaseVersion >= 0)
                {
                    product.databaseVersion = request.DatabaseVersion;
                }

                HttpWebResponseWithStream response = _recsProvider.AddProduct(product);
                //cast response stream based on status code
                if (response.statusCode == System.Net.HttpStatusCode.OK)
                {
                    addProductResult.success = true;
                    addProductResult.productHeader =
                        JsonConvert.DeserializeObject<ProductHeader>(response.responseStream);
                }
                else
                {
                    addProductResult.success = false;
                    addProductResult.errorList = JsonConvert.DeserializeObject<List<Error>>(response.responseStream);
                }
            }
            catch (Exception ex)
            {
                addProductResult.errorList.Add(new Error
                {
                    Code = "System Error",
                    FieldName = "",
                    Message = ex.Message.ToString()
                });
                addProductResult.success = false;
            }

            return addProductResult;
        }

        public AddProductResult AddProduct(AddProductRequest request)
        {
            var addProductResult = new AddProductResult();
            addProductResult.success = false;
            addProductResult.productHeader = new ProductHeader();
            addProductResult.errorList = new List<Error>();

            try
            {
                //requires a dynamic json object, cannot post using static models
                dynamic product, artist, label, config, configtype; //, newconfig, newconfigtype;

                artist = new ExpandoObject();
                //                artist.name = StringHelper.UrlEncode(request.artist.name);
                artist.name = request.artist.name.ToString().Trim();

                if (request.artist.id > 0)
                {
                    artist.id = request.artist.id;
                }

                product = new ExpandoObject();
                //               product.title = StringHelper.UrlEncode(request.title);
                product.title = request.title.ToString().Trim();

                product.artist = artist;

                if (request.label != null)
                {
                    if (request.label.name.Length > 0)
                    {
                        label = new ExpandoObject();
                        label.name = request.label.name;
                        if (request.label.label_id > 0)
                        {
                            label.id = request.label.label_id;
                        }
                        product.label = label;
                    }
                }

                if (request.configurations.Count > 0)
                {
                    var configs = new List<ExpandoObject>();
                    //update existing configs
                    foreach (var configuration in request.configurations)
                    {
                        config = new ExpandoObject();
                        configtype = new ExpandoObject();
                        //config.id = configuration.id;
                        //config.name = configuration.name;
                        configtype.id = configuration.id;
                        configtype.name = configuration.name;
                        config.configuration = configtype;
                        config.upc = configuration.upc;
                        if (configuration.releaseDate != null && configuration.releaseDate.Length > 0)
                        {
                            config.releaseDate = Convert.ToDateTime(configuration.releaseDate).ToString("dd-MMM-yyyy");
                        }
                        configs.Add(config);
                    }
                    product.configurations = configs;
                }

                HttpWebResponseWithStream response = _recsProvider.AddProduct(product);
                //cast response stream based on status code
                if (response.statusCode == System.Net.HttpStatusCode.OK)
                {
                    addProductResult.success = true;
                    addProductResult.productHeader =
                        JsonConvert.DeserializeObject<ProductHeader>(response.responseStream);
                }
                else
                {
                    addProductResult.success = false;
                    addProductResult.errorList = JsonConvert.DeserializeObject<List<Error>>(response.responseStream);
                }
            }
            catch (Exception ex)
            {
                addProductResult.errorList.Add(new Error
                {
                    Code = "System Error",
                    FieldName = "",
                    Message = ex.Message.ToString()
                });
                addProductResult.success = false;
            }
            return addProductResult;
        }

        public SingleResult<Track> RetrieveTrack(RetrieveTrackRequest request)
        {
            /*
            var callerInfo = new CallerInfo
            {
                ContactId = 1,
                SafeUserId = "510141cdbd2afb8727b1e765",  //Tom Hotmail safeId
                SiteLocationCode = "US2"
            };
            */
            return _recsProvider.RetrieveTrack(request.trackId, request.callerInfo);
        }

        public UpdateProductLinkResult SaveProductLink(ProductLink productLink)
        {
            var updateProductLinkResult = new UpdateProductLinkResult();
            updateProductLinkResult.success = false;
            updateProductLinkResult.productLink = new ProductLink();
            updateProductLinkResult.errorList = new List<Error>();

            if (productLink.id == 0)
            {
                productLink.id = null;
            }
            if (productLink.track.id == 0)
            {
                productLink.track.id = null;
                productLink.track.controlled = "NO";
                productLink.track.copyrights = new List<ProductLinkTrackCopyright>();
            }

            if (productLink.track.artist.id == 0)
            {
                productLink.track.artist.id = null;
            }
            //            productLink.track.title = StringHelper.UrlEncode(productLink.track.title.ToString());
            //            productLink.track.artist.name = StringHelper.UrlEncode(productLink.track.artist.name.ToString());
            productLink.track.title = productLink.track.title.ToString().Trim();
            productLink.track.artist.name = productLink.track.artist.name.ToString().Trim();

            //commented this out 2016-02-23 to avoid product link database version differences in recs returning an "in use" message
            //if (productLink.databaseVersion == 0)
            //{
            //    productLink.databaseVersion = 1;
            //}

            if (productLink.track.databaseVersion == 0)
            {
                productLink.track.databaseVersion = 1;
            }

            try
            {
                HttpWebResponseWithStream response = _recsProvider.SaveProductLink(productLink);

                //cast response stream based on status code
                if (response.statusCode == System.Net.HttpStatusCode.OK)
                {
                    updateProductLinkResult.success = true;
                    updateProductLinkResult.productLink =
                        JsonConvert.DeserializeObject<ProductLink>(response.responseStream);
                }
                else
                {
                    updateProductLinkResult.success = false;
                    updateProductLinkResult.errorList =
                        JsonConvert.DeserializeObject<List<Error>>(response.responseStream);
                }
            }
            catch (Exception ex)
            {
                updateProductLinkResult.errorList.Add(new Error
                {
                    Code = "System Error",
                    FieldName = "",
                    Message = ex.Message.ToString()
                });
                updateProductLinkResult.success = false;
            }
            return updateProductLinkResult;
        }

        public List<GetProductLink> GetProductLinks(int productId)
        {
            return _recsProvider.GetProductLinks(productId);
        }

        public UpdateProductLinkResult DeleteProductLink(ProductLink productLink)
        {
            var updateProductLinkResult = new UpdateProductLinkResult();
            updateProductLinkResult.success = false;
            updateProductLinkResult.productLink = new ProductLink();
            updateProductLinkResult.errorList = new List<Error>();

            try
            {
                HttpWebResponseWithStream response = _recsProvider.RemoveProductLink(productLink);

                //cast response stream based on status code
                if (response.statusCode == System.Net.HttpStatusCode.OK)
                {
                    updateProductLinkResult.success = true;
                    updateProductLinkResult.productLink =
                        JsonConvert.DeserializeObject<ProductLink>(response.responseStream);
                }
                else
                {
                    updateProductLinkResult.success = false;
                    updateProductLinkResult.errorList =
                        JsonConvert.DeserializeObject<List<Error>>(response.responseStream);
                }
            }
            catch (Exception ex)
            {
                updateProductLinkResult.errorList.Add(new Error
                {
                    Code = "System Error",
                    FieldName = "",
                    Message = ex.Message.ToString()
                });
                updateProductLinkResult.success = false;
            }
            return updateProductLinkResult;
        }

        //This builds the recs 'licenseProduct' information.  it gets all recs data for this particular product
        public RecsLicenseProduct BuildRecsLicenseProduct(int productId)
        {
            var newRecsLicenseProduct = new RecsLicenseProduct();
            newRecsLicenseProduct.ProductId = productId;
            newRecsLicenseProduct.RecordingsFromWorks = GetProductRecsRecordings(productId);

            foreach (var track in newRecsLicenseProduct.RecordingsFromWorks)
            {
                if (track.Writers == null)
                {
                    track.Writers = new List<WorksWriter>();
                }
                if (track.Track.Copyrights != null)
                {
                    foreach (var copyWrite in track.Track.Copyrights)
                    {
                        var workCode = copyWrite.WorkCode;

                        //Get all writers for workCode
                        var writers = GetWorksWriters(workCode);
                        //Add each found writer to track
                        foreach (var writer in writers)
                        {
                            track.Writers.Add(writer);
                        }
                    }
                }
            }
            return newRecsLicenseProduct;
        }

        //this compares the MechsLicenseData with the RecsLicenseProductData.  If there are inconsistencies, log them, then return them.
        public List<RecsProductChanges> FindOutOfSyncRecItems(List<LicenseProduct> mechsLicenseProducts, int licenseId)
        {
            //MechsLicenseProducts === From Recs, displayted on licenseDetails and ProductDetail page
            var exists = _snapshotLicenseManager.DoesSnapshotExists(licenseId);
            if (!exists)
            {
                return new List<RecsProductChanges>();
            }
            //snapshot product === our snapshot
            var snapshotLicense = _snapshotLicenseManager.GetSnapshotLicenseBySnapshotLicenseId(licenseId);
            /*
            var mechsProductIds = mechsLicenseProducts.Select(x => x.ProductId).ToList();
            var recsLicenseProducts = new List<RecsLicenseProduct>();

            //Get RecsLicenseProduct for each productId
            foreach (var productId in mechsProductIds)
            {
                var result = BuildRecsLicenseProduct(productId);
                recsLicenseProducts.Add(result);
            }
            */
            //do logic on recdsLicenseProducts againse mehcs
            // old recsCongruenceyCheckmethod: var recsDifferences = RecsCongruencyCheck(mechsLicenseProducts, snapshotLicense.LicenseProducts);
            var recsDifferences = CheckSnapshotAgainstRecs(mechsLicenseProducts, snapshotLicense.LicenseProducts);
            return recsDifferences;
        }



        public List<RecsProductChanges> CheckSnapshotAgainstRecs(List<LicenseProduct> mechsLicenseProducts,
            List<Snapshot_LicenseProduct> licenseProductSnapshots)
        {
            //Start code_____________
            var listOfChanges = new List<RecsProductChanges>();

            //check for product changes
            listOfChanges.AddRange(_recsProductChangeLogService.CheckForLicenseProductChanges(mechsLicenseProducts, licenseProductSnapshots));



            



            return listOfChanges;
        }


       
    }
}