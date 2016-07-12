using System.Linq;
using System.Collections.Generic;
using System.Data;
using System;
using UMPG.USL.API.Data.Utils;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicenseNoteRepository : ILicenseNoteRepository
    {

        public LicenseNote Add(LicenseNote licensenote)
        {
            using (var context = new AuthContext())
            {
                context.LicenseNotes.Add(licensenote);
                context.SaveChanges();

                return licensenote;
            }
        }

        public LicenseNote Get(int id)
        {
            using (var context = new AuthContext())
            {
                var licensenote = context.LicenseNotes
                    .Include("NoteType")
                    .Include("Contact")
                    .FirstOrDefault(c => c.licenseNoteId == id);
                return licensenote;
            }
        }

        public List<LicenseNote> GetAll()
        {
            using (var context = new AuthContext())
            {
                var licensenotes = context.LicenseNotes.ToList();
                return licensenotes;

            }
        }

        public List<LicenseNote> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var licensenotes = context.LicenseNotes
                    .AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return licensenotes.Where(p => p.Note.ToString().ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return licensenotes.ToList();
                }
            }
        }

        public LicenseNote GetLicenseNote(int licensenoteid)
        {
            using (var context = new AuthContext())
            {
                var licensenote = context.LicenseNotes
                    .Include("NoteType")
                    .Include("Contact")
                    .FirstOrDefault(c => c.licenseNoteId == licensenoteid);
                return licensenote;
            }
        }

        public List<LicenseNote> GetLicenseNotes(int licenseid)
        {
            using (var context = new AuthContext())
            {
                var licensenotes = context.LicenseNotes
                    .Include("NoteType")
                    .Include("Contact")
                    .Where(x => x.licenseId == licenseid && !x.Deleted.HasValue)
                    .ToList();
                return licensenotes;

            }
        }

        public void UpdateLicenseNote(LicenseNote licensenote)
        {
            using (var context = new AuthContext())
            {
                context.Entry(licensenote).State = (EntityState) System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
