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


        public Snapshot_License GetLicenseSnapshot(int licenseId)
        {
            return _snapshotLicenseManager.GetSnapshotLicenseBySnapshotLicenseId(licenseId);
        }

        public bool TakeLicenseSnapshot(License licenseToBeSnapshotted)
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
            return true;
        }

        public bool SaveLocalLicenseProductSnapshot(List<LicenseProduct> localLicenseProducts)
        {
            foreach (var lp in localLicenseProducts)
            {
                _snapshotLicenseProductManager.SaveSnapshotLicenseProduct(CastToLicenseProductSnapshot(lp));
            }

            return true;
        }

        private Snapshot_LicenseProduct CastToLicenseProductSnapshot(LicenseProduct licenseProduct)
        {
            var snapshot = new Snapshot_LicenseProduct();

            snapshot.CloneLicenseProductId = licenseProduct.LicenseProductId;
            snapshot.LicenseId = licenseProduct.LicenseId;
            snapshot.ProductHeaderId = (int)licenseProduct.ProductHeader.Id;
            snapshot.ProductHeader = CastToProductHeaderSnapshot(licenseProduct.ProductHeader);

            return snapshot;
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