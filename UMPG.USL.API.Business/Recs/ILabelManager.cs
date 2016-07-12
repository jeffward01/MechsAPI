using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Recs
{
    public interface ILabelManager
    {

        List<Label> GetAll();
        List<Publisher> GetPublishers(string query);
        List<Configuration> GetRecsConfigurations();
    }
}
