using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models;

namespace UMPG.USL.API.Business.Recs
{
    public interface IProductManager
    {
        PagedResponse<Product> PagedSearch(ProductRequest request);
        ProductHeader GetProductHeader(int productId);
        List<WorksRecording> GetProductRecsRecordings(int productId);
        List<WorksWriter> GetWorksWriters(string worksCode);
        //ProductHeader AddProductConfiguration(UpdateProductRequest updateProductRequest);
        AddProductResult AddProduct(AddProductRequest updateProductRequest);
        ProductHeader UpdateProduct(object updateProductRequest);
        List<WorksRecording> RetrieveTracks(int productId);
        List<RecordLabel> RetrieveLabels();
        SingleResult<Track> RetrieveTrack(RetrieveTrackRequest request);
        AddProductResult SaveProduct(ProductHeader request);
        UpdateProductLinkResult SaveProductLink(ProductLink productLink);
        List<GetProductLink> GetProductLinks(int productId);
        UpdateProductLinkResult DeleteProductLink(ProductLink productLink);
        bool UpdateProductPriority(UpdatePriorityRequest request);

    }
}
