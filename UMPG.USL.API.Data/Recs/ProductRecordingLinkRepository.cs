using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public class ProductRecordingLinkRepository: IProductRecordingLinkRepository
    {
        public int GetRecordingsNo(int productId)
        {
            using (var context = new AuthContext())
            {
                return context.ProductRecordingLink.Count(x => x.product_id == productId);
            }
            
        }

        public ProductRecordingLink GetProductRecordingLink(long trackId)
        {
            using (var context = new AuthContext())
            {
                return context.ProductRecordingLink.Where(x => x.track_id == trackId)
                    .FirstOrDefault();
            }
        }

        public List<int> GetRecordingsIds(int productId)
        {
            using (var context = new AuthContext())
            {
                return context.ProductRecordingLink.Where(x => x.product_id == productId)
                    .Select(x => (int)x.track_id)
                    .ToList();
            }
        }

        public List<ProductRecordingLink> GetProductRecordings(int productId)
        {
            using (var context = new AuthContext())
            {
                var recordings = context.ProductRecordingLink
                    .Include("RecsRecording")
                    .Include("RecsRecording.RecsArtist")
                    .Where(pr => pr.product_id == productId);
                return recordings.ToList();
            }
        } 
    }
}
