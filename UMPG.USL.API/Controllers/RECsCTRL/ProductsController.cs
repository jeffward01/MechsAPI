using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using UMPG.USL.API.ActionFilters;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.API.ExtensionMethods;
using UMPG.USL.Models;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Controllers.RECsCTRL
{
    [RoutePrefix("api/RECsCTRL/Products")]
    public class ProductController : ApiController
    {
        private readonly IProductManager _productManager ;
        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }
       

        [AuthorizationRequired]
        [Route("PagedSearch")]
        [HttpPost]
        public PagedResponse<Product> PagedSearch(ProductRequest request)
        {

            return _productManager.PagedSearch(request);
        }

        [Route("PagedSearchDummy")]
        [HttpPost]
        public PagedResponse<Product> PagedSearchDummy(ProductRequest request)
        {
            var result = "{\"results\":[{\"product_id\":41756,\"title\":\"The Original Masters: Rare Vintage Italian Soundtracks: Italian Comedy 60\'s Volume 1 \",\"artist_id\":0,\"label_id\":null,\"catalog_identifier\":null,\"release_date\":null,\"configuration_Id\":null,\"upc\":null,\"recordingsNo\":3,\"recordings\":[{\"track_id\":220609,\"title\":\"Chi Lavora E Perduto (Tema Di Un Sogno) \",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Piero Piccioni\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":220632,\"title\":\"Amore Mio Aiutami (M18) \",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Piero Piccioni\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":220638,\"title\":\"La Donna E Una Cosa Meravigliosa (M4) \",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Piero Piccioni\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0}],\"licensesNo\":0,\"recsArtist\":{\"artist_id\":0,\"name\":\"Piero Piccioni\"},\"recsLabel\":null,\"recsLabelGroup\":null,\"recsConfiguration\":null},{\"product_id\":45557,\"title\":\"The Original Masters: Rare Vintage Italian Soundtracks: Crime And Jazz \",\"artist_id\":0,\"label_id\":null,\"catalog_identifier\":null,\"release_date\":null,\"configuration_Id\":null,\"upc\":null,\"recordingsNo\":2,\"recordings\":[{\"track_id\":241750,\"title\":\"Le Mani Sulla Citta (Fuoco) \",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Piero Piccioni\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":241775,\"title\":\"Dukes In Dixie \",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Piero Piccioni\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0}],\"licensesNo\":0,\"recsArtist\":{\"artist_id\":0,\"name\":\"Piero Piccioni\"},\"recsLabel\":null,\"recsLabelGroup\":null,\"recsConfiguration\":null},{\"product_id\":45579,\"title\":\"The Original Masters: Rare Vintage Italian Soundtracks: Italian Comedy 70\'s Volume 1 \",\"artist_id\":0,\"label_id\":null,\"catalog_identifier\":null,\"release_date\":null,\"configuration_Id\":null,\"upc\":null,\"recordingsNo\":2,\"recordings\":[{\"track_id\":241832,\"title\":\"In Viaggio Attraverso L\' Australia \",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Piero Piccioni\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":241841,\"title\":\"Little Italy \",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Piero Piccioni\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0}],\"licensesNo\":0,\"recsArtist\":{\"artist_id\":0,\"name\":\"Piero Piccioni\"},\"recsLabel\":null,\"recsLabelGroup\":null,\"recsConfiguration\":null},{\"product_id\":45458,\"title\":\"The Original Masters: Rare Vintage Italian Soundtracks: Surf And Beat \",\"artist_id\":0,\"label_id\":null,\"catalog_identifier\":null,\"release_date\":null,\"configuration_Id\":null,\"upc\":null,\"recordingsNo\":1,\"recordings\":[{\"track_id\":241137,\"title\":\"Shake On The Boat \",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Piero Piccioni\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0}],\"licensesNo\":0,\"recsArtist\":{\"artist_id\":0,\"name\":\"Piero Piccioni\"},\"recsLabel\":null,\"recsLabelGroup\":null,\"recsConfiguration\":null},{\"product_id\":55054,\"title\":\"Italian Soundtrack DigiDelivery 2\",\"artist_id\":0,\"label_id\":null,\"catalog_identifier\":null,\"release_date\":null,\"configuration_Id\":null,\"upc\":null,\"recordingsNo\":1,\"recordings\":[{\"track_id\":278437,\"title\":\"Vacanze Intelligenti\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Piero Piccioni\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0}],\"licensesNo\":0,\"recsArtist\":{\"artist_id\":0,\"name\":\"Piero Piccioni\"},\"recsLabel\":null,\"recsLabelGroup\":null,\"recsConfiguration\":null},{\"product_id\":55268,\"title\":\"UMPG Latin Sampler 2011\",\"artist_id\":0,\"label_id\":null,\"catalog_identifier\":null,\"release_date\":null,\"configuration_Id\":null,\"upc\":null,\"recordingsNo\":1,\"recordings\":[{\"track_id\":278742,\"title\":\"Asi Fue\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Playa Limbo\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0}],\"licensesNo\":0,\"recsArtist\":{\"artist_id\":0,\"name\":\"Playa Limbo\"},\"recsLabel\":null,\"recsLabelGroup\":null,\"recsConfiguration\":null},{\"product_id\":58220,\"title\":\"POCHY Y SU COCOBAND\",\"artist_id\":0,\"label_id\":null,\"catalog_identifier\":null,\"release_date\":null,\"configuration_Id\":null,\"upc\":null,\"recordingsNo\":10,\"recordings\":[{\"track_id\":287536,\"title\":\"Queremos Tiempo\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pochi Y Su Cocoband\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":287553,\"title\":\"Coco Con Reggae\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pochi Y Su Cocoband\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":287540,\"title\":\"Sexy, La\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pochi Y Su Cocoband\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":287544,\"title\":\"Monstruo, El\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pochi Y Su Cocoband\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":287548,\"title\":\"Pon A Funcionar Tu Corazon\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pochi Y Su Cocoband\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":287542,\"title\":\"Boca Chula\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pochi Y Su Cocoband\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":287550,\"title\":\"Que Te Gusta De Mi\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pochi Y Su Cocoband\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":287539,\"title\":\"Quiero Ser Presidente\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pochi Y Su Cocoband\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":287538,\"title\":\"Control\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pochi Y Su Cocoband\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0},{\"track_id\":287543,\"title\":\"Gracias\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pochi Y Su Cocoband\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0}],\"licensesNo\":0,\"recsArtist\":{\"artist_id\":0,\"name\":\"Pochi Y Su Cocoband\"},\"recsLabel\":null,\"recsLabelGroup\":null,\"recsConfiguration\":null},{\"product_id\":54991,\"title\":\"We Were So Turned On: Tribute To David Bowie\",\"artist_id\":0,\"label_id\":null,\"catalog_identifier\":null,\"release_date\":null,\"configuration_Id\":null,\"upc\":null,\"recordingsNo\":1,\"recordings\":[{\"track_id\":278299,\"title\":\"Cat People (Putting Out The Fire)\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Polyamorous Affair\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0}],\"licensesNo\":0,\"recsArtist\":{\"artist_id\":0,\"name\":\"Polyamorous Affair\"},\"recsLabel\":null,\"recsLabelGroup\":null,\"recsConfiguration\":null},{\"product_id\":25641,\"title\":\"Heartwarming Songs \",\"artist_id\":0,\"label_id\":null,\"catalog_identifier\":null,\"release_date\":null,\"configuration_Id\":null,\"upc\":null,\"recordingsNo\":1,\"recordings\":[{\"track_id\":169308,\"title\":\"Angel Band \",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Porter Wagoner\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0}],\"licensesNo\":0,\"recsArtist\":{\"artist_id\":0,\"name\":\"Porter Wagoner\"},\"recsLabel\":null,\"recsLabelGroup\":null,\"recsConfiguration\":null},{\"product_id\":54191,\"title\":\"AMS Add Ins 12\",\"artist_id\":0,\"label_id\":null,\"catalog_identifier\":null,\"release_date\":null,\"configuration_Id\":null,\"upc\":null,\"recordingsNo\":1,\"recordings\":[{\"track_id\":276598,\"title\":\"Speaking Of Happiness\",\"artist_id\":0,\"duration\":\"00:00:00\",\"isrc\":null,\"created_date\":\"0001-01-01T00:00:00\",\"artistname\":\"Pretty Lights\",\"licensePRWriters\":null,\"cd_index\":0,\"cd_number\":0,\"writersNo\":0}],\"licensesNo\":0,\"recsArtist\":{\"artist_id\":0,\"name\":\"Pretty Lights\"},\"recsLabel\":null,\"recsLabelGroup\":null,\"recsConfiguration\":null}],\"total\":49377,\"errorMessage\":null}";
           JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<PagedResponse<Product>>(result);
        }
        
       
        /// <summary>
        /// New method to get product header from recs
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("GetProductHeader/{productId}")]
        [HttpGet]
        public ProductHeader GetProductHeader(int productId)
        {
            
            return _productManager.GetProductHeader(productId);
        }

        [Route("GetProductHeaderFull/{licenseProductId}/{productId}")]
        [HttpGet]
        public ProductHeader GetProductHeaderFull(int licenseProductId, int productId)
        {
            
            return _productManager.GetProductHeaderFull(productId, licenseProductId);
        }


        [Route("UpdateProductPriority")]
        [HttpPost]
        public bool UpdateProductPriority(UpdatePriorityRequest request, HttpRequestMessage headers)
        {

            var safeIdHeader = headers.GetHeaderValue("x-modified-by");
            if (safeIdHeader == null) // add header handling
            {
                safeIdHeader = "";
            }
            return _productManager.UpdateProductPriority(request, safeIdHeader);
        }

        /// <summary>
        /// New method to get tracks from recs
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("GetProductRecsRecordings/{productId}")]
        [HttpGet]
        public List<WorksRecording> GetProductRecsRecordings(int productId)
        {
            return _productManager.GetProductRecsRecordings(productId);
        }

        /// <summary>
        /// New method to get writers from works
        /// </summary>
        /// <param name="worksCode"></param>
        /// <returns></returns>
        [Route("GetWorksWriters")]
        [HttpPost]
        public List<WorksWriter> GetWorksWriters([FromBody]string worksCode)
        {
            return _productManager.GetWorksWriters(worksCode);
        }
        //[Route("FindOutOfSyncRecItems/{licenseId}")]
        //[HttpGet]
        //public List<RecsProductChanges> FindOutOfSyncRecItems(int licenseId)
        //{
        //    return _productManager.FindOutOfSyncRecItems(licenseId);
        //}


        /*
        /// <summary>
        /// New method to add product configuration
        /// </summary>
        /// <param name="worksCode"></param>
        /// <returns></returns>
        [Route("AddProductConfiguration")]
        [HttpPost]
        public ProductHeader AddProductConfiguration(UpdateProductRequest request)
        {
            return _productManager.AddProductConfiguration(request);
        }
        */

        /// <summary>
        /// New method to add product
        /// </summary>
        /// <param name="worksCode"></param>
        /// <returns></returns>
        [Route("AddProduct")]
        [HttpPost]
        public AddProductResult AddProduct(AddProductRequest request)
        {
            return _productManager.AddProduct(request);
        }

        /// <summary>
        /// New method to add product
        /// </summary>
        /// <param name="worksCode"></param>
        /// <returns></returns>
        [Route("RetrieveTracks")]
        [HttpPost]
        public List<WorksRecording> RetrieveTracks([FromBody]int productId)
        {
            return _productManager.RetrieveTracks(productId);
        }

        /// <summary>
        /// New method to add product
        /// </summary>
        /// <param name="worksCode"></param>
        /// <returns></returns>
        [Route("RetrieveLabels")]
        [HttpGet]
        public List<RecordLabel> RetrieveLabels()
        {
            return _productManager.RetrieveLabels();
        }

        [Route("RetrieveTrack")]
        [HttpPost]
        //public UMPG.USL.Models.Recs.SingleResult<Track> RetrieveTrack([FromBody]int trackId)
        public UMPG.USL.Models.Recs.SingleResult<Track> RetrieveTrack(RetrieveTrackRequest request)
        {
            return _productManager.RetrieveTrack(request);
        }

        /// <summary>
        /// New method to save/add product
        /// </summary>
        /// <returns></returns>
        [Route("SaveProduct")]
        [HttpPost]
        public AddProductResult SaveProduct(ProductHeader request, HttpRequestMessage headers)
        {
            var safeIdHeader = headers.GetHeaderValue("x-modified-by");
            if (safeIdHeader == null) // add header handling
            {
                safeIdHeader = "";
            }
            return _productManager.SaveProduct(request, safeIdHeader);
        }

        /// <summary>
        /// New method to save/add productLink
        /// </summary>
        /// <returns></returns>
        [Route("SaveProductLink")]
        [HttpPost]
        public UpdateProductLinkResult SaveProductLink(ProductLink productLink, HttpRequestMessage headers)
        {
            var safeIdHeader = headers.GetHeaderValue("x-modified-by");
            if (safeIdHeader == null) // add header handling
            {
                safeIdHeader = "";
            }
            return _productManager.SaveProductLinkWithHeader(productLink, safeIdHeader);
        }

        /// <summary>
        /// New method to save/add productLink
        /// </summary>
        /// <returns></returns>
        [Route("GetProductLinks/{productId}")]
        [HttpGet]
        public List<GetProductLink> GetProductLinks(int productId)
        {
            return _productManager.GetProductLinks(productId);
        }

        /// <summary>
        /// New method to save/add productLink
        /// </summary>
        /// <returns></returns>
        [Route("DeleteProductLink")]
        [HttpPost]
        public UpdateProductLinkResult DeleteProductLink(ProductLink productLink)
        {
            return _productManager.DeleteProductLink(productLink);
        }

    }


    #region Helpers



    #endregion
}
