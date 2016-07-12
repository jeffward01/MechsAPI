using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.Models.Enums;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Licenses
{

    using UMPG.USL.Models.LicenseGenerate;

    public class LicenseManager : ILicenseManager
    {

        private readonly ILicenseRepository _licenseRepository;
        private readonly ILicenseProductRepository _licenseProductRepository;
        private readonly ILicenseNoteRepository _licenseNoteRepository;
        private readonly ILicenseSequenceRepository _licenseSequenceRepository;
        private readonly ISearchProvider _solrSearchProvider;
        private readonly ILicensePRWriterRateRepository _licensePRWriterRateRepository;
        private readonly ILicenseProductRecordingRepository _licenseProductRecordingRepository;
        private readonly ILicensePRWriterRepository _licensePRWriterRepository;
        private readonly ILicensePRWriterRateStatusRepository _licensePRWriterRateStatusRepository;
        private readonly ILicenseUploadPreviewLicenseRepository _licenseUploadPreviewLicenseRepository;
        private readonly IRecs _recs;
        private readonly IRecsDataProvider _recsProvider;

        public LicenseManager(
            ILicenseRepository licenseRepository,
            ILicenseProductRepository licenseProductRepository,
            ILicenseNoteRepository licenseNoteRepository,
            ILicenseSequenceRepository licenseSequenceRepository,
            ISearchProvider solrSearchProvider,
            ILicensePRWriterRateRepository licensePRWriterRateRepository,
            ILicenseProductRecordingRepository licenseProductRecordingRepository,
            ILicensePRWriterRepository licensePRWriterRepository,
            ILicensePRWriterRateStatusRepository licensePRWriterRateStatusRepository,
            ILicenseUploadPreviewLicenseRepository licenseUploadPreviewLicenseRepository,
            IRecs recs,
            IRecsDataProvider recsProvider
            )
        {
            _licenseRepository = licenseRepository;
            _licenseProductRepository = licenseProductRepository;
            _licenseNoteRepository = licenseNoteRepository;
            _licenseSequenceRepository = licenseSequenceRepository;
            _solrSearchProvider = solrSearchProvider;
            _licensePRWriterRateRepository = licensePRWriterRateRepository;
            _licenseProductRecordingRepository = licenseProductRecordingRepository;
            _licensePRWriterRepository = licensePRWriterRepository;
            _licensePRWriterRateStatusRepository = licensePRWriterRateStatusRepository;
            _licenseUploadPreviewLicenseRepository = licenseUploadPreviewLicenseRepository;
            _recs = recs;
            _recsProvider = recsProvider;
        }

        public License Get(int id)
        {
            var license = _licenseRepository.Get(id);



            // Get LicenseProductIds
            var licenseProducts = _licenseProductRepository.GetLicenseProducts(license.LicenseId).OrderBy(x => x.ScheduleId);

            //// need label info - unfortunate another hit to Recs for just this name
            //// moved this to get products
            //foreach (var licprodid in licenseProducts)
            //{

            //    var test2 = _recs.RetrieveTracks(licprodid.ProductId);
            //    foreach (var recording in test2)
            //    {
            //        if (recording.Track.ClaimException)
            //        {
            //            license.ClaimException = "CLAIM EXCEPTION";
            //            break;

            //        };
            //    };

            //};

            license.ProductsNo = licenseProducts.Count();


            var licenseProductIds = licenseProducts
                .Select(x => x.LicenseProductId)
                .DefaultIfEmpty(0)
                .ToList();

            // Get LicenseRecordingIds
            var licenseRecordingIds =
                _licenseProductRecordingRepository.GetLicenseProductRecordingsFromList(licenseProductIds)
                    .Select(x => x.LicenseRecordingId)
                    .DefaultIfEmpty(0)
                    .ToList();

            // Get LicenseWriterIds
            var licenseWriterIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(licenseRecordingIds)
                .ToList();

            // Get LicenseWriterRateIds
            var licenseWriterRateIds = _licensePRWriterRateRepository.GetLicenseRecordingWriterRateIds(licenseWriterIds)
                .Select(x => x.LicenseWriterRateId)
                .ToList();
            //populate key value with statuses and their occurances
            license.StatusesRollup = GetStatusRollup(licenseWriterRateIds);
            var licenseWriterRateIdsWithHolds = _licensePRWriterRateStatusRepository.GetLicenseWriterRatesIdsWithStatus(licenseWriterRateIds)
                .ToList();

            if (licenseWriterRateIdsWithHolds.Count > 0)
            {
                var licenseWriterRateWithHolds = _licensePRWriterRateStatusRepository.GetLicenseWriterRateStatus(licenseWriterRateIdsWithHolds);

                // note: Groupby was not working properly, decided to just enumerate
                var ids = new List<int>();
                var first = true;

                foreach (var licenseWriterRateWithHold in licenseWriterRateWithHolds.OrderBy(x => x.SpecialStatusId).Distinct())
                {
                    if (first)
                    {
                        ids.Add(licenseWriterRateWithHold.SpecialStatusId);
                        first = false;
                    }
                    else
                    {
                        if (ids.Contains(licenseWriterRateWithHold.SpecialStatusId))
                        {
                            //Remove from statusholdlist
                            licenseWriterRateWithHolds.RemoveAll(x => x.LicenseWriterRateStatusId == licenseWriterRateWithHold.LicenseWriterRateStatusId);
                        }
                        else
                        {
                            ids.Add(licenseWriterRateWithHold.SpecialStatusId);
                        }
                    }

                };
                // now add the special status records to license model
                license.LicenseSpecialStatusList = licenseWriterRateWithHolds;  //populates with filtered list
            }



            return license;
        }

        public List<License> GetInboxLicenses(int assigneeId)
        {
            var licenses = _licenseRepository.GetInboxLicenses(assigneeId);
            foreach (var license in licenses)
            {
                license.ProductsNo = _licenseProductRepository.GetProductsNo(license.LicenseId);
            }
            return licenses;
        }

        public List<License> GetAll()
        {
            return _licenseRepository.GetAll();
        }

        public License Add(License license)
        {
            if (_licenseRepository.LicenseNameExists(license.LicenseName) != null)
            {
                return new License() { LicenseId = -1 };
            }
            var newLicense = new License
            {
                LicenseName = license.LicenseName,
                AssignedToId = license.AssignedToId,
                LicenseStatusId = (int)LicenseStatus.Verifying,
                LicenseMethodId = license.LicenseMethodId,
                LicenseTypeId = license.LicenseTypeId,
                PriorityId = license.PriorityId,
                LicenseeId = license.LicenseeId,
                //need to be changed
                CountryId = 1,
                CreatedDate = license.CreatedDate,
                ModifiedDate = license.ModifiedDate,
                //need to be changed when login is finished
                ModifiedBy = license.AssignedToId,
                CreatedBy = license.AssignedToId,
                ContactId = license.ContactId,
                EffectiveDate = license.EffectiveDate,
                ReceivedDate = license.ReceivedDate,
                SignedDate = license.SignedDate,
                LicenseNumber = _licenseSequenceRepository.Get().LicenseNumber.ToString()
            };
            if (license.LicenseeLabelGroup.LicenseeLabelGroupId > 0)
            {
                newLicense.LicenseeLabelGroupId = license.LicenseeLabelGroup.LicenseeLabelGroupId;
            };

            var returnLicense = _licenseRepository.Add(newLicense);
            return _licenseRepository.Get(returnLicense.LicenseId);
        }

        public List<License> Search(string query)
        {
            return _licenseRepository.Search(query);

        }

        public PagedResponse<License> PagedSearch(LicenseRequest request)
        {
            //var response = _licenseRepository.PagedSearch(request);
            //foreach (var license in response.Results)
            //{
            //    license.ProductsNo = _licenseProductRepository.GetProductsNo(license.LicenseId);
            //}
            return _solrSearchProvider.SearchLicenses(request);
            // return response;

        }

        public bool UpdateLicense(UpdateLicenseAssigneeRequest request)
        {

            foreach (var licenseId in request.LicenseIds)
            {
                var license = _licenseRepository.Get(licenseId);
                license.Contact = null;
                license.LicensePriority = null;
                if (request.PriorityId == -1 && license.AssignedToId == request.NewAssigneeId)
                {
                    license.AssignedToId = request.NewAssigneeId;
                }
                else
                {
                    license.PriorityId = request.PriorityId;
                    if (request.NewAssigneeId != 0 && license.AssignedToId != request.NewAssigneeId)
                    {
                        //setting up LicensePriorityId to Inbox;
                        var licensePriorityInboxId = 4;
                        license.PriorityId = licensePriorityInboxId;

                        license.AssignedToId = request.NewAssigneeId;
                    }

                }
                license.ModifiedBy = request.ModifiedBy;
                _licenseRepository.UpdateLicense(license);
                if (!string.IsNullOrEmpty(request.Note))
                {
                    var licenseNote = new LicenseNote
                    {
                        licenseId = licenseId,
                        NoteTypeId = (int)NoteTypes.Internal,
                        CreatedDate = DateTime.Now,
                        //to be modified when we have login
                        CreatedBy = request.CreatedBy,
                        Note = request.Note
                    };
                    _licenseNoteRepository.Add(licenseNote);
                }

            }
            return true;
        }


        public int UploadGeneratedLicensePreview(GenerateLicensePreviewRequest data)
        {
            int generateLicenseQueueId = -1;

            GenerateLicenseQueue license = new GenerateLicenseQueue();
            if (data.FromEmail == null)
            {
                data.FromEmail = "test@test.com";
            }
            license.FromEmail = data.FromEmail;
            license.LicenseId = data.licenseId;
            license.ToEmail = data.ToEmail;
            license.Subject = data.Subject;
            license.Message = data.Content;
            license.CreatedDate = DateTime.Now;

            license.GenerateLicenseStatusId = (int)GenerateLicenseStatus.Pending;
            generateLicenseQueueId = _licenseUploadPreviewLicenseRepository.AddOnLicenseQueue(license);

            GenerateLicenseAttachment licenseAttachment = new GenerateLicenseAttachment();

            licenseAttachment.GenerateLicenseQueueId = generateLicenseQueueId;
            licenseAttachment.FileName = data.fileName;
            licenseAttachment.FilePath = data.Directory;
            licenseAttachment.CreatedDate = DateTime.Now;
            _licenseUploadPreviewLicenseRepository.AddOnLicenseAttachment(licenseAttachment);

            return generateLicenseQueueId;
        }

        public List<License> GetLicensesForProduct(int productId)
        {
            var ids = _licenseProductRepository.GetLicenseIds(productId);
            return _licenseRepository.GetByIds(ids);
        }

        public License EditLicense(License license)
        {
            var localLicense = _licenseRepository.GetLite(license.LicenseId);
            var licenseExists = _licenseRepository.LicenseNameExists(license.LicenseName);
            if (licenseExists != null && licenseExists.LicenseId != localLicense.LicenseId)
            {
                return new License() { LicenseId = -1 };
            }
            localLicense.AssignedToId = license.AssignedToId;
            localLicense.PriorityId = license.PriorityId;
            localLicense.LicenseName = license.LicenseName;
            localLicense.LicenseMethodId = license.LicenseMethodId;
            localLicense.LicenseStatusId = license.LicenseStatusId;
            localLicense.LicenseTypeId = license.LicenseTypeId;
            localLicense.LicenseeId = license.LicenseeId;

            localLicense.ModifiedBy = license.ModifiedBy;  // passed in by user

            localLicense.ModifiedDate = license.ModifiedDate;
            localLicense.EffectiveDate = license.EffectiveDate;
            localLicense.SignedDate = license.SignedDate;
            localLicense.ReceivedDate = license.ReceivedDate;
            localLicense.ContactId = license.ContactId;

            if (license.LicenseeLabelGroup.LicenseeLabelGroupId > 0)
            {
                localLicense.LicenseeLabelGroupId = license.LicenseeLabelGroup.LicenseeLabelGroupId;
            };


            _licenseRepository.UpdateLicense(localLicense);
            return localLicense;
        }

        public bool EditStatus(License license, bool isAutomaticProcess, DateTime? automaticSignedDate)
        {
            var result = false;
            var localLicense = _licenseRepository.GetLite(license.LicenseId);
            if (license != null)
            {
                localLicense.LicenseStatusId = license.LicenseStatusId;
                if (license.LicenseStatusId == 6)
                {
                    if (localLicense.EffectiveDate == null)  // don't overwrite if populated
                    {
                        localLicense.EffectiveDate = DateTime.Now;
                    }
                    // Get LicenseProductIds
                    var licenseProducts = _licenseProductRepository.GetLicenseProducts(license.LicenseId);
                    foreach (var licenseProduct in licenseProducts)
                    {
                        var recordings = _licenseProductRecordingRepository.GetLicenseProductRecordingsBrief(licenseProduct.LicenseProductId);
                        var recsTracks = _recsProvider.RetrieveProductRecordings(licenseProduct.ProductId);
                        foreach (var worksRecording in recsTracks)
                        {
                            foreach (var recording in recordings)
                            {
                                if (recording.TrackId == worksRecording.Track.Id)
                                {
                                    // get the licenserecording object
                                    recording.WorkCode = worksRecording.Track.Copyrights.FirstOrDefault().WorkCode;
                                    //
                                    // for writer % roll-ups at recording level
                                    //
                                    var workWriters = _recsProvider.RetrieveTrackWriters(recording.WorkCode);
                                    var localWriters =
                                        _licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(
                                            recording.LicenseRecordingId);

                                    foreach (var workWriter in workWriters)
                                    {
                                        foreach (var writer in localWriters)
                                        {
                                            // Here we have to check if there is a Split override or a CE override at writer level
                                            // if not, then use Recs contribution value
                                            if (workWriter.CaeNumber == writer.CAECode)
                                            {
                                                if (writer.SplitOverride >= 0)  // has a split override
                                                {
                                                    writer.ExecutedSplit = writer.SplitOverride;
                                                    writer.ModifiedDate = DateTime.Now;
                                                    writer.ModifiedBy = license.ModifiedBy;
                                                    _licensePRWriterRepository.Update(writer);
                                                }
                                                else
                                                {
                                                    if (writer.ClaimExceptionOverride > 0)  // has a claim exception override
                                                    {
                                                        writer.ExecutedSplit = writer.ClaimExceptionOverride;
                                                        writer.ModifiedDate = DateTime.Now;
                                                        writer.ModifiedBy = license.ModifiedBy;
                                                        _licensePRWriterRepository.Update(writer);
                                                    }
                                                    else
                                                    {       // take split from recs because you do not have either of the two above
                                                        writer.ExecutedSplit = (decimal)workWriter.Contribution;
                                                        writer.ModifiedDate = DateTime.Now;
                                                        writer.ModifiedBy = license.ModifiedBy;
                                                        _licensePRWriterRepository.Update(writer);
                                                    }
                                                }

                                            }

                                        }
                                    }

                                }
                            }

                        }
                    }

                }
                // condition for Execute License
                // 1- Update LicenseWriterRate (LicensedDate) with current date for writers with no licenseWriterRateStatus attached
                // this routine checks
                if (license.LicenseStatusId == 5 || license.LicenseStatusId == 7)
                {
                    //Update all LicenseWriterRate | LicenseDates for writers that do not have a status attached.
                    //And on the same LicenseWriter, stamp the ExecutedSplit with the Recs Contribution value.
                    // Get LicenseProductIds
                    var licenseProducts = _licenseProductRepository.GetLicenseProducts(license.LicenseId);
                    var licenseProductIds = licenseProducts
                           .Select(x => x.LicenseProductId)
                           .DefaultIfEmpty(0)
                           .ToList();

                    // Get LicenseRecordingIds
                    var licenseRecordingIds =
                        _licenseProductRecordingRepository.GetLicenseProductRecordingsFromList(licenseProductIds)
                            .Select(x => x.LicenseRecordingId)
                            .DefaultIfEmpty(0)
                            .ToList();

                    // Get LicenseWriterIds
                    var licenseWriterIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(licenseRecordingIds)
                        .ToList();

                    // Get LicenseWriterRateIds
                    var licenseWriterRateIds = _licensePRWriterRateRepository.GetLicenseRecordingWriterRateIds(licenseWriterIds)
                        .Select(x => x.LicenseWriterRateId)
                        .ToList();

                    var ratesToUpdate =
                        _licensePRWriterRateRepository.GetRatesByRatesIds(licenseWriterRateIds);
                    //Now update all of these LicenseWriterRates with LicenseDate and executed split

                    localLicense.SignedDate = license.SignedDate;

                    foreach (var rate in ratesToUpdate)
                    {
                        if (rate.WriterRateInclude)  //Note this is only a small check for date, should be checked everywhere before executing. th
                        {
                            if (isAutomaticProcess)
                            {
                                rate.licenseDate = automaticSignedDate;
                                localLicense.SignedDate = automaticSignedDate;
                            }
                            else
                            {
                                rate.licenseDate = (license.SignedDate != null) ? license.SignedDate : DateTime.Now;

                                //NOI type 
                                if (license.LicenseTypeId == 2 && !isAutomaticProcess)
                                {
                                    rate.licenseDate = (license.EffectiveDate != null) ? license.EffectiveDate : DateTime.Now;
                                }
                            }

                        }
                        //when license has a License Date, default value

                        rate.ModifiedDate = DateTime.Now;
                        rate.ModifiedBy = license.ModifiedBy;
                        _licensePRWriterRateRepository.Update(rate);
                    }

                    localLicense.EffectiveDate = license.EffectiveDate;
                    localLicense.ReceivedDate = license.ReceivedDate;
                    localLicense.ModifiedDate = DateTime.Now;
                    localLicense.ModifiedBy = license.ModifiedBy;
                    foreach (var licenseProduct in licenseProducts)
                    {
                        var recordings =
                            _licenseProductRecordingRepository.GetLicenseProductRecordingsBrief(
                                licenseProduct.LicenseProductId);
                        var recsTracks = _recsProvider.RetrieveProductRecordings(licenseProduct.ProductId);
                        foreach (var worksRecording in recsTracks)
                        {
                            foreach (var recording in recordings)
                            {
                                if (recording.TrackId == worksRecording.Track.Id)
                                {
                                    // get the licenserecording object
                                    recording.WorkCode = worksRecording.Track.Copyrights.FirstOrDefault().WorkCode;
                                    //
                                    // for writer % roll-ups at recording level
                                    //
                                    var workWriters = _recsProvider.RetrieveTrackWriters(recording.WorkCode);
                                    var localWriters =
                                        _licensePRWriterRepository.GetLicenseProductRecordingWritersBrief(
                                            recording.LicenseRecordingId);

                                    foreach (var workWriter in workWriters)
                                    {
                                        foreach (var writer in localWriters)
                                        {
                                            // Here we have to check if there is a Split override or a CE override at writer level
                                            // if not, then use Recs contribution value
                                            if (workWriter.CaeNumber == writer.CAECode)
                                            {
                                                foreach (var rate in ratesToUpdate)
                                                {
                                                    if (rate.LicenseWriterId == writer.LicenseWriterId)
                                                    {
                                                        if (rate.WriterRateInclude)  //Note this is only a small check for date, should be checked everywhere before executing. th
                                                        {
                                                            writer.isLicensed = true;
                                                            writer.LicensedDate = DateTime.Now;
                                                            writer.ModifiedBy = license.ModifiedBy;
                                                            writer.ModifiedDate = DateTime.Now;

                                                        }
                                                    }
                                                }

                                                if (writer.isLicensed == true)
                                                {

                                                    if (writer.SplitOverride >= 0)
                                                    // has a split override
                                                    {
                                                        writer.ExecutedSplit = writer.SplitOverride;
                                                        writer.ExecutedControlledWriter = true;
                                                        writer.WriterChangeDate = DateTime.Now;
                                                        writer.ModifiedDate = DateTime.Now;
                                                        writer.ModifiedBy = license.ModifiedBy;
                                                        _licensePRWriterRepository.Update(writer);
                                                    }
                                                    else
                                                    {
                                                        if (writer.ClaimExceptionOverride > 0)  // has a claim exception override
                                                        {
                                                            writer.ExecutedSplit = writer.ClaimExceptionOverride;
                                                            writer.ExecutedControlledWriter = true;
                                                            writer.WriterChangeDate = DateTime.Now;
                                                            writer.ModifiedDate = DateTime.Now;
                                                            writer.ModifiedBy = license.ModifiedBy;
                                                            _licensePRWriterRepository.Update(writer);
                                                        }
                                                        else
                                                        {       // take split from recs because you do not have either of the two above
                                                            writer.ExecutedSplit = (decimal)workWriter.Contribution;
                                                            writer.ExecutedControlledWriter = true;
                                                            writer.WriterChangeDate = DateTime.Now;
                                                            writer.ModifiedDate = DateTime.Now;
                                                            writer.ModifiedBy = license.ModifiedBy;
                                                            _licensePRWriterRepository.Update(writer);
                                                        }
                                                    }
                                                }
                                                else
                                                {

                                                    writer.ExecutedSplit = null;
                                                    writer.ExecutedControlledWriter = null;
                                                    writer.WriterChangeDate = DateTime.Now;
                                                    writer.ModifiedDate = DateTime.Now;
                                                    writer.ModifiedBy = license.ModifiedBy;
                                                    _licensePRWriterRepository.Update(writer);

                                                }

                                            }




                                        }
                                    }

                                }
                            }

                        }
                    }
                    if (localLicense.EffectiveDate == null)  // don't overwrite if populated
                    {
                        localLicense.EffectiveDate = DateTime.Now.AddHours(-8);
                    }
                    if (localLicense.SignedDate == null)  // don't overwrite if populated
                    {
                        localLicense.SignedDate = DateTime.Now.AddHours(-8);
                    }
                    if (localLicense.ReceivedDate == null)  // don't overwrite if populated
                    {
                        localLicense.ReceivedDate = DateTime.Now.AddHours(-8);
                    }

                }
                _licenseRepository.UpdateLicense(localLicense);

                result = true;
            }
            return result;
        }


        public bool EditStatusLicenseProcessor(int licenseId, DateTime signedDate)
        {

            //executed 
            var license = _licenseRepository.Get(licenseId);
            license.LicenseStatusId = 5;
            _licenseRepository.UpdateLicense(license);

            EditStatus(license, true, signedDate);


            return true;
        }

        public bool EditLicenseStatusReport(int licenseId)
        {
            var license = _licenseRepository.Get(licenseId);
            license.StatusReport = !license.StatusReport;
            _licenseRepository.UpdateLicense(license);
            return true;
        }

        public SendLicenseInfo GetSendLicenseInfo(int licenseId)
        {

            var sendLicenseInfo = _licenseRepository.GetSendLicenseInfo(licenseId);
            if (sendLicenseInfo != null)
            {
                sendLicenseInfo.SendLicenseContactList = _licenseRepository.GetSendLicenseContacts(sendLicenseInfo.LicenseSentId);
            }
            return sendLicenseInfo;
        }


        public bool UpdateSendLicenseInfo(SendLicenseInfo request)
        {
            try
            {
                var updatedDate = DateTime.Now;
                var updatedBy = 1;


                if (request.LicenseSentId == 0)
                {
                    request.CreatedDate = updatedDate;
                    request.CreatedBy = updatedBy;


                    List<SendLicenseContact> sendLicenseContactList = new List<SendLicenseContact>();
                    foreach (var contact in request.SendLicenseContactList)
                    {
                        var sendLicenseContact = new SendLicenseContact
                        {
                            //LicenseSentId = newSendLicense.LicenseSentId,
                            ContactId = contact.ContactId,
                            EmailAddress = contact.EmailAddress,
                            CreatedBy = updatedBy,
                            CreatedDate = updatedDate
                        };
                        sendLicenseContactList.Add(sendLicenseContact);
                    }
                    var sendLicense = new SendLicenseInfo
                    {
                        LicenseId = request.LicenseId,
                        LicenseeId = request.LicenseeId,
                        LicenseTemplateId = request.LicenseTemplateId,
                        SendLicenseContactList = sendLicenseContactList,
                        CreatedBy = updatedBy,
                        CreatedDate = updatedDate
                    };

                    var newSendLicense = _licenseRepository.AddSendLicenseInfo(sendLicense);

                    return true;
                }
                else
                {

                    request.ModifiedDate = updatedDate;
                    request.ModifiedBy = updatedBy;
                    foreach (var contact in request.SendLicenseContactList)
                    {
                        if (contact.LicenseSentContactId > 0)
                        {
                            contact.ModifiedDate = updatedDate;
                            contact.ModifiedBy = updatedBy;
                            if (contact.Action == "DELETE")
                            {
                                contact.Deleted = updatedDate;
                            }
                            _licenseRepository.UpdateSendLicenseContact(contact);
                        }
                        else
                        {
                            contact.CreatedDate = updatedDate;
                            contact.CreatedBy = updatedBy;
                            contact.LicenseSentId = request.LicenseSentId; // contact.LicenseSentId;
                            var newcontact = _licenseRepository.AddSendLicenseContact(contact);

                        }
                    }

                    _licenseRepository.UpdateSendLicenseInfo(request);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Dictionary<string, int> GetStatusRollup(List<int> writerRatesIds)
        {
            var lReturnlist = new Dictionary<string, int>();
            var statuses = _licensePRWriterRateStatusRepository.GetLicenseWriterRateStatus(writerRatesIds);

            foreach (var status in statuses)
            {

                if (lReturnlist.ContainsKey(status.LU_SpecialStatuses.SpecialStatus.ToLower()))
                {
                    lReturnlist[status.LU_SpecialStatuses.SpecialStatus.ToLower()]++;
                }
                else
                {
                    lReturnlist.Add(status.LU_SpecialStatuses.SpecialStatus.ToLower(), 1);
                }
            }
            return lReturnlist;
        }

    }
}
