using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotAddressRepository : ISnapshotAddressRepository
    {
        public List<Snapshot_Address> GetAllAddressesForCloneContactId(int cloneContactId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Addresses.Where(_ => _.ContactId == cloneContactId).ToList();
            }
        }


        public bool DeleteAddressBySnapshotAddressId(int snapshotAddressId)
        {
            using (var context = new AuthContext())
            {
                var address = context.Snapshot_Addresses.Find(snapshotAddressId);
                context.Snapshot_Addresses.Attach(address);
                context.Snapshot_Addresses.Remove(address);
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
