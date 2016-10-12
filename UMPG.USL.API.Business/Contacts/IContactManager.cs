using System.Collections.Generic;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.RegistrationModel;

namespace UMPG.USL.API.Business.Contacts
{
    public interface IContactManager
    {
        List<Contact> GetAll();

        Contact Add(Contact contact);

        bool DeleteContactFromLabelGroup(DeleteContactFromLabelGroupRequest request);

        Contact Get(int id);

        List<Contact> Search(string query);

        RegistrationResult Register(ContactRegistration contactRegistration);

        List<Contact> GetContactsForLicensee(int licenseeId);

        List<LicenseeLabelGroup> GetLabelsForLicensee(int licenseeId);

        List<LicenseeLabelGroupLink> GetContactsForLicenseeLabel(int licenseeLabelGroupId);

        List<Contact> GetAssignees();

        bool EmailExists(string email, int licenseeId);

        ContactEmail GetContactEmail(int contactId);

        List<Contact> GetContactsWithRole(int roleId);

        List<LicenseeLabelGroup> GetAllLabelGroups();

        Contact EditContact(Contact contact);

        LicenseeLabelGroup AddLabelGroup(LicenseeLabelGroup labelGroup);

        LicenseeLabelGroup EditLabelGroup(LicenseeLabelGroup labelGroup);

        LicenseeLabelGroup DeleteLabelGroup(LicenseeLabelGroup labelGroup);

        Contact AddContactAndLink(AddContactAndLinqRequest request);

        Contact AddLicenseeContactAndLink(AddLicenseeContactAndLinqRequest request);

        bool DeleteContactandLink(DeleteContactRequest contact);

        bool DeleteLicenseeContactAndLink(DeleteContactRequest contact);
    }
}