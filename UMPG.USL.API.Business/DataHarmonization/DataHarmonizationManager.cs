﻿using System;
using NLog;
using System.Collections.Generic;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using License = UMPG.USL.Models.LicenseModel.License;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class DataHarmonizationManager : IDataHarmonizationManager
    {
        private readonly ISnapshotManager _snapshotManager;
        private readonly ISnapshotLicenseManager _snapshotLicenseManager;
        private readonly ILicenseProductRecordingRepository _licenseProductRecordingRepository;
        private readonly ILicensePRWriterRepository _licensePrWriterRepository;
        private readonly ILicensePRWriterRateRepository _licensePrWriterRateRepository;
        private readonly ILicensePRWriterNoteRepository _licensePrWriterNoteRepository;
        private readonly ILicenseProductConfigurationRepository _licenseProductConfigurationRepository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public DataHarmonizationManager(ISnapshotManager snapshotManager, ILicenseProductRecordingRepository licenseProductRecordingRepository, ILicensePRWriterRepository licensePrWriterRepository, ILicensePRWriterRateRepository licensePrWriterRateRepository, ILicensePRWriterNoteRepository licensePrWriterNoteRepository, ISnapshotLicenseManager snapshotLicenseManager, ILicenseProductConfigurationRepository licenseProductConfigurationRepository)
        {
            _licenseProductConfigurationRepository = licenseProductConfigurationRepository;
            _snapshotLicenseManager = snapshotLicenseManager;
            _licensePrWriterNoteRepository = licensePrWriterNoteRepository;
            _licensePrWriterRateRepository = licensePrWriterRateRepository;
            _licensePrWriterRepository = licensePrWriterRepository;
            _licenseProductRecordingRepository = licenseProductRecordingRepository;
            _snapshotManager = snapshotManager;
        }

        public bool DoesSnapshotExist(int licenseId)
        {
            return _snapshotManager.DoesLicenseSnapshotExist(licenseId);
        }

        public bool DoesSnapshotExistAndComplete(int licenseId)
        {
            return _snapshotManager.DoesLicenseSnapshotExistAndComplete(licenseId);
        }

        public bool IsSnapshotInProcess(int licenseId)
        {
            //check if license snapshot exists
            var result = _snapshotManager.DoesLicenseSnapshotExist(licenseId);
            if (result)
            {
                //check if snapshot is not complete
                var complete = _snapshotManager.DoesLicenseSnapshotExistAndComplete(licenseId);
                if (!complete)
                {
                    return true;
                }
            }
            return false;
        }

        public Snapshot_ProductHeader GetSnapshotProductHeaderForLicenseId(int licenseId)
        {
            return _snapshotManager.GetSnapshotProductHeaderByLicenseId(licenseId);
        }

        public bool RemoveLicenseProductFromSnapshot(int licenseId, int productId)
        {
            // get LicenseSnapshot
            var licenseSnapshot = GetLicenseSnapshotFull(licenseId);

            //Delete licenseProduct and chuldren
            //try
           // {
                _snapshotLicenseManager.DeleteLicenseProductAndChildEntities(licenseSnapshot, productId);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    return false;
            //}
            return true;
        }




        public Snapshot_License GetLicenseSnapshot(int licenseId)
        {
            return _snapshotManager.GeLicenseSnapshotByLicenseId(licenseId);
        }

        public Snapshot_License GetLicenseSnapshotFull(int licenseId)
        {
            return _snapshotManager.GetLicenseSnapshotFull(licenseId);
        }

        /*

                foreach (var writer in license.LicenseProductRecordingWriters)
                {
                    List<int> writerIds = new List<int>();
                    writerIds.Add(writer.LicenseWriterId);
                    writer.RateList = _licensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(writerIds, licenseProductConfiguration.Select(x => (int)x.product_configuration_id).ToList());

                    //Populate RateList with UPC codes & Populate  License Name/Id/Number

                    var writerCaeCode = writer.CAECode;
                    foreach (var rate in writer.RateList)
                    {
                        foreach (var config in license.LicenseProductConfigurations)
                        {
                            if (config.product_configuration_id == rate.product_configuration_id)
                            {
                                rate.CaeCode = writerCaeCode;
                                rate.LicenseId = license.LicenseId;
                                rate.LicenseNumber = license.LicenseNumber;
                                rate.LicenseTitle = license.LicenseName;
                                rate.upc = config.upc_code;
                                //This call is slow and we can fill it in JavaScript much faster.
                                //if (rate.RateTypeId != null)
                                //{
                                //    rate.RateType = _rateTypeRepository.Get((int)rate.RateTypeId);
                                //}
                                //if (rate.writersConsentTypeId != null)
                                //{
                                //    rate.WritersConsentType = _writersConsentTypeRepository.Get((int)rate.writersConsentTypeId);
                                //}
                            }
                        }
                    }

                    // add notes here as well
                    writer.WriterNotes = _licensePRWriterNoteRepository.GetLicenseProductRecordingWriterNotes(writerIds);
                }
         */

        public bool TakeLicenseSnapshotLite(License licenseToBeSnapshotted, bool snapshotComplete)
        {
            return _snapshotManager.TakeLicenseSnapshotLite(licenseToBeSnapshotted, snapshotComplete);
        }

        public Snapshot_LicenseProduct TakeLicenseProductSnapshotLite(LicenseProduct licenseProductToBeSnapshotted)
        {
            return _snapshotManager.TakeLicenseProductSnapshotLite(licenseProductToBeSnapshotted);
        }

        public bool TakeLicenseSnapshot(License licenseToBeSnapshotted, List<LicenseProduct> licenseProducts)
        {
            return _snapshotManager.TakeLicenseSnapshot(licenseToBeSnapshotted, licenseProducts);
        }

        public bool DeleteLicenseSnapshot(int licenseSnapshotId)
        {
            return _snapshotManager.DeleteLicenseSnapshot(licenseSnapshotId);
        }

       
        #region oldCastToSnapshotCode

        /*
        private Snapshot_LicenseProduct CastToLicenseProductSnapshot(LicenseProduct licenseProduct)
        {
            var snapshot = new Snapshot_LicenseProduct();

            snapshot.CloneLicenseProductId = licenseProduct.LicenseProductId;
            snapshot.LicenseId = licenseProduct.LicenseId;
            snapshot.ProductHeaderId = (int)licenseProduct.ProductHeader.Id;
            snapshot.ProductHeader = CastToProductHeaderSnapshot(licenseProduct.ProductHeader);
            snapshot.ScheduleId = licenseProduct.ScheduleId;
            snapshot.ProductId = licenseProduct.ProductId;
            snapshot.LicensePRecordingsNo = licenseProduct.LicensePRecordingsNo;
            snapshot.LicenseClaimException = licenseProduct.LicenseClaimException;
            snapshot.TotalLicenseConfigAmount = (int)licenseProduct.TotalLicenseConfigAmount;
            snapshot.title = licenseProduct.title;
            snapshot.PaidQuarter = licenseProduct.PaidQuarter;
            snapshot.RelatedLicensesNo = licenseProduct.RelatedLicensesNo;

          //  if (licenseProduct.ProductConfigurations != null)
          //  {
          //      snapshot.ProductConfigurations = CastToRecsConfigurationsSnapshot(licenseProduct.ProductConfigurations,
          //          (int)snapshot.ProductHeaderId);
          //  }
          //
            snapshot.CreatedDate = licenseProduct.CreatedDate;
            snapshot.CreatedBy = licenseProduct.CreatedBy;
            snapshot.ModifiedDate = licenseProduct.ModifiedDate;
            snapshot.ModifiedBy = licenseProduct.ModifiedBy;
            snapshot.Deleted = licenseProduct.Deleted;
            if (licenseProduct.Recordings != null)
            {
                snapshot.Recordings = CastToSnapshotRecordings(licenseProduct.Recordings, licenseProduct.ProductId,
                    licenseProduct.LicenseProductId);
            }
            return snapshot;
        }

        private List<Snapshot_WorksRecording> CastToSnapshotRecordings(List<WorksRecording> worksRecording,
            int productId, int licenseProductId)
        {
            var snapshotList = new List<Snapshot_WorksRecording>();

            foreach (var rec in worksRecording)
            {
                var snapshot = new Snapshot_WorksRecording();

                snapshot.ProductId = productId;
                snapshot.CloneTrackId = rec.TrackId;
                snapshot.TrackId = rec.TrackId;
                snapshot.LicenseProductId = licenseProductId;

                snapshot.CdIndex = rec.CdIndex;
                snapshot.UmpgPercentageRollup = (int)rec.UmpgPercentageRollup;
                snapshot.CdNumber = rec.CdNumber;
                snapshot.LicensedRollup = (int)rec.LicensedRollup;
                if (rec.LicenseRecording != null)
                {
                    snapshot.LicenseProductRecordingId = rec.LicenseRecording.LicenseRecordingId;
                }
                // snapshot.WorkTrackId = rec.TrackId
                snapshot.Message = rec.Message;
                if (rec.Writers != null)
                {
                    snapshot.Writers = CastToSnapshotWorksWriter(rec.Writers, rec.TrackId);
                }
                if (rec.Track != null)
                {
                    snapshot.Track = CastToSnapshotWorksTrack(rec.Track, rec.TrackId);
                }
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private Snapshot_WorksTrack CastToSnapshotWorksTrack(WorksTrack track, int trakId)
        {
            var snapshot = new Snapshot_WorksTrack();
            snapshot.CloneWorksTrackId = trakId;
            snapshot.Title = track.Title;
            snapshot.ArtistRecsId = (int)track.Artists.id;
            snapshot.WritersNo = track.WritersNo;
            snapshot.ControledWritersNo = track.ControledWritersNo;
            snapshot.Controlled = track.Controlled;
            snapshot.Duration = track.Duration;
            snapshot.Isrc = track.Isrc;
            if (track.Artists != null)
            {
                snapshot.Artist = CastToArtistRecsSnapshot(track.Artists);
            }
            if (track.Copyrights != null)
            {
                snapshot.Copyrights = CastToSnapshotRecsCopyrights(track.Copyrights, trakId);
            }
            return snapshot;
        }

        private List<Snapshot_RecsCopyright> CastToSnapshotRecsCopyrights(List<RecsCopyrights> recsCopyrights,
            int workTrackId)
        {
            var snapshotList = new List<Snapshot_RecsCopyright>();
            foreach (var rec in recsCopyrights)
            {
                var snapshot = new Snapshot_RecsCopyright();
                snapshot.CloneWorksTrackId = workTrackId;
                snapshot.WorkCode = rec.WorkCode;
                snapshot.Title = rec.Title;
                snapshot.PrincipalArtist = rec.PrincipalArtist;
                snapshot.Writers = rec.Writers;
                snapshot.WriteString = rec.WriteString;
                snapshot.MechanicalCollectablePercentage = (int)rec.MechanicalCollectablePercentage;
                snapshot.MechanicalOwnershipPercentage = (int)rec.MechanicalOwnershipPercentage;
                if (rec.Composers != null)
                {
                    snapshot.Composers = CastToSnapshotComposers(rec.Composers, workTrackId);
                }
                if (rec.Samples != null)
                {
                    snapshot.Samples = CastToSnapshotSamples(rec.Samples, workTrackId);
                }
                if (rec.LocalClients != null)
                {
                    snapshot.LocalClients = CastToSnapshotLocalClientCopyrights(rec.LocalClients, workTrackId);
                }
                if (rec.AquisitionLocationCode != null)
                {
                    snapshot.AquisitionLocationCodes = CastToSnapshotAquisitionLocationCode(rec.AquisitionLocationCode,
                        workTrackId);
                }
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private List<Snapshot_Composer> CastToSnapshotComposers(List<WorksWriter> composers, int workTrackId)
        {
            var snapshotList = new List<Snapshot_Composer>();

            foreach (var writer in composers)
            {
                var snapshot = new Snapshot_Composer();
                //cast writer base!
                snapshot.CloneWorksTrackId = workTrackId;
                if (writer.Contribution != null)
                {
                    snapshot.Contribution = (int)writer.Contribution;
                }
                if (writer.OriginalPublishers != null)
                {
                    snapshot.OriginalPublishers = CastToComposerOriginalPublishers(writer.OriginalPublishers,
                        writer.CaeNumber);
                }
                snapshot.Affiliation = CastToComposerAffiliations(writer.Affiliation, writer.CaeNumber);
                if (writer.KnownAs != null)
                {
                    snapshot.KnownAs = CastComposerKnownAs(writer.KnownAs, writer.CaeNumber);
                }
                if (writer.LicenseProductRecordingWriter != null)
                {
                    snapshot.LicenseRecordingId = writer.LicenseProductRecordingWriter.LicenseRecordingId;
                }

                snapshot.ParentSongDuration = writer.ParentSongDuration;
                snapshot.CloneCaeNumber = writer.CaeNumber;
                snapshot.IpCode = writer.IpCode;
                snapshot.FullName = writer.FullName;
                snapshot.CapacityCode = writer.CapacityCode;
                snapshot.Capacity = writer.Capacity;
                snapshot.MechanicalCollectablePercentage = writer.MechanicalCollectablePercentage.ToString();
                snapshot.MechanicalOwnershipPercentage = writer.MechanicalOwnershipPercentage.ToString();

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_ComposerAffiliation> CastToComposerAffiliations(List<Affiliation> affiliations,
            int caeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerAffiliation>();
            foreach (var affiliation in affiliations)
            {
                var snapshot = new Snapshot_ComposerAffiliation();
                snapshot.IncomeGroup = affiliation.IncomeGroup;
                snapshot.CloneWriterCaeNumber = caeNumber;
                snapshot.WriterCaeNumber = caeNumber;
                snapshot.Affiliations = castToComposerAffiliationsBases(affiliation.Affiliations, caeNumber);

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_ComposerAffiliationBase> castToComposerAffiliationsBases(List<AffiliationBase> affiliations,
            int caeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerAffiliationBase>();
            foreach (var affiliation in affiliations)
            {
                var snapshot = new Snapshot_ComposerAffiliationBase();
                snapshot.EndDate = affiliation.EndDate;
                snapshot.StartDate = affiliation.StartDate;
                snapshot.SocietyAcronym = affiliation.SocietyAcronym;
                snapshot.CloneWriterCaeNumber = caeNumber;

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }
        private List<Snapshot_ComposerKnownAs> CastComposerKnownAs(List<string> knownAsList, int caeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerKnownAs>();
            foreach (var knownAs in knownAsList)
            {
                var snapshot = new Snapshot_ComposerKnownAs();
                snapshot.KnownAs = knownAs;
                snapshot.CloneWriterCaeCode = caeNumber;

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_ComposerOriginalPublisher> CastToComposerOriginalPublishers(
            List<OriginalPublisher> originalPublishers, int caeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerOriginalPublisher>();
            foreach (var originalPublisher in originalPublishers)
            {
                var snapshot = new Snapshot_ComposerOriginalPublisher();
                snapshot.CloneCaeNumber = originalPublisher.CaeNumber;
                snapshot.CloneWorksWriterCaeNumber = caeNumber;
                snapshot.IpCode = originalPublisher.IpCode;
                snapshot.FullName = originalPublisher.FullName;
                snapshot.CapacityCode = originalPublisher.CapacityCode;
                snapshot.MechanicalCollectablePercentage =  originalPublisher.MechanicalCollectablePercentage.ToString();
                snapshot.MechanicalOwnershipPercentage = originalPublisher.MechanicalOwnershipPercentage.ToString();
                snapshot.Controlled = originalPublisher.Controlled;
                if (originalPublisher.Affiliation != null)
                {
                    snapshot.Affiliation = CastToComposerOriginalPublisherAffiliations(originalPublisher.Affiliation,
                        caeNumber);
                }
                if (originalPublisher.KnownAs != null)
                {
                    snapshot.KnownAs = CastComposerOriginalPublisherKnownAs(originalPublisher.KnownAs, caeNumber);
                }
                if (originalPublisher.Administrator != null)
                {
                    snapshot.Administrator =
                        CastSnapshotComposerOriginalPublisherAdministrators(originalPublisher.Administrator, caeNumber);
                }

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_ComposerOriginalPublisherAdministrator>
            CastSnapshotComposerOriginalPublisherAdministrators(List<WriterBase> administrators, int caeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerOriginalPublisherAdministrator>();
            foreach (var admin in administrators)
            {
                var snapshot = new Snapshot_ComposerOriginalPublisherAdministrator();
                snapshot.CloneCaeNumber = caeNumber;
                snapshot.IpCode = admin.IpCode;
                snapshot.FullName = admin.FullName;
                snapshot.CapacityCode = admin.CapacityCode;
                snapshot.Capacity = admin.Capacity;
                snapshot.MechanicalOwnershipPercentage = (int) admin.MechanicalOwnershipPercentage;
                snapshot.MechanicalCollectablePercentage = (int) admin.MechanicalCollectablePercentage;
                snapshot.Controlled = admin.Controlled;
                if (admin.Affiliation != null)
                {
                    snapshot.Affiliation = CastSnapshotComposerOriginalPublisherAdminAffiliations(admin.Affiliation,caeNumber);
                }
                if (admin.KnownAs != null)
                {
                    snapshot.KnownAs = CastSnapshotComposerOriginalPublisherAdminKnownAs(admin.KnownAs, caeNumber);
                }
                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_ComposerOriginalPublisherAdminAffiliation>
            CastSnapshotComposerOriginalPublisherAdminAffiliations(List<Affiliation> affiliations, int caeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerOriginalPublisherAdminAffiliation>();
            foreach (var affiliation in affiliations)
            {
                var snapshot = new Snapshot_ComposerOriginalPublisherAdminAffiliation();

                snapshot.CloneWriterCaeNumber = caeNumber;
                snapshot.WriterCaeNumber = caeNumber;
                snapshot.IncomeGroup = affiliation.IncomeGroup;
                snapshot.Affiliations = CastToComposerOriginalPublisherAdminAffiliationBase(affiliation.Affiliations,
                    caeNumber);

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_ComposerOriginalPublisherAdminAffiliationBase>
            CastToComposerOriginalPublisherAdminAffiliationBase(List<AffiliationBase> affiliationBases, int CaeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerOriginalPublisherAdminAffiliationBase>();
            foreach (var affiliationBase in affiliationBases)
            {
                var snapshot = new Snapshot_ComposerOriginalPublisherAdminAffiliationBase();
                snapshot.CloneWriterCaeNumber = CaeNumber;
                snapshot.EndDate = affiliationBase.EndDate;
                snapshot.SocietyAcronym = affiliationBase.SocietyAcronym;
                snapshot.StartDate = affiliationBase.StartDate;

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_ComposerOriginalPublisherAdminKnownAs> CastSnapshotComposerOriginalPublisherAdminKnownAs(
            List<string> knowAsList, int caeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerOriginalPublisherAdminKnownAs>();
            foreach (var knownAs in knowAsList)
            {
                var snapshot = new Snapshot_ComposerOriginalPublisherAdminKnownAs();
                snapshot.KnownAs = knownAs;
                snapshot.CloneWriterCaeCode = caeNumber;

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_ComposerOriginalPublisherKnownAs> CastComposerOriginalPublisherKnownAs(
            List<string> knownAsList, int caeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerOriginalPublisherKnownAs>();

            foreach (var knownAs in knownAsList)
            {
                var snapshot = new Snapshot_ComposerOriginalPublisherKnownAs();
                snapshot.CloneWriterCaeCode = caeNumber;
                snapshot.KnownAs = knownAs;
                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_ComposerOriginalPublisherAffiliation> CastToComposerOriginalPublisherAffiliations(
            List<Affiliation> affiliations, int caeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerOriginalPublisherAffiliation>();
            foreach (var affiliation in affiliations)
            {
                var snapshot = new Snapshot_ComposerOriginalPublisherAffiliation();

                snapshot.CloneWriterCaeNumber = caeNumber;
                snapshot.WriterCaeNumber = caeNumber;
                snapshot.IncomeGroup = affiliation.IncomeGroup;
                snapshot.Affiliations = CastTiSnapshotComposerOriginalPublisherAffiliationBases(
                    affiliation.Affiliations, caeNumber);

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_ComposerOriginalPublisherAffiliationBase>
            CastTiSnapshotComposerOriginalPublisherAffiliationBases(List<AffiliationBase> affiliationBases,
                int caeNumber)
        {
            var snapshotList = new List<Snapshot_ComposerOriginalPublisherAffiliationBase>();
            foreach (var affiliationBase in affiliationBases)
            {
                var snapshot = new Snapshot_ComposerOriginalPublisherAffiliationBase();
                snapshot.CloneWriterCaeNumber = caeNumber;
                snapshot.EndDate = affiliationBase.EndDate;
                snapshot.StartDate = affiliationBase.StartDate;
                snapshot.SocietyAcronym = affiliationBase.SocietyAcronym;
                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_Sample> CastToSnapshotSamples(List<RecsCopyrights> samples, int workTrackId)
        {
            var snapshotList = new List<Snapshot_Sample>();
            foreach (var sample in samples)
            {
                var snapshot = new Snapshot_Sample();
                snapshot.CloneWorksTrackId = workTrackId;
                snapshot.WorkCode = sample.WorkCode;
                snapshot.Title = sample.Title;
                snapshot.PrincipalArtist = sample.PrincipalArtist;
                snapshot.Writers = sample.Writers;
                snapshot.WriteString = sample.WriteString;
                snapshot.MechanicalCollectablePercentage = (int) sample.MechanicalCollectablePercentage;
                snapshot.MechanicalOwnershipPercentage = (int) sample.MechanicalOwnershipPercentage;
                snapshot.LocalClients = CastToLocalClientSnapshot(sample.LocalClients, workTrackId);
                snapshot.AquisitionLocationCodes = CastToAquisitionLocationCodes(sample.AquisitionLocationCode,workTrackId);

                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private List<Snapshot_SampleAquisitionLocationCode> CastToAquisitionLocationCodes(List<string> locationCodes,
            int worksTrackId)
        {
            var snapshotList = new List<Snapshot_SampleAquisitionLocationCode>();

            foreach (var locationCode in locationCodes)
            {
                var snapshot = new Snapshot_SampleAquisitionLocationCode();
                snapshot.CloneWorksTrackId = worksTrackId;
                snapshot.AquisitionLocationCode = locationCode;
            }
            return snapshotList;
        }

        private List<Snapshot_SampleLocalClientCopyright> CastToLocalClientSnapshot(
            List<LocalClientCopyright> localClientCopyrights, int workTrackId)
        {
            var snapshotList = new List<Snapshot_SampleLocalClientCopyright>();
            foreach (var localClientCopyright in localClientCopyrights)
            {
                var snapshot = new Snapshot_SampleLocalClientCopyright();
                snapshot.CloneWorksTrackId = workTrackId;
                snapshot.ClientCode = localClientCopyright.ClientCode;
                snapshot.ClientName = localClientCopyright.ClientName;
                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_WorksWriter> CastToSnapshotWorksWriter(List<WorksWriter> writers, int workTrackId)
        {
            var snapshotList = new List<Snapshot_WorksWriter>();

            foreach (var writer in writers)
            {
                var snapshot = new Snapshot_WorksWriter();
                //cast writer base!
                snapshot.CloneWorksTrackId = workTrackId;
                if (writer.Contribution != null)
                {
                    snapshot.Contribution = (int)writer.Contribution;
                }
                if (writer.OriginalPublishers != null)
                {
                    snapshot.OriginalPublishers = CastOriginalPublishersToSnapshot(writer.OriginalPublishers,
                        writer.CaeNumber);
                }
                if (writer.LicenseProductRecordingWriter != null)
                {
                    // snapshot.LicenseProductRecordingWriter =  TURNED OFF ***
                    //     CastToLicensProductRecordingSnapshot(writer.LicenseProductRecordingWriter, writer.CaeNumber);
                }
                snapshot.ParentSongDuration = writer.ParentSongDuration;
                snapshot.CloneCaeNumber = writer.CaeNumber;
                snapshot.IpCode = writer.IpCode;
                snapshot.FullName = writer.FullName;
                snapshot.CapacityCode = writer.CapacityCode;
                snapshot.Capacity = writer.Capacity;
                snapshot.MechanicalCollectablePercentage = writer.MechanicalCollectablePercentage.ToString();
                snapshot.MechanicalOwnershipPercentage = writer.MechanicalOwnershipPercentage.ToString();
                snapshot.Affiliation = CastToAffiliationSnapshot(writer.Affiliation, writer.CaeNumber);
                if (writer.KnownAs != null)
                {
                    snapshot.KnownAs = CastToKnownAs(writer.KnownAs, writer.CaeNumber);
                }
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }
        /*
        private Snapshot_LicenseProductRecordingWriter CastToLicensProductRecordingSnapshot(
            LicenseProductRecordingWriter lprw, int caeNumber)
        {
            var snapshot = new Snapshot_LicenseProductRecordingWriter();
            snapshot.CloneLicenseWriterId = lprw.LicenseWriterId;
            snapshot.LicenseRecordingId = lprw.LicenseRecordingId;
            snapshot.IpCode = lprw.IpCode;
            snapshot.ExecutedSplit = lprw.ExecutedSplit;
            snapshot.SplitOverride = lprw.SplitOverride;
            snapshot.ClaimExceptionOverride = lprw.ClaimExceptionOverride;
            snapshot.StatYear = lprw.StatYear;
            snapshot.PaidQuarter = lprw.PaidQuarter;
            snapshot.Sample = lprw.Sample;
            snapshot.IsLicensed = lprw.isLicensed;
            snapshot.ExecutedControlledWriter = lprw.ExecutedControlledWriter;
            snapshot.WriterChangeDate = lprw.WriterChangeDate;
            snapshot.LicensedDate = lprw.LicensedDate;
            snapshot.Publisher = lprw.Publisher;
            snapshot.CAECode = lprw.CAECode;
            snapshot.WriterNoteCount = lprw.WriterNoteCount;
            snapshot.CreatedDate = lprw.CreatedDate;
            snapshot.CreatedBy = lprw.CreatedBy;
            snapshot.ModifiedDate = lprw.ModifiedDate;
            snapshot.ModifiedBy = lprw.ModifiedBy;
            snapshot.Deleted = lprw.Deleted;
            if (lprw.RateList != null)
            {
                snapshot.RateList = CastToLicenseProductRecordingWriterRateSnapshot(lprw.RateList, lprw.CAECode);
            }
            if (lprw.WriterNotes != null)
            {
                snapshot.WriterNotes = CastToWriterNoteSnapshot(lprw.WriterNotes, lprw.CAECode);
            }
            return snapshot;
        }
        */
        /*
        private List<Snapshot_LicenseProductRecordingWriterNote> CastToWriterNoteSnapshot(
            List<LicenseProductRecordingWriterNote> writerNotes, int caeCode)
        {
            var snapshotList = new List<Snapshot_LicenseProductRecordingWriterNote>();
            foreach (var writerNote in writerNotes)
            {
                var snapshot = new Snapshot_LicenseProductRecordingWriterNote();
                snapshot.WriterCaeCode = caeCode;
                snapshot.CloneLicenseWriterId = writerNote.LicenseWriterId;
                snapshot.LicenseWriterNoteId = writerNote.LicenseWriterNoteId;
                snapshot.LicenseWriterId = writerNote.LicenseWriterId;
                snapshot.Note = writerNote.Note;
                snapshot.Configuration_Id = writerNote.Configuration_Id;
                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }
        */
        /*

        private List<Snapshot_LicenseProductRecordingWriterRate> CastToLicenseProductRecordingWriterRateSnapshot(
            List<LicenseProductRecordingWriterRate> rateList, int caeCode)
        {
            var snapshotList = new List<Snapshot_LicenseProductRecordingWriterRate>();
            foreach (var rate in rateList)
            {
                var snapshot = new Snapshot_LicenseProductRecordingWriterRate();
                snapshot.CloneLicenseWriterId = rate.LicenseWriterRateId;
                snapshot.CloneLicenseWriterRateId = rate.LicenseWriterRateId;
                snapshot.Configuration_Id = rate.configuration_id;
                snapshot.PercentOfStat = rate.PercentOfStat;
                snapshot.EscalatedRate = rate.EscalatedRate;
                snapshot.RateTypeId = rate.RateTypeId;
                snapshot.Rate = rate.Rate;
                snapshot.ProRataRate = rate.ProRataRate;
                snapshot.PerSongRate = rate.PerSongRate;
                snapshot.LongStatRate = rate.LongStatRate;
                snapshot.StatYear = rate.StatYear;
                snapshot.Configuration_Name = rate.configuration_name;
                snapshot.Configuration_Type = rate.configuration_type;
                snapshot.LicenseDate = rate.licenseDate;
                snapshot.PaidQuarter = rate.paidQuarter;
                snapshot.WritersConsentTypeId = rate.writersConsentTypeId;
                snapshot.WritersConsentDate = rate.writersConsentDate;
                snapshot.RateNoteCount = rate.RateNoteCount;
                snapshot.MostRecentNote = rate.MostRecentNote;
                snapshot.WriterRateInclude = rate.WriterRateInclude;
                snapshot.Product_Configuration_Id = rate.product_configuration_id;
                snapshot.Upc = rate.upc;
                snapshot.TrackId = rate.trackId;
                snapshot.LicenseRecordingId = rate.licenseRecordingId;
                snapshot.CaeCode = rate.CaeCode;
                snapshot.LicenseTitle = rate.LicenseTitle;
                snapshot.LicenseNumber = rate.LicenseNumber;
                snapshot.SpecialStatusList = CastToLicenseProductRecordingWriterStatus(rate.SpecialStatusList,
                    rate.LicenseWriterRateId);

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }
       //
        //private List<Snapshot_LicenseProductRecordingWriterRateStatus> CastToLicenseProductRecordingWriterStatus(
        //    List<LicenseProductRecordingWriterRateStatus> lprwStatuses, int licenseWriterRateId)
        //{
        //    var snapshotList = new List<Snapshot_LicenseProductRecordingWriterRateStatus>();

        //    foreach (var status in lprwStatuses)
        //    {
        //        var snapshot = new Snapshot_LicenseProductRecordingWriterRateStatus();

        //        snapshot.LicenseWriterRateStatusId = status.LicenseWriterRateStatusId;
        //        snapshot.LicenseWriterRateId = status.LicenseWriterRateId;
        //        snapshot.SpecialStatusId = status.SpecialStatusId;
        //        snapshotList.Add(snapshot);
        //    }

        //    return snapshotList;
        //}

        private List<Snapshot_OriginalPublisher> CastOriginalPublishersToSnapshot(
            List<OriginalPublisher> originalPublishers, int caeNumber)
        {
            var snapshotList = new List<Snapshot_OriginalPublisher>();

            foreach (var op in originalPublishers)
            {
                var snapshot = new Snapshot_OriginalPublisher();
                snapshot.CloneWorksWriterCaeNumber = caeNumber;
                snapshot.Administrator = CastToAdministrator(op.Administrator, caeNumber);
                snapshot.CloneCaeNumber = op.CaeNumber;
                snapshot.IpCode = op.IpCode;
                snapshot.FullName = op.FullName;
                snapshot.CapacityCode = op.CapacityCode;
                snapshot.Capacity = op.Capacity;
                snapshot.MechanicalCollectablePercentage = op.MechanicalCollectablePercentage.ToString();
                snapshot.MechanicalOwnershipPercentage = op.MechanicalOwnershipPercentage.ToString();
                snapshot.Affiliation = CastToOriginalPublisherAffiliationSnapshot(op.Affiliation, op.CaeNumber);
                if (op.KnownAs != null)
                {
                    snapshot.KnownAs = CastToKnownAs(op.KnownAs, op.CaeNumber);
                }
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private List<Snapshot_AdminKnownAs> CastToAdminKnownAs(List<string> knownAs, int writerCaeCode)
        {
            var snapshotList = new List<Snapshot_AdminKnownAs>();

            foreach (var known in knownAs)
            {
                var snapshot = new Snapshot_AdminKnownAs();
                snapshot.KnownAs = known;
                snapshot.SnapshotAdministratorId = 0;
                snapshot.CloneWriterCaeCode = writerCaeCode;
                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_Administrator> CastToAdministrator(List<WriterBase> admins, int caeNumber)
        {
            var snapshotList = new List<Snapshot_Administrator>();

            foreach (var admin in admins)
            {
                var snapshot = new Snapshot_Administrator();

                snapshot.CloneCaeNumber = admin.CaeNumber;
                snapshot.IpCode = admin.IpCode;
                snapshot.FullName = admin.FullName;
                snapshot.CapacityCode = admin.CapacityCode;
                snapshot.Capacity = admin.Capacity;
                snapshot.Controlled = admin.Controlled;
                snapshot.MechanicalCollectablePercentage = (int)admin.MechanicalCollectablePercentage;
                snapshot.MechanicalOwnershipPercentage = (int)admin.MechanicalOwnershipPercentage;
                snapshot.Affiliation = CastToAdminAffiliationSnapshot(admin.Affiliation, admin.CaeNumber);
                if (admin.KnownAs != null)
                {
                    snapshot.KnownAs = CastToAdminKnownAs(admin.KnownAs, admin.CaeNumber);
                }
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private List<Snapshot_AdminAffiliation> CastToAdminAffiliationSnapshot(List<Affiliation> affiliations, int caeNumber)

        {
            var snapshotList = new List<Snapshot_AdminAffiliation>();

            foreach (var affilation in affiliations)
            {
                var snapshot = new Snapshot_AdminAffiliation();

                snapshot.CloneWriterCaeNumber = caeNumber;
                snapshot.SnapshotAdministratorId = 0;
                snapshot.WriterCaeNumber = caeNumber;
                snapshot.IncomeGroup = affilation.IncomeGroup;
                snapshot.Affiliations = CastToAdminAffiliationBaseSnapshot(affilation.Affiliations, caeNumber);

                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_AdminAffiliationBase> CastToAdminAffiliationBaseSnapshot(List<AffiliationBase> affiliationBases, int writerCaeCode)
        {
            var snapshotList = new List<Snapshot_AdminAffiliationBase>();

            foreach (var ab in affiliationBases)
            {
                var snapshot = new Snapshot_AdminAffiliationBase();
                snapshot.SnapshotAdminAffiliationId = 0;
                snapshot.CloneWriterCaeNumber = writerCaeCode;
                snapshot.SocietyAcronym = ab.SocietyAcronym;
                snapshot.EndDate = ab.EndDate;
                snapshot.StartDate = ab.StartDate;
                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_KnownAs> CastToKnownAs(List<string> knownAs, int writerCaeCode)
        {
            var snapshotList = new List<Snapshot_KnownAs>();

            foreach (var known in knownAs)
            {
                var snapshot = new Snapshot_KnownAs();
                snapshot.KnownAs = known;
                snapshot.CloneWriterCaeCode = writerCaeCode;
                snapshotList.Add(snapshot);
            }
            return snapshotList;
        }

        private List<Snapshot_OriginalPublisherAffiliation> CastToOriginalPublisherAffiliationSnapshot(List<Affiliation> affiliations, int caeNumber)
        {
            var snapshotList = new List<Snapshot_OriginalPublisherAffiliation>();

            foreach (var affilation in affiliations)
            {
                var snapshot = new Snapshot_OriginalPublisherAffiliation();
                snapshot.CloneWriterCaeNumber = caeNumber;
                snapshot.WriterCaeNumber = caeNumber;
                snapshot.IncomeGroup = affilation.IncomeGroup;
                snapshot.Affiliations = CastToOriginalPublisherAffiliationBaseSnapshot(affilation.Affiliations, caeNumber);
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private List<Snapshot_Affiliation> CastToAffiliationSnapshot(List<Affiliation> affiliations, int caeNumber)
        {
            var snapshotList = new List<Snapshot_Affiliation>();

            foreach (var affilation in affiliations)
            {
                var snapshot = new Snapshot_Affiliation();
                snapshot.CloneWriterCaeNumber = caeNumber;
                snapshot.WriterCaeNumber = caeNumber;
                snapshot.IncomeGroup = affilation.IncomeGroup;
                snapshot.Affiliations = CastToAffiliationBaseSnapshot(affilation.Affiliations, caeNumber);
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private List<Snapshot_OriginalPubAffiliationBase> CastToOriginalPublisherAffiliationBaseSnapshot(List<AffiliationBase> affiliationBases, int caeNumber)
        {
            var snapshotList = new List<Snapshot_OriginalPubAffiliationBase>();

            foreach (var affilation in affiliationBases)
            {
                var snapshot = new Snapshot_OriginalPubAffiliationBase();
                snapshot.CloneWriterCaeNumber = caeNumber;
                snapshot.SocietyAcronym = affilation.SocietyAcronym;
                snapshot.StartDate = affilation.StartDate;
                snapshot.EndDate = affilation.EndDate;
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private List<Snapshot_AffiliationBase> CastToAffiliationBaseSnapshot(List<AffiliationBase> affiliationBases, int caeNumber)
        {
            var snapshotList = new List<Snapshot_AffiliationBase>();

            foreach (var affilation in affiliationBases)
            {
                var snapshot = new Snapshot_AffiliationBase();
                snapshot.CloneWriterCaeNumber = caeNumber;
                snapshot.SocietyAcronym = affilation.SocietyAcronym;
                snapshot.StartDate = affilation.StartDate;
                snapshot.EndDate = affilation.EndDate;
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private List<Snapshot_LocalClientCopyright> CastToSnapshotLocalClientCopyrights(List<LocalClientCopyright> localClientCopyrights, int workTrackId)
        {
            var snapshotList = new List<Snapshot_LocalClientCopyright>();

            foreach (var localClientCopyright in localClientCopyrights)
            {
                var snapshot = new Snapshot_LocalClientCopyright();
                //do this
                snapshot.CloneWorksTrackId = workTrackId;
                snapshot.ClientCode = localClientCopyright.ClientCode;
                snapshot.ClientName = localClientCopyright.ClientName;
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private List<Snapshot_AquisitionLocationCode> CastToSnapshotAquisitionLocationCode(List<string> aquisitionLocationCodes, int workTrackId)
        {
            var snapshotList = new List<Snapshot_AquisitionLocationCode>();

            foreach (var code in aquisitionLocationCodes)
            {
                var snapshot = new Snapshot_AquisitionLocationCode();
                snapshot.CloneWorksTrackId = workTrackId;
                snapshot.AquisitionLocationCode = code;
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private Snapshot_ProductHeader CastToProductHeaderSnapshot(ProductHeader productHeader)
        {
            var snapshotProductHeader = new Snapshot_ProductHeader();
            snapshotProductHeader.CloneProductHeaderId = (int)productHeader.Id;
            snapshotProductHeader.Title = productHeader.Title;
            snapshotProductHeader.CatalogueId = productHeader.CatalogueId;
            snapshotProductHeader.AlbumArtUrl = productHeader.AlbumArtUrl;
            snapshotProductHeader.DatabaseVersion = productHeader.DatabaseVersion;
            if (productHeader.SoundscanSales != null)
            {
                snapshotProductHeader.SoundscanSales = (int)productHeader.SoundscanSales;
            }
            snapshotProductHeader.MechsPriority = productHeader.MechsPriority;
            snapshotProductHeader.RelatedLicensesNo = productHeader.RelatedLicensesNo;
            snapshotProductHeader.ArtistRecsId = (int)productHeader.Artist.id;
            snapshotProductHeader.Artist = CastToArtistRecsSnapshot(productHeader.Artist);
            snapshotProductHeader.LabelId = (int)productHeader.Label.label_id;
            snapshotProductHeader.Label = CastToLabelSnapshot(productHeader.Label);
            snapshotProductHeader.Configurations = CastToRecsConfigurationsSnapshot(productHeader.Configurations, (int)productHeader.Id);

            return snapshotProductHeader;
        }

        private Snapshot_ArtistRecs CastToArtistRecsSnapshot(ArtistRecs artistRecs)
        {
            var snapshotArtists = new Snapshot_ArtistRecs();
            snapshotArtists.CloneArtistRecsId = (int)artistRecs.id;
            snapshotArtists.Name = artistRecs.name;
            return snapshotArtists;
        }

        private Snapshot_Label CastToLabelSnapshot(Label label)
        {
            var snapshot = new Snapshot_Label();
            snapshot.CloneLabelId = (int)label.label_id;
            snapshot.RecordLabelGroups = CastToLabelGroupSnapshot(label.recordLabelGroups, label);
            snapshot.Name = label.name;
            return snapshot;
        }

        private List<Snapshot_LabelGroup> CastToLabelGroupSnapshot(List<LabelGroup> labelGroup, Label label)
        {
            var labelId = (int)label.label_id;
            var snapshotList = new List<Snapshot_LabelGroup>();

            foreach (var group in labelGroup)
            {
                var snapshot = new Snapshot_LabelGroup();
                snapshot.CloneLabelGroupId = (int)group.id;
                snapshot.Name = group.Name;
                snapshot.CloneLabelId = labelId;
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private List<Snapshot_RecsConfiguration> CastToRecsConfigurationsSnapshot(
            List<RecsConfiguration> recsConfigurations, int productHeaderId)
        {
            var snapshotList = new List<Snapshot_RecsConfiguration>();

            foreach (var config in recsConfigurations)
            {
                var snapshot = new Snapshot_RecsConfiguration();
                snapshot.CloneRecsConfigurationId = (int)config.configuration_id;
                if (config.LicenseProductConfiguration != null)
                {
                    snapshot.LicenseProductId = config.LicenseProductConfiguration.LicenseProductId;
                }
                snapshot.ConfigurationId = (int)config.Configuration.ConfigId;
                snapshot.Configuration = CastToConfigurationSnapshot(config.Configuration);
                snapshot.ProductHeaderId = productHeaderId;
                snapshot.Name = config.name;
                snapshot.UPC = config.UPC;
                snapshot.ReleaseDate = config.ReleaseDate;
                snapshot.DatabaseVersion = config.DatabaseVersion;
                if (config.LicenseProductConfiguration != null)
                {
                    snapshot.LicenseProductConfigurationId =
                        config.LicenseProductConfiguration.LicenseProductConfigurationId;
                }
                //snapshot.LicenseProductConfiguration = config.LicenseProductConfiguration;  temp off
                snapshotList.Add(snapshot);
            }

            return snapshotList;
        }

        private Snapshot_Configuration CastToConfigurationSnapshot(Configuration configuration)
        {
            var snapshot = new Snapshot_Configuration();
            snapshot.CloneConfigId = (int)configuration.ConfigId;
            snapshot.Name = configuration.name;
            snapshot.Type = configuration.type;
            return snapshot;
        }

    */

        #endregion oldCastToSnapshotCode
    }
}

//db save changes, primary key http://stackoverflow.com/questions/17523568/entity-framework-retrieve-id-before-savechanges-inside-a-transaction