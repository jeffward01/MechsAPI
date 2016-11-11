﻿using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class RecCongruencyCheckService : IRecCongruencyCheckService
    {
        private readonly IRecsProductChangeLogService _productChangeLogService;

        public RecCongruencyCheckService(IRecsProductChangeLogService recsProductChangeLogService)
        {
            _productChangeLogService = recsProductChangeLogService;
        }

        public List<RecsProductChanges> CheckForLicenseProductChanges(List<LicenseProduct> recsLicenseProducts,
            List<Snapshot_LicenseProduct> licenseProductsSnapshots)
        {
            //Start code_____________
            var listOfChanges = new List<RecsProductChanges>();

            //check for product changes
            //Log productsAdded and removed
            listOfChanges.AddRange(CheckForAddedRemovedLicenseProducts(recsLicenseProducts, licenseProductsSnapshots));

            //Log Added Removed Tracks

            return listOfChanges;
        }

        private List<RecsProductChanges> CheckForAddedRemovedLicenseProducts(List<LicenseProduct> recsLicenseProducts,
            List<Snapshot_LicenseProduct> licenseProductsSnapshots)
        {
            var listOfChanges = new List<RecsProductChanges>();

            //-------Product Area----------
            //Find which products have been added and removed
            var snapshotProductIds = licenseProductsSnapshots.Select(x => Convert.ToInt32(x.ProductId)).ToList();
            var recsProductIds = recsLicenseProducts.Select(c => Convert.ToInt32(c.ProductId)).ToList();

            var productsRemovedFromRecs = snapshotProductIds.Except(recsProductIds).ToList();
            var productsAddedToRecs = recsProductIds.Except(snapshotProductIds).ToList();

            //Log productsAdded and removed
            listOfChanges.AddRange(_productChangeLogService.ProductAddedToRecs(recsLicenseProducts, productsAddedToRecs));
            listOfChanges.AddRange(_productChangeLogService.ProductRemovedFromRecs(licenseProductsSnapshots,
                productsRemovedFromRecs));

            //clean added or removedProducts
            //remove added/removed writers from control lists
            recsLicenseProducts = CleanProducts(recsLicenseProducts, productsAddedToRecs);
            licenseProductsSnapshots = CleanProductsnapshots(licenseProductsSnapshots, productsRemovedFromRecs);

            //--------Product Header Area---------
            listOfChanges.AddRange(FindProductHeaderDifferences(recsLicenseProducts, licenseProductsSnapshots));

            //------Recording Area------
            //Find which recordings Have been added and removed
            var snapshotRecordings = GetAllSnapshotWorksRecordings(licenseProductsSnapshots);
            var recsRecordings = GetAllWorksRecordings(recsLicenseProducts);

            var snapshotRecordingIds = snapshotRecordings.Select(_ => _.CloneTrackId).ToList();
            var recsRecordingIds = recsRecordings.Select(_ => _.TrackId).ToList();

            var recordingIdsRemovedFromRecs = recsRecordingIds.Except(snapshotRecordingIds).ToList();
            var recordingIdsAddedToRecs = snapshotRecordingIds.Except(recsRecordingIds).ToList();

            listOfChanges.AddRange(_productChangeLogService.RecordingAddedToRecs(recsRecordings, recordingIdsAddedToRecs));
            listOfChanges.AddRange(_productChangeLogService.RecordingRemovedFromRecs(snapshotRecordings, recordingIdsRemovedFromRecs));

            //Clean added or removed recordings
            //Remove added/removed recordings from list
            recsRecordings = CleanWorksRecordings(recsRecordings, recordingIdsAddedToRecs);
            snapshotRecordings = CleanWorksRecordingSnapshots(snapshotRecordings, recordingIdsRemovedFromRecs);

            //-------Track Area---------
            var recsTracks = GetRecsTracks(recsRecordings);
            var snapshotTracks = GetSnapshotTracks(snapshotRecordings);

            //find track differences
            listOfChanges.AddRange(FindTrackDifferences(recsTracks, snapshotTracks));

            //---------Writer Area------------
            var recsWriters = GetRecsWriters(recsRecordings);
            var snapshotWriters = GetSnapshotWriters(snapshotRecordings);

            //find writer differences
            listOfChanges.AddRange(FindWriterDifferences(recsWriters, snapshotWriters));
            return listOfChanges;
        }

        private List<RecsProductChanges> FindWriterDifferences(List<WorksWriter> recsWriters,
            List<Snapshot_WorksWriter> snapshotWriters)
        {
            var listOfChanges = new List<RecsProductChanges>();

            if (recsWriters.Count != snapshotWriters.Count)
            {
                //Get Writer Ids
                var snapshotWriterCaeCodes = snapshotWriters.Select(_ => _.CloneCaeNumber).ToList();
                var recsWriterCaeCodes = recsWriters.Select(_ => _.CaeNumber).ToList();

                //find which writers were added and removed
                var writerCaeCodesRemovedFromRecs = recsWriterCaeCodes.Except(snapshotWriterCaeCodes).ToList();
                var writerCaeCodesAddedToRecs = snapshotWriterCaeCodes.Except(recsWriterCaeCodes).ToList();

                //log added or removed writers
                listOfChanges.AddRange(_productChangeLogService.WritersAddedToRecs(recsWriters, writerCaeCodesAddedToRecs));
                listOfChanges.AddRange(_productChangeLogService.WritersRemovedFromRecs(snapshotWriters, writerCaeCodesRemovedFromRecs));

                //Clean added or removed writer
                //Remove added/removed writers from list
                recsWriters = CleanRecsWriters(recsWriters, writerCaeCodesAddedToRecs);
                snapshotWriters = CleanSnapshotWriters(snapshotWriters, writerCaeCodesRemovedFromRecs);
            }

            for (var i = 0; i < recsWriters.Count; i++)
            {
                var recsWriter = recsWriters[i];
                var snapshotWriter = snapshotWriters[i];

                //check name
                if (recsWriter.FullName != snapshotWriter.FullName)
                {
                    listOfChanges.Add(LogRecsProductChanges("Writer Name was Changed", "Writer", "Writer Name", snapshotWriter.FullName, recsWriter.FullName));
                }

                //check caeNumber
                if (recsWriter.CaeNumber != snapshotWriter.CloneCaeNumber)
                {
                    listOfChanges.Add(LogRecsProductChanges("Writer Cae Number was Changed", "Cae Number", "Writer CAE number", snapshotWriter.CloneCaeNumber.ToString(), recsWriter.CaeNumber.ToString()));
                }

                //check capacity code
                if (recsWriter.CapacityCode != snapshotWriter.CapacityCode)
                {
                    listOfChanges.Add(LogRecsProductChanges("Writer Capacity Code was Changed on " + snapshotWriter.FullName, "Writer Capacity Code", "Capacity Code", snapshotWriter.CapacityCode, recsWriter.CapacityCode));
                }

                //check capacity
                if (recsWriter.Capacity != snapshotWriter.Capacity)
                {
                    listOfChanges.Add(LogRecsProductChanges("Writer Capacity  was Changed on " + snapshotWriter.FullName, "Writer Capacity ", "Capacity ", snapshotWriter.Capacity, recsWriter.Capacity));
                }
                //check contrlled
                if (recsWriter.Controlled != snapshotWriter.Controlled)
                {
                    listOfChanges.Add(LogRecsProductChanges("Writer Controlled Status  was Changed on " + snapshotWriter.FullName, "Writer Controlled Status ", "Controlled Status ", snapshotWriter.Controlled.ToString(), recsWriter.Controlled.ToString()));
                }

                //check contribution
                if (recsWriter.Contribution != snapshotWriter.Contribution)
                {
                    listOfChanges.Add(LogRecsProductChanges("Writer contribution  was Changed on " + snapshotWriter.FullName, "Writer contribution ", "contribution ", snapshotWriter.Contribution.ToString(), recsWriter.Contribution.ToString()));
                }
                //check ipCode
                if (recsWriter.IpCode != snapshotWriter.IpCode)
                {
                    listOfChanges.Add(LogRecsProductChanges("Writer IpCode  was Changed on " + snapshotWriter.FullName, "Writer IpCode ", "IpCode ", snapshotWriter.IpCode, recsWriter.IpCode));
                }
                //check affilationsString
                if (recsWriter.AffiliationsString != snapshotWriter.AffiliationsString)
                {
                    listOfChanges.Add(LogRecsProductChanges("Writer AffiliationsString  was Changed on " + snapshotWriter.FullName, "Writer AffiliationsString Changed", "AffiliationsString ", snapshotWriter.AffiliationsString, recsWriter.AffiliationsString));
                }

                //------------------
                //check nested Original Publishers
                listOfChanges.AddRange(FindOrigainlPublisherDifferences(recsWriter.OriginalPublishers, snapshotWriter.OriginalPublishers));

                //check nested affilations
            }

            return listOfChanges;
        }

        private List<RecsProductChanges> FindOrigainlPublisherDifferences(
            List<OriginalPublisher> recsOriginalPublishers, List<Snapshot_OriginalPublisher> snapshotOriginalPublishers)
        {
            var listOfChanges = new List<RecsProductChanges>();

            if (recsOriginalPublishers.Count != snapshotOriginalPublishers.Count)
            {
                //Get Writer Ids
                var snapshotOriginalPublisherIpCodes = snapshotOriginalPublishers.Select(_ => _.IpCode).ToList();
                var recsOriginalPublisherIpCodes = recsOriginalPublishers.Select(_ => _.IpCode).ToList();

                //find which writers were added and removed
                var originalPublisherRemovedIpCodes = recsOriginalPublisherIpCodes.Except(snapshotOriginalPublisherIpCodes).ToList();
                var originalPublisherAddedIpCodes = snapshotOriginalPublisherIpCodes.Except(recsOriginalPublisherIpCodes).ToList();

                //log added or removed writers
                listOfChanges.AddRange(_productChangeLogService.OriginalPublishersAddedToRecs(recsOriginalPublishers, originalPublisherAddedIpCodes));
                listOfChanges.AddRange(_productChangeLogService.OriginalPublishersRemovedFromRecs(snapshotOriginalPublishers, originalPublisherRemovedIpCodes));

                //Clean added or removed writer
                //Remove added/removed writers from list
                recsOriginalPublishers = CleanRecsOriginalPublishers(recsOriginalPublishers, originalPublisherAddedIpCodes);
                snapshotOriginalPublishers = CleanSnapshotsOriginalPublishers(snapshotOriginalPublishers, originalPublisherRemovedIpCodes);
            }

            for (var i = 0; i < recsOriginalPublishers.Count; i++)
            {
                var recOriginalPublisher = recsOriginalPublishers[i];
                var snapshotOriginalPublisher = snapshotOriginalPublishers[i];

                //check name
                if (recOriginalPublisher.FullName != snapshotOriginalPublisher.FullName)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Name was Changed " + recOriginalPublisher.FullName, "Original Publisher", "Original Publisher Name", snapshotOriginalPublisher.FullName, recOriginalPublisher.FullName));
                }

                //check CapacityCode
                if (recOriginalPublisher.CapacityCode != snapshotOriginalPublisher.CapacityCode)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher CapacityCode was Changed " + recOriginalPublisher.CapacityCode, "Original Publisher", "Original Publisher CapacityCode", snapshotOriginalPublisher.CapacityCode, recOriginalPublisher.CapacityCode));
                }
                //check capacity
                if (recOriginalPublisher.Capacity != snapshotOriginalPublisher.Capacity)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Capacity was Changed " + recOriginalPublisher.Capacity, "Original Publisher", "Original Publisher Capacity", snapshotOriginalPublisher.Capacity, recOriginalPublisher.Capacity));
                }
                //check controlled
                if (recOriginalPublisher.Controlled != snapshotOriginalPublisher.Controlled)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Controlled Status was Changed " + recOriginalPublisher.Controlled.ToString(), "Original Publisher", "Original Publisher Controlled Status ", snapshotOriginalPublisher.Controlled.ToString(), recOriginalPublisher.Controlled.ToString()));
                }

                //---------------------

                //check nested admins
                //Not Implemented
                //check nested nested affilations
                //check nested nested nested affilationbases

                //check nested affiliations
                listOfChanges.AddRange(FindAffiliationDifferences(snapshotOriginalPublisher.Affiliation, recOriginalPublisher.Affiliation));
            }

            return listOfChanges;
        }
        private List<RecsProductChanges> FindOrigainlPublisherDifferences(
           List<OriginalPublisher> recsOriginalPublishers, List<Snapshot_ComposerOriginalPublisher> snapshotOriginalPublishers)
        {
            var listOfChanges = new List<RecsProductChanges>();

            if (recsOriginalPublishers.Count != snapshotOriginalPublishers.Count)
            {
                //Get Writer Ids
                var snapshotOriginalPublisherIpCodes = snapshotOriginalPublishers.Select(_ => _.IpCode).ToList();
                var recsOriginalPublisherIpCodes = recsOriginalPublishers.Select(_ => _.IpCode).ToList();

                //find which writers were added and removed
                var originalPublisherRemovedIpCodes = recsOriginalPublisherIpCodes.Except(snapshotOriginalPublisherIpCodes).ToList();
                var originalPublisherAddedIpCodes = snapshotOriginalPublisherIpCodes.Except(recsOriginalPublisherIpCodes).ToList();

                //log added or removed writers
                listOfChanges.AddRange(_productChangeLogService.OriginalPublishersAddedToRecs(recsOriginalPublishers, originalPublisherAddedIpCodes));
                listOfChanges.AddRange(_productChangeLogService.OriginalPublishersRemovedFromRecs(snapshotOriginalPublishers, originalPublisherRemovedIpCodes));

                //Clean added or removed writer
                //Remove added/removed writers from list
                recsOriginalPublishers = CleanRecsOriginalPublishers(recsOriginalPublishers, originalPublisherAddedIpCodes);
                snapshotOriginalPublishers = CleanSnapshotsComposerOriginalPublishers(snapshotOriginalPublishers, originalPublisherRemovedIpCodes);
            }

            for (var i = 0; i < recsOriginalPublishers.Count; i++)
            {
                var recOriginalPublisher = recsOriginalPublishers[i];
                var snapshotOriginalPublisher = snapshotOriginalPublishers[i];

                //check name
                if (recOriginalPublisher.FullName != snapshotOriginalPublisher.FullName)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Name was Changed " + recOriginalPublisher.FullName, "Original Publisher", "Original Publisher Name", snapshotOriginalPublisher.FullName, recOriginalPublisher.FullName));
                }

                //check CapacityCode
                if (recOriginalPublisher.CapacityCode != snapshotOriginalPublisher.CapacityCode)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher CapacityCode was Changed " + recOriginalPublisher.CapacityCode, "Original Publisher", "Original Publisher CapacityCode", snapshotOriginalPublisher.CapacityCode, recOriginalPublisher.CapacityCode));
                }
                //check capacity
                if (recOriginalPublisher.Capacity != snapshotOriginalPublisher.Capacity)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Capacity was Changed " + recOriginalPublisher.Capacity, "Original Publisher", "Original Publisher Capacity", snapshotOriginalPublisher.Capacity, recOriginalPublisher.Capacity));
                }
                //check controlled
                if (recOriginalPublisher.Controlled != snapshotOriginalPublisher.Controlled)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Controlled Status was Changed " + recOriginalPublisher.Controlled.ToString(), "Original Publisher", "Original Publisher Controlled Status ", snapshotOriginalPublisher.Controlled.ToString(), recOriginalPublisher.Controlled.ToString()));
                }

                //---------------------

                //check nested admins
                //Not Implemented
                //check nested nested affilations
                //check nested nested nested affilationbases

                //check nested affiliations
                listOfChanges.AddRange(FindAffiliationDifferences(snapshotOriginalPublisher.Affiliation, recOriginalPublisher.Affiliation));
            }

            return listOfChanges;
        }
        private List<RecsProductChanges> FindAffiliationDifferences(List<Snapshot_OriginalPublisherAffiliation> snapshotAffiliations,
            List<Affiliation> recsAffiliations)
        {
            var listOfChanges = new List<RecsProductChanges>();

            if (recsAffiliations.Count != snapshotAffiliations.Count)
            {
                //Get Writer Ids
                var snapshotOPAffilationIncomeGroup = snapshotAffiliations.Select(_ => _.IncomeGroup).ToList();
                var recsOPAffilationIncomeGroup = recsAffiliations.Select(_ => _.IncomeGroup).ToList();

                //find which writers were added and removed
                var originalPublisherRemovedIncomeGroups = recsOPAffilationIncomeGroup.Except(snapshotOPAffilationIncomeGroup).ToList();
                var originalPublisherAddedIncomeGroups = snapshotOPAffilationIncomeGroup.Except(recsOPAffilationIncomeGroup).ToList();

                //log added or removed writers
                listOfChanges.AddRange(_productChangeLogService.OriginalPublisherAffiliationAddedToRecs(recsAffiliations, originalPublisherAddedIncomeGroups));
                listOfChanges.AddRange(_productChangeLogService.OriginalPublisherAffiliationRemovedFromRecs(snapshotAffiliations, originalPublisherRemovedIncomeGroups));

                //Clean added or removed writer
                //Remove added/removed writers from list
                recsAffiliations = CleanRecsOriginalPublishersAffiliations(recsAffiliations, originalPublisherAddedIncomeGroups);
                snapshotAffiliations = CleanRecsOriginalPublishersAffiliations(snapshotAffiliations, originalPublisherRemovedIncomeGroups);
            }

            //check nested affilationBase
            for (var i = 0; i < recsAffiliations.Count; i++)
            {
                var recAffilation = recsAffiliations[i];
                var snapshotAffilation = snapshotAffiliations[i];

                //check affilation base
                listOfChanges.AddRange(FindAffiliationBaseChanges(recAffilation.Affiliations, snapshotAffilation.Affiliations));
            }

            return listOfChanges;
        }

        private List<RecsProductChanges> FindAffiliationDifferences(List<Snapshot_ComposerOriginalPublisherAffiliation> snapshotAffiliations,
          List<Affiliation> recsAffiliations)
        {
            var listOfChanges = new List<RecsProductChanges>();

            if (recsAffiliations.Count != snapshotAffiliations.Count)
            {
                //Get Writer Ids
                var snapshotOPAffilationIncomeGroup = snapshotAffiliations.Select(_ => _.IncomeGroup).ToList();
                var recsOPAffilationIncomeGroup = recsAffiliations.Select(_ => _.IncomeGroup).ToList();

                //find which writers were added and removed
                var originalPublisherRemovedIncomeGroups = recsOPAffilationIncomeGroup.Except(snapshotOPAffilationIncomeGroup).ToList();
                var originalPublisherAddedIncomeGroups = snapshotOPAffilationIncomeGroup.Except(recsOPAffilationIncomeGroup).ToList();

                //log added or removed writers
                listOfChanges.AddRange(_productChangeLogService.OriginalPublisherAffiliationAddedToRecs(recsAffiliations, originalPublisherAddedIncomeGroups));
                listOfChanges.AddRange(_productChangeLogService.OriginalPublisherAffiliationRemovedFromRecs(snapshotAffiliations, originalPublisherRemovedIncomeGroups));

                //Clean added or removed writer
                //Remove added/removed writers from list
                recsAffiliations = CleanRecsOriginalPublishersAffiliations(recsAffiliations, originalPublisherAddedIncomeGroups);
                snapshotAffiliations = CleanRecsOriginalPublishersAffiliations(snapshotAffiliations, originalPublisherRemovedIncomeGroups);
            }

            //check nested affilationBase
            for (var i = 0; i < recsAffiliations.Count; i++)
            {
                var recAffilation = recsAffiliations[i];
                var snapshotAffilation = snapshotAffiliations[i];

                //check affilation base
                listOfChanges.AddRange(FindAffiliationBaseChanges(recAffilation.Affiliations, snapshotAffilation.Affiliations));
            }

            return listOfChanges;
        }

        private List<RecsProductChanges> FindAffiliationBaseChanges(
            List<AffiliationBase> recsOriginalPubAffiliationBases,
            List<Snapshot_OriginalPubAffiliationBase> snapshotOriginalPubAffiliationBases)
        {
            var listOfChanges = new List<RecsProductChanges>();
            if (snapshotOriginalPubAffiliationBases.Count != recsOriginalPubAffiliationBases.Count)
            {
                //Get Writer Ids
                var snapshotOPAffilationSociertAcronym =
                    snapshotOriginalPubAffiliationBases.Select(_ => _.SocietyAcronym).ToList();
                var recsOPAffilationSocietyAcronym =
                    recsOriginalPubAffiliationBases.Select(_ => _.SocietyAcronym).ToList();

                //find which writers were added and removed
                var originalPublisherRemovedSocietyAcronym =
                    recsOPAffilationSocietyAcronym.Except(snapshotOPAffilationSociertAcronym).ToList();
                var originalPublisherAddedSocietyAcronym =
                    snapshotOPAffilationSociertAcronym.Except(recsOPAffilationSocietyAcronym).ToList();

                //log added or removed writers
                listOfChanges.AddRange(
                    _productChangeLogService.AffiliationBaseAddedToRecs(recsOriginalPubAffiliationBases,
                        originalPublisherAddedSocietyAcronym));
                listOfChanges.AddRange(
                    _productChangeLogService.AffiliationBaseRemovedFromRecs(snapshotOriginalPubAffiliationBases,
                        originalPublisherRemovedSocietyAcronym));

                //Clean added or removed writer
                //Remove added/removed writers from list
                recsOriginalPubAffiliationBases = CleanRecsAffiliationsBases(recsOriginalPubAffiliationBases,
                    originalPublisherAddedSocietyAcronym);
                snapshotOriginalPubAffiliationBases = CleanSnapshotAffiliationsBases(snapshotOriginalPubAffiliationBases,
                    originalPublisherRemovedSocietyAcronym);
            }

            for (var i = 0; i < snapshotOriginalPubAffiliationBases.Count; i++)
            {
                var snapshotOpAffBase = snapshotOriginalPubAffiliationBases[i];
                var recsOriginalPubBase = recsOriginalPubAffiliationBases[i];

                if (snapshotOpAffBase.StartDate != recsOriginalPubBase.StartDate)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Controlled Affiliation Base Start Date was Changed ", "Original Publisher Affiliaton Base", "Original Publisher Affiliaton Base - Start Date ", snapshotOpAffBase.StartDate.ToString(), recsOriginalPubBase.StartDate.ToString()));
                }

                if (snapshotOpAffBase.EndDate != recsOriginalPubBase.EndDate)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Controlled Affiliation Base End Date was Changed ", "Original Publisher Affiliaton Base", "Original Publisher Affiliaton Base - End Date ", snapshotOpAffBase.EndDate.ToString(), recsOriginalPubBase.EndDate.ToString()));
                }
            }

            return listOfChanges;
        }
        private List<RecsProductChanges> FindComposerAffiliationBaseChanges(
          List<AffiliationBase> recsOriginalPubAffiliationBases,
          List<Snapshot_ComposerOriginalPublisherAffiliationBase> snapshotOriginalPubAffiliationBases)
        {
            var listOfChanges = new List<RecsProductChanges>();
            if (snapshotOriginalPubAffiliationBases.Count != recsOriginalPubAffiliationBases.Count)
            {
                //Get Writer Ids
                var snapshotOPAffilationSociertAcronym =
                    snapshotOriginalPubAffiliationBases.Select(_ => _.SocietyAcronym).ToList();
                var recsOPAffilationSocietyAcronym =
                    recsOriginalPubAffiliationBases.Select(_ => _.SocietyAcronym).ToList();

                //find which writers were added and removed
                var originalPublisherRemovedSocietyAcronym =
                    recsOPAffilationSocietyAcronym.Except(snapshotOPAffilationSociertAcronym).ToList();
                var originalPublisherAddedSocietyAcronym =
                    snapshotOPAffilationSociertAcronym.Except(recsOPAffilationSocietyAcronym).ToList();

                //log added or removed writers
                listOfChanges.AddRange(
                    _productChangeLogService.AffiliationBaseAddedToRecs(recsOriginalPubAffiliationBases,
                        originalPublisherAddedSocietyAcronym));
                listOfChanges.AddRange(
                    _productChangeLogService.AffiliationBaseRemovedFromRecs(snapshotOriginalPubAffiliationBases,
                        originalPublisherRemovedSocietyAcronym));

                //Clean added or removed writer
                //Remove added/removed writers from list
                recsOriginalPubAffiliationBases = CleanRecsAffiliationsBases(recsOriginalPubAffiliationBases,
                    originalPublisherAddedSocietyAcronym);
                snapshotOriginalPubAffiliationBases = CleanRecsComposerAffiliationsBases(snapshotOriginalPubAffiliationBases,
                    originalPublisherRemovedSocietyAcronym);
            }

            for (var i = 0; i < snapshotOriginalPubAffiliationBases.Count; i++)
            {
                var snapshotOpAffBase = snapshotOriginalPubAffiliationBases[i];
                var recsOriginalPubBase = recsOriginalPubAffiliationBases[i];

                if (snapshotOpAffBase.StartDate != recsOriginalPubBase.StartDate)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Controlled Affiliation Base Start Date was Changed ", "Original Publisher Affiliaton Base", "Original Publisher Affiliaton Base - Start Date ", snapshotOpAffBase.StartDate.ToString(), recsOriginalPubBase.StartDate.ToString()));
                }

                if (snapshotOpAffBase.EndDate != recsOriginalPubBase.EndDate)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Controlled Affiliation Base End Date was Changed ", "Original Publisher Affiliaton Base", "Original Publisher Affiliaton Base - End Date ", snapshotOpAffBase.EndDate.ToString(), recsOriginalPubBase.EndDate.ToString()));
                }
            }

            return listOfChanges;
        }
        private List<RecsProductChanges> FindProductHeaderDifferences(List<LicenseProduct> licenseProducts,
            List<Snapshot_LicenseProduct> snapshotLicenseProducts)
        {
            var listOfChanges = new List<RecsProductChanges>();

            var mechsProductHeaders = GetSnapshotProductHeaders(snapshotLicenseProducts);
            var recsProductHeaders = GetRecsProductHeader(licenseProducts);
            for (var i = 0; i < mechsProductHeaders.Count; i++)
            {
                var snapshotProductHeader = mechsProductHeaders[i];
                var recProductHeader = recsProductHeaders[i];

                //check for title difference
                if (snapshotProductHeader.Title != recProductHeader.Title)
                {
                    listOfChanges.Add(LogRecsProductChanges("Product Header Title was Changed", "Product Header Title", "Product Header Title", snapshotProductHeader.Title, recProductHeader.Title));
                }

                //check album art URL
                if (snapshotProductHeader.AlbumArtUrl != recProductHeader.AlbumArtUrl)
                {
                    listOfChanges.Add(LogRecsProductChanges("Product Header Album Art URL Changed", "Product Header Album Art URL", "Product Header AlbumArt URL", snapshotProductHeader.AlbumArtUrl, recProductHeader.AlbumArtUrl));
                }
                //Check nested Artist
                if (snapshotProductHeader.Artist != null && recProductHeader.Artist != null)
                {
                    if (snapshotProductHeader.Artist.Name != recProductHeader.Artist.name)
                    {
                        listOfChanges.Add(FindArtistDifferences(recProductHeader.Artist, snapshotProductHeader.Artist));
                    }
                }

                //Check nested RecordLabel
                if (snapshotProductHeader.Label != null && recProductHeader.Label != null)
                {
                    if (snapshotProductHeader.Label.Name != recProductHeader.Label.name)
                    {
                        listOfChanges.Add(FindRecordLabelDifferences(recProductHeader.Label, snapshotProductHeader.Label));
                    }

                    if (snapshotProductHeader.Label.RecordLabelGroups != null &&
                        recProductHeader.Label.recordLabelGroups != null)
                    {
                        if (snapshotProductHeader.Label.RecordLabelGroups[0].Name !=
                            recProductHeader.Label.recordLabelGroups[0].Name)
                        {
                            listOfChanges.Add(FindRecordLabelGroupDifferences(recProductHeader.Label.recordLabelGroups[0], snapshotProductHeader.Label.RecordLabelGroups[0]));
                        }
                    }
                }

                //Check nested Configuratuions
                if (snapshotProductHeader.Configurations != null && recProductHeader.Configurations != null)
                {
                    //Check for added or removed configurations
                    var snapshotRecordingIds = snapshotProductHeader.Configurations.Select(_ => _.CloneRecsConfigurationId).ToList();
                    var recsRecordingIds = recProductHeader.Configurations.Select(_ => Convert.ToInt32(_.configuration_id)).ToList();

                    var configurationIdsRemovedFromRecs = recsRecordingIds.Except(snapshotRecordingIds).ToList();
                    var configurationIdsAddedToRecs = snapshotRecordingIds.Except(recsRecordingIds).ToList();

                    listOfChanges.AddRange(_productChangeLogService.ConfigurationAddedToRecs(recProductHeader.Configurations, configurationIdsAddedToRecs));
                    listOfChanges.AddRange(_productChangeLogService.ConfigurationRemovedFromRecs(snapshotProductHeader.Configurations, configurationIdsRemovedFromRecs));

                    //Clean (remove added or removed configurations)
                    var recsConfigurtaions = CleanRecsConfigurations(recProductHeader.Configurations, configurationIdsAddedToRecs);
                    var snapshotConfigurations = CleanSnapshotRecsConfigurations(snapshotProductHeader.Configurations, configurationIdsRemovedFromRecs);

                    //check for differences
                    for (var j = 0; j < recsConfigurtaions.Count; j++)
                    {
                        var recsConfiguration = recsConfigurtaions[j];
                        var snapshotConfiguration = snapshotConfigurations[j];

                        //check UPC
                        if (recsConfiguration.UPC != snapshotConfiguration.UPC)
                        {
                            listOfChanges.Add(LogRecsProductChanges("UPC Code was changed on configuration.", "UPC Code has been changed", "UPC Code", snapshotConfiguration.UPC, recsConfiguration.UPC));
                        }

                        //check nested Configuration type
                        if (recsConfiguration.Configuration != null && snapshotConfiguration.Configuration != null)
                        {
                            if ((recsConfiguration.Configuration.name != snapshotConfiguration.Configuration.Name) ||
                                (recsConfiguration.Configuration.type != snapshotConfiguration.Configuration.Type))
                            {
                                listOfChanges.Add(LogRecsProductChanges("Configuration Details have changed on configuration. UPC Code:" + recsConfiguration.UPC, "Configuration Details have been changed", "Configuration Details", snapshotConfiguration.Configuration.Name + " " + snapshotConfiguration.Configuration.Type, recsConfiguration.Configuration.name + " " + recsConfiguration.Configuration.type));
                            }
                        }
                    }
                }
            }
            return listOfChanges;
        }

        private List<RecsProductChanges> FindTrackDifferences(List<WorksTrack> recsTracks,
            List<Snapshot_WorksTrack> snapshotWorksTracks)
        {
            var listOfChanges = new List<RecsProductChanges>();
            for (var i = 0; i < recsTracks.Count; i++)
            {
                var snapshotTrack = snapshotWorksTracks[i];
                var recsTrack = recsTracks[i];

                //Find artist differences
                if (recsTrack.Artists.name != snapshotTrack.Artist.Name)
                {
                    listOfChanges.Add(FindArtistDifferences(recsTrack.Artists, snapshotTrack.Artist));
                }

                //Check for track title change
                if (recsTrack.Title != snapshotTrack.Title)
                {
                    listOfChanges.Add(LogRecsProductChanges("Track Title has been changed to " + recsTrack.Title, "Track Title Changed", "Track Title", snapshotTrack.Title, recsTrack.Title));
                }

                //check for track duration change
                if (recsTrack.Duration != snapshotTrack.Duration)
                {
                    listOfChanges.Add(LogRecsProductChanges("Track Duration has been changed to " + recsTrack.Duration, "Track Duration Changed", "Track Duration", snapshotTrack.Duration, recsTrack.Duration));
                }

                //check for isrc change
                if (recsTrack.Isrc != snapshotTrack.Isrc)
                {
                    listOfChanges.Add(LogRecsProductChanges("Track Isrc has been changed to " + recsTrack.Isrc, "Track Isrc Changed", "Track Isrc", snapshotTrack.Isrc, recsTrack.Isrc));
                }

                //Dive into Copyrights and check for changes
                listOfChanges.AddRange(FindCopyRightDifferences(recsTrack.Copyrights, snapshotTrack.Copyrights));
            }

            return listOfChanges;
        }


        private List<RecsProductChanges> FindCopyRightDifferences(List<RecsCopyrights> recsCopyrights,
            List<Snapshot_RecsCopyright> snapshotRecsCopyrights)
        {
            var listOfChanges = new List<RecsProductChanges>();
            if (recsCopyrights.Count != snapshotRecsCopyrights.Count)
            {
                //Check for added or removed copyrights
                var snapshotCopyrightsWorkCodes = recsCopyrights.Select(_ => _.WorkCode).ToList();
                var recCopyrightsWorkCodes = snapshotRecsCopyrights.Select(_ => _.WorkCode).ToList();

                var copyrightWorkcodesRemovedFromRecs =
                    recCopyrightsWorkCodes.Except(snapshotCopyrightsWorkCodes).ToList();
                var copyrightWorkcodesAddedToRecs = snapshotCopyrightsWorkCodes.Except(recCopyrightsWorkCodes).ToList();

                listOfChanges.AddRange(_productChangeLogService.CopyrightsAddedToRecs(recsCopyrights,
                    copyrightWorkcodesAddedToRecs));
                listOfChanges.AddRange(_productChangeLogService.CopyrightsRemovedFromRecs(snapshotRecsCopyrights,
                    copyrightWorkcodesRemovedFromRecs));

                //Clean (remove added or removed copyrights)
                recsCopyrights = CleanCopyrights(recsCopyrights, copyrightWorkcodesAddedToRecs);
                snapshotRecsCopyrights = CleanSnapshotsCopyrights(snapshotRecsCopyrights,
                    copyrightWorkcodesRemovedFromRecs);
            }

            for (var i = 0; i < recsCopyrights.Count; i++)
            {
                var recsCopyright = recsCopyrights[i];
                var snapshotCopyright = snapshotRecsCopyrights[i];

                //check title
                if (recsCopyright.Title != snapshotCopyright.Title)
                {
                    listOfChanges.Add(LogRecsProductChanges("Copyright Title has been changed to " + recsCopyright.Title, "Copyright Title Changed", "Copyright Title", snapshotCopyright.Title, recsCopyright.Title));
                }
                //check principle artists
                if (recsCopyright.PrincipalArtist != snapshotCopyright.PrincipalArtist)
                {
                    listOfChanges.Add(LogRecsProductChanges("Copyright PrincipalArtist has been changed to " + recsCopyright.PrincipalArtist, "Copyright PrincipalArtist Changed", "Copyright PrincipalArtist", snapshotCopyright.PrincipalArtist, recsCopyright.PrincipalArtist));
                }
                //check writers
                if (recsCopyright.Writers != snapshotCopyright.Writers)
                {
                    listOfChanges.Add(LogRecsProductChanges("Copyright Writers has been changed to " + recsCopyright.Writers, "Copyright Writers Changed", "Copyright Writers", snapshotCopyright.Writers, recsCopyright.Writers));
                }

                //check writer string
                if (recsCopyright.WriteString != snapshotCopyright.WriteString)
                {
                    listOfChanges.Add(LogRecsProductChanges("Copyright WriteString has been changed to " + recsCopyright.WriteString, "Copyright WriteString Changed", "Copyright WriteString", snapshotCopyright.WriteString, recsCopyright.WriteString));
                }
                //chek sampled works (TODO:not implemented)
                

                //-------------
                //check local Clients
                listOfChanges.AddRange(CheckForLocalClientChanges(recsCopyright.LocalClients, snapshotCopyright.LocalClients));
                //check deal owning locations
                listOfChanges.AddRange(CheckForDealOwningLocaitonChanges(recsCopyright.AquisitionLocationCode, snapshotCopyright.AquisitionLocationCodes));
                //cechk composers
                listOfChanges.AddRange(CheckForComposerChanges(recsCopyright.Composers, snapshotCopyright.Composers));


            }




        return listOfChanges;
        }

        private List<RecsProductChanges> CheckForComposerChanges(List<WorksWriter> recsComposersList,
            List<Snapshot_Composer> snapshotComposers)
        {
            var listOfChanges = new List<RecsProductChanges>();
            if (recsComposersList.Count != snapshotComposers.Count)
            {
                //Check for added or removed copyrights
                var snapshotCopyrightsWorkCodes = recsComposersList.Select(_ => _.IpCode).ToList();
                var recCopyrightsWorkCodes = snapshotComposers.Select(_ => _.IpCode).ToList();

                var copyrightWorkcodesRemovedFromRecs =
                    recCopyrightsWorkCodes.Except(snapshotCopyrightsWorkCodes).ToList();
                var copyrightWorkcodesAddedToRecs = snapshotCopyrightsWorkCodes.Except(recCopyrightsWorkCodes).ToList();

                listOfChanges.AddRange(_productChangeLogService.ComposersAddedToRecs(recsComposersList,
                    copyrightWorkcodesAddedToRecs));
                listOfChanges.AddRange(_productChangeLogService.ComposerRemovedFromRecs(snapshotComposers,
                    copyrightWorkcodesRemovedFromRecs));

                //Clean (remove added or removed copyrights)
                recsComposersList = CleanComposers(recsComposersList, copyrightWorkcodesAddedToRecs);
                snapshotComposers = CleanComposers(snapshotComposers,
                    copyrightWorkcodesRemovedFromRecs);
            }

            for (int i = 0; i < recsComposersList.Count; i++)
            {

                var recComposer = recsComposersList[i];
                var snapshotComposer = snapshotComposers[i];
                //check name
                if (recComposer.FullName != snapshotComposer.FullName)
                {
                    listOfChanges.Add(LogRecsProductChanges("Composer Name was Changed " + recComposer.FullName, "Composer", "Composer Name", snapshotComposer.FullName, recComposer.FullName));
                }

                //check CapacityCode
                if (recComposer.CapacityCode != snapshotComposer.CapacityCode)
                {
                    listOfChanges.Add(LogRecsProductChanges("Composer CapacityCode was Changed " + recComposer.CapacityCode, "Composer", "Composer CapacityCode", snapshotComposer.CapacityCode, recComposer.CapacityCode));
                }
                //check capacity
                if (recComposer.Capacity != snapshotComposer.Capacity)
                {
                    listOfChanges.Add(LogRecsProductChanges("Composer Capacity was Changed " + recComposer.Capacity, "Composer", "Composer Capacity", snapshotComposer.Capacity, recComposer.Capacity));
                }
                //check controlled
                if (recComposer.Controlled != snapshotComposer.Controlled)
                {
                    listOfChanges.Add(LogRecsProductChanges("Composer Controlled Status was Changed " + recComposer.Controlled.ToString(), "Composer", "Composer Controlled Status ", snapshotComposer.Controlled.ToString(), recComposer.Controlled.ToString()));
                }


                //check original publisers
                listOfChanges.AddRange(FindOrigainlPublisherDifferences(recComposer.OriginalPublishers, snapshotComposer.OriginalPublishers));
                //check affilaitons
            }


            return listOfChanges;
        }

        private List<RecsProductChanges> CheckForLocalClientChanges(List<LocalClientCopyright> recsLocalClients,
            List<Snapshot_LocalClientCopyright> snapshotLocalClients)
        {
            var listOfChanges = new List<RecsProductChanges>();

            //Check for added or removed copyrights
            var snapshotCopyrightsWorkCodes = recsLocalClients.Select(_ => _.ClientCode).ToList();
            var recCopyrightsWorkCodes = snapshotLocalClients.Select(_ => _.ClientCode).ToList();

            var copyrightWorkcodesRemovedFromRecs =
                recCopyrightsWorkCodes.Except(snapshotCopyrightsWorkCodes).ToList();
            var copyrightWorkcodesAddedToRecs = snapshotCopyrightsWorkCodes.Except(recCopyrightsWorkCodes).ToList();

            listOfChanges.AddRange(_productChangeLogService.LocalClientAddedToRecs(recsLocalClients,
                copyrightWorkcodesAddedToRecs));
            listOfChanges.AddRange(_productChangeLogService.LocalClientRemovedFromRecs(snapshotLocalClients,
                copyrightWorkcodesRemovedFromRecs));

            //Clean (remove added or removed copyrights)
            recsLocalClients = CleanLocalClient(recsLocalClients, copyrightWorkcodesAddedToRecs);
            snapshotLocalClients = CleanLocalClient(snapshotLocalClients,
                copyrightWorkcodesRemovedFromRecs);


            for (int i = 0; i < recsLocalClients.Count; i++)
            {
                var recLocalClient = recsLocalClients[i];
                var snapshotLocalClient = snapshotLocalClients[i];


                if (recLocalClient.ClientName != snapshotLocalClient.ClientName)
                {
                    listOfChanges.Add(LogRecsProductChanges("Local ClientName Local ClientName Status was Changed " + recLocalClient.ClientName.ToString(), "Local ClientName", "Local ClientName Controlled Status ", snapshotLocalClient.ClientName.ToString(), recLocalClient.ClientName.ToString()));
                }
            }

            return listOfChanges;
        }


        private List<RecsProductChanges> CheckForDealOwningLocaitonChanges(List<string> recsLocationCodes,
            List<Snapshot_AquisitionLocationCode> snapshotAquisitionLocationCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();

            //Check for added or removed copyrights
            var recsLocaitonCodes = recsLocationCodes.Select(_ => _).ToList();
            var snapshotLocationCodes = snapshotAquisitionLocationCodes.Select(_ => _.AquisitionLocationCode).ToList();

            var copyrightWorkcodesRemovedFromRecs =
                recsLocaitonCodes.Except(snapshotLocationCodes).ToList();
            var copyrightWorkcodesAddedToRecs = snapshotLocationCodes.Except(recsLocaitonCodes).ToList();

            listOfChanges.AddRange(_productChangeLogService.LocationCodeAddedToRecs(recsLocationCodes,
                copyrightWorkcodesAddedToRecs));
            listOfChanges.AddRange(_productChangeLogService.LocationCodeRemovedFromRecs(snapshotAquisitionLocationCodes,
                copyrightWorkcodesRemovedFromRecs));

            return listOfChanges;
        }
        private RecsProductChanges LogRecsProductChanges(string changeMessage, string propertyLocation,
            string propertyChanged, string originalValue, string newValue)
        {
            var newChange = new RecsProductChanges();
            newChange.PropertyLocation = propertyLocation;
            newChange.ChangeMessage = changeMessage;
            newChange.OriginalValue = originalValue;
            newChange.ChangedValue = newValue;
            newChange.PropertyChanged = propertyChanged;
            return newChange;
        }

        private RecsProductChanges FindArtistDifferences(ArtistRecs recsArtist, Snapshot_ArtistRecs snapshotArtist)
        {
            var newChange = new RecsProductChanges();

            newChange.ChangeMessage = "The Artist Name has been changed";
            newChange.PropertyLocation = "Artist Name";
            newChange.OriginalValue = snapshotArtist.Name;
            newChange.ChangedValue = recsArtist.name;
            newChange.PropertyChanged = "Artist Name";

            return newChange;
        }

        private RecsProductChanges FindRecordLabelDifferences(Label recsArtist, Snapshot_Label snapshotArtist)
        {
            var newChange = new RecsProductChanges();

            newChange.ChangeMessage = "The Record Label Name has been changed";
            newChange.PropertyLocation = "Record Label Name";
            newChange.OriginalValue = snapshotArtist.Name;
            newChange.ChangedValue = recsArtist.name;
            newChange.PropertyChanged = "Record Label Name";

            return newChange;
        }

        private RecsProductChanges FindRecordLabelGroupDifferences(LabelGroup recsLabelGroup, Snapshot_LabelGroup snapshotLabelGroup)
        {
            var newChange = new RecsProductChanges();

            newChange.ChangeMessage = "The Record Label Group Name has been changed";
            newChange.PropertyLocation = "Record Label Group Name";
            newChange.OriginalValue = snapshotLabelGroup.Name;
            newChange.ChangedValue = recsLabelGroup.Name;
            newChange.PropertyChanged = "Record Label Group Name";

            return newChange;
        }

        private List<RecsProductChanges> CheckForAddedRemovedRecordings(List<LicenseProduct> recsLicenseProducts,
            List<Snapshot_LicenseProduct> licenseProductsSnapshots)
        {
            var listOfChanges = new List<RecsProductChanges>();

            return listOfChanges;
        }

        private List<ProductHeader> GetRecsProductHeader(List<LicenseProduct> recsLicenseProducts)
        {
            var listOfTracks = new List<ProductHeader>();
            foreach (var recsRecording in recsLicenseProducts)
            {
                if (recsRecording.ProductHeader != null)
                {
                    listOfTracks.Add(recsRecording.ProductHeader);
                }
            }
            return listOfTracks;
        }

        private List<Snapshot_ProductHeader> GetSnapshotProductHeaders(List<Snapshot_LicenseProduct> snapshotLicenseProducts)
        {
            var listOfTracks = new List<Snapshot_ProductHeader>();
            foreach (var recsRecording in snapshotLicenseProducts)
            {
                if (recsRecording.ProductHeader != null)
                {
                    listOfTracks.Add(recsRecording.ProductHeader);
                }
            }
            return listOfTracks;
        }

        private List<WorksTrack> GetRecsTracks(List<WorksRecording> recsRecordings)
        {
            var listOfTracks = new List<WorksTrack>();
            foreach (var recsRecording in recsRecordings)
            {
                if (recsRecording.Track != null)
                {
                    listOfTracks.Add(recsRecording.Track);
                }
            }
            return listOfTracks;
        }

        private List<WorksWriter> GetRecsWriters(List<WorksRecording> recsRecordings)
        {
            var listOfTracks = new List<WorksWriter>();
            foreach (var recsRecording in recsRecordings)
            {
                if (recsRecording.Writers != null)
                {
                    foreach (var writer in recsRecording.Writers)
                    {
                        listOfTracks.Add(writer);
                    }
                }
            }
            return listOfTracks;
        }

        private List<Snapshot_WorksWriter> GetSnapshotWriters(List<Snapshot_WorksRecording> recsRecordings)
        {
            var listOfTracks = new List<Snapshot_WorksWriter>();
            foreach (var recsRecording in recsRecordings)
            {
                if (recsRecording.Writers != null)
                {
                    foreach (var writer in recsRecording.Writers)
                    {
                        listOfTracks.Add(writer);
                    }
                }
            }
            return listOfTracks;
        }

        private List<Snapshot_WorksTrack> GetSnapshotTracks(List<Snapshot_WorksRecording> recsRecordings)
        {
            var listOfTracks = new List<Snapshot_WorksTrack>();
            foreach (var recsRecording in recsRecordings)
            {
                if (recsRecording.Track != null)
                {
                    listOfTracks.Add(recsRecording.Track);
                }
            }
            return listOfTracks;
        }

        private List<LicenseProduct> CleanProducts(List<LicenseProduct> list,
            List<int> ids)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (ids.Contains(list[i].ProductId))
                {
                    list.Remove(list[i]);
                }
            }

            return list;
        }

        private List<Snapshot_LicenseProduct> CleanProductsnapshots(List<Snapshot_LicenseProduct> list,
            List<int> ids)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (ids.Contains((int)list[i].ProductId))
                {
                    list.Remove(list[i]);
                }
            }

            return list;
        }

        private List<Snapshot_WorksRecording> CleanWorksRecordingSnapshots(List<Snapshot_WorksRecording> list, List<int> ids)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (ids.Contains((int)list[i].CloneTrackId))
                {
                    list.Remove(list[i]);
                }
            }

            return list;
        }

        private List<Snapshot_WorksWriter> CleanSnapshotWriters(List<Snapshot_WorksWriter> list, List<int> ids)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (ids.Contains((int)list[i].CloneCaeNumber))
                {
                    list.Remove(list[i]);
                }
            }

            return list;
        }

  private List<RecsProductChanges> FindAffiliationBaseChanges(
            List<AffiliationBase> recsOriginalPubAffiliationBases,
            List<Snapshot_ComposerOriginalPublisherAffiliationBase> snapshotOriginalPubAffiliationBases)
        {
            var listOfChanges = new List<RecsProductChanges>();
            if (snapshotOriginalPubAffiliationBases.Count != recsOriginalPubAffiliationBases.Count)
            {
                //Get Writer Ids
                var snapshotOPAffilationSociertAcronym =
                    snapshotOriginalPubAffiliationBases.Select(_ => _.SocietyAcronym).ToList();
                var recsOPAffilationSocietyAcronym =
                    recsOriginalPubAffiliationBases.Select(_ => _.SocietyAcronym).ToList();

                //find which writers were added and removed
                var originalPublisherRemovedSocietyAcronym =
                    recsOPAffilationSocietyAcronym.Except(snapshotOPAffilationSociertAcronym).ToList();
                var originalPublisherAddedSocietyAcronym =
                    snapshotOPAffilationSociertAcronym.Except(recsOPAffilationSocietyAcronym).ToList();

                //log added or removed writers
                listOfChanges.AddRange(
                    _productChangeLogService.AffiliationBaseAddedToRecs(recsOriginalPubAffiliationBases,
                        originalPublisherAddedSocietyAcronym));
                listOfChanges.AddRange(
                    _productChangeLogService.AffiliationBaseRemovedFromRecs(snapshotOriginalPubAffiliationBases,
                        originalPublisherRemovedSocietyAcronym));

                //Clean added or removed writer
                //Remove added/removed writers from list
                recsOriginalPubAffiliationBases = CleanRecsAffiliationsBases(recsOriginalPubAffiliationBases,
                    originalPublisherAddedSocietyAcronym);
                snapshotOriginalPubAffiliationBases = CleanRecsComposerAffiliationsBases(snapshotOriginalPubAffiliationBases,
                    originalPublisherRemovedSocietyAcronym);
            }

            for (var i = 0; i < snapshotOriginalPubAffiliationBases.Count; i++)
            {
                var snapshotOpAffBase = snapshotOriginalPubAffiliationBases[i];
                var recsOriginalPubBase = recsOriginalPubAffiliationBases[i];

                if (snapshotOpAffBase.StartDate != recsOriginalPubBase.StartDate)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Controlled Affiliation Base Start Date was Changed ", "Original Publisher Affiliaton Base", "Original Publisher Affiliaton Base - Start Date ", snapshotOpAffBase.StartDate.ToString(), recsOriginalPubBase.StartDate.ToString()));
                }

                if (snapshotOpAffBase.EndDate != recsOriginalPubBase.EndDate)
                {
                    listOfChanges.Add(LogRecsProductChanges("Original Publisher Controlled Affiliation Base End Date was Changed ", "Original Publisher Affiliaton Base", "Original Publisher Affiliaton Base - End Date ", snapshotOpAffBase.EndDate.ToString(), recsOriginalPubBase.EndDate.ToString()));
                }
            }

            return listOfChanges;
        }

        private List<AffiliationBase> CleanRecsAffiliationsBases(List<AffiliationBase> list, List<string> ids)
        {
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ids.Contains(list[i].SocietyAcronym))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<Snapshot_OriginalPubAffiliationBase> CleanSnapshotAffiliationsBases(List<Snapshot_OriginalPubAffiliationBase> list, List<string> ids)
        {
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ids.Contains(list[i].SocietyAcronym))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<Snapshot_ComposerOriginalPublisherAffiliationBase> CleanRecsComposerAffiliationsBases(List<Snapshot_ComposerOriginalPublisherAffiliationBase> list, List<string> ids)
        {
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ids.Contains(list[i].SocietyAcronym))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<Affiliation> CleanRecsOriginalPublishersAffiliations(List<Affiliation> list, List<string> ids)
        {
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ids.Contains(list[i].IncomeGroup))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<Snapshot_OriginalPublisherAffiliation> CleanRecsOriginalPublishersAffiliations(List<Snapshot_OriginalPublisherAffiliation> list, List<string> ids)
        {
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ids.Contains(list[i].IncomeGroup))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }


        private List<Snapshot_ComposerOriginalPublisherAffiliation> CleanRecsOriginalPublishersAffiliations(List<Snapshot_ComposerOriginalPublisherAffiliation> list, List<string> ids)
        {
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ids.Contains(list[i].IncomeGroup))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<OriginalPublisher> CleanRecsOriginalPublishers(List<OriginalPublisher> list, List<string> ids)
        {
            var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (listOfIds.Contains(Convert.ToInt32(list[i].IpCode)))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<Snapshot_RecsCopyright> CleanSnapshotsCopyrights(List<Snapshot_RecsCopyright> list, List<string> ids)
        {
            var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (listOfIds.Contains(Convert.ToInt32(list[i].WorkCode)))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<RecsCopyrights> CleanCopyrights(List<RecsCopyrights> list, List<string> ids)
        {
            var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            if (ids.Count > 0)
            {
                if (ids.Count > 0)
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (listOfIds.Contains(Convert.ToInt32(list[i].WorkCode)))
                        {
                            list.Remove(list[i]);
                        }
                    }
                }
            }
            return list;
        }

        private List<LocalClientCopyright> CleanLocalClient(List<LocalClientCopyright> list, List<string> ids)
        {
            var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (listOfIds.Contains(Convert.ToInt32(list[i])))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }


        private List<Snapshot_LocalClientCopyright> CleanLocalClient(List<Snapshot_LocalClientCopyright> list, List<string> ids)
        {
            var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (listOfIds.Contains(Convert.ToInt32(list[i])))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<WorksWriter> CleanComposers(List<WorksWriter> list, List<string> ids)
        {
            var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (listOfIds.Contains(Convert.ToInt32(list[i].IpCode)))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }
        private List<Snapshot_Composer> CleanComposers(List<Snapshot_Composer> list, List<string> ids)
        {
            var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (listOfIds.Contains(Convert.ToInt32(list[i].IpCode)))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }


        private List<Snapshot_OriginalPublisher> CleanSnapshotsOriginalPublishers(List<Snapshot_OriginalPublisher> list, List<string> ids)
        {
            var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (listOfIds.Contains(Convert.ToInt32(list[i].IpCode)))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }
        private List<Snapshot_ComposerOriginalPublisher> CleanSnapshotsComposerOriginalPublishers(List<Snapshot_ComposerOriginalPublisher> list, List<string> ids)
        {
            var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (listOfIds.Contains(Convert.ToInt32(list[i].IpCode)))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<WorksWriter> CleanRecsWriters(List<WorksWriter> list, List<int> ids)
        {
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ids.Contains((int) list[i].CaeNumber))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<WorksRecording> CleanWorksRecordings(List<WorksRecording> list, List<int> ids)
        {
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ids.Contains((int) list[i].TrackId))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<RecsConfiguration> CleanRecsConfigurations(List<RecsConfiguration> list, List<int> ids)
        {
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ids.Contains((int) list[i].configuration_id))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<Snapshot_RecsConfiguration> CleanSnapshotRecsConfigurations(List<Snapshot_RecsConfiguration> list, List<int> ids)
        {
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (ids.Contains((int) list[i].CloneRecsConfigurationId))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            return list;
        }

        private List<Snapshot_WorksRecording> GetAllSnapshotWorksRecordings(
            List<Snapshot_LicenseProduct> licenseProductSnapshots)
        {
            var listOfRecordings = new List<Snapshot_WorksRecording>();
            foreach (var licenseProductSnapshot in licenseProductSnapshots)
            {
                listOfRecordings.AddRange(licenseProductSnapshot.Recordings);
            }
            return listOfRecordings;
        }

        private List<WorksRecording> GetAllWorksRecordings(
            List<LicenseProduct> licenseProductSnapshots)
        {
            var listOfRecordings = new List<WorksRecording>();
            foreach (var licenseProductSnapshot in licenseProductSnapshots)
            {
                listOfRecordings.AddRange(licenseProductSnapshot.Recordings);
            }
            return listOfRecordings;
        }
    }
}