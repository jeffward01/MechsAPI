using System.Collections.Generic;
using UMPG.USL.Models.Security;

namespace UMPG.USL.API.Data.ContactData
{
    public interface IRoleRepository
    {
        Role GetRoleForContact(int contactId);
        Role GetRole(int rollNumber);
        List<Role> GetAllRoles(bool includeContextsAndActions);
        List<Role> GetRoles(List<int> roleNumbers, bool includeContextsAndActions);
    }
}