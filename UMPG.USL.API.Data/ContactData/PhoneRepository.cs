using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Data.ContactData
{
    public class PhoneRepository : IPhoneRepository
    {

        public int Add(Phone phone)
        {
            using (var context = new AuthContext())
            {
                context.Phones.Add(phone);
                context.SaveChanges();

                return phone.PhoneId;
            }
        }

        public Phone Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Phones.FirstOrDefault(c => c.PhoneId == id);
            }
        }

        public List<Phone> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.Phones.ToList();
            }
        }

        public List<Phone> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var Phones = context.Phones.Where(p => p.PhoneNumber.ToString() == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return Phones.Where(p => p.PhoneNumber.ToString().ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return Phones.ToList();
                }
            }
        }

        public void Update(Phone phone)
        {
            using (var context = new AuthContext())
            {
                context.Entry(phone).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}
