using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.Configuration;
using UMPG.USL.Common;

namespace UMPG.USL.API.Data.Recs
{
    public class SolrUpdateServerConfigurationRetriever : ISolrUpdateServerConfigurationRetriever
    {
         private readonly RecsConfiguration _recsConfiguration;

         public SolrUpdateServerConfigurationRetriever()
        {
            _recsConfiguration = new RecsConfiguration
                                     {
                                         SecureUrl = ConfigHelper.GetAppSettingValue("SolrUpdateSecureUrl", true),
                                         UnSecureUrl = ConfigHelper.GetAppSettingValue("SolrUpdateUnSecureUrl", true),
                                     };
        }

        public RecsConfiguration RecsConfiguration
        {
            get { return _recsConfiguration; }
        }
    }
}
