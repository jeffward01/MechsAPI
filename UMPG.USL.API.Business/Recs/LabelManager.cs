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
    public class LabelManager : ILabelManager
    {

        private readonly IRecsDataProvider _recsProvider;

        public LabelManager(IRecsDataProvider recsProvider)
        {
            _recsProvider = recsProvider;
        }


        public List<Label> GetAll()
        {
            return _recsProvider.GetLabels();
        }

        public List<Publisher> GetPublishers(string query)
        {
            return _recsProvider.GetPublshers(query);
        }

        public List<Configuration> GetRecsConfigurations()
        {
            return _recsProvider.GetRecsConfigurations();
        }
    }
}
