using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Threading.Tasks;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.Models.RegistrationModel;
using UMPG.USL.Security;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.Contacts
{
    public class ContactManager : IContactManager
    {

        private readonly IContactRepository _contactRepository;
        private readonly IContactDefaultRepository _contactDefaultRepository;
        private readonly IRegistrationHandler _registrationHandler;
        private readonly IContactEmailRepository _contactEmailRepository;
        //contact related data
        private readonly IAddressRepository _addressRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IContactEmailRepository _emailRepository;

        public ContactManager(IContactRepository contactRepository, 
            IContactDefaultRepository contactDefaultRepository, 
            IContactEmailRepository contactEmailRepository,
            IRegistrationHandler registrationHandler,
            IAddressRepository addressRepository,
            IPhoneRepository phoneRepository,
            IContactEmailRepository emailRepository)
        {
            _contactRepository = contactRepository;
            _contactDefaultRepository = contactDefaultRepository;
            _contactEmailRepository = contactEmailRepository;
            _registrationHandler = registrationHandler;
            _addressRepository = addressRepository;
            _phoneRepository = phoneRepository;
            _emailRepository = emailRepository;
        }

        public List<Contact> GetAll()
        {
            return _contactRepository.GetAll();
        }

        public List<Contact> GetAssignees()
        {
            return _contactRepository.GetAssignees();
        }

        public bool EmailExists(string email, int licenseeId)
        {
            return _contactRepository.EmailExists(email,licenseeId);
        }

        public Contact Get(int id)
        {
            return _contactRepository.Get(id);
        }

        public Contact Add(Contact contact)
        {
            return _contactRepository.Add(contact);
        }

        public List<Contact> Search(string query)
        {
            return _contactRepository.Search(query);
            
        }

        public ContactEmail GetContactEmail(int contactId)
        {
            return _contactEmailRepository.GetContactEmail(contactId);
        }

        public List<Contact> GetContactsWithRole(int roleId)
        {
            return _contactRepository.GetContactsWithRole(roleId);
        }

        public List<LicenseeLabelGroup> GetAllLabelGroups()
        {
            return _contactRepository.GetAlLabelGroups();
        }
        
        public List<LicenseeLabelGroup> GetLabelsForLicensee(int licenseeId)
        {
            
            return _contactRepository.GetLabelsForLicensee(licenseeId).OrderBy(x => x.LicenseeLabelGroupName).ToList();

        }

        public List<LicenseeLabelGroupLink> GetContactsForLicenseeLabel(int licenseeLabelGroupId)
        {

            var contacts = _contactRepository.GetContactsForLicenseeLabel(licenseeLabelGroupId);
            
            //get the full names for the ids
            foreach (var contact in contacts)
            {
                contact.FullName = _contactRepository.Get(contact.ContactId).FullName;
            }

            return contacts.OrderBy(x => x.FullName).ToList();

        }


        public List<Contact> GetContactsForLicensee(int licenseeId)
        {
            return _contactRepository.GetContactsForLicensee(licenseeId).OrderBy(x => x.FullName).ToList();

        }

        public RegistrationResult Register(ContactRegistration contactRegistration)
        {

            /*
            1) check if email already exists in ContactEmail 
            2) Add to Safe
            3) Add to Contact
            4) Add to ContactEmail 
            */

            // 1) check if email already exists in ContactEmail 
            Contact contactExists = _contactEmailRepository.Get(contactRegistration.EmailAddress); //returns contact
            if (contactExists != null) {
                return new RegistrationResult 
                    {
                        Success = false,
                        Contact = contactExists,
                        GlobalErrors = new List<string>{"Email Already Exists"}
                    };
            }

           
            // 2) Add to safe
            var registrationRequest = new ExternalContactRegistration
            {
                Name = string.Format("{0} {1}", contactRegistration.FirstName,
                                     contactRegistration.LastName),
                Email = contactRegistration.EmailAddress,
                OfficeLocationCode = "US2",

                // may need to do a separate call to get company name           
                CompanyName = contactRegistration.CompanyName,
                CompanyAddressLine1 = contactRegistration.CompanyAddress1,
                CompanyAddressLine2 = contactRegistration.CompanyAddress2,
                CompanyCity = contactRegistration.CompanyCity,
                CompanyCountry = contactRegistration.CompanyCountry,
                CompanyPostcode = contactRegistration.CompanyZipCode
            };

            // safe registration request
            string safeId = string.Empty;


            RegistrationResult safeRegistrationResult = _registrationHandler.RegisterExternal(registrationRequest);
            // return if error adding to safe
            if (!safeRegistrationResult.Success)
            {
                return safeRegistrationResult;
            }

            safeId = safeRegistrationResult.UserId;

            // test string safeId = "1234567";

            RegistrationResult registrationResult = new RegistrationResult
            {
                Success = false
            };

            // 3) Add to Contact/ContactDefault/ContactEmail 
            try
            {
                using (var transactionScope = new TransactionScope())
                {

                    // Add Contact
                    var newContact = new Contact
                    {

                    LicenseeId = 1,
                    FirstName = contactRegistration.FirstName,
                    LastName = contactRegistration.LastName,
                    FullName = string.Format("{0} {1}", contactRegistration.FirstName,
                                     contactRegistration.LastName),
                    AlsoKnownAs = string.Empty,
                    IsActive = true,
                    SafeId = safeId
                    //RoleId { get; set; }
                    //LastLoginDate { get; set; }
                    //Note { get; set; }
                    //Role Role { get; set; }
                    };

                    var contact = _contactRepository.Add(newContact);
                    if (contact == null)
                    {
                        registrationResult.GlobalErrors = new List<string> { "Error adding contact" };
                    }
                    else 
                    {
                        // Add ContactDefault
                        var newContactDefault = new ContactDefault
                        {
                            ContactId = contact.ContactId,
                            UserSetting = "{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"licenseSearch\":{\"savedSearches\":[{\"searchTerm\":\"a\",\"advancedSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}},{\"searchTerm\":\"aa\",\"advancedSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}},{\"searchTerm\":\"\",\"advancedSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}},{\"searchTerm\":\"a\",\"advancedSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}}]},\"MyLicenseSearch\":{\"advancedSearch\":null,\"savedSearches\":[{\"searchTerm\":\"a\",\"advancedSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}},{\"searchTerm\":\"\",\"advancedSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}}],\"pageSize\":0},\"ProductSearch\":{\"advancedSearch\":null,\"savedSearches\":[{\"searchTerm\":\"a\",\"advancedSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}},{\"searchTerm\":\"\",\"advancedSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}}],\"pageSize\":0},\"LicenseSearch\":{\"advancedSearch\":null,\"savedSearches\":[{\"searchTerm\":\"a\",\"advancedSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}},{\"searchTerm\":\"\",\"advancedSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}}],\"pageSize\":0},\"SEARCH_PAGE\":{\"defaultOptions\":{\"MyLicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"LicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"ProductSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}},\"savedSearches\":[{\"searchTerm\":\"a\",\"advancedSearchData\":{\"MyLicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"LicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"ProductSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}}},{\"searchTerm\":\"a\",\"advancedSearchData\":{\"MyLicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"LicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"ProductSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}}},{\"searchTerm\":\"\",\"advancedSearchData\":{\"MyLicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"LicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"ProductSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}}},{\"searchTerm\":\"\",\"advancedSearchData\":{\"MyLicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"LicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"ProductSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}}},{\"searchTerm\":\"b\",\"advancedSearchData\":{\"MyLicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"LicenseSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]},\"ProductSearch\":{\"includeRecordingTitle\":false,\"includeProductTitle\":false,\"includeLicenseTitle\":false,\"includeArtist\":false,\"includeWriter\":false,\"includePIPS\":false,\"includeUPC\":false,\"includeClient\":false,\"includeHFALicense\":false,\"Asignees\":[],\"Labels\":[],\"Licensees\":[],\"Publishers\":[],\"RateTypes\":[],\"Dristributors\":[],\"LicesingMethods\":[],\"Configuration\":[],\"SpecialStatus\":[]}}}],\"pageSize\":0}}"
                        };
                        var contactDefault = _contactDefaultRepository.Add(newContactDefault);

                        if (contactDefault == null)
                        {
                            registrationResult.GlobalErrors = new List<string> { "Error adding contact default" };
                        }
                        else
                        {
                            // Add ContactEmail
                            var newContactEmail = new ContactEmail
                            {
                                ContactId = contact.ContactId,
                                EmailAddress = contactRegistration.EmailAddress,
                                IsPrimary = true,
                                CreatedDate = DateTime.Now.ToUniversalTime()
                            };

                            var contactEmail = _contactEmailRepository.Add(newContactEmail);
                            if (contactEmail == null)
                            {
                                //error
                                registrationResult.GlobalErrors = new List<string> { "Error adding contact email" };
                            }
                            else
                            {
                                transactionScope.Complete();
                                registrationResult.Success = true;
                                registrationResult.UserId = safeId;
                                registrationResult.Contact = contact;
                                registrationResult.FieldErrors= new Dictionary<string, List<string>>();
                                registrationResult.GlobalErrors = new List<string>();
      
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                registrationResult.FieldErrors = new Dictionary<string, List<string>>();
                registrationResult.GlobalErrors = new List<string> { e.Message };

            }

            return registrationResult;
        }

        public Contact EditContact(Contact contact)
        {
            //because is  1-* in db for phone address and email address tables from contact
            //i update only the firts if it exists
            var lcontact = _contactRepository.Get(contact.ContactId);
            lcontact.FirstName = contact.FirstName;
            lcontact.LastName = contact.LastName;
            lcontact.FullName = contact.FirstName + " " + contact.LastName;
            lcontact.ModifiedDate = DateTime.Now;
            lcontact.ModifiedBy = contact.ModifiedBy;
            if (contact.Phone.Count>0)
            {
                var lphone = lcontact.Phone.FirstOrDefault();
                if (lphone != null)
                {
                    var firstOrDefault = contact.Phone.FirstOrDefault();
                    if (firstOrDefault != null)
                        lphone.PhoneNumber = firstOrDefault.PhoneNumber;
                    _phoneRepository.Update(lphone);
                }
            }
            if (contact.Address.Count > 0)
            {
                var lAddress = lcontact.Address.FirstOrDefault();
                if (lAddress != null)
                {
                    var firstOrDefault = contact.Address.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        lAddress.Address1 = firstOrDefault.Address1;
                        lAddress.Address2 = firstOrDefault.Address2;
                        lAddress.City = firstOrDefault.City;
                        lAddress.State = firstOrDefault.State;
                        lAddress.Country = firstOrDefault.Country;
                        lAddress.PostalCode = firstOrDefault.PostalCode;
                    }
                    _addressRepository.Update(lAddress);
                }
            }
            if (contact.Email.Count > 0)
            {
                var lEmail = lcontact.Email.FirstOrDefault();
                if (lEmail != null)
                {
                    var firstOrDefault = contact.Email.FirstOrDefault();
                    if (firstOrDefault != null)
                        lEmail.EmailAddress = firstOrDefault.EmailAddress;
                    _emailRepository.Update(lEmail);
                }
            }
            return _contactRepository.EditContact(lcontact);
        }

        public LicenseeLabelGroup AddLabelGroup(LicenseeLabelGroup labelGroup)
        {
            labelGroup.CreatedDate = DateTime.Now;
            return _contactRepository.AddLabelGroup(labelGroup);
        }

        public LicenseeLabelGroup EditLabelGroup(LicenseeLabelGroup labelGroup)
        {
            labelGroup.ModifiedDate = DateTime.Now;
            return _contactRepository.EditLabelGroup(labelGroup);
        }
        public LicenseeLabelGroup DeleteLabelGroup(LicenseeLabelGroup labelGroup)
        {
            labelGroup.ModifiedDate = DateTime.Now;
            labelGroup.Deleted = DateTime.Now;
            return _contactRepository.EditLabelGroup(labelGroup);
        }

        public Contact AddLicenseeContactAndLink(AddLicenseeContactAndLinqRequest request)
        {
            var newContact = new Contact
                {
                    FirstName = request.Contact.FirstName,
                    LastName = request.Contact.LastName,
                    FullName = request.Contact.FullName
                };

                newContact.IsActive = true;
                newContact.RoleId = 0;
                newContact.CreatedDate = DateTime.Now;
                newContact.CreatedBy = request.CreatedBy;
                newContact.LicenseeId = request.LicenseeId;
                var createdContact = _contactRepository.Add(newContact);
                //add phone
                var phone = new Phone();
                phone.ContactId = createdContact.ContactId;
                var firstOrDefault = request.Contact.Phone.FirstOrDefault();
                if (firstOrDefault != null)
                    phone.PhoneNumber = firstOrDefault.PhoneNumber;
                phone.CreatedDate = DateTime.Now;
                phone.CreatedBy = request.CreatedBy;
                phone.PhoneTypeId = 1;
                var addphone = _phoneRepository.Add(phone);
                //add email
                var email = new ContactEmail();
                email.ContactId = createdContact.ContactId;
                var contactEmail = request.Contact.Email.FirstOrDefault();
                if (contactEmail != null)
                    email.EmailAddress = contactEmail.EmailAddress;
                email.CreatedDate = DateTime.Now;
                email.CreatedBy = request.CreatedBy;
                var addEmail = _emailRepository.Add(email);
                //address
                var address = new Address();
                address.ContactId = createdContact.ContactId;
                var orDefault = request.Contact.Address.FirstOrDefault();
                if (orDefault != null)
                {
                    address.Address1 = orDefault.Address1;
                    address.Address2 = orDefault.Address2;
                    address.City = orDefault.City;
                    address.State = orDefault.State;
                    address.Country = orDefault.Country;
                    address.PostalCode = orDefault.PostalCode;
                }
                address.CreatedDate = DateTime.Now;
                address.CreatedBy = request.CreatedBy;
                address.AddressTypeId = 1;
                var addAddress = _addressRepository.Add(address);
                var lcontact = _contactRepository.Get(newContact.ContactId);
                return lcontact;
        }
        
        public Contact AddContactAndLink(AddContactAndLinqRequest request)
        {
            var returnedContact = new Contact();
            if (request.IsAddExisting)
            {
                returnedContact = _contactRepository.Get(request.Contact.ContactId);
                var link = new LicenseeLabelGroupLink
                {
                    ContactId = request.Contact.ContactId,
                    LicenseeLabelGroupId = request.LabelGroupId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = request.CreatedBy
                };
                //need to revisit
                _contactRepository.AddLabelGroupLink(link);
            }
            else
            {
                var newContact = new Contact
                {
                    FirstName = request.Contact.FirstName,
                    LastName = request.Contact.LastName,
                    FullName = request.Contact.FullName
                };

                newContact.IsActive = true;
                newContact.RoleId = 0;
                newContact.CreatedDate = DateTime.Now;
                newContact.CreatedBy = request.CreatedBy;
                newContact.LicenseeId = request.LicenseeId;
                var createdContact = _contactRepository.Add(newContact);
                //add phone
                var phone = new Phone();
                phone.ContactId = createdContact.ContactId;
                var firstOrDefault = request.Contact.Phone.FirstOrDefault();
                if (firstOrDefault != null)
                    phone.PhoneNumber = firstOrDefault.PhoneNumber;
                phone.CreatedDate = DateTime.Now;
                phone.CreatedBy = request.CreatedBy;
                phone.PhoneTypeId = 1;
                var addphone = _phoneRepository.Add(phone);
                //add email
                var email = new ContactEmail();
                email.ContactId = createdContact.ContactId;
                var contactEmail = request.Contact.Email.FirstOrDefault();
                if (contactEmail != null)
                    email.EmailAddress = contactEmail.EmailAddress;
                email.CreatedDate = DateTime.Now;
                email.CreatedBy = request.CreatedBy;
                var addEmail= _emailRepository.Add(email);
                //address
                var address = new Address();
                address.ContactId = createdContact.ContactId;
                var orDefault = request.Contact.Address.FirstOrDefault();
                if (orDefault != null)
                {
                    address.Address1 = orDefault.Address1;
                    address.Address2 = orDefault.Address2;
                    address.City = orDefault.City;
                    address.State = orDefault.State;
                    address.Country = orDefault.Country;
                    address.PostalCode = orDefault.PostalCode;
                }
                address.CreatedDate = DateTime.Now;
                address.CreatedBy = request.CreatedBy;
                address.AddressTypeId = 1;
               var addAddress= _addressRepository.Add(address);
                //link
               var newlink = new LicenseeLabelGroupLink
               {
                   ContactId = createdContact.ContactId,
                   LicenseeLabelGroupId = request.LabelGroupId,
                   CreatedDate = DateTime.Now,
                   CreatedBy = request.CreatedBy
               };

               //need to revisit
               _contactRepository.AddLabelGroupLink(newlink);

               returnedContact = _contactRepository.Get(createdContact.ContactId);
            }
            return returnedContact;
        }

        public bool DeleteContactandLink(DeleteContactRequest contact)
        {
            var contactToDelete = _contactRepository.Get(contact.ContactId);
            var linksTodelete = _contactRepository.GetLinksForContact(contact.ContactId);
            foreach (var link in linksTodelete)
            {
                link.ModifiedBy = contact.ModifiedBy;
                link.Deleted = DateTime.Now;
                _contactRepository.EditLabelGroupLink(link);
            }
            contactToDelete.Deleted = DateTime.Now;
            contactToDelete.ModifiedBy = contact.ModifiedBy;
            _contactRepository.EditContact(contactToDelete);
            return true;
        }

        public bool DeleteLicenseeContactAndLink(DeleteContactRequest contact)
        {
            var contactToDelete = _contactRepository.Get(contact.ContactId);
            //var linksTodelete = _contactRepository.GetLicenseeLinksForContact(contact.ContactId);
            //foreach (var link in linksTodelete)
            //{
            //    link.ModifiedBy = contact.ModifiedBy;
            //    link.Deleted = DateTime.Now;
            //    _contactRepository.EditLicenseeContactLink(link);
            //}
            contactToDelete.Deleted = DateTime.Now;
            contactToDelete.ModifiedBy = contact.ModifiedBy;
            _contactRepository.EditContact(contactToDelete);
            return true;
        }

        public bool DeleteContactFromLabelGroup(DeleteContactFromLabelGroupRequest request)
        {
            var relatedLinks = _contactRepository.GetLinksForContact(request.ContactId);
            var linkToDelete = relatedLinks.FirstOrDefault(c => c.LicenseeLabelGroupId == request.LicenseeLabelGroupId);
            if (linkToDelete != null)
            {
                linkToDelete.ModifiedBy = request.ModifiedBy;
                linkToDelete.Deleted = DateTime.Now;
            }
            _contactRepository.EditLabelGroupLink(linkToDelete);
            return true;
        } 
    }
}
