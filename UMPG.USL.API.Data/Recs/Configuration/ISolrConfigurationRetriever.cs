namespace UMPG.USL.API.Data.Configuration
{
    public interface ISolrConfigurationRetriever
    {
        RecsConfiguration RecsConfiguration { get; }
    }
}