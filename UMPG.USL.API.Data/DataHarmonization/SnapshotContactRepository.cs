using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotContactRepository : ISnapshotContactRepository
    {
        public Snapshot_Contact SaveSnapshotContact(Snapshot_Contact contactSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Contacts.Add(contactSnapshot);
                context.SaveChanges();
                return contactSnapshot;
            }
        }

        public Snapshot_Contact GetSanSnapshotContactByContactId(int contactId)
        {
            using (var contect = new AuthContext())
            {
                return contect.Snapshot_Contacts.Find(contactId);
            }
        }
    }
}