using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Data.Recs
{
    public interface IProductRecordingLinkRepository
    {
       int GetRecordingsNo(int productId);

       List<int> GetRecordingsIds(int productId);
        List<ProductRecordingLink> GetProductRecordings(int productId);

        ProductRecordingLink GetProductRecordingLink(long trackId);

    }
}
