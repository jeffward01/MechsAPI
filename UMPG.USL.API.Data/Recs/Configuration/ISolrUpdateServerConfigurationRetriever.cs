using UMPG.USL.API.Data.Configuration;

namespace UMPG.USL.API.Data.Recs
{
    public interface ISolrUpdateServerConfigurationRetriever
    {
        RecsConfiguration RecsConfiguration { get; }
    }
}