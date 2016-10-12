
using UMPG.USL.Common;

namespace UMPG.USL.API.Data.Configuration
{
    public class SolrConfigurationRetriever : ISolrConfigurationRetriever
    {
        private readonly RecsConfiguration _recsConfiguration;

        public SolrConfigurationRetriever()
        {
            _recsConfiguration = new RecsConfiguration
                                     {
                                         SecureUrl = ConfigHelper.GetAppSettingValue("SolrSearchSecureUrl", true),
                                         UnSecureUrl = ConfigHelper.GetAppSettingValue("SolrSearchUnSecureUrl", true),
                                     };
        }

        public RecsConfiguration RecsConfiguration
        {
            get { return _recsConfiguration; }
        }
    }
}