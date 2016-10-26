using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotPhoneRepository : ISnapshotPhoneRepository
    {
        public List<Snapshot_Phone> GetAllPhonesForCloneContactId(int cloneContactId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Phones.Where(_ => _.ContactId == cloneContactId).ToList();
            }
        }


        public bool DeletePhoneBySnapshotPhoneId(int snapshotPhoneId)
        {
            using (var context = new AuthContext())
            {
                var address = context.Snapshot_Phones.Find(snapshotPhoneId);
                context.Snapshot_Phones.Attach(address);
                context.Snapshot_Phones.Remove(address);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return true;
        }
    }
}
