using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UMPG.USL.API.Business.DataHarmonization;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.Security;

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
        private readonly ILicensePRWriterRepository _licensePrWriterRepository;
        private readonly ISnapshotWorkTrackRepository _snapshotWorkTrackRepository;
        private readonly ISnapshotLicenseProductManager _snapshotLicenseProductManager;
        

        public ProductManager(ISearchProvider recSearchProvider, IRecsDataProvider recsProvider,
        
            ILicensePRWriterRepository licensePrWriterRepository,
            ISnapshotLicenseProductManager snapshotLicenseProductManager,
             IRecCongruencyCheckService recsProductChangeLogService,
            ILicenseProductRecordingRepository licenseProductRecordingRepository,
            ILicenseProductRepository licenseProductRepository, ILicenseProductManager licenseProductManager,
             ISnapshotLicenseManager snapshotLicenseManager,
             ISnapshotWorkTrackRepository snapshotWorkTrackRepository
        )
        {
            
            _snapshotLicenseProductManager = snapshotLicenseProductManager;
            _snapshotWorkTrackRepository = snapshotWorkTrackRepository;
            _licensePrWriterRepository = licensePrWriterRepository;
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

        public bool UpdateProductPriority(UpdatePriorityRequest request, string safeIdHeader)
        {
            return _recsProvider.UpdateProductPriority(request, safeIdHeader);
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
            //This populates each recording with more data.  turned off
            foreach (var recording in recs_recordings)
            {
                if (recording.Track != null)
                {
                    var trackId = recording.Track.Id;
                    if (recording.Track.Copyrights != null)
                    {
                        var workCode = recording.Track.Copyrights[0].WorkCode;
                        recording.LicenseRecording =
                            _licenseProductRecordingRepository.GetLicenseRecordingsByRecsTrackId(trackId);
                        if (recording.LicenseRecording != null)
                        {
                            var recordingId = recording.LicenseRecording.LicenseRecordingId;
                            recording.Writers = GetWorksWriters(workCode);
                            if (recording.Writers != null)
                            {
                                foreach (var writer in recording.Writers)
                                {
                                    writer.LicenseProductRecordingWriter =
                                        _licensePrWriterRepository.GetByRecordingIdAndCaeNumber(recordingId,
                                            writer.CaeNumber,
                                            writer.IpCode);
                                }
                            }
                        }
                    }
                    else
                    {
                        recording.LicenseRecording =
                          _licenseProductRecordingRepository.GetLicenseRecordingsByRecsTrackId(trackId);

                    }
                }
            }

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

        public AddProductResult SaveProduct(ProductHeader request, string header)
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

                HttpWebResponseWithStream response = _recsProvider.AddProductWithHeader(product, header);
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

        public UpdateProductLinkResult SaveProductLinkWithHeader(ProductLink productLink, string header)
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
                var response = _recsProvider.SaveProductLinkWithHeader(productLink, header);

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
                var response = _recsProvider.RemoveProductLink(productLink);

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
        public List<RecsProductChanges> FindOutOfSyncRecItems(List<LicenseProduct> recsLicenseProducts, int licenseId)
        {
//          recsLicenseProducts = TestWorksModifiedData(recsLicenseProducts);
            //MechsLicenseProducts === From Recs, displayted on licenseDetails and ProductDetail page
            var exists = _snapshotLicenseManager.DoesSnapshotExistAndComplete(licenseId);
            if (!exists)
            {
                return GetTrackDifferences(recsLicenseProducts, licenseId);
            }
            //snapshot product === our snapshot
            var snapshotLicense = _snapshotLicenseManager.GetSnapshotLicenseBySnapshotLicenseId(licenseId);

            // var mechsProductIds = mechsLicenseProducts.Select(x => x.ProductId).ToList();
            // var recsLicenseProducts = new List<RecsLicenseProduct>();
            //
            // //Get RecsLicenseProduct for each productId
            // foreach (var productId in mechsProductIds)
            // {
            //     var result = BuildRecsLicenseProduct(productId);
            //     recsLicenseProducts.Add(result);
            // }

            //do logic on recdsLicenseProducts againse mehcs
            var recsDifferences = _recsProductChangeLogService.CheckSnapshotAgainstRecs(recsLicenseProducts, snapshotLicense.LicenseProducts);
            recsDifferences.AddRange(GetTrackDifferences(recsLicenseProducts, licenseId));  //Turned off, adds track check to E,A or I licenses
            return recsDifferences;
        }

        private List<LicenseProduct> TestWorksModifiedData(List<LicenseProduct> recsLicenseProducts)
        {
            foreach (var lp in recsLicenseProducts)
            {
                foreach (var recording in lp.Recordings)
                {
                    if (recording.TrackId == 246307)
                    {
                        //Test writer controlled staus changed
                        var writer = recording.Writers[0];
                        writer.Contribution = 99;
                        writer.Controlled = false;
                        writer.FullName = "TEST teST";
                     recording.Writers[0] = writer;
               
               
                       //Test Add writer
                       var writer2 = new WorksWriter
                       {
                           CaeNumber = 123456789,
                           Contribution = 50,
                           IpCode = "000234564",
                           FullName = "Jeff Ward",
                           CapacityCode = "CA",
                           Capacity = "Composer & Author",
                           MechanicalCollectablePercentage = 50,
                           MechanicalOwnershipPercentage = 75,
                           Controlled = true
                       };
                       recording.Writers.Add(writer2);
               //
               //
               //
                       //Test Remove writer
                       var writerToRemove = recording.Writers[1];
                      recording.Writers.Remove(writerToRemove);
                        #region testData
                        // //Test original Publisher Changes
                           var op = writer.OriginalPublishers[0];
                            op.MechanicalOwnershipPercentage = 89;
                            op.MechanicalCollectablePercentage = 99;
                        op.Capacity = "Changed";
                        op.CapacityCode = "Also changed";
                            op.Controlled = !op.Controlled;
                            op.CaeNumber = 988899;
                            writer.OriginalPublishers[0] = op;
                            
                            
                              //Test Add new Original Publisher
                              var opToAdd = new OriginalPublisher
                              {
                                  CaeNumber = 88888888,
                                  IpCode = "987654321",
                                  FullName = "Awesome OP",
                                  Capacity = "Original Publisher",
                                  CapacityCode = "E",
                                  MechanicalCollectablePercentage = 88,
                                  MechanicalOwnershipPercentage = 74,
                                  Controlled = true
                              };
                            
                              writer.OriginalPublishers.Add(opToAdd);
                            
                            
                            //Test remove OP
                            writer.OriginalPublishers.Remove(op);

                      //Test affiliation on OP
                     
                      var affBase = op.Affiliation[0].Affiliations[0];
                      affBase.EndDate = DateTime.Today.AddDays(1);
                       affBase.SocietyAcronym = "XYZ";

                      op.Affiliation[0].Affiliations[0] = affBase;
                     


                          //Test Copyrights
                          var copyRightToModify = recording.Track.Copyrights[0];
                          copyRightToModify.MechanicalCollectablePercentage = 50;
                          copyRightToModify.MechanicalOwnershipPercentage = 50;
                          recording.Track.Copyrights[0] = copyRightToModify;
                        
                        #endregion
                    }
                }
            }
            return recsLicenseProducts;
        }

        public List<RecsProductChanges> GetTrackDifferences(List<LicenseProduct> recsLicenseProducts, int licenseId)
        {
            var listOfChanges = new List<RecsProductChanges>();
            IList<ProductTracks> listOfRecsProductTracks = new List<ProductTracks>();
            //Get all tracks from licenseProducts.
            //      var listOfProductsAndTracks = GetAllTrackIds(recsLicenseProducts, licenseId);  //Original, turned Off
            foreach (var licenseProduct in recsLicenseProducts)
            {
                listOfRecsProductTracks.Add(new ProductTracks
                {
                    TrackIds = _recsProvider.RetrieveTracks(licenseProduct.ProductId).Select(_ => _.Track.Id).ToList(),
                    ProductId = licenseProduct.ProductId,
                    LicenseId = licenseProduct.LicenseId,
                    LicenseProductId = licenseProduct.LicenseProductId
                });
            }

            //Get Tracks that were originally on the license that may of been removed
            var listOfMechsProductsAndTracks = GetOriginalMechsTracks(licenseId);

            //Find differences

            //Find if tracks were removed from recs
            //listOfChanges.AddRange(FindTracksRemovedFromRecs(listOfProductsAndTracks, listOfMechsProductsAndTracks)); //Original, turned Off
            listOfChanges.AddRange(FindTracksRemovedFromRecs(listOfRecsProductTracks, listOfMechsProductsAndTracks));

            //Find if tracks were added to recs
            //listOfChanges.AddRange(FindTracksAddedToRecs(listOfProductsAndTracks,listOfMechsProductsAndTracks)); //Original, turned Off
            listOfChanges.AddRange(FindTracksAddedToRecs(listOfRecsProductTracks, listOfMechsProductsAndTracks));

            return listOfChanges;
        }

        private List<RecsProductChanges> FindTracksAddedToRecs(IList<ProductTracks> recsTracks,
            IList<ProductTracks> originalMechsTracks)
        {
            var listOfChanges = new List<RecsProductChanges>();
            for (var i = 0; i < recsTracks.Count; i++)
            {
                var firstRecsProduct = recsTracks[i];

                firstRecsProduct.TrackIds = GetTrackIdsFromRecs(firstRecsProduct.ProductId);

                var firstOriginalMechsProduct = originalMechsTracks[i];

                var recsTrackIds = firstRecsProduct.TrackIds.Except(firstOriginalMechsProduct.TrackIds).ToList();
                var product = GetProductHeader(firstRecsProduct.ProductId);

                foreach (var trackId in recsTrackIds)
                {
                    var track = _recsProvider.RetrieveTrack(trackId, GetCallerInfo());
                    if (track == null)
                    {
                        track = GetTrackFromRecs(trackId, firstOriginalMechsProduct.ProductId);
                    }
                        if (track != null && track.Result != null)
                    {
                        var newChanges = new RecsProductChanges
                        {
                            PropertyLocation = "Track \"" + track.Result.Title + "\" added to Recs",

                            PropertyChanged = "Track \"" + track.Result.Title + "\" added to Recs",
                            ChangedValue = "\"" + track.Result.Title + "\"" + " By Artist: " + track.Result.Artist.name,
                            OriginalValue = "N/A"
                        };
                        if (track.Result.Isrc != null && track.Result != null)
                        {
                            if (track.Result.Isrc.Trim() != "" && track.Result.Isrc.Trim().Length > 3)
                            {
                                newChanges.ChangeMessage = "Track: \"" + track.Result.Title + "\" isrc:(" +
                                                           track.Result.Isrc + ") added to Recs on product \"" +
                                                           product.Title + "\"";
                            }
                        }
                        else
                        {
                            newChanges.ChangeMessage = "Track: \"" + track.Result.Title + "\" added to Recs on product \"" +
                                                       product.Title + "\"";
                        }

                        listOfChanges.Add(newChanges);
                    }
                    else
                    {
                        var newChanges = new RecsProductChanges
                        {
                            PropertyLocation = "Track Id\"" + trackId + "\" added to Recs",
                            ChangeMessage = "Track: \"" + trackId + "\" added to Recs. (Track Information could not be located in Recs) on product \"" + product.Title + "\"",
                            PropertyChanged = "Track \"" + trackId + "\" added to Recs",
                            OriginalValue = "N/A",
                            ChangedValue = "Added to Recs"
                        };
                        listOfChanges.Add(newChanges);
                    }
                }

                //Run this if a track has been added or removed from a product
                if (firstRecsProduct.TrackIds.Count != firstOriginalMechsProduct.TrackIds.Count)
                {
                    //Turned off
                    // listOfChanges.Add(GetTrackCountChanges(firstRecsProduct, firstOriginalMechsProduct));
                }
            }

            return listOfChanges;
        }

        private RecsProductChanges GetTrackCountChanges(ProductTracks recsTracks,
            ProductTracks originalMechsTracks)
        {
            var newChanges = new RecsProductChanges
            {
                PropertyLocation = "Track count on licenseId: " + recsTracks.LicenseId + " changed from " + recsTracks.OriginalTrackCountOnMechsLicense + " to " + originalMechsTracks.TrackIds.Count,
                ChangeMessage = "Original License had " + recsTracks.OriginalTrackCountOnMechsLicense + " tracks.  Now mechs has: " + originalMechsTracks.TrackIds.Count + " and recs has " + recsTracks.TrackIds.Count,
                PropertyChanged = "Track count has changed",
                OriginalValue = "Mechs Original Track Count: " + recsTracks.OriginalTrackCountOnMechsLicense,
                ChangedValue = "New track count on mechs: " + originalMechsTracks.TrackIds.Count + " -- " + recsTracks.TrackIds.Count
            };
            return newChanges;
        }

        private List<int> GetTrackIdsFromRecs(int productId)
        {
            var listOfIds = new List<int>();
            var result = _recsProvider.RetrieveTracks(productId);
            foreach (var track in result)
            {
                listOfIds.Add(track.Track.Id);
            }
            return listOfIds;
        }

        private List<RecsProductChanges> FindTracksRemovedFromRecs(IList<ProductTracks> recsTracks,
            IList<ProductTracks> originalMechsTracks)
        {
        
            var listOfChanges = new List<RecsProductChanges>();
            for (var i = 0; i < recsTracks.Count; i++)
            {
                var firstRecsProduct = recsTracks[i];
                var firstOriginalMechsProduct = originalMechsTracks[i];
                var product = GetProductHeader(firstRecsProduct.ProductId);
                var recsTrackIds = firstOriginalMechsProduct.TrackIds.Except(firstRecsProduct.TrackIds).ToList();

                foreach (var trackId in recsTrackIds)
                {
                    bool added = false;
                    var track = new SingleResult<Track>();
                    var newResult = new SingleResult<Track>();
                    track.Result = new Track();
                     track = _recsProvider.RetrieveTrack(trackId, GetCallerInfo());
                    if (track == null)
                    {
                        var result = _snapshotWorkTrackRepository.GetTrackForCloneTrackId(trackId);


                        if (result != null)
                        {
                            
                            

                                var newArtist = new ArtistRecs
                                {
                                    name = result.Artist.Name
                                };

                                var newTrack = new Track
                                {
                                    Title = result.Title,
                                    Artist = newArtist

                                };
                            newResult.Result = newTrack;

                            if (newResult.Result != null)
                            {
                     //           var product =
                     //_snapshotLicenseProductManager.GetProductForTrackId(firstSnapshotRecording.SnapshotWorkTrackId);

                                var newChanges = new RecsProductChanges
                                {
                                    PropertyLocation = "Track \"" + newResult.Result.Title + "\" removed from Recs",
                                    ChangeMessage = "Track: \"" + newResult.Result.Title + "\" removed from Recs on product \"" + product.Title+"\"",
                                    PropertyChanged = "Track \"" + newResult.Result.Title + "\" removed from Recs",
                                    OriginalValue =
                                        "\"" + newResult.Result.Title + "\" By Artist: " + newResult.Result.Artist.name,
                                };
                                added = true;
                                listOfChanges.Add(newChanges);
                            }
                        }
                        
                    }
                  
                    if (track != null && track.Result != null)
                    {
                        var newChanges = new RecsProductChanges
                        {
                            PropertyLocation = "Track \"" + track.Result.Title + "\" removed from Recs",
                            ChangeMessage = "Track: \"" + track.Result.Title + "\" removed from Recs on product \"" + product.Title + "\"",
                            
                            PropertyChanged = "Track \"" + track.Result.Title + "\" removed from Recs",
                            OriginalValue = "\"" + track.Result.Title + "\" By Artist: " + track.Result.Artist.name,
                        };
                        listOfChanges.Add(newChanges);
                    } 
                    else
                    {
                        if (!added)
                        {
                            var newChanges = new RecsProductChanges
                            {
                                PropertyLocation = "Track \"" + trackId + "\" removed from Recs",
                                ChangeMessage =
                                    "Track: \"" + trackId + "\" removed from Recs (Could not be located in Recs) on product " + product.Title,
                            
                                PropertyChanged = "Track \"" + trackId + "\" removed from Recs",
                                OriginalValue =
                                    "Track: \"" + trackId + "\" removed from Recs (Could not be located in Recs)",
                            };
                            listOfChanges.Add(newChanges);
                        }
                    }
                }
            }
            return listOfChanges;
        }

        /* To be Implemented
        private RecsProductChanges CheckForAddedOrRemovedProducts(List<ProductTracks> recsTracks,
            List<ProductTracks> originalMechsTracks)
        {
            if (originalMechsTracks.Count == originalMechsTracks.Count)
            {
                return null;
                return null;
            }
            else
            {
                if (originalMechsTracks[0].ProductId != recsTracks[0].ProductId)
                {
                }
            }
        }
        */

        private SingleResult<Track> GetTrackFromRecs(int trackId, int productId)
        {
            var worksRecording = _recsProvider.RetrieveTracks(productId).FirstOrDefault(_ => _.Track.Id == trackId);
            if(worksRecording != null)
            {

                var track = new Track
                {
                    Title = worksRecording.Track.Title,
                    Id = worksRecording.Track.Id,
                    Artist = worksRecording.Track.Artists
                };
                var singleTrack = new SingleResult<Track>
                {
                    Result = track
                };
         
                return singleTrack;
            }
            else
            {
                return new SingleResult<Track>();
            }
        }

        private IList<ProductTracks> GetAllTrackIds(List<LicenseProduct> recsLicenseProducts, int licenseId)
        {
            var productsAndTrackList = new List<ProductTracks>();
            foreach (var lp in recsLicenseProducts)
            {
                var productTracks = new ProductTracks
                {
                    OriginalTrackCountOnMechsLicense = lp.LicensePRecordingsNo,
                    LicenseId = licenseId,
                    ProductId = lp.ProductId,
                    LicenseProductId = lp.LicenseProductId,
                    TrackIds = ExtractTrackIds(lp)
                };
                productsAndTrackList.Add(productTracks);
            }
            return productsAndTrackList;
        }

        private List<int> ExtractTrackIds(LicenseProduct licenseProduct)
        {
            var listOfIds = new List<int>();
            foreach (var recording in licenseProduct.Recordings)
            {
                listOfIds.Add(recording.Track.Id);
            }
            return listOfIds;
        }

        private List<int> ExtractTrackMechsIds(LicenseProduct licenseProduct)
        {
            var listOfIds = new List<int>();
            foreach (var recording in licenseProduct.LicenseProductRecordings)
            {
                listOfIds.Add(recording.TrackId);
            }
            return listOfIds;
        }

        private IList<ProductTracks> GetOriginalMechsTracks(int licenseId)
        {
            var licenseProducts = GetLicenseProductsFromDataBase(licenseId);

            var productsAndTracks = new List<ProductTracks>();
            foreach (var lp in licenseProducts)
            {
                var productTrackInformation = new ProductTracks
                {
                    ProductId = lp.ProductId,
                    LicenseId = lp.LicenseId,
                    LicenseProductId = lp.LicenseProductId,
                    TrackIds = ExtractTrackMechsIds(lp)
                };
                productsAndTracks.Add(productTrackInformation);
            }
            return productsAndTracks;
        }

        private IList<LicenseProduct> GetLicenseProductsFromDataBase(int licenseId)

        {
            var licenseProducts = _licenseProductRepository.GetAllLicenseProductsForLicenseId(licenseId);
            foreach (var licenseProduct in licenseProducts)
            {
                licenseProduct.LicenseProductRecordings =
                    _licenseProductRecordingRepository.GetLicenseRecordingsByLicenseProductId(
                        licenseProduct.LicenseProductId);
            }
            return licenseProducts;
        }

        private CallerInfo GetCallerInfo()
        {
            var callerinfo = new CallerInfo
            {
                ContactId = 67,
                SafeUserId = "53a18b03426600241eb125d1",
                SiteLocationCode = "US2"
            };
            return callerinfo;
        }
    }
}