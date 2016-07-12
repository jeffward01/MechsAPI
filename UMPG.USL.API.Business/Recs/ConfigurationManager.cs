using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Recs
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IRecsDataProvider _recsProvider;
        public ConfigurationManager(IRecsDataProvider recsProvider)
        {
            _recsProvider = recsProvider;
        }

        /// <summary>
        /// New method to call recs for all recs_configurations 
        /// </summary>
        /// <returns></returns>
        public List<RecsConfigurations> GetConfigurations()
        {
            return _recsProvider.RetrieveConfigurations();
        }

    }
}
