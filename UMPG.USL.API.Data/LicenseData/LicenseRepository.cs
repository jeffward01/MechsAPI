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
    public class LicenseRepository : ILicenseRepository
    {

        public License Add(License license)
        {
            using (var context = new AuthContext())
            {
                context.Licenses.Add(license);
                context.SaveChanges();

                return license;
            }
        }

        public License Get(int id)
        {
            using (var context = new AuthContext())
            {
                var license = context.Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("Licensee.LicenseeLabelGroup")
                    .Include("LicenseStatus")
                    .Include("Licensee")
                    .Include("LicenseMethod")
                    .Include("Contact")
                    .Include("Contact2")
                    .Include("LicenseeContact")
                    .Include("LicenseeContact.Address")

                    
                    .FirstOrDefault(c => c.LicenseId == id);
                license.LicenseNoteList = context.LicenseNotes
                                            .Include("NoteType")
                                            .Include("Contact")
                                            .Where(a => a.licenseId == id&&!a.Deleted.HasValue).ToList();
                //license.Licensee.LicenseeLabelGroup = context.LicenseeLabelGroups
                //           .Where(a => a.LicenseeId == license.Licensee.LicenseeId).ToList();
                
                return license;
            }
        }

        public License GetLicnese(int id)
        {
            using (var context = new AuthContext())
            {
                var license = context.Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("Licensee.LicenseeLabelGroup")
                    .Include("LicenseStatus")
                    .Include("Licensee")
                    .Include("LicenseMethod")
                    .Include("Contact")
                    .Include("Contact2")
                    .Include("LicenseeContact")
                    .Include("LicenseeContact.Address")


                    .FirstOrDefault(c => c.LicenseId == id);
         
                //           .Where(a => a.LicenseeId == license.Licensee.LicenseeId).ToList();

                return license;
            }
        }

        public List<License> GetProductLicenses(long productId)
        {
            using (var context = new AuthContext())
            {
                var licensesIds =
                    context.LicenseProducts.Where(lp => lp.Deleted == null && lp.ProductId == productId)
                        .Select(lp => lp.LicenseId)
                        .ToList();
                var licenses = context.Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                    .Include("Licensee")
                    .Include("LicenseMethod")
                    .Include("Contact")
                    .Include("Contact2")
                    .Include("LicenseeContact")
                    .Include("LicenseeContact.Address")
                    .Where(l => licensesIds.Contains(l.LicenseId)).ToList();
                foreach (var license in licenses)
                {
                    license.LicenseNoteList = context.LicenseNotes
                                           .Include("NoteType")
                                           .Include("Contact")
                                           .Where(a => a.licenseId == license.LicenseId && !a.Deleted.HasValue).ToList();
                    //license.Licensee.Address = context.Addresses
                    //                           .Where(a => a.ContactId == license.ContactId).ToList();

                    license.Licensee.LicenseeLabelGroup = context.LicenseeLabelGroups
                               .Where(a => a.LicenseeId == license.Licensee.LicenseeId).ToList();
                }
               

                return licenses;
            }
        } 

        public List<License> GetAll()
        {
            using (var context = new AuthContext())
            {
                var licenses = context.Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                    .Include("Licensee")
                    .Include("LicenseMethod")
                    .Include("Contact")
                    .ToList();
                return licenses;

            }
        }


        public List<License> GetAll(int startLicenseIdIndex, int endLicenseIdIndex)
        {
            using (var context = new AuthContext())
            {
                var licenses = context.Licenses
                    .Include("Contact")
                    .Include("Contact2")
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                    .Include("Licensee")
                    .Include("LicenseMethod")
                    .OrderBy(x => x.LicenseId)
                    .Where(x => x.LicenseId >= startLicenseIdIndex && x.LicenseId <= endLicenseIdIndex && x.Deleted == null)
                    .ToList();
                return licenses;
            }
        }


        public List<License> GetInboxLicenses(int assigneeId)
        {
            using (var context = new AuthContext())
            {
                var licenses = context.Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                    .Include("Licensee")
                    .Include("LicenseMethod")
                    .Include("Contact")
                    .Include("Contact2")
                    .Where(x => x.PriorityId == 4 && x.AssignedToId == assigneeId)
                    .ToList();
                return licenses;

            }
        }


        public List<License> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var licenses = context.Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                    .Include("Licensee")
                    .Include("LicenseMethod")
                    .Include("Contact")
                    .AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return licenses.Where(p => p.LicenseName.ToString().ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return licenses.ToList();
                }
            }
        }

        public PagedResponse<License> PagedSearch(LicenseRequest request)
        {
            using (var context = new AuthContext())
            {
                var licenses = context.Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                    .Include("Licensee")
                    .Include("LicenseMethod")
                    .Include("Contact")
                    .Include("Contact2")
                    .AsQueryable();

                var response = new PagedResponse<License>();
                var sortOrder = (string.IsNullOrEmpty(request.SortOrder) || request.SortOrder == "asc") ? false : true;
                var orderByDict = new Dictionary<string, string>
                {
                    {"title", "LicenseName"},
                    {"asignee", "Contact.FullName"},
                    {"createdDate", "License.CreatedDate"},
                    {"modifiedDate", "License.ModifiedDate"},
                    {"createdby", "Contact2.FullName"},
                    {"status", "LicenseStatus"},
                    {"type", "LicenseType"},
                    {"licensee", "Licensee.Name"},
                    {"licenseNumber", "LicenseNumber"},
                    {"method", "LicenseMethod"},
                    {"priority", "LicensePriority"},
                    {"signedDate", "License.SignedDate"},
                    {"artistRollup", "License.ArtistRollup"},
                    {"licenseConfigurationRollup", "License.LicenseConfigurationRollup"}

                };
                if (request.OrderBy != null && orderByDict.ContainsKey(request.OrderBy))
                    request.OrderBy = orderByDict[request.OrderBy];
                IQueryable<License> results;
                if (!String.IsNullOrEmpty(request.SearchTerm))
                {
                    results =
                        licenses.Where(p => p.LicenseName.ToString().ToLower().Contains(request.SearchTerm.ToLower()));
                }
                else
                {
                    results = licenses;
                }
                if (request.Assignees.Count > 0)
                {
                    results = results.Where(l => request.Assignees.Contains(l.Contact.ContactId));
                }
                if (request.Licensees.Count > 0)
                {
                    results = results.Where(l => request.Licensees.Contains(l.LicenseeId));
                }
                if (request.LicenseStatuses.Count > 0)
                {
                    results = results.Where(l => request.LicenseStatuses.Contains(l.LicenseStatusId));
                }
                if (request.LicenseTypes.Count > 0)
                {
                    results = results.Where(l => request.LicenseTypes.Contains(l.LicenseTypeId));
                }
                if (request.LicMethods.Count > 0)
                {
                    results = results.Where(l => request.LicMethods.Contains(l.LicenseMethodId));
                }
                if (string.IsNullOrEmpty(request.OrderBy))
                {
                    results = results.DynamicOrderBy("LicenseId", sortOrder).AsQueryable();
                }
                else if (request.OrderBy == "Contact.FullName")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.Contact.FullName);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.Contact.FullName);
                    }
                }
                else if (request.OrderBy == "LicenseStatus")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.LicenseStatus.LicenseStatus);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.LicenseStatus.LicenseStatus);
                    }

                }
                else if (request.OrderBy == "LicenseMethod")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.LicenseMethod.LicenseMethod);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.LicenseMethod.LicenseMethod);
                    }

                }
                else if (request.OrderBy == "Contact2.FullName") // Created By
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.Contact2.FullName);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.Contact2.FullName);
                    }
                }
                else if (request.OrderBy == "Licensee.Name")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.Licensee.Name);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.Licensee.Name);
                    }
                }
                else if (request.OrderBy == "LicenseType")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.LicenseType.LicenseType);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.LicenseType.LicenseType);
                    }
                }
                else if (request.OrderBy == "LicensePriority")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.LicensePriority.PriorityId);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.LicensePriority.PriorityId);
                    }
                }
                else if (request.OrderBy == "License.ModifiedDate")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.ModifiedDate);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.ModifiedDate);
                    }
                }
                else if (request.OrderBy == "License.CreatedDate")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.CreatedDate);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.CreatedDate);
                    }
                }
                else if (request.OrderBy == "License.SignedDate")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.SignedDate);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.SignedDate);
                    }
                }
                else if (request.OrderBy == "License.ArtistRollup")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.ArtistRollup);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.ArtistRollup);
                    }
                }
                else if (request.OrderBy == "License.LicenseConfigurationRollup")
                {
                    if (!sortOrder)
                    {
                        results = results.OrderBy(l => l.LicenseConfigurationRollup);
                    }
                    else
                    {
                        results = results.OrderByDescending(l => l.LicenseConfigurationRollup);
                    }
                }
                else
                {
                    results = results.DynamicOrderBy(request.OrderBy, sortOrder).AsQueryable();
                }

                response.Total = results.Count();
                results = results.Skip(request.PageNo*request.PageSize).Take(request.PageSize);
                response.Results = results.ToList();
                return response;

            }
        }

        public void UpdateLicense(License license)
        {
            using (var context = new AuthContext())
            {
                context.Entry(license).State = (EntityState) System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<License> GetByIds(List<int> ids)
        {
            using (var context = new AuthContext())
            {
                var licenses = context.Licenses
                    .Include("Contact")
                    .Include("Contact2")
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                    .Include("Licensee")
                    .Include("LicenseMethod")
                    
                    .Where(x => ids.Contains((int) x.LicenseId)).OrderByDescending(x=>x.CreatedDate);


                return licenses.ToList();
            }

        }

        public List<int> GetLicenseProductIds(int licenseid)
        {
            using (var context = new AuthContext())
            {
                var productids = context.LicenseProducts
                    .Where(x => x.LicenseId == licenseid)
                    .Select(x => x.LicenseProductId)
                    .DefaultIfEmpty(0)  // set to zero if no results
                    .ToList();

                return productids;
            }

        }

        //public List<LicenseProduct> GetLicenseProducts(int licenseid)
        //{
        //    using (var context = new AuthContext())
        //    {
        //        var products = context.LicenseProducts
        //            .Where(x => x.LicenseId == licenseid)
        //            .ToList();


        //        foreach (var product in products)
        //        {
        //            var producttitle = context.Products.Where(x => x.product_id == product.ProductId)
        //                .Select(x => x.Title)
        //                .DefaultIfEmpty("")
        //                .FirstOrDefault();
        //            product.title = producttitle;
        //        }

        //        return products;
        //    }

        //}
        public List<int> GetLicenseRecordingsTrackIds(int licenseproductid)
        {
            using (var context = new AuthContext())
            {
                var trackids = context.LicenseProductRecordings
                    .Where(x => x.LicenseProductId == licenseproductid)
                    .Select(x=>x.TrackId)
                    .DefaultIfEmpty(0)  // set to zero if no results
                    .ToList();

                return trackids;
            }

        }

        public License GetLite(int id)
        {
            using (var context = new AuthContext())
            {
                var license = context.Licenses
                    .FirstOrDefault(c => c.LicenseId == id);
                return license;
            }
        }

        public List<SendLicenseContact> GetSendLicenseContacts(int licenseSentId)
        {
            using (var context = new AuthContext())
            {
                var info = context.SendIssueLicenseContacts
                    .Where(x => x.LicenseSentId == licenseSentId && x.Deleted == null).ToList();
                return info;
            }

        }

        public SendLicenseInfo GetSendLicenseInfo(int licenseId)
        {
            using (var context = new AuthContext())
            {
                var info = context.SendIssueLicenses
                    .Where(x => x.LicenseId == licenseId).FirstOrDefault();
                return info;
            }


        }


        public SendLicenseInfo AddSendLicenseInfo(SendLicenseInfo sendLicenseInfo)
        {
            using (var context = new AuthContext())
            {
                context.SendIssueLicenses.Add(sendLicenseInfo);
                context.SaveChanges();
                return sendLicenseInfo;
            }
        }

        public void UpdateSendLicenseInfo(SendLicenseInfo sendLicenseInfo)
        {
            using (var context = new AuthContext())
            {
                context.Entry(sendLicenseInfo).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }

        }

        public void UpdateSendLicenseContact(SendLicenseContact sendLicenseContact)
        {
            using (var context = new AuthContext())
            {
                context.Entry(sendLicenseContact).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }

        }

        public SendLicenseContact AddSendLicenseContact(SendLicenseContact sendLicenseContact)
        {
            using (var context = new AuthContext())
            {
                context.SendIssueLicenseContacts.Add(sendLicenseContact);
                context.SaveChanges();
                return sendLicenseContact;
            }
        }

        public License LicenseNameExists(string licenseName)
        {
            using (var context = new AuthContext())
            {
                return context.Licenses.FirstOrDefault(x => x.LicenseName == licenseName);
            }
        }

        public int CountAllLicenses()
        {
            using (var context = new AuthContext())
            {
                return context.Licenses.Where(l => l.Deleted == null).Count();
            }
        }

        public List<int> GetNextLicenseIds(int from, int pageSize)
        {
            using (var context = new AuthContext())
            {
                return
                    context.Licenses
                    .Where(l => l.Deleted == null)
                    .OrderBy(l => l.LicenseId)
                        .Skip(from)
                        .Take(pageSize)
                        .Select(l => l.LicenseId)
                        .ToList();
            }
        }

        public List<int> GetAllRelatedLicenseIds(int recProductId)  //64817
        {
            using (var context = new AuthContext())
            {
                var productids = context.LicenseProducts
                    .Where(x => x.ProductId == recProductId)
                    .Select(x => x.LicenseProductId)
                    .DefaultIfEmpty(0)  // set to zero if no results
                    .ToList();

                return productids;
            }
        }

    }
}
