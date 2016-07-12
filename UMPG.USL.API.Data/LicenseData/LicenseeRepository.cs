using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicenseeRepository : ILicenseeRepository
    {

        public Licensee Add(Licensee licensee)
        {
            using (var context = new AuthContext())
            {
                context.Licensees.Add(licensee);
                context.SaveChanges();

                return licensee;
            }
        }

        public Licensee Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Licensees.FirstOrDefault(c => c.LicenseeId == id);
            }
        }

        public List<Licensee> GetAll()
        {
            using (var context = new AuthContext())
            {
                var test =  context.Licensees
                    .Include(x =>x.LicenseeLabelGroup)
                    .Include(x =>x.LicenseeLabelGroup.Select(y=>y.LabelGroupLinks))
                    //.Include("LicenseeLabelGroup.LabelGroupLinks.Contact")
                    .Include(x=>x.LicenseeLabelGroup.Select(y=>y.LabelGroupLinks.Select(z=>z.Contact)))
                    .Where(x=>!x.Deleted.HasValue).OrderBy(x=>x.Name)
                    .ToList();
                return test;
            }
        }

        public List<Licensee> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var Licensees = context.Licensees.Where(c => c.Name == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return Licensees.Where(c => c.Name.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return Licensees.ToList();
                }
            }
        }

        public PagedResponse<Licensee> GetLicensees(LicenseeAdminRequest request)
        {
            using (var context = new AuthContext())
            {
                var response = new PagedResponse<Licensee>();
                var results = context.Licensees
                    .Include(x => x.Address)
                    .Include(x => x.LicenseeContacts)
                    .Include(x => x.LicenseeContacts.Select(y => y.Address))
                    .Include(x => x.LicenseeContacts.Select(y => y.Email))
                    .Include(x => x.LicenseeContacts.Select(y => y.Phone))
                    .Include(x => x.LicenseeLabelGroup)
                    .Include(x => x.LicenseeLabelGroup.Select(y => y.LabelGroupLinks))
                    //.Include("LicenseeLabelGroup.LabelGroupLinks.Contact")
                    .Include(x => x.LicenseeLabelGroup.Select(y => y.LabelGroupLinks.Select(z => z.Contact)))
                    .Include(x => x.LicenseeLabelGroup.Select(y => y.LabelGroupLinks.Select(z => z.Contact.Address)))
                    .Include(x => x.LicenseeLabelGroup.Select(y => y.LabelGroupLinks.Select(z => z.Contact.Phone)))
                    .Include(x => x.LicenseeLabelGroup.Select(y => y.LabelGroupLinks.Select(z => z.Contact.Email))).Where(x=>!x.Deleted.HasValue)
                    .AsQueryable();
                response.Total = results.Count();
                response.Results = results.OrderBy(x => x.Name).Skip(request.PageNo * request.PageSize).Take(request.PageSize).ToList();
                //var results = context.Licensees
                //    .Include(x => x.Address)
                //    .Include(x => x.LicenseeLabelGroup)
                //    .Include(x => x.LicenseeLabelGroup.Select(y => y.LabelGroupLinks))
                //    //.Include("LicenseeLabelGroup.LabelGroupLinks.Contact")
                //    .Include(x => x.LicenseeLabelGroup.Select(y => y.LabelGroupLinks.Select(z => z.Contact)))
                //    .Include(x => x.LicenseeLabelGroup.Select(y => y.LabelGroupLinks.Select(z => z.Contact.Address)))
                //    .Include(x => x.LicenseeLabelGroup.Select(y => y.LabelGroupLinks.Select(z => z.Contact.Phone)))
                //    .Include(x => x.LicenseeLabelGroup.Select(y => y.LabelGroupLinks.Select(z => z.Contact.Email)))
                //    .Where(x => x.LicenseeId == 1).Distinct();
                //System.Diagnostics.Trace.WriteLine(results.ToString());

                return response;
            }
        }

        public Licensee EditLicensee(Licensee request)
        {
            using (var context = new AuthContext())
            {
                context.Entry(request).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
                return request;
            }
        }
    }
}
