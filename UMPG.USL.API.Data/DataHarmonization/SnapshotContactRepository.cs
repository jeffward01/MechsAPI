using System;
using System.Linq;
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

        public bool DeleteContactBySnapshotContactId(int snapshotContactId)
        {
            using (var context = new AuthContext())
            {
                var contact = context.Snapshot_Contacts.Find(snapshotContactId);
                context.Snapshot_Contacts.Attach(contact);
                context.Snapshot_Contacts.Remove(contact);
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

        public int GetRoleIdForCOntactId(int contactId)
        {
            using (var context = new AuthContext())
            {
                var cotact = context.Snapshot_Contacts.FirstOrDefault(_ => _.SnapshotContactId == contactId);
                return cotact.RoleId.Value;
            }
        }
        public int GetCloneContactIdForContactId(int contactId)
        {
            using (var context = new AuthContext())
            {
                var cotact = context.Snapshot_Contacts.FirstOrDefault(_ => _.SnapshotContactId == contactId);
                return cotact.CloneContactId;
            }
        }

    }
}