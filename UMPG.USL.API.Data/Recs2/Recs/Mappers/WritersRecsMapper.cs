using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.Common.Mappers
{
    public class WritersRecsRMapper : IMapper<Writer, WorksWriter>
    {

        public Writer Map(WorksWriter source)
        {
            var writer = new Writer();
            writer.cae = source.CaeNumber;
            writer.fullName = source.FullName;
            return writer;
        }
    }
}