
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.Security;

namespace UMPG.USL.API.Data.ContactData
{
    public class RoleRepository : IRoleRepository
    {
        public Role GetRoleForContact(int contactId)
        {
            using (var context = new AuthContext())
            {
                var contact = context.Contacts
                    .Include("Role")
                    .Include("Role.Actions")
                    .FirstOrDefault(c => c.ContactId == contactId);
                
                if(contact != null)
                {
                    return contact.Role;
                }
            }

            return null;
        }

        public Role GetRole(int rollNumber)
        {
            using (var context = new AuthContext())
            {
                return context.Roles
                    .Include("Actions")
                    .First(r => r.Level == rollNumber);
            }
        }

        public List<Role> GetAllRoles(bool includeContextsAndActions)
        {
            using (var context = new AuthContext())
            {
                if (includeContextsAndActions)
                {
                    return context.Roles
                        .Include("Actions")
                        .ToList();
                }

                return context.Roles.ToList();
            }
        }

        public List<Role> GetRoles(List<int> roleNumbers, bool includeContextsAndActions)
        {
            using (var context = new AuthContext())
            {
                if (includeContextsAndActions)
                    context.Roles.Include("Action");

                return context.Roles
                    .Where( r => roleNumbers.Contains(r.Level) )
                    .ToList();
            }
        }        
    }
}