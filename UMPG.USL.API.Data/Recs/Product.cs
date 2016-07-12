using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.API.Data.Utils;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public class ProductRepository : IProductRepository
    {

        //public Int64 Add(Product Product)
        //{
        //    using (var context = new AuthContext())
        //    {
        //        context.Products.Add(Product);
        //        context.SaveChanges();

        //        return Product.Id;
        //    }
        //}

        public Product Get(int id)
        {
            using (var context = new AuthContext())
            {
                var test = context.Products
                    .Include("RecsArtist")
                    .Include("RecsLabel")
                    .Include("RecsConfiguration")
                    .FirstOrDefault(c => c.product_id == id);

                var labelgroupid = context.LabelGroupLabels
                    .Where(x => x.label_id == test.label_id)
                    .Select(x => x.label_group_id)
                    .DefaultIfEmpty(0)
                    .First();

                if (labelgroupid != 0)
                {
                    test.RecsLabelGroup = context.LabelGroups
                                    .Where(y => y.label_group_id == labelgroupid)
                                    .Select(y => y.Name)
                                    .First();
                }

                return test;
            }
        }

        public List<Product> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.Products.OrderBy(c => c.Title).ToList();
            }
        }



        public List<Product> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var Products = context.Products.Where(c => c.Title == query).AsQueryable();

                foreach (var product in Products)
                {
                    //Get Recs track Count 
                    product.RecordingsNo = context.ProductRecordingLink
                        .Where(x => x.product_id == product.product_id).Count();

                }

                if (!String.IsNullOrEmpty(query))
                {
                    return Products.Where(c => c.Title.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return Products.ToList();
                }
            }
        }

        public PagedResponse<Product> PagedSearch(ProductRequest request)
        {

            using (var context = new AuthContext())
            {
                var products = context.Products
                    .Include("RecsArtist")
                    .Include("RecsLabel")
                    .Include("RecsConfiguration")
                    .AsQueryable();

                var response = new PagedResponse<Product>();
                var sortOrder = (string.IsNullOrEmpty(request.SortOrder) || request.SortOrder == "asc") ? false : true;
                var orderByDict = new Dictionary<string, string>
                {
                    {"title", "title"},
                    {"createdDate", ""},
                    {"modifiedDate", ""},
                    {"createdby", ""}
                };
                if (request.OrderBy != null && orderByDict.ContainsKey(request.OrderBy))
                    request.OrderBy = orderByDict[request.OrderBy];

                IQueryable<Product> results;

                if (!String.IsNullOrEmpty(request.SearchTerm))
                {
                    results =
                        products.Where(p => p.Title.ToString().ToLower().Contains(request.SearchTerm.ToLower()));
                }
                else
                {
                    results = products;
                }


                if (string.IsNullOrEmpty(request.OrderBy))
                {
                    results = results.DynamicOrderBy("product_id", sortOrder).AsQueryable();
                }
                else
                {
                    if (request.OrderBy == "title")
                    {
                        if (!sortOrder)
                        {
                            results = results.OrderBy(l => l.Title);
                        }
                        else
                        {
                            results = results.OrderByDescending(l => l.Title);
                        }
                    }
                    else if (request.OrderBy == "artist")
                    {
                        if (!sortOrder)
                        {
                            results = results.OrderBy(l => l.RecsArtist.name);
                        }
                        else
                        {
                            results = results.OrderByDescending(l => l.RecsArtist.name);
                        }

                    }
                    else if (request.OrderBy == "upc")
                    {
                        if (!sortOrder)
                        {
                            results = results.OrderBy(l => l.Upc);
                        }
                        else
                        {
                            results = results.OrderByDescending(l => l.Upc);
                        }
                    }
                    else if (request.OrderBy == "label")
                    {
                        if (!sortOrder)
                        {
                            results = results.OrderBy(l => l.RecsLabel.name);
                        }
                        else
                        {
                            results = results.OrderByDescending(l => l.RecsLabel.name);
                        }
                    }
                    else if (request.OrderBy == "releasedate")
                    {
                        if (!sortOrder)
                        {
                            results = results.OrderBy(l => l.release_date);
                        }
                        else
                        {
                            results = results.OrderByDescending(l => l.release_date);
                        }
                    }
                    else if (request.OrderBy == "licenses")
                    {
                        if (!sortOrder)
                        {
                            results = results.OrderBy(l => l.release_date);
                        }
                        else
                        {
                            results = results.OrderByDescending(l => l.release_date);
                        }
                    }
                    //results = results.DynamicOrderBy(request.OrderBy, sortOrder).AsQueryable();

                }


                response.Total = results.Count();
                results = results.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

                var test = results.ToList();
                foreach (var product in test)
                {
                    //Get Recs track Count 
                    product.RecordingsNo = context.ProductRecordingLink
                        .Where(x => x.product_id == product.product_id).Count();

                }

                response.Results = test.ToList();
                return response;

            }
        }


        public List<Product> GetProducts(List<int> ids)
        {
            using (var context = new AuthContext())
            {

                var products =
                    context.Products.AsNoTracking()
                    .Include("RecsArtist")
                    .Include("RecsLabel")
                    .Include("RecsConfiguration")
                    .Where(x => ids.Contains((int)x.product_id))
                    .ToList();

                foreach (var product in products)
                {
                    //Get Recs track Count 
                    product.RecordingsNo = context.ProductRecordingLink
                        .Where(x => x.product_id == product.product_id).Count();
                }

                return products;
            }
        }


        public List<Product> GetProductsBrief(List<int> ids)
        {
            using (var context = new AuthContext())
            {

                var products =
                    context.Products.AsNoTracking()
                    .Include("RecsArtist")
                    //.Include("RecsConfiguration").Where(r => r.RecsConfiguration.configuration_id == configurationId)
                     .Where(x => ids.Contains((int)x.product_id))
                    .ToList();
                return products;
            }
        }

        public ProductConfiguration GetProductConfiguration(int product_id, int configuration_id)
        {
            using (var context = new AuthContext())
            {
                var productConfig =
                    context.ProductConfigurations.AsNoTracking()
                    //.Include("RecsConfiguration")
                    .Where(x => x.product_id == product_id && x.configuration_Id == configuration_id).FirstOrDefault();
                return productConfig;
            }
        }

        public List<RecsConfiguration> GetProductConfigurations(int licenseProductId, int product_id)
        {
            var productConfigurations = new List<RecsConfiguration>();
            using (var context = new AuthContext())
            {
                var licenseProductConfig = from lp in context.LicenseProducts
                                           join lpc in context.LicenseProductConfigurations on lp.LicenseProductId equals lpc.LicenseProductId
                                           where lp.ProductId == product_id && lpc.LicenseProductId == licenseProductId && lp.Deleted == null && lpc.Deleted == null
                                           select lpc;

                foreach (var config in licenseProductConfig)
                {
                    productConfigurations.Add(new RecsConfiguration() { configuration_id = config.configuration_id, name = config.configuration_name });
                }
            }
            return productConfigurations;
        }

        public List<ProductConfiguration> GetRecsProductConfiguration(int product_id)
        {
            using (var context = new AuthContext())
            {
                var productConfigs =
                    context.ProductConfigurations.AsNoTracking()
                    .Where(x => x.product_id == product_id).ToList();

                foreach (var productConfig in productConfigs)
                {
                    var config = GetRecsConfiguration((int)productConfig.configuration_Id);
                    productConfig.configuration_name = config.name;
                }
                return productConfigs;
            }
        }

        public RecsConfiguration GetRecsConfiguration(int configuration_id)
        {
            using (var context = new AuthContext())
            {
                var config =
                    context.RecsConfigurations.AsNoTracking()
                    .Where(x => x.configuration_id == configuration_id).FirstOrDefault();
                return config;
            }
        }




    }
}
