using System.Collections.Generic;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.ContactData
{
    public interface IContactRepository
    {
        Contact Add(Contact contact);

        Contact Get(int Id);

        Contact Get(string Id);

        List<Contact> GetAll();

        List<Contact> Search(string query);
        List<Contact> GetContactsForLicensee(int licenseeId);

        List<Contact> GetContactsWithRole(int roleId);
        bool EmailExists(string email,int licenseeId);
        List<LicenseeLabelGroup> GetLabelsForLicensee(int licenseeId);
        List<LicenseeLabelGroupLink> GetContactsForLicenseeLabel(int licenseeLabelGroupId);

        List<Contact> GetAssignees();

        List<LicenseeLabelGroup> GetAlLabelGroups();

        Contact EditContact(Contact contact);

        LicenseeLabelGroup AddLabelGroup(LicenseeLabelGroup labelGroup);
        LicenseeLabelGroup EditLabelGroup(LicenseeLabelGroup labelGroup);
        void AddLabelGroupLink(LicenseeLabelGroupLink labelGroup);
        List<LicenseeLabelGroupLink> GetLinksForContact(int contactId);
        LicenseeLabelGroupLink EditLabelGroupLink(LicenseeLabelGroupLink labelGroupLink);
    }
}
