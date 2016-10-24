using NLog;
using System;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class DataHarmonizationManager : IDataHarmonizationManager
    {
        //private readonly ISnapshotLicenseManager _snapshotLicenseManager;
        //private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        //public DataHarmonizationManager(SnapshotLicenseManager snapshotLicenseManager)
        //{
        //    _snapshotLicenseManager = snapshotLicenseManager;
        //}

        //public bool TakeLicenseSnapshot(License licenseToBeSnapshotted)
        //{
        //    var newLicense = new Snapshot_License();
        //    newLicense.LicenseId = licenseToBeSnapshotted.LicenseId;
        //    newLicense.LicenseNumber = licenseToBeSnapshotted.LicenseNumber;
        //    newLicense.LicenseName = licenseToBeSnapshotted.LicenseName;

        //    newLicense.LicenseStatusId = licenseToBeSnapshotted.LicenseStatusId;

        //    newLicense.EffectiveDate = licenseToBeSnapshotted.EffectiveDate;

        //    newLicense.SignedDate = licenseToBeSnapshotted.SignedDate;

        //    newLicense.ReceivedDate = licenseToBeSnapshotted.ReceivedDate;

        //    newLicense.LicenseMethodId = licenseToBeSnapshotted.LicenseMethodId;

        //    newLicense.ManagerApprovalDate = licenseToBeSnapshotted.ManagerApprovalDate;

        //    newLicense.CountryId = licenseToBeSnapshotted.CountryId;

        //    newLicense.LicenseeId = licenseToBeSnapshotted.LicenseeId;

        //    newLicense.LicenseeLabelGroupId = licenseToBeSnapshotted.LicenseeLabelGroupId;

        //    newLicense.HFARollupLicenseId = licenseToBeSnapshotted.HFARollupLicenseId;

        //    newLicense.AssignedToId = licenseToBeSnapshotted.AssignedToId;

        //    newLicense.PriorityId = licenseToBeSnapshotted.PriorityId;

        //    newLicense.LicenseTypeId = licenseToBeSnapshotted.LicenseTypeId;

        //    newLicense.ProductsNo = licenseToBeSnapshotted.ProductsNo;

        //    newLicense.LicenseConfigurationRollup = licenseToBeSnapshotted.LicenseConfigurationRollup;

        //    newLicense.Label = licenseToBeSnapshotted.Label;

        //    newLicense.ArtistRollup = licenseToBeSnapshotted.ArtistRollup;

        //    newLicense.ProductRollup = licenseToBeSnapshotted.ProductRollup;

        //    newLicense.ContactId = licenseToBeSnapshotted.ContactId;

        //    newLicense.ClaimException = licenseToBeSnapshotted.ClaimException;

        //    newLicense.RelatedLicenseId = licenseToBeSnapshotted.RelatedLicenseId;

        //    newLicense.amsLegacyPath = licenseToBeSnapshotted.amsLegacyPath;

        //    newLicense.StatusReport = licenseToBeSnapshotted.StatusReport;

        //    newLicense.StatusesRollup = licenseToBeSnapshotted.StatusesRollup;

        //    //Virtuals?

        //    //Snapshot here
        //    try
        //    {
        //        _snapshotLicenseManager.SaveSnapshotLicense(newLicense);
        //    }
        //    catch (Exception exception)
        //    {
        //        Logger.Debug(exception.ToString);
        //        return false;
        //    }
        //    return true;
        //}
    }
}