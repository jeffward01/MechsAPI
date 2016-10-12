using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public interface IProductRepository
    {
        //int Add(Product priority);

        Product Get(int Id);

        List<Product> GetAll();

        List<Product> GetProducts(List<int> ids);

        List<Product> Search(string query);

        PagedResponse<Product> PagedSearch(ProductRequest request);

        ProductConfiguration GetProductConfiguration(int product_id, int configuration_id);

        List<Product> GetProductsBrief(List<int> ids);

        List<RecsConfiguration> GetProductConfigurations(int licenseProductId, int product_id);

        List<ProductConfiguration> GetRecsProductConfiguration(int product_id);

        RecsConfiguration GetRecsConfiguration(int configuration_id);

    }
}
