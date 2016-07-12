using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class LicenseMethodRepository : ILicenseMethodRepository
    {

        public int Add(LU_LicenseMethod licensemethod)
        {
            using (var context = new AuthContext())
            {
                context.LU_LicenseMethods.Add(licensemethod);
                context.SaveChanges();

                return licensemethod.LicenseMethodId;
            }
        }

        public LU_LicenseMethod Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.LU_LicenseMethods.FirstOrDefault(c => c.LicenseMethodId == id);
            }
        }

        public List<LU_LicenseMethod> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.LU_LicenseMethods.OrderBy(c=> c.LicenseMethod).ToList();
            }
        }

        public List<LU_LicenseMethod> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var LicenseMethods = context.LU_LicenseMethods.Where(c => c.LicenseMethod == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return LicenseMethods.Where(c => c.LicenseMethod.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return LicenseMethods.ToList();
                }
            }
        }

    }
}
