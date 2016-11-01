using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLicenseNoteRepository : ISnapshotLicenseNoteRepository
    {
        public Snapshot_LicenseNote SaveSnapshotLicenseNote(Snapshot_LicenseNote snapshotLicenseNote)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_LicenseNotes.Add(snapshotLicenseNote);
                context.SaveChanges();
                return snapshotLicenseNote;
            }
        }

        public Snapshot_LicenseNote GetSnapshotLicenseNoteByNoteId(int licenseNoteId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseNotes.Find(licenseNoteId);
            }
        }

        public bool DeleteLicenseNoteSnapshotByLicenseNoteId(int snapshotNoteId)
        {
            using (var context = new AuthContext())
            {
                var note = context.Snapshot_LicenseNotes.FirstOrDefault(_ => _.SnapshotLicenseNoteId == snapshotNoteId);
                context.Snapshot_LicenseNotes.Attach(note);
                context.Snapshot_LicenseNotes.Remove(note);
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

        public List<Snapshot_LicenseNote> GetAllLicenseNoteForLicenseId(int licenseId)
        {
            using (var context = new AuthContext())
            {
                return
                    context.Snapshot_LicenseNotes.Where(_ => _.LicenseId == licenseId)
                        .ToList();
            }
        }




        public List<int> GetAllLicenseNoteIdsForLicenseId(int licenseId)
        {
            using (var context = new AuthContext())
            {
                return
                    context.Snapshot_LicenseNotes.Where(_ => _.LicenseId == licenseId)
                        .Select(_ => _.SnapshotLicenseNoteId)
                        .ToList();
            }
        }


        public List<int> GetAllContactIdsRelatedToNote(int licneseId)
        {
            using (var context = new AuthContext())
            {
                var notes =
                    context.Snapshot_LicenseNotes.Include("Contact").
                        Where(_ => _.LicenseId == licneseId);
                return notes.Select(_ => _.Contact.SnapshotContactId).ToList();

            }
        }
    }
}