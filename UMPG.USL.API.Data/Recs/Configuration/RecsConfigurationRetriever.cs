
using UMPG.USL.Common;

namespace UMPG.USL.API.Data.Configuration
{
    public class RecsConfigurationRetriever : IRecsConfigurationRetriever
    {
        private readonly RecsConfiguration _recsConfiguration;

        public RecsConfigurationRetriever()
        {
            _recsConfiguration = new RecsConfiguration
                                     {
                                         SecureUrl = ConfigHelper.GetAppSettingValue("RecsSecureUrl", true),
                                         UnSecureUrl = ConfigHelper.GetAppSettingValue("RecsUnSecureUrl", true),
                                         WorksUnSecureUrl = ConfigHelper.GetAppSettingValue("QualifyingWorksUnSecureUrl",true)
                                     };
        }

        public RecsConfiguration RecsConfiguration
        {
            get { return _recsConfiguration; }
        }
    }
}