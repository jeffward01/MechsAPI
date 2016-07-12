using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Data.ContactData
{
    public class AddressRepository : IAddressRepository
    {

        public int Add(Address address)
        {
            using (var context = new AuthContext())
            {
                context.Addresses.Add(address);
                context.SaveChanges();

                return address.AddressId;
            }
        }

        public Address Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Addresses.FirstOrDefault(c => c.AddressId == id);
            }
        }

        public List<Address> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.Addresses.ToList();
            }
        }

        public List<Address> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var addresses = context.Addresses.Where(p => p.Address1 == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return addresses.Where(p => p.Address1.ToString().ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return addresses.ToList();
                }
            }
        }

        public List<Address> GetAddressesForLicensee(int licenseeId)
        {
            using (var context = new AuthContext())
            {
                return context.Addresses.Where(x=>x.LicenseeId == licenseeId).ToList();
            }
        }

        public void Update(Address address)
        {
            using (var context = new AuthContext())
            {
                context.Entry(address).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
                
            }
        }
    }
}
