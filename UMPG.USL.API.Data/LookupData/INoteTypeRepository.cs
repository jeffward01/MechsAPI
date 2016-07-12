using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface INoteTypeRepository
    {
        List<LU_NoteType> GetLicenseNoteTypes();
    }
}
