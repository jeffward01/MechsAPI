using System.Linq;
using UMPG.USL.Models.LicenseModel;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.LicenseData
{
    public  class LicenseSequenceRepository: ILicenseSequenceRepository
    {
        public LicenseSequence Get()
        {
            using (var context = new AuthContext())
            {
                return context.LicenseSequence.FirstOrDefault();
            }
        }

        public void Update(LicenseSequence newValue)
        {
            using (var context = new AuthContext())
            {
                context.Entry(newValue).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
