using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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

        public ProductManager(ISearchProvider recSearchProvider, IRecsDataProvider recsProvider,
            ILicenseProductRecordingRepository licenseProductRecordingRepository,
            ILicenseProductRepository licenseProductRepository, ILicenseProductManager licenseProductManager
      ) //, IRecordingWorkLinkRepository recordingWorkLinkRepository
        {
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
            return newRecsLicenseProduct;
        }

        //this compares the MechsLicenseData with the RecsLicenseProductData.  If there are inconsistencies, log them, then return them.
        //TODO: make faster by passing entire obj
        public List<RecsProductChanges> FindOutOfSyncRecItems(List<LicenseProduct> mechsLicenseProduct2s)
        {
            var mechsLicenseProducts = mechsLicenseProduct2s;//_licenseProductManager.GetProductsNew(licenseId);
            var mechsProductIds = mechsLicenseProducts.Select(x => x.ProductId).ToList();
            var recsLicenseProducts = new List<RecsLicenseProduct>();

            //Get RecsLicenseProduct for each productId
            foreach (var productId in mechsProductIds)
            {
                var result = BuildRecsLicenseProduct(productId);
                recsLicenseProducts.Add(result);
            }

            //do logic on recdsLicenseProducts againse mehcs
            var recsDifferences = RecsCongruencyCheck(mechsLicenseProducts, recsLicenseProducts);
            return recsDifferences;
        }

        public List<RecsProductChanges> RecsCongruencyCheck(List<LicenseProduct> mechsLicenseProducts,
            List<RecsLicenseProduct> recsProducts)
        {
            //check for track differences
            var trackDifferences = RecsCongruencyTrackCheck(mechsLicenseProducts, recsProducts);
            var writerDiffereences = RecsCongruencyWriterCheck(mechsLicenseProducts, recsProducts);
            var configDifferences = RecsCongruencyConfigCheck(mechsLicenseProducts, recsProducts);
            //compile result
            trackDifferences.AddRange(writerDiffereences);
            trackDifferences.AddRange(configDifferences);
            return trackDifferences;
        }

        private List<RecsProductChanges> RecsCongruencyTrackCheck(List<LicenseProduct> mechsLicenseProducts,
            List<RecsLicenseProduct> recsProducts)
        {
            //Start code_____________
            var listOfChanges = new List<RecsProductChanges>();

            var mechsTracks = new List<WorksRecording>();
            foreach (var product in mechsLicenseProducts)
            {
                foreach (var recording in product.Recordings)
                {
                    mechsTracks.Add(recording);
                }
            }

            var recsTracks = new List<WorksRecording>();
            foreach (var product in recsProducts)
            {
                foreach (var recording in product.RecordingsFromWorks)
                {
                    recsTracks.Add(recording);
                }
            }

            //Start TestCode______________________
            //mechsTracks = mechsTracks.ToList();
            //var trackToAdd = mechsTracks[0];
            //var lastId = mechsTracks[mechsTracks.Count - 1].TrackId;
            //trackToAdd.TrackId = lastId + 1;
            //mechsTracks.Add(trackToAdd);

            //Finish TestCode______________________

            //Find which tracks have been added and removed
            var mechTrackIds = mechsTracks.Select(x => x.TrackId).ToList();
            var recTrackIds = recsTracks.Select(c => c.TrackId).ToList();

            var tracksRemovedFromRecs = mechTrackIds.Except(recTrackIds).ToList();
            var tracksAddedToRecs = recTrackIds.Except(mechTrackIds).ToList();

            //Log changes
            var listOfAddedProduct = LogRecsAddedProduct(mechsTracks, tracksAddedToRecs);
            var listOfRemovedProduct = LogRecsRemovedProduct(mechsTracks, tracksRemovedFromRecs);

            //Add to list of changes
            listOfChanges.AddRange(listOfAddedProduct);
            listOfChanges.AddRange(listOfRemovedProduct);

            //remove added/removed tracks from control lists
            recsTracks = CleanTrackList(recsTracks, tracksAddedToRecs);
            mechsTracks = CleanTrackList(mechsTracks, tracksRemovedFromRecs);

            //Ensure tracks are from smallest to largest order
            recsTracks = recsTracks.OrderByDescending(x => x.TrackId).ToList();
            mechsTracks = mechsTracks.OrderByDescending(x => x.TrackId).ToList();
            recsTracks.Reverse();
            mechsTracks.Reverse();

            var longestCount = Math.Max(recsTracks.Count, mechsTracks.Count);
            var counter = 0;

            while (counter < longestCount)
            {
                //get first element in each
                var recTrackElement0 = recsTracks.FirstOrDefault();
                if (recsTracks.Count > 0)
                {
                    recsTracks.Remove(recsTracks[0]);
                }

                var mechTrackElement0 = mechsTracks.FirstOrDefault();
                if (mechsTracks.Count > 0)
                {
                    mechsTracks.Remove(mechsTracks[0]);
                }

                //we already handled add remove above || list should be the same size.
                if (mechTrackElement0 == null || recTrackElement0 == null)
                {
                    counter++;
                    continue;
                }

                if (recTrackElement0.TrackId == mechTrackElement0.TrackId)
                {
                    //do logic
                    //Check for TrackId changes
                    if (recTrackElement0.TrackId != mechTrackElement0.TrackId)
                    {
                        var trackName = recTrackElement0.Track.Title;
                        //Log error
                        var trackIdChangeError = LogRecsProductChanges(mechTrackElement0.TrackId.ToString(),
                            recTrackElement0.TrackId.ToString(), trackName + " trackID has been changed", "TrackId");
                        listOfChanges.Add(trackIdChangeError);
                    }

                    //Check for CD Index changes
                    if (recTrackElement0.CdIndex != mechTrackElement0.CdIndex)
                    {
                        var trackName = recTrackElement0.Track.Title;
                        //Log error
                        var trackIdChangeError = LogRecsProductChanges(mechTrackElement0.CdIndex.ToString(),
                            recTrackElement0.CdIndex.ToString(), trackName + " CD-Index has been changed", "CD-Index");
                        listOfChanges.Add(trackIdChangeError);
                    }

                    //Check for CD Number changes
                    if (recTrackElement0.CdNumber != mechTrackElement0.CdNumber)
                    {
                        var trackName = recTrackElement0.Track.Title;
                        //Log error
                        var trackIdChangeError = LogRecsProductChanges(mechTrackElement0.CdNumber.ToString(),
                            recTrackElement0.CdNumber.ToString(), trackName + " CD-Number has been changed", "CD-Number");
                        listOfChanges.Add(trackIdChangeError);
                    }

                    //Check for UMPG rollup changes
                    if (recTrackElement0.UmpgPercentageRollup.ToString() !=
                        mechTrackElement0.UmpgPercentageRollup.ToString())
                    {
                        var trackName = recTrackElement0.Track.Title;
                        //Log error
                        var trackIdChangeError = LogRecsProductChanges(
                            mechTrackElement0.UmpgPercentageRollup.ToString(),
                            recTrackElement0.UmpgPercentageRollup.ToString(),
                            trackName + " UMPGPercentageRollup has been changed", "UMPG-PercentageRollup");
                        listOfChanges.Add(trackIdChangeError);
                    }

                    //Check for UMPG rollup changes
                    if (recTrackElement0.UmpgPercentageRollup.ToString() !=
                        mechTrackElement0.UmpgPercentageRollup.ToString())
                    {
                        var trackName = recTrackElement0.Track.Title;
                        //Log error
                        var trackIdChangeError = LogRecsProductChanges(
                            mechTrackElement0.UmpgPercentageRollup.ToString(),
                            recTrackElement0.UmpgPercentageRollup.ToString(),
                            trackName + " UMPGPercentageRollup has been changed", "UMPG-PercentageRollup");
                        listOfChanges.Add(trackIdChangeError);
                    }

                    //Check for UMPG rollup changes
                    if (recTrackElement0.LicensedRollup.ToString() != mechTrackElement0.LicensedRollup.ToString())
                    {
                        var trackName = recTrackElement0.Track.Title;
                        //Log error
                        var trackIdChangeError = LogRecsProductChanges(mechTrackElement0.LicensedRollup.ToString(),
                            recTrackElement0.LicensedRollup.ToString(), trackName + " LicensedRollup has been changed",
                            "LicensedRollup");
                        listOfChanges.Add(trackIdChangeError);
                    }

                    //evaluate WorksTrack Track
                    var result = EvaluateTrackChanges(recTrackElement0.Track, mechTrackElement0.Track);
                    listOfChanges.AddRange(result);

                    counter++;
                }
            }
            return listOfChanges;
        }

        private List<RecsProductChanges> EvaluateTrackChanges(WorksTrack recTrack, WorksTrack mechTrack)
        {
            var listOfChanges = new List<RecsProductChanges>();

            if (recTrack.Title != mechTrack.Title)
            {
                var result = LogRecsProductChanges(mechTrack.Title.ToString(), recTrack.Title.ToString(),
                    "Recs Track title has changed from " + mechTrack.Title.ToString() + ", to " +
                    recTrack.Title.ToString(), "Track Title Changed");
                listOfChanges.Add(result);
            }

            if (recTrack.WritersNo != mechTrack.WritersNo)
            {
                var result = LogRecsProductChanges(mechTrack.WritersNo.ToString(), recTrack.WritersNo.ToString(),
                    "A writer has been added or removed", "writer Added or removed");
                listOfChanges.Add(result);
            }

            if (recTrack.Controlled != mechTrack.Controlled)
            {
                var result = LogRecsProductChanges(mechTrack.Controlled.ToString(), recTrack.Controlled.ToString(),
                       "A track Control status has changed from " + mechTrack.Controlled.ToString() + " to " + recTrack.Controlled, "track control status changed");
                listOfChanges.Add(result);
            }

            if (recTrack.ClaimException != mechTrack.ClaimException)
            {
                var result = LogRecsProductChanges(mechTrack.ClaimException.ToString(), recTrack.ClaimException.ToString(),
                       "A track ClaimException status has changed from " + mechTrack.ClaimException.ToString() + " to " + recTrack.ClaimException, "track ClaimException status changed");
                listOfChanges.Add(result);
            }
            if (recTrack.Duration != mechTrack.Duration)
            {
                var result = LogRecsProductChanges(mechTrack.Duration.ToString(), recTrack.Duration.ToString(),
                       "A track Duration  has changed from " + mechTrack.Duration.ToString() + " to " + recTrack.Duration, "track Duration changed");
                listOfChanges.Add(result);
            }

            if (recTrack.Isrc != mechTrack.Isrc)
            {
                var result = LogRecsProductChanges(mechTrack.Isrc.ToString(), recTrack.Isrc.ToString(),
                       "A track Isrc  has changed from " + mechTrack.Isrc.ToString() + " to " + recTrack.Isrc, "track Isrc changed");
                listOfChanges.Add(result);
            }

            //Artist Evaluate
            var artsistChanges = EvaluateArtistChanges(recTrack.Artists, mechTrack.Artists, recTrack);
            listOfChanges.AddRange(artsistChanges);

            //Copyright Evaluate
            var copyRightChanges = EvaluateCopyRightChanges(recTrack.Copyrights, mechTrack.Copyrights, recTrack);
            listOfChanges.AddRange(copyRightChanges);

            return listOfChanges;
        }

        private List<RecsProductChanges> EvaluateArtistChanges(ArtistRecs recsArtist, ArtistRecs mechsArtist, WorksTrack trackInfo)
        {
            var listOfChanges = new List<RecsProductChanges>();

            if (recsArtist.id != mechsArtist.id)
            {
                var result = LogRecsProductChanges(mechsArtist.id.ToString(), recsArtist.id.ToString(),
                      "An Artist Id " + mechsArtist.id.ToString() + " to " + recsArtist.id.ToString(), "Artist Idchanged");
                listOfChanges.Add(result);
            }

            if (recsArtist.id != mechsArtist.id)
            {
                var result = LogRecsProductChanges(mechsArtist.id.ToString(), recsArtist.id.ToString(),
                      "An Artist (" + recsArtist.name.ToString() + ") was added or removed on the track: " + trackInfo.Title + ".", "Artist added or removed");
                listOfChanges.Add(result);
            }
            return listOfChanges;
        }

        private List<RecsProductChanges> EvaluateCopyRightChanges(List<RecsCopyrights> recsCopyrights,
            List<RecsCopyrights> mechCopyRights, WorksTrack trackInfo)
        {
            var listOfChanges = new List<RecsProductChanges>();

            //Find which tracks have been added and removed
            var mechTrackIds = mechCopyRights.Select(x => x.WorkCode).ToList();
            var recTrackIds = recsCopyrights.Select(c => c.WorkCode).ToList();

            var copyRightsRemovedFromRecs = mechTrackIds.Except(recTrackIds).ToList();
            var copyRightsAddedToRecs = recTrackIds.Except(mechTrackIds).ToList();

            //Log changes
            var listOfAddedProduct = LogRecsAddedCopyRight(mechCopyRights, copyRightsAddedToRecs);
            var listOfRemovedProduct = LogRecsRemovedCopyRight(mechCopyRights, copyRightsRemovedFromRecs);

            //Add to list of changes
            listOfChanges.AddRange(listOfAddedProduct);
            listOfChanges.AddRange(listOfRemovedProduct);

            //remove added/removed tracks from control lists
            recsCopyrights = CleanCopyrightList(recsCopyrights, copyRightsAddedToRecs);
            mechCopyRights = CleanCopyrightList(mechCopyRights, copyRightsRemovedFromRecs);

            var longestCount = Math.Max(recsCopyrights.Count, mechCopyRights.Count);
            var counter = 0;

            while (counter < longestCount)
            {
                //get first element in each
                var recCopyrightElement0 = recsCopyrights.FirstOrDefault();
                if (recsCopyrights.Count > 0)
                {
                    recsCopyrights.Remove(recsCopyrights[0]);
                }

                var mechCopyRightElement0 = mechCopyRights.FirstOrDefault();
                if (mechCopyRights.Count > 0)
                {
                    mechCopyRights.Remove(mechCopyRights[0]);
                }

                //we already handled add remove above || list should be the same size.
                if (recCopyrightElement0 == null || mechCopyRightElement0 == null)
                {
                    counter++;
                    continue;
                }

                if (recCopyrightElement0.WorkCode == mechCopyRightElement0.WorkCode)
                {
                    //logic
                    if (recCopyrightElement0.PrincipalArtist != mechCopyRightElement0.PrincipalArtist)
                    {
                        var result = LogRecsProductChanges(mechCopyRightElement0.PrincipalArtist, recCopyrightElement0.PrincipalArtist,
                    "An Principle Artist (" + recCopyrightElement0.PrincipalArtist + ") has changed from " + mechCopyRightElement0.PrincipalArtist + " to " + recCopyrightElement0.PrincipalArtist + ".", "PrincipalArtist changed");
                        listOfChanges.Add(result);
                    }

                    if (recCopyrightElement0.Writers != mechCopyRightElement0.Writers)
                    {
                        var result = LogRecsProductChanges(mechCopyRightElement0.Writers, recCopyrightElement0.Writers,
                    "Writer were changed from " + mechCopyRightElement0.Writers + " to " + recCopyrightElement0.Writers + ".", "Writers changed");
                        listOfChanges.Add(result);
                    }

                    if (recCopyrightElement0.WriteString != mechCopyRightElement0.WriteString)
                    {
                        var result = LogRecsProductChanges(mechCopyRightElement0.WriteString, recCopyrightElement0.WriteString,
                    "WriteString were changed from " + mechCopyRightElement0.WriteString + " to " + recCopyrightElement0.WriteString + ".", "WriteString  changed");
                        listOfChanges.Add(result);
                    }

                    if (recCopyrightElement0.MechanicalCollectablePercentage != mechCopyRightElement0.MechanicalCollectablePercentage)
                    {
                        var result = LogRecsProductChanges(mechCopyRightElement0.MechanicalCollectablePercentage.ToString(), recCopyrightElement0.MechanicalCollectablePercentage.ToString(),
                    "MechanicalCollectablePercentage was changed from " + mechCopyRightElement0.MechanicalCollectablePercentage.ToString() + " to " + recCopyrightElement0.MechanicalCollectablePercentage.ToString() + ".", "MechanicalCollectablePercentage changed");
                        listOfChanges.Add(result);
                    }

                    if (recCopyrightElement0.MechanicalCollectablePercentage != mechCopyRightElement0.MechanicalCollectablePercentage)
                    {
                        var result = LogRecsProductChanges(mechCopyRightElement0.MechanicalOwnershipPercentage.ToString(), recCopyrightElement0.MechanicalOwnershipPercentage.ToString(),
                    "MechanicalOwnershipPercentage was changed from " + mechCopyRightElement0.MechanicalOwnershipPercentage.ToString() + " to " + recCopyrightElement0.MechanicalOwnershipPercentage.ToString() + ".", "MechanicalOwnershipPercentage changed");
                        listOfChanges.Add(result);
                    }

                    //log AquisitionLocationCode
                    List<string> aquisitionCodesRemovedFromRecs = mechCopyRightElement0.AquisitionLocationCode.Except(recCopyrightElement0.AquisitionLocationCode).ToList();
                    List<string> aquisitionCodesAddedToRecs = recCopyrightElement0.AquisitionLocationCode.Except(mechCopyRightElement0.AquisitionLocationCode).ToList();
                    var addedAquisitionLocationCode = LogRecsAddedAquistionCode(aquisitionCodesAddedToRecs);
                    listOfChanges.AddRange(addedAquisitionLocationCode);
                    var removedAquisitionLocationCode = LogRecsRemovedAquistionCode(aquisitionCodesRemovedFromRecs);
                    listOfChanges.AddRange(removedAquisitionLocationCode);

                    //local clients
                    var listOfLocalClientChanges = EvaluateLocalClientChanges(recCopyrightElement0.LocalClients,
                        mechCopyRightElement0.LocalClients, recCopyrightElement0);
                    listOfChanges.AddRange(listOfLocalClientChanges);

                    //works writers ( Composers)

                    counter++;
                }
            }

            return listOfChanges;
        }

        private List<RecsProductChanges> EvaluateLocalClientChanges(
            List<LocalClientCopyright> recsLocalClientCopyrights, List<LocalClientCopyright> mechsLocalClientCopyrights,
            RecsCopyrights copyrightInfo)
        {
            var listOfChanges = new List<RecsProductChanges>();

            //Find which tracks have been added and removed
            var mechTrackIds = mechsLocalClientCopyrights.Select(x => x.ClientCode).ToList();
            var recTrackIds = recsLocalClientCopyrights.Select(c => c.ClientCode).ToList();

            var copyRightsRemovedFromRecs = mechTrackIds.Except(recTrackIds).ToList();
            var copyRightsAddedToRecs = recTrackIds.Except(mechTrackIds).ToList();

            //Log changes
            var listOfAddedProduct = LogRecsAddedLocalClientCopyright(mechsLocalClientCopyrights, copyRightsAddedToRecs);
            var listOfRemovedProduct = LogRecsRemovedLocalClientCopyright(mechsLocalClientCopyrights, copyRightsRemovedFromRecs);

            //Add to list of changes
            listOfChanges.AddRange(listOfAddedProduct);
            listOfChanges.AddRange(listOfRemovedProduct);

            //remove added/removed tracks from control lists
            recsLocalClientCopyrights = CleanLocalClientCopyrightList(recsLocalClientCopyrights, copyRightsAddedToRecs);
            mechsLocalClientCopyrights = CleanLocalClientCopyrightList(mechsLocalClientCopyrights, copyRightsRemovedFromRecs);

            var longestCount = Math.Max(recsLocalClientCopyrights.Count, mechsLocalClientCopyrights.Count);
            var counter = 0;

            while (counter < longestCount)
            {
                //get first element in each
                var recCopyrightElement0 = recsLocalClientCopyrights.FirstOrDefault();
                if (recsLocalClientCopyrights.Count > 0)
                {
                    recsLocalClientCopyrights.Remove(recsLocalClientCopyrights[0]);
                }

                var mechCopyRightElement0 = mechsLocalClientCopyrights.FirstOrDefault();
                if (mechsLocalClientCopyrights.Count > 0)
                {
                    mechsLocalClientCopyrights.Remove(mechsLocalClientCopyrights[0]);
                }

                //we already handled add remove above || list should be the same size.
                if (recCopyrightElement0 == null || mechCopyRightElement0 == null)
                {
                    counter++;
                    continue;
                }

                if (recCopyrightElement0.ClientCode == mechCopyRightElement0.ClientCode)
                {
                    if (recCopyrightElement0.ClientName != mechCopyRightElement0.ClientName)
                    {
                        var localClientChanges = LogRecsProductChanges(mechCopyRightElement0.ClientName,
                            recCopyrightElement0.ClientName,
                            "LocalClientCopyright has been changed from " + mechCopyRightElement0.ClientName + " to " +
                            recCopyrightElement0.ClientName + ". ", "LocalClientCopyright changed");
                        listOfChanges.Add(localClientChanges);
                    }

                    counter++;
                }
            }

            return listOfChanges;
        }

        private List<WorksRecording> CleanTrackList(List<WorksRecording> list, List<int> ids)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (ids.Contains(list[i].TrackId))
                {
                    list.Remove(list[i]);
                }
            }

            return list;
        }

        private List<RecsCopyrights> CleanCopyrightList(List<RecsCopyrights> list, List<string> ids)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (ids.Contains(list[i].WorkCode))
                {
                    list.Remove(list[i]);
                }
            }

            return list;
        }

        private List<LocalClientCopyright> CleanLocalClientCopyrightList(List<LocalClientCopyright> list, List<string> ids)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (ids.Contains(list[i].ClientCode))
                {
                    list.Remove(list[i]);
                }
            }

            return list;
        }

        private List<RecsProductChanges> LogRecsAddedAquistionCode(
      List<string> aquistionCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var code in aquistionCodes)
            {
                if (code != null)
                {
                    var result = LogRecsProductChanges(null, null,
                        "AquisitionLocationCode " + code + " has been added to Recs",
                        "AquisitionLocationCode " + code + " added to Recs");
                    listOfChanges.Add(result);
                }
            }
            return listOfChanges;
        }

        private List<RecsProductChanges> LogRecsRemovedAquistionCode(
      List<string> aquistionCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var code in aquistionCodes)
            {
                if (code != null)
                {
                    var result = LogRecsProductChanges(null, null,
                        "AquisitionLocationCode " + code + " has been removed from Recs",
                        "AquisitionLocationCode " + code + " removed from Recs");
                    listOfChanges.Add(result);
                }
            }
            return listOfChanges;
        }

        private List<RecsProductChanges> LogRecsAddedProduct(List<WorksRecording> tracks,
            List<int> tracksIdsInQuestion)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var trackId in tracksIdsInQuestion)
            {
                var track = tracks.FirstOrDefault(x => x.TrackId == trackId);
                if (track != null)
                {
                    var result = LogRecsProductChanges(null, null,
                        "Track " + track.Track.Title + " has been added to Recs",
                        "Track " + track.Track.Title + " added to Recs");
                    listOfChanges.Add(result);
                }
            }
            return listOfChanges;
        }

        private List<RecsProductChanges> LogRecsAddedLocalClientCopyright(List<LocalClientCopyright> tracks,
    List<string> tracksIdsInQuestion)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var trackId in tracksIdsInQuestion)
            {
                var track = tracks.FirstOrDefault(x => x.ClientCode == trackId);
                if (track != null)
                {
                    var result = LogRecsProductChanges(null, null,
                        "LocalClientCopyright " + track.ClientCode + " has been added to Recs",
                        "LocalClientCopyright " + track.ClientCode + " added to Recs");
                    listOfChanges.Add(result);
                }
            }
            return listOfChanges;
        }

        private List<RecsProductChanges> LogRecsRemovedLocalClientCopyright(List<LocalClientCopyright> tracks,
List<string> tracksIdsInQuestion)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var trackId in tracksIdsInQuestion)
            {
                var track = tracks.FirstOrDefault(x => x.ClientCode == trackId);
                if (track != null)
                {
                    var result = LogRecsProductChanges(null, null,
                        "LocalClientCopyright " + track.ClientCode + " has been removed from Recs",
                        "LocalClientCopyright " + track.ClientCode + " removed from Recs");
                    listOfChanges.Add(result);
                }
            }
            return listOfChanges;
        }

        private List<RecsProductChanges> LogRecsAddedCopyRight(List<RecsCopyrights> tracks,
    List<string> workCodesInQuestion)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var workCode in workCodesInQuestion)
            {
                var track = tracks.FirstOrDefault(x => x.WorkCode == workCode);
                if (track != null)
                {
                    var result = LogRecsProductChanges(null, null,
                        "WorkCode " + track.WorkCode + " has been added to Recs",
                        "WorkCode " + track.WorkCode + " added to Recs");
                    listOfChanges.Add(result);
                }
            }
            return listOfChanges;
        }

        private List<RecsProductChanges> LogRecsRemovedCopyRight(List<RecsCopyrights> tracks,
List<string> workCodesInQuestion)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var workCode in workCodesInQuestion)
            {
                var track = tracks.FirstOrDefault(x => x.WorkCode == workCode);
                if (track != null)
                {
                    var result = LogRecsProductChanges(null, null,
                        "WorkCode " + track.WorkCode + " has been removed from Recs",
                        "WorkCode " + track.WorkCode + " removed from Recs");
                    listOfChanges.Add(result);
                }
            }
            return listOfChanges;
        }

        private List<RecsProductChanges> LogRecsRemovedProduct(List<WorksRecording> tracks,
            List<int> tracksIdsInQuestion)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var trackId in tracksIdsInQuestion)
            {
                var track = tracks.FirstOrDefault(x => x.TrackId == trackId);
                if (track != null)
                {
                    var result = LogRecsProductChanges(null, null,
                        "Track " + track.Track.Title + " has been removed from Recs",
                        "Track " + track.Track.Title + " removed from Recs");

                    listOfChanges.Add(result);
                }
            }
            return listOfChanges;
        }

        private RecsProductChanges LogRecsProductChanges(string originalValue, string changedValue, string changeMessage, string propertyChanged)
        {
            var newRecsProductChanges = new RecsProductChanges();
            newRecsProductChanges.OriginalValue = originalValue;
            newRecsProductChanges.ChangedValue = changedValue;
            newRecsProductChanges.ChangeMessage = changeMessage;
            newRecsProductChanges.PropertyChanged = propertyChanged;
            return newRecsProductChanges;
        }

        private List<RecsProductChanges> RecsCongruencyWriterCheck(List<LicenseProduct> mechsLicenseProducts,
       List<RecsLicenseProduct> recsProducts)
        {
            var listOfChanges = new List<RecsProductChanges>();
            return listOfChanges;
        }

        private List<RecsProductChanges> RecsCongruencyConfigCheck(List<LicenseProduct> mechsLicenseProducts,
     List<RecsLicenseProduct> recsProducts)
        {
            var listOfChanges = new List<RecsProductChanges>();
            return listOfChanges;
        }
    }
}