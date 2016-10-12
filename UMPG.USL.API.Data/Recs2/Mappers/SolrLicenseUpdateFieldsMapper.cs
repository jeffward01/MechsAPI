using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Common.Mappers;
using UMPG.USL.Models;

namespace UMPG.USL.API.Data.Recs.Mappers
{
    public class SolrLicenseUpdateFieldsMapper : IMapper<List<object>, SolrUpdateCommand>
    {
        public List<object> Map(SolrUpdateCommand source)
        {
            var json = new List<KeyValuePair<string, object>>();
            json.Add(new KeyValuePair<string, object>("Id", source.Id));
            foreach (var field in source.Fields)
            {
                json.Add(new KeyValuePair<string, object>(field.Key, field.Value));
            }
            var inList = new List<object>();
            inList.Add(json);
            return inList;
        }
    }
}