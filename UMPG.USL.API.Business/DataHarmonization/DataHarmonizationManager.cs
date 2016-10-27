using NLog;
using System;
using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class DataHarmonizationManager : IDataHarmonizationManager
    {
        private readonly ISnapshotLicenseManager _snapshotLicenseManager;
        private readonly ISnapshotLicenseNoteManager _snapshotLicenseNoteManager;
        private readonly ISnapshotLicenseProductManager _snapshotLicenseProductManager;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public DataHarmonizationManager(ISnapshotLicenseManager snapshotLicenseManager, ISnapshotLicenseNoteManager snapshotLicenseNoteManager, ISnapshotLicenseProductManager snapshotLicenseProductManager)
        {
            _snapshotLicenseProductManager = snapshotLicenseProductManager;
            _snapshotLicenseNoteManager = snapshotLicenseNoteManager;
            _snapshotLicenseManager = snapshotLicenseManager;
        }

        public bool DoesSnapshotExist(int licenseId)
        {
            return _snapshotLicenseManager.DoesSnapshotExists(licenseId);
        }

        public Snapshot_License GetLicenseSnapshot(int licenseId)
        {
            return _snapshotLicenseManager.GetSnapshotLicenseBySnapshotLicenseId(licenseId);
        }

        public bool TakeLicenseSnapshot(License licenseToBeSnapshotted, List<LicenseProduct> licenseProducts)
        {
            var newLicense = new Snapshot_License();
            newLicense.CloneLicenseId = licenseToBeSnapshotted.LicenseId;

            newLicense.LicenseNumber = licenseToBeSnapshotted.LicenseNumber;
            newLicense.LicenseName = licenseToBeSnapshotted.LicenseName;

            newLicense.LicenseStatusId = licenseToBeSnapshotted.LicenseStatusId;

            newLicense.EffectiveDate = licenseToBeSnapshotted.EffectiveDate;

            newLicense.SignedDate = licenseToBeSnapshotted.SignedDate;

            newLicense.ReceivedDate = licenseToBeSnapshotted.ReceivedDate;

            newLicense.LicenseMethodId = licenseToBeSnapshotted.LicenseMethodId;

            newLicense.ManagerApprovalDate = licenseToBeSnapshotted.ManagerApprovalDate;

            newLicense.CountryId = licenseToBeSnapshotted.CountryId;

            newLicense.LicenseeId = licenseToBeSnapshotted.LicenseeId;

            newLicense.LicenseeLabelGroupId = licenseToBeSnapshotted.LicenseeLabelGroupId;

            newLicense.HFARollupLicenseId = licenseToBeSnapshotted.HFARollupLicenseId;

            newLicense.AssignedToId = licenseToBeSnapshotted.AssignedToId;

            newLicense.PriorityId = licenseToBeSnapshotted.PriorityId;

            newLicense.LicenseTypeId = licenseToBeSnapshotted.LicenseTypeId;

            newLicense.ProductsNo = licenseToBeSnapshotted.ProductsNo;

            newLicense.LicenseConfigurationRollup = licenseToBeSnapshotted.LicenseConfigurationRollup;

            newLicense.Label = licenseToBeSnapshotted.Label;

            newLicense.ArtistRollup = licenseToBeSnapshotted.ArtistRollup;

            newLicense.ProductRollup = licenseToBeSnapshotted.ProductRollup;

            //  newLicense.ContactId = licenseToBeSnapshotted.ContactId;

            newLicense.ClaimException = licenseToBeSnapshotted.ClaimException;

            newLicense.RelatedLicenseId = licenseToBeSnapshotted.RelatedLicenseId;

            newLicense.amsLegacyPath = licenseToBeSnapshotted.amsLegacyPath;

            newLicense.StatusReport = licenseToBeSnapshotted.StatusReport;

            newLicense.StatusesRollup = licenseToBeSnapshotted.StatusesRollup;

            //Virtuals?

            //Snapshot here
            try
            {
                _snapshotLicenseManager.SaveSnapshotLicense(newLicense);
            }
            catch (Exception exception)
            {
                Logger.Debug(exception.ToString);
                return false;
            }

            //snapshot LicenseProducts
            SaveLocalLicenseProductSnapshot(licenseProducts);

            return true;
        }

        private bool SaveLocalLicenseProductSnapshot(List<LicenseProduct> localLicenseProducts)
        {
            foreach (var lp in localLicenseProducts)
            {
                _snapshotLicenseProductManager.SaveSnapshotLicenseProduct(CastToLicenseProductSnapshot(lp));
            }

            return true;
        }

        public bool DeleteLicenseSnapshot(int licenseSnapshotId)
        {
            return _snapshotLicenseManager.DeleteLicenseSnapshot(licenseSnapshotId);
        }

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
            snapshot.Recordings = CastToSnapshotRecordings(licenseProduct.Recordings, licenseProduct.ProductId, licenseProduct.LicenseProductId);
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
                snapshot.LicenseProductId = licenseProductId;
                snapshot.CdIndex = rec.CdIndex;
                snapshot.UmpgPercentageRollup = (int) rec.UmpgPercentageRollup;
                snapshot.CdNumber = rec.CdNumber;
                snapshot.LicensedRollup = (int)rec.LicensedRollup;
               // snapshot.WorkTrackId = rec.TrackId
                snapshot.Message = rec.Message;
                snapshot.Writers = CastToSnapshotWorksWriter(rec.Writers, rec.TrackId);
                snapshot.Track = CastToSnapshotWorksTrack(rec.Track, rec.TrackId);


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
            snapshot.Artist = CastToArtistRecsSnapshot(track.Artists);
            snapshot.Copyrights = CastToSnapshotRecsCopyrights(track.Copyrights, trakId);
            return snapshot;
        }

        private List<Snapshot_RecsCopyrights> CastToSnapshotRecsCopyrights(List<RecsCopyrights> recsCopyrights,
            int workTrackId)
        {
            var snapshotList = new List<Snapshot_RecsCopyrights>();
            foreach (var rec in recsCopyrights)
            {
                var snapshot = new Snapshot_RecsCopyrights();
                snapshot.CloneWorksTrackId = workTrackId;
                snapshot.WorkCode = rec.WorkCode;
                snapshot.Title = rec.Title;
                snapshot.PrincipalArtist = rec.PrincipalArtist;
                snapshot.Writers = rec.Writers;
                snapshot.WriteString = rec.WriteString;
                snapshot.MechanicalCollectablePercentage = rec.MechanicalCollectablePercentage;
                snapshot.MechanicalOwnershipPercentage = rec.MechanicalOwnershipPercentage;

                snapshot.Composers = CastToSnapshotWorksWriter(rec.Composers, workTrackId);
                snapshot.Samples = CastToSnapshotSamples(rec.Samples, workTrackId);
                snapshot.LocalClients = CastToSnapshotLocalClientCopyrights(rec.LocalClients, workTrackId);
                snapshot.AquisitionLocationCodes = CastToSnapshotAquisitionLocationCode(rec.AquisitionLocationCode,
                    workTrackId);



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
                snapshot.Contribution = writer.Contribution;
                snapshot.OriginalPublishers = CastOriginalPublishersToSnapshot(writer.OriginalPublishers,
                    writer.CaeNumber);
                snapshot.LicenseProductRecordingWriter =
                    CastToLicensProductRecordingSnapshot(writer.LicenseProductRecordingWriter, writer.CaeNumber);
                snapshot.ParentSongDuration = writer.ParentSongDuration;



                snapshotList.Add(snapshot);
                
            }


            return snapshotList;
        }

        private Snapshot_LicenseProductRecordingWriter CastToLicensProductRecordingSnapshot(
            LicenseProductRecordingWriter lprw, int caeNumber)
        {
            var snapshot = new Snapshot_LicenseProductRecordingWriter();
            //do this and child entities


            return snapshot;
        }

        private List<Snapshot_OriginalPublisher> CastOriginalPublishersToSnapshot(
            List<OriginalPublisher> originalPublishers, int caeNumber)
        {
            var snapshotList = new List<Snapshot_OriginalPublisher>();


            foreach (var op in originalPublishers)
            {
                var snapshot = new Snapshot_OriginalPublisher();
                snapshot.CloneWorksWriterCaeNumber = caeNumber;
                snapshot.Administrator = CastToAdministrator(op.Administrator, caeNumber);

                //cast writer base!



                snapshotList.Add(snapshot);

            }


            return snapshotList;
        }

        private List<Snapshot_WriterBase> CastToAdministrator(List<WriterBase> admins, int caeNumber)
        {
            var snapshotList = new List<Snapshot_WriterBase>();

            
            foreach (var admin in admins)
            {
                var snapshot = new Snapshot_WriterBase();

                snapshot.CloneCaeNumber = admin.CaeNumber;
                snapshot.IpCode = admin.IpCode;
                snapshot.FullName = admin.FullName;
                snapshot.CapacityCode = admin.CapacityCode;
                snapshot.Capacity = admin.Capacity;
                snapshot.MechanicalCollectablePercentage = admin.MechanicalCollectablePercentage;
                snapshot.MechanicalOwnershipPercentage = admin.MechanicalOwnershipPercentage;
                snapshot.Affiliation = admin.Affiliation;
                //do known as (create a new class





                snapshotList.Add(snapshot);

            }


            return snapshotList;

        }

        private List<Snapshot_RecsCopyrights> CastToSnapshotSamples(List<RecsCopyrights> samples, int workTrackId)
        {
            var snapshotList = new List<Snapshot_RecsCopyrights>();


            foreach (var sample in samples)
            {
                var snapshot = new Snapshot_RecsCopyrights();





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
                snapshot.LicenseProductId = config.LicenseProductConfiguration.LicenseProductId;
                snapshot.ConfigurationId = (int)config.Configuration.ConfigId;
                snapshot.Configuration = CastToConfigurationSnapshot(config.Configuration);
                snapshot.ProductHeaderId = productHeaderId;
                snapshot.Name = config.name;
                snapshot.UPC = config.UPC;
                snapshot.ReleaseDate = config.ReleaseDate;
                snapshot.DatabaseVersion = config.DatabaseVersion;
                snapshot.LicenseProductConfigurationId =
                    config.LicenseProductConfiguration.LicenseProductConfigurationId;
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
    }
}