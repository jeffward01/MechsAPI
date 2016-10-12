using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.Common.Mappers
{
    public class ProductSOLRMapper : IMapper<Product, ProductSOLR>
    {
        private readonly IRecsDataProvider _recsProvider;

        public ProductSOLRMapper(IRecsDataProvider recsProvider) //, IRecordingWorkLinkRepository recordingWorkLinkRepository
        {
            _recsProvider = recsProvider;
            
        }


        public Product Map(ProductSOLR source)
        {
            var product = new Product();
            product.LicensesNo = source.LicenseCount;

            product.RecordingsNo = source.RecordingsNo; 
            product.RecsArtist = source.Artist;
            product.Title = source.Title;
            product.Upc = source.Upc;
            product.product_id = source.Id;
            product.artist_id = source.Artist.artist_id;
            if (source.Label != null)
            {
                product.RecsLabel = new Label { label_id = source.Label.label_id.GetValueOrDefault(), name = source.Label.name };
            }


            return product;
        }
    }
}