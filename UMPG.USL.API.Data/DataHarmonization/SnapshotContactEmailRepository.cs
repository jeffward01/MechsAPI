using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotContactEmailRepository : ISnapshotContactEmailRepository
    {
        public List<Snapshot_ContactEmail> GetAllContactEmailsForCloneContactId(int cloneContactId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ContactEmails.Where(_ => _.ContactId == cloneContactId).ToList();
            }
        }

        public bool DeleteContactEmailBySnapshotContactEmailId(int snapshotAddressId)
        {
            using (var context = new AuthContext())
            {
                var address = context.Snapshot_ContactEmails.Find(snapshotAddressId);
                context.Snapshot_ContactEmails.Attach(address);
                context.Snapshot_ContactEmails.Remove(address);
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