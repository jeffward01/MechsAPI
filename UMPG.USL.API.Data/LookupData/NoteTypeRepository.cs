using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    class NoteTypeRepository:INoteTypeRepository
    {
        public List<LU_NoteType> GetLicenseNoteTypes()
        {
            using (var context = new AuthContext())
            {
                return context.LU_NoteTypes.ToList();
            }
        }
    }
}
