using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class RecCongruencyCheckService : IRecCongruencyCheckService
    {
        private readonly IRecsProductChangeLogService _productChangeLogService;
        private readonly ISnapshotLicenseProductManager _snapshotLicenseProductManager;
        private readonly ISnapshotWorksRecordingManager _snapshotWorksRecordingManager;
        private readonly ISnapshotProductHeaderManager _snapshotProductHeaderManager;

        public RecCongruencyCheckService(IRecsProductChangeLogService recsProductChangeLogService, ISnapshotProductHeaderManager snapshotProductHeaderManager,
            ISnapshotLicenseProductManager snapshotLicenseProductManager,
            ISnapshotWorksRecordingManager snapshotWorksRecordingManager)
        {
            _snapshotProductHeaderManager = snapshotProductHeaderManager;
            _snapshotWorksRecordingManager = snapshotWorksRecordingManager;
            _snapshotLicenseProductManager = snapshotLicenseProductManager;
            _productChangeLogService = recsProductChangeLogService;
        }

        public List<RecsProductChanges> CheckSnapshotAgainstRecs(List<LicenseProduct> mechsLicenseProducts,
            List<Snapshot_LicenseProduct> licenseProductSnapshots)
        {
            //Start code_____________
            var listOfChanges = new List<RecsProductChanges>();

            //check for product changes
            listOfChanges.AddRange(CheckForLicenseProductChanges(mechsLicenseProducts, licenseProductSnapshots));

            return listOfChanges;
        }

        private List<RecsProductChanges> CheckForLicenseProductChanges(List<LicenseProduct> recsLicenseProducts,
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
            listOfChanges.AddRange(_productChangeLogService.RecordingRemovedFromRecs(snapshotRecordings,
                recordingIdsRemovedFromRecs));

            //Clean added or removed recordings
            //Remove added/removed recordings from list
            recsRecordings = CleanWorksRecordings(recsRecordings, recordingIdsAddedToRecs);
            snapshotRecordings = CleanWorksRecordingSnapshots(snapshotRecordings, recordingIdsRemovedFromRecs);




            //--New Writer Area
            recsRecordings = recsRecordings.OrderBy(_ => _.Track.Id).ToList();
            snapshotRecordings = snapshotRecordings.OrderBy(_ => _.Track.CloneWorksTrackId).ToList();


            var larger = "";
            var count = 0;
            if (recsRecordings.Count > snapshotRecordings.Count)
            {
                count = recsRecordings.Count;
                larger = "recs";
            }
            else
            {

                count = snapshotRecordings.Count;
                larger = "snapshot";
            }

            if (larger == "snapshot")
            {
                foreach (var firstSnapshotRecording in snapshotRecordings)
                {
                    var recording = firstSnapshotRecording;
                    var firstRecsRecording =
                        recsRecordings.FirstOrDefault(_ => _.Track.Id == recording.Track.CloneWorksTrackId);
                    if (firstRecsRecording == null)
                    {
                        continue;
                    }
                    else
                    {
                        //find worksRecording differences
                        listOfChanges.AddRange(FindWorksRecordingDifferences(firstRecsRecording, firstSnapshotRecording));

                        if (firstSnapshotRecording.Track.Copyrights == null ||
                            firstSnapshotRecording.Track.Copyrights.Count == 0)
                        {
                            firstSnapshotRecording.Track.Copyrights = new List<Snapshot_RecsCopyright>();
                            var newCopyRight = new Snapshot_RecsCopyright
                            {
                                WorkCode = ""
                            };
                            firstSnapshotRecording.Track.Copyrights.Add(newCopyRight);
                        }
                        if (firstRecsRecording.Track.Copyrights == null ||
                            firstRecsRecording.Track.Copyrights.Count == 0)
                        {
                            firstRecsRecording.Track.Copyrights = new List<RecsCopyrights>();
                            var newCopyRight = new RecsCopyrights
                            {
                                WorkCode = ""
                            };
                            firstRecsRecording.Track.Copyrights.Add(newCopyRight);
                        }


                        //Check Control
                        if (firstSnapshotRecording.Track.Controlled != firstRecsRecording.Track.Controlled)
                        {
                            var product =
                                _snapshotLicenseProductManager.GetProductForTrackId(
                                    firstSnapshotRecording.SnapshotWorkTrackId);
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Track Control Status Changed on track \"" + firstSnapshotRecording.Track.Title +
                                    "\" on Product: " + product.Title,
                                    "Workcode", "Workcode",
                                    "Workcode: " + firstSnapshotRecording.Track.Copyrights[0].WorkCode +
                                    " \n Controlled: " +
                                    firstSnapshotRecording.Track.Controlled.ToString(),
                                    "Workcode: " + firstSnapshotRecording.Track.Copyrights[0].WorkCode +
                                    " \n Controlled: " + firstRecsRecording.Track.Controlled.ToString()));


                        }
                        else
                        {


                            //find writer differences
                            //If workcode was changed in recs (different workcode in snapshot than in recs), then a different number of writers are added to it.
                            //Currently we dont check the added/removed writers
                            if (firstRecsRecording.Track.Copyrights != null &&
                                firstSnapshotRecording.Track.Copyrights != null)
                            {
                                if (firstSnapshotRecording.Track.Copyrights[0].WorkCode ==
                                    firstRecsRecording.Track.Copyrights[0].WorkCode)
                                {
                                    if (firstSnapshotRecording.Writers != null && firstRecsRecording.Writers != null)
                                    {
                                        listOfChanges.AddRange(FindWriterDifferences(firstRecsRecording.Writers,
                                            firstSnapshotRecording.Writers, firstSnapshotRecording));
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                foreach (var firstRecsRecording in recsRecordings)
                {
                    var recording = firstRecsRecording;
                    var firstSnapshotRecording =
                        snapshotRecordings.FirstOrDefault(_ => _.Track.CloneWorksTrackId == recording.Track.Id);
                    if (firstSnapshotRecording == null)
                    {
                        continue;
                    }
                    else
                    {
                        //find worksRecording differences
                        listOfChanges.AddRange(FindWorksRecordingDifferences(firstRecsRecording, firstSnapshotRecording));


                        //Check Control
                        if (firstSnapshotRecording.Track.Controlled != firstRecsRecording.Track.Controlled)
                        {
                            var product =
                                _snapshotLicenseProductManager.GetProductForTrackId(
                                    firstSnapshotRecording.SnapshotWorkTrackId);

                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Track Control Status Changed on track " + firstSnapshotRecording.Track.Title +
                                    "on Product: " + product.Title,
                                    "Track Control Status", "Track Control Status",
                                    firstSnapshotRecording.Track.Controlled.ToString(),
                                    firstRecsRecording.Track.Controlled.ToString()));


                        }
                        else
                        {


                            //find writer differences
                            //If workcode was changed in recs (different workcode in snapshot than in recs), then a different number of writers are added to it.
                            //Currently we dont check the added/removed writers
                            if (firstRecsRecording.Track.Copyrights != null &&
                                firstSnapshotRecording.Track.Copyrights != null)
                            {
                                if (firstSnapshotRecording.Track.Copyrights[0].WorkCode ==
                                    firstRecsRecording.Track.Copyrights[0].WorkCode)
                                {
                                    if (firstSnapshotRecording.Writers != null && firstRecsRecording.Writers != null)
                                    {
                                        listOfChanges.AddRange(FindWriterDifferences(firstRecsRecording.Writers,
                                            firstSnapshotRecording.Writers, firstSnapshotRecording));
                                    }
                                }
                            }
                        }
                    }
                }
            }




            //-------Track Area---------
            var recsTracks = GetRecsTracks(recsRecordings);
            var snapshotTracks = GetSnapshotTracks(snapshotRecordings);

            var snapshotTrackIds = snapshotTracks.Select(_ => _.CloneWorksTrackId).ToList();
            var recsTrackIds = recsTracks.Select(_ => _.Id).ToList();

            var trackIdsAddedToRecs = recsTrackIds.Except(snapshotTrackIds).ToList();
            var trackIdsRemovedFromRecs = snapshotTrackIds.Except(recsTrackIds).ToList();

            //create message for added/removed tracks
            //Temp off, the other trackDifference function does this better
            //listOfChanges.AddRange(_productChangeLogService.TracksAddedToRecs(recsTracks, trackIdsAddedToRecs));
            //listOfChanges.AddRange(_productChangeLogService.TracksRemovedFromRecs(snapshotTracks,
            //    trackIdsRemovedFromRecs));

            //find track differences
            listOfChanges.AddRange(FindTrackDifferences(recsTracks, snapshotTracks));

            //---------Writer Area------------
            var recsWriters = GetRecsWriters(recsRecordings);
            var snapshotWriters = GetSnapshotWriters(snapshotRecordings);

            ////find writer differences
            //  listOfChanges.AddRange(FindWriterDifferences(recsWriters, snapshotWriters));  //Turned off temp, we check above
            return listOfChanges;
        }

        private List<RecsProductChanges> FindWorksRecordingDifferences(WorksRecording recsWorksRecording,
            Snapshot_WorksRecording snapshotWorksRecording)
        {
            var listOfChanges = new List<RecsProductChanges>();

            if (recsWorksRecording.CdNumber != snapshotWorksRecording.CdNumber)
            {
                listOfChanges.Add(
                    LogRecsProductChanges(
                        "CD Number was Changed on " + recsWorksRecording.Track.Title +
                        " on all products on this license", "CD Number", "Cd Number",
                        snapshotWorksRecording.CdNumber.ToString(), recsWorksRecording.CdNumber.ToString()));
            }


            if (recsWorksRecording.CdIndex != snapshotWorksRecording.CdIndex)
            {
                listOfChanges.Add(
                    LogRecsProductChanges(
                        "Track Index  was Changed on " + recsWorksRecording.Track.Title +
                        " on all products on this license", "Track Index", "Track Index",
                        snapshotWorksRecording.CdIndex.ToString(), recsWorksRecording.CdIndex.ToString()));
            }





            return listOfChanges;





        }

        private List<RecsProductChanges> FindWriterDifferences(List<WorksWriter> recsWriters,
            List<Snapshot_WorksWriter> snapshotWriters, Snapshot_WorksRecording snapshotWorksRecording)
        {
            var listOfChanges = new List<RecsProductChanges>();

            var info = _snapshotWorksRecordingManager.GetRecordingInfoForSnapshotRecordingId(snapshotWorksRecording);
            //Get Writer Ids

            var snapshotWriterCaeCodes = snapshotWriters.Select(_ => _.CloneCaeNumber).ToList();
            var recsWriterCaeCodes = recsWriters.Select(_ => _.CaeNumber).ToList();

            //find which writers were added and removed
            var writerCaeCodesRemovedFromRecs = snapshotWriterCaeCodes.Except(recsWriterCaeCodes).ToList();
            var writerCaeCodesAddedToRecs = recsWriterCaeCodes.Except(snapshotWriterCaeCodes).ToList();


            //log added or removed writers
            listOfChanges.AddRange(_productChangeLogService.WritersAddedToRecs(recsWriters, writerCaeCodesAddedToRecs,
                snapshotWorksRecording));
            listOfChanges.AddRange(_productChangeLogService.WritersRemovedFromRecs(snapshotWriters,
                writerCaeCodesRemovedFromRecs, snapshotWorksRecording));

            //Clean added or removed writer
            //Remove added/removed writers from list
            recsWriters = CleanRecsWriters(recsWriters, writerCaeCodesAddedToRecs);
            snapshotWriters = CleanSnapshotWriters(snapshotWriters, writerCaeCodesRemovedFromRecs);

            recsWriters = recsWriters.OrderBy(_ => _.CaeNumber).ToList();
            snapshotWriters = snapshotWriters.OrderBy(_ => _.CloneCaeNumber).ToList();

            var larger = "";
            if (recsWriters.Count > snapshotWriters.Count)
            {
                larger = "recs";
            }
            else
            {
                larger = "snapshot";
            }

            if (larger == "snapshot")
            {
                foreach (var snapshotWriter in snapshotWriters)
                {
                    var recsWriter = recsWriters.FirstOrDefault(_ => _.CaeNumber == snapshotWriter.CloneCaeNumber);
                    if (recsWriter == null)
                    {
                        continue;
                    }
                    else
                    {


                        //check name
                        if (recsWriter.FullName != snapshotWriter.FullName)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Writer Name was Changed on track: " + info.SnapshotWorksTrack.Title +
                                    " / product: " + info.SnapshotProductHeader.Title, "Writer", "Writer Name",
                                    snapshotWriter.FullName, recsWriter.FullName));
                        }

                        //check caeNumber
                        if (recsWriter.CaeNumber != snapshotWriter.CloneCaeNumber)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Writer Cae Number was Changed on track: " + info.SnapshotWorksTrack.Title +
                                    " / product: " + info.SnapshotProductHeader.Title, "Cae Number",
                                    "Writer CAE number", snapshotWriter.CloneCaeNumber.ToString(),
                                    recsWriter.CaeNumber.ToString()));
                        }

                        //check capacity code
                        if (recsWriter.CapacityCode != snapshotWriter.CapacityCode)
                        {
                            listOfChanges.Add(LogRecsProductChanges(
                                "Writer Capacity Code was Changed on " + snapshotWriter.FullName + " on track: " +
                                info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title,
                                "Writer Capacity Code",
                                "Capacity Code", snapshotWriter.CapacityCode, recsWriter.CapacityCode));
                        }

                        //check capacity
                        if (recsWriter.Capacity != snapshotWriter.Capacity)
                        {
                            listOfChanges.Add(LogRecsProductChanges(
                                "Writer Capacity  was Changed on " + snapshotWriter.FullName + " on track: " +
                                info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title,
                                "Writer Capacity ", "Capacity ", snapshotWriter.Capacity, recsWriter.Capacity));
                        }
                        //check contrlled
                        if (recsWriter.Controlled != snapshotWriter.Controlled)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Writer Controlled Status  was Changed on " + snapshotWriter.FullName +
                                    " on track: " + info.SnapshotWorksTrack.Title + " / product: " +
                                    info.SnapshotProductHeader.Title,
                                    "Writer Controlled Status ", "Writer Controlled Status ",
                                    ControlledHelper(snapshotWriter.Controlled), ControlledHelper(recsWriter.Controlled)));
                        }

                        //check contribution
                        if (recsWriter.Contribution != null && snapshotWriter.Contribution != null)
                        {
                            if ((int) Math.Floor((double) recsWriter.Contribution) !=
                                (int) Math.Floor((double) snapshotWriter.Contribution))
                            {
                                listOfChanges.Add(LogRecsProductChanges(
                                    "Writer contribution  was changed on: " + snapshotWriter.FullName + " on track: " +
                                    info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title,
                                    "Writer contribution ",
                                    "Writer Contribution/Split ", snapshotWriter.Contribution.ToString() + "%",
                                    recsWriter.Contribution.ToString() + "%"));
                            }
                        }
                        //check ipCode
                        if (recsWriter.IpCode != snapshotWriter.IpCode)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Writer IpCode  was Changed on " + snapshotWriter.FullName + " on track: " +
                                    info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title,
                                    "Writer IpCode ", "IpCode ", snapshotWriter.IpCode, recsWriter.IpCode));
                        }
                        //check affilationsString
                        //if (recsWriter.AffiliationsString != snapshotWriter.AffiliationsString)
                        //{
                        //    listOfChanges.Add(LogRecsProductChanges("Writer AffiliationsString  was Changed on " + snapshotWriter.FullName, "Writer AffiliationsString Changed", "AffiliationsString ", snapshotWriter.AffiliationsString, recsWriter.AffiliationsString));
                        //}

                        //------------------
                        //check nested Original Publishers
                        listOfChanges.AddRange(FindOrigainlPublisherDifferences(recsWriter.OriginalPublishers,
                            snapshotWriter.OriginalPublishers, snapshotWorksRecording));

                        //check nested affilations
                    }
                }
            }
            else
            {
                foreach (var recsWriter in recsWriters)
                {
                    var snapshotWriter = snapshotWriters.FirstOrDefault(_ => _.CloneCaeNumber == recsWriter.CaeNumber);
                    if (snapshotWriter == null)
                    {
                        continue;
                    }
                    else
                    {
                        //check name
                        if (recsWriter.FullName != snapshotWriter.FullName)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Writer Name was Changed on track: " + info.SnapshotWorksTrack.Title +
                                    " / product: " + info.SnapshotProductHeader.Title, "Writer", "Writer Name",
                                    snapshotWriter.FullName, recsWriter.FullName));
                        }

                        //check caeNumber
                        if (recsWriter.CaeNumber != snapshotWriter.CloneCaeNumber)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Writer Cae Number was Changed on track: " + info.SnapshotWorksTrack.Title +
                                    " / product: " + info.SnapshotProductHeader.Title, "Cae Number",
                                    "Writer CAE number", snapshotWriter.CloneCaeNumber.ToString(),
                                    recsWriter.CaeNumber.ToString()));
                        }

                        //check capacity code
                        if (recsWriter.CapacityCode != snapshotWriter.CapacityCode)
                        {
                            listOfChanges.Add(LogRecsProductChanges(
                                "Writer Capacity Code was Changed on " + snapshotWriter.FullName + " on track: " +
                                info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title,
                                "Writer Capacity Code",
                                "Capacity Code", snapshotWriter.CapacityCode, recsWriter.CapacityCode));
                        }

                        //check capacity
                        if (recsWriter.Capacity != snapshotWriter.Capacity)
                        {
                            listOfChanges.Add(LogRecsProductChanges(
                                "Writer Capacity  was Changed on " + snapshotWriter.FullName + " on track: " +
                                info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title,
                                "Writer Capacity ", "Capacity ", snapshotWriter.Capacity, recsWriter.Capacity));
                        }
                        //check contrlled
                        if (recsWriter.Controlled != snapshotWriter.Controlled)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Writer Controlled Status  was Changed on " + snapshotWriter.FullName +
                                    " on track: " + info.SnapshotWorksTrack.Title + " / product: " +
                                    info.SnapshotProductHeader.Title, "Writer Controlled Status ",
                                    "Writer Controlled Status ", snapshotWriter.Controlled.ToString(),
                                    recsWriter.Controlled.ToString()));
                        }

                        //check contribution
                        if (recsWriter.Contribution != null && snapshotWriter.Contribution != null)
                        {
                            if ((int) Math.Floor((double) recsWriter.Contribution) !=
                                (int) Math.Floor((double) snapshotWriter.Contribution))
                            {
                                listOfChanges.Add(LogRecsProductChanges(
                                    "Writer contribution  was changed on: " + snapshotWriter.FullName + " on track: " +
                                    info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title,
                                    "Writer contribution ",
                                    "Writer Contribution/Split ", snapshotWriter.Contribution.ToString() + "%",
                                    recsWriter.Contribution.ToString() + "%"));
                            }
                        }
                        //check ipCode
                        if (recsWriter.IpCode != snapshotWriter.IpCode)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Writer IpCode  was Changed on " + snapshotWriter.FullName + " on track: " +
                                    info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title,
                                    "Writer IpCode ", "IpCode ", snapshotWriter.IpCode, recsWriter.IpCode));
                        }
                        //check affilationsString
                        //if (recsWriter.AffiliationsString != snapshotWriter.AffiliationsString)
                        //{
                        //    listOfChanges.Add(LogRecsProductChanges("Writer AffiliationsString  was Changed on " + snapshotWriter.FullName, "Writer AffiliationsString Changed", "AffiliationsString ", snapshotWriter.AffiliationsString, recsWriter.AffiliationsString));
                        //}

                        //------------------
                        //check nested Original Publishers
                        listOfChanges.AddRange(FindOrigainlPublisherDifferences(recsWriter.OriginalPublishers,
                            snapshotWriter.OriginalPublishers, snapshotWorksRecording));

                        //check nested affilations
                    }
                }
            }



            return listOfChanges;
        }

        private List<RecsProductChanges> FindOrigainlPublisherDifferences(
            List<OriginalPublisher> recsOriginalPublishers, List<Snapshot_OriginalPublisher> snapshotOriginalPublishers,
            Snapshot_WorksRecording worksRecording)
        {
            var listOfChanges = new List<RecsProductChanges>();

            var info = _snapshotWorksRecordingManager.GetRecordingInfoForSnapshotRecordingId(
                worksRecording);

            //Get Writer Ids
            var snapshotOriginalPublisherIpCodes = snapshotOriginalPublishers.Select(_ => _.IpCode).ToList();
            var recsOriginalPublisherIpCodes = recsOriginalPublishers.Select(_ => _.IpCode).ToList();

            //find which writers were added and removed
            var originalPublisherAddedIpCodes =
                recsOriginalPublisherIpCodes.Except(snapshotOriginalPublisherIpCodes).ToList();
            var originalPublisherRemovedIpCodes =
                snapshotOriginalPublisherIpCodes.Except(recsOriginalPublisherIpCodes).ToList();



            //log added or removed writers
            listOfChanges.AddRange(_productChangeLogService.OriginalPublishersAddedToRecs(recsOriginalPublishers,
                originalPublisherAddedIpCodes, worksRecording));
            listOfChanges.AddRange(_productChangeLogService.OriginalPublishersRemovedFromRecs(
                snapshotOriginalPublishers, originalPublisherRemovedIpCodes, worksRecording));

            //Clean added or removed writer
            //Remove added/removed writers from list
            recsOriginalPublishers = CleanRecsOriginalPublishers(recsOriginalPublishers, originalPublisherAddedIpCodes);
            snapshotOriginalPublishers = CleanSnapshotsOriginalPublishers(snapshotOriginalPublishers,
                originalPublisherRemovedIpCodes);

            var larger = "";
            if (recsOriginalPublishers.Count > snapshotOriginalPublishers.Count)
            {
                larger = "recs";
            }
            else
            {
                larger = "snapshot";
            }
            if (larger == "snapshot")
            {
                foreach (var snapshotOriginalPublisher in snapshotOriginalPublishers)
                {
                    var recOriginalPublisher =
                        recsOriginalPublishers.FirstOrDefault(_ => _.IpCode == snapshotOriginalPublisher.IpCode);
                    if (recOriginalPublisher == null)
                    {
                        continue;
                    }
                    else
                    {
                        //TODO
                        if (recOriginalPublisher.FullName != snapshotOriginalPublisher.FullName)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Name was Changed " + recOriginalPublisher.FullName +
                                    " on track: " + info.SnapshotWorksTrack.Title + "/ product: " +
                                    info.SnapshotProductHeader.Title, "Original Publisher", "Original Publisher Name",
                                    snapshotOriginalPublisher.FullName, recOriginalPublisher.FullName));
                        }

                        //check CapacityCode
                        if (recOriginalPublisher.CapacityCode != snapshotOriginalPublisher.CapacityCode)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher CapacityCode was Changed " + recOriginalPublisher.CapacityCode +
                                    " on track: " + info.SnapshotWorksTrack.Title + "/ product: " +
                                    info.SnapshotProductHeader.Title, "Original Publisher",
                                    "Original Publisher CapacityCode", snapshotOriginalPublisher.CapacityCode,
                                    recOriginalPublisher.CapacityCode));
                        }
                        //check capacity
                        if (recOriginalPublisher.Capacity != snapshotOriginalPublisher.Capacity)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Capacity was Changed " + recOriginalPublisher.Capacity +
                                    " on track: " + info.SnapshotWorksTrack.Title + "/ product: " +
                                    info.SnapshotProductHeader.Title, "Original Publisher",
                                    "Original Publisher Capacity", snapshotOriginalPublisher.Capacity,
                                    recOriginalPublisher.Capacity));
                        }
                        //check controlled
                        if (recOriginalPublisher.Controlled != snapshotOriginalPublisher.Controlled)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Controlled Status was Changed to " +
                                    ControlledHelper(recOriginalPublisher.Controlled) + " on track: " +
                                    info.SnapshotWorksTrack.Title + "/ product: " + info.SnapshotProductHeader.Title,
                                    "Original Publisher", "Original Publisher Controlled Status ",
                                    ControlledHelper(snapshotOriginalPublisher.Controlled),
                                    ControlledHelper(recOriginalPublisher.Controlled)));
                        }



                        //JEFF TODO 12-9

                        //check mechanicalCollectablePercentage
                        //snapshotOriginalPublisher.MechanicalCollectablePercentage =
                        //    snapshotOriginalPublisher.MechanicalCollectablePercentage.Substring(0,
                        //        snapshotOriginalPublisher.MechanicalCollectablePercentage.IndexOf(".", 0));

                        var snapshotOPMechCollectablePercent =
                            Convert.ToInt32(
                                Math.Floor(Decimal.Parse(snapshotOriginalPublisher.MechanicalCollectablePercentage)));

                        var snapshotOPMechanicalOwnershipPercentage =
                            Convert.ToInt32(
                                Math.Floor(Decimal.Parse(snapshotOriginalPublisher.MechanicalOwnershipPercentage)));


                        //snapshotOriginalPublisher.MechanicalOwnershipPercentage = snapshotOriginalPublisher.MechanicalOwnershipPercentage.Substring(0,
                        //        snapshotOriginalPublisher.MechanicalOwnershipPercentage.IndexOf(".", 0));

                        // string recMechColPerString = Convert.ToInt32(recOriginalPublisher.MechanicalCollectablePercentage).ToString().Substring(0,
                        //         recOriginalPublisher.MechanicalCollectablePercentage.ToString().IndexOf(".", 0));
                        // string recMechOwnPerString = Convert.ToInt32(recOriginalPublisher.MechanicalOwnershipPercentage).ToString().Substring(0,
                        //recOriginalPublisher.MechanicalOwnershipPercentage.ToString().IndexOf(".", 0));

                        if (Math.Floor(recOriginalPublisher.MechanicalCollectablePercentage) !=
                            snapshotOPMechCollectablePercent)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Mechanical Collectable Percentage modified " + " on track: " +
                                    info.SnapshotWorksTrack.Title + "/ product: " + info.SnapshotProductHeader.Title,
                                    "Original Publisher", "Original Publisher Collectable Percentage modified ",
                                    snapshotOriginalPublisher.MechanicalCollectablePercentage.ToString() + "%",
                                    recOriginalPublisher.MechanicalCollectablePercentage.ToString("0.#") + "%"));
                        }

                        //check mechanicalOwnershipPercentage
                        if (Math.Floor(recOriginalPublisher.MechanicalOwnershipPercentage) !=
                            snapshotOPMechanicalOwnershipPercentage)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Mechanical Ownership Percentage modified" + " on track: " +
                                    info.SnapshotWorksTrack.Title + "/ product: " + info.SnapshotProductHeader.Title,
                                    "Original Publisher", "Original Publisher Ownership Percentage modified ",
                                    snapshotOriginalPublisher.MechanicalOwnershipPercentage.ToString() + "%",
                                    recOriginalPublisher.MechanicalOwnershipPercentage.ToString("0.#") + "%"));
                        }




                        //---------------------

                        //check nested admins
                        //Not Implemented
                        //check nested nested affilations
                        //check nested nested nested affilationbases

                        //check nested affiliations
                        listOfChanges.AddRange(FindAffiliationDifferences(snapshotOriginalPublisher.Affiliation,
                            recOriginalPublisher.Affiliation));
                    }
                }
            }
            else
            {
                foreach (var recOriginalPublisher in recsOriginalPublishers)
                {
                    var snapshotOriginalPublisher =
                        snapshotOriginalPublishers.FirstOrDefault(_ => _.IpCode == recOriginalPublisher.IpCode);
                    if (snapshotOriginalPublisher == null)
                    {
                        continue;

                    }
                    else
                    {
                        //TODO
                        if (recOriginalPublisher.FullName != snapshotOriginalPublisher.FullName)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Name was Changed " + recOriginalPublisher.FullName,
                                    "Original Publisher", "Original Publisher Name", snapshotOriginalPublisher.FullName,
                                    recOriginalPublisher.FullName));
                        }

                        //check CapacityCode
                        if (recOriginalPublisher.CapacityCode != snapshotOriginalPublisher.CapacityCode)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher CapacityCode was Changed " + recOriginalPublisher.CapacityCode,
                                    "Original Publisher", "Original Publisher CapacityCode",
                                    snapshotOriginalPublisher.CapacityCode, recOriginalPublisher.CapacityCode));
                        }
                        //check capacity
                        if (recOriginalPublisher.Capacity != snapshotOriginalPublisher.Capacity)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Capacity was Changed " + recOriginalPublisher.Capacity,
                                    "Original Publisher", "Original Publisher Capacity",
                                    snapshotOriginalPublisher.Capacity, recOriginalPublisher.Capacity));
                        }
                        //check controlled
                        if (recOriginalPublisher.Controlled != snapshotOriginalPublisher.Controlled)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Controlled Status was Changed to " +
                                    ControlledHelper(recOriginalPublisher.Controlled) + " on track: " +
                                    info.SnapshotWorksTrack.Title + "/ product: " + info.SnapshotProductHeader.Title,
                                    "Original Publisher", "Original Publisher Controlled Status ",
                                    ControlledHelper(snapshotOriginalPublisher.Controlled),
                                    ControlledHelper(recOriginalPublisher.Controlled)));
                        }

                        var snapshotOPMechCollectablePercent =
                            Convert.ToInt32(
                                Math.Floor(Decimal.Parse(snapshotOriginalPublisher.MechanicalCollectablePercentage)));

                        var snapshotOPMechanicalOwnershipPercentage =
                            Convert.ToInt32(
                                Math.Floor(Decimal.Parse(snapshotOriginalPublisher.MechanicalOwnershipPercentage)));


                        //JEFF TODO 12-9
                        /*
                        //check mechanicalCollectablePercentage
                        snapshotOriginalPublisher.MechanicalCollectablePercentage =
                            snapshotOriginalPublisher.MechanicalCollectablePercentage.Substring(0,
                                snapshotOriginalPublisher.MechanicalCollectablePercentage.IndexOf(".", 0));

                        snapshotOriginalPublisher.MechanicalOwnershipPercentage = snapshotOriginalPublisher.MechanicalOwnershipPercentage.Substring(0,
                                snapshotOriginalPublisher.MechanicalOwnershipPercentage.IndexOf(".", 0));

                       // string recMechColPerString = Convert.ToInt32(recOriginalPublisher.MechanicalCollectablePercentage).ToString().Substring(0,
                       //         recOriginalPublisher.MechanicalCollectablePercentage.ToString().IndexOf(".", 0));
                       // string recMechOwnPerString = Convert.ToInt32(recOriginalPublisher.MechanicalOwnershipPercentage).ToString().Substring(0,
                       //recOriginalPublisher.MechanicalOwnershipPercentage.ToString().IndexOf(".", 0));

                        if (Math.Floor(recOriginalPublisher.MechanicalCollectablePercentage).ToString() != snapshotOriginalPublisher.MechanicalCollectablePercentage)
                        {
                            listOfChanges.Add(LogRecsProductChanges("Original Publisher Mechanical Collectable Percentage modified", "Original Publisher", "Original Publisher Collectable Percentage modified ", snapshotOriginalPublisher.MechanicalCollectablePercentage.ToString() + "%", recOriginalPublisher.MechanicalCollectablePercentage.ToString("0.#") + "%"));
                        }

                        //check mechanicalOwnershipPercentage
                        if (Math.Floor(recOriginalPublisher.MechanicalOwnershipPercentage).ToString() != snapshotOriginalPublisher.MechanicalOwnershipPercentage)
                        {
                            listOfChanges.Add(LogRecsProductChanges("Original Publisher Mechanical Ownership Percentage modified", "Original Publisher", "Original Publisher Ownership Percentage modified ", snapshotOriginalPublisher.MechanicalOwnershipPercentage.ToString() + "%", recOriginalPublisher.MechanicalOwnershipPercentage.ToString("0.#") + "%"));
                        }


                        */


                        if (Math.Floor(recOriginalPublisher.MechanicalCollectablePercentage) !=
                            snapshotOPMechCollectablePercent)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Mechanical Collectable Percentage modified" + " on track: " +
                                    info.SnapshotWorksTrack.Title + "/ product: " + info.SnapshotProductHeader.Title,
                                    "Original Publisher", "Original Publisher Collectable Percentage modified ",
                                    snapshotOriginalPublisher.MechanicalCollectablePercentage.ToString() + "%",
                                    recOriginalPublisher.MechanicalCollectablePercentage.ToString("0.#") + "%"));
                        }

                        //check mechanicalOwnershipPercentage
                        if (Math.Floor(recOriginalPublisher.MechanicalOwnershipPercentage) !=
                            snapshotOPMechanicalOwnershipPercentage)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Mechanical Ownership Percentage modified" + " on track: " +
                                    info.SnapshotWorksTrack.Title + "/ product: " + info.SnapshotProductHeader.Title,
                                    "Original Publisher", "Original Publisher Ownership Percentage modified ",
                                    snapshotOriginalPublisher.MechanicalOwnershipPercentage.ToString() + "%",
                                    recOriginalPublisher.MechanicalOwnershipPercentage.ToString("0.#") + "%"));
                        }

                        //---------------------

                        //check nested admins
                        //Not Implemented
                        //check nested nested affilations
                        //check nested nested nested affilationbases

                        //check nested affiliations
                        listOfChanges.AddRange(FindAffiliationDifferences(snapshotOriginalPublisher.Affiliation,
                            recOriginalPublisher.Affiliation));
                    }
                }
            }


            return listOfChanges;
        }

        //Composer not currently used
        //private List<RecsProductChanges> FindOrigainlPublisherDifferences(
        //   List<OriginalPublisher> recsOriginalPublishers, List<Snapshot_ComposerOriginalPublisher> snapshotOriginalPublishers)
        //{
        //    var listOfChanges = new List<RecsProductChanges>();

        //    if (recsOriginalPublishers.Count != snapshotOriginalPublishers.Count)
        //    {
        //        //Get Writer Ids
        //        var snapshotOriginalPublisherIpCodes = snapshotOriginalPublishers.Select(_ => _.IpCode).ToList();
        //        var recsOriginalPublisherIpCodes = recsOriginalPublishers.Select(_ => _.IpCode).ToList();

        //        //find which writers were added and removed
        //        var originalPublisherRemovedIpCodes = recsOriginalPublisherIpCodes.Except(snapshotOriginalPublisherIpCodes).ToList();
        //        var originalPublisherAddedIpCodes = snapshotOriginalPublisherIpCodes.Except(recsOriginalPublisherIpCodes).ToList();

        //        //log added or removed writers
        //        listOfChanges.AddRange(_productChangeLogService.OriginalPublishersAddedToRecs(recsOriginalPublishers, originalPublisherAddedIpCodes));
        //        listOfChanges.AddRange(_productChangeLogService.OriginalPublishersRemovedFromRecs(snapshotOriginalPublishers, originalPublisherRemovedIpCodes));

        //        //Clean added or removed writer
        //        //Remove added/removed writers from list
        //        recsOriginalPublishers = CleanRecsOriginalPublishers(recsOriginalPublishers, originalPublisherAddedIpCodes);
        //        snapshotOriginalPublishers = CleanSnapshotsComposerOriginalPublishers(snapshotOriginalPublishers, originalPublisherRemovedIpCodes);
        //    }

        //    if (recsOriginalPublishers.Count == snapshotOriginalPublishers.Count)
        //    {
        //        for (var i = 0; i < recsOriginalPublishers.Count; i++)
        //        {
        //            var recOriginalPublisher = recsOriginalPublishers[i];
        //            var snapshotOriginalPublisher = snapshotOriginalPublishers[i];

        //            //check name
        //            if (recOriginalPublisher.FullName != snapshotOriginalPublisher.FullName)
        //            {
        //                listOfChanges.Add(
        //                    LogRecsProductChanges(
        //                        "Original Publisher Name was Changed " + recOriginalPublisher.FullName,
        //                        "Original Publisher", "Original Publisher Name", snapshotOriginalPublisher.FullName,
        //                        recOriginalPublisher.FullName));
        //            }

        //            //check CapacityCode
        //            if (recOriginalPublisher.CapacityCode != snapshotOriginalPublisher.CapacityCode)
        //            {
        //                listOfChanges.Add(
        //                    LogRecsProductChanges(
        //                        "Original Publisher CapacityCode was Changed " + recOriginalPublisher.CapacityCode,
        //                        "Original Publisher", "Original Publisher CapacityCode",
        //                        snapshotOriginalPublisher.CapacityCode, recOriginalPublisher.CapacityCode));
        //            }
        //            //check capacity
        //            if (recOriginalPublisher.Capacity != snapshotOriginalPublisher.Capacity)
        //            {
        //                if (snapshotOriginalPublisher.Capacity == null)
        //                {
        //                    continue;
        //                }
        //                else
        //                {
        //                    listOfChanges.Add(
        //                        LogRecsProductChanges(
        //                            "Original Publisher Capacity was Changed " + recOriginalPublisher.Capacity,
        //                            "Original Publisher", "Original Publisher Capacity",
        //                            snapshotOriginalPublisher.Capacity, recOriginalPublisher.Capacity));
        //                }
        //            }
        //            //check controlled
        //            if (recOriginalPublisher.Controlled != snapshotOriginalPublisher.Controlled)
        //            {
        //                listOfChanges.Add(
        //                    LogRecsProductChanges(
        //                        "Original Publisher Controlled Status was Changed " +
        //                        recOriginalPublisher.Controlled.ToString(), "Original Publisher",
        //                        "Original Publisher Controlled Status ", snapshotOriginalPublisher.Controlled.ToString(),
        //                        recOriginalPublisher.Controlled.ToString()));
        //            }

        //            //---------------------

        //            //check nested admins
        //            //Not Implemented
        //            //check nested nested affilations
        //            //check nested nested nested affilationbases

        //            //check nested affiliations
        //            listOfChanges.AddRange(FindAffiliationDifferences(snapshotOriginalPublisher.Affiliation,
        //                recOriginalPublisher.Affiliation));
        //        }
        //    }
        //    return listOfChanges;
        //}

        private List<RecsProductChanges> FindAffiliationDifferences(
            List<Snapshot_OriginalPublisherAffiliation> snapshotAffiliations,
            List<Affiliation> recsAffiliations)
        {
            var listOfChanges = new List<RecsProductChanges>();

            //Get Writer Ids
            var snapshotOPAffilationIncomeGroup = snapshotAffiliations.Select(_ => _.IncomeGroup).ToList();
            var recsOPAffilationIncomeGroup = recsAffiliations.Select(_ => _.IncomeGroup).ToList();

            //find which writers were added and removed
            var originalPublisherRemovedIncomeGroups =
                recsOPAffilationIncomeGroup.Except(snapshotOPAffilationIncomeGroup).ToList();
            var originalPublisherAddedIncomeGroups =
                snapshotOPAffilationIncomeGroup.Except(recsOPAffilationIncomeGroup).ToList();

            //log added or removed writers
            listOfChanges.AddRange(_productChangeLogService.OriginalPublisherAffiliationAddedToRecs(recsAffiliations,
                originalPublisherAddedIncomeGroups));
            listOfChanges.AddRange(
                _productChangeLogService.OriginalPublisherAffiliationRemovedFromRecs(snapshotAffiliations,
                    originalPublisherRemovedIncomeGroups));

            //Clean added or removed writer
            //Remove added/removed writers from list
            recsAffiliations = CleanRecsOriginalPublishersAffiliations(recsAffiliations,
                originalPublisherAddedIncomeGroups);
            snapshotAffiliations = CleanRecsOriginalPublishersAffiliations(snapshotAffiliations,
                originalPublisherRemovedIncomeGroups);


            var larger = "";
            if (snapshotAffiliations.Count > recsAffiliations.Count)
            {
                larger = "snapshot";
            }
            else
            {
                larger = "recs";
            }
            if (larger == "snapshot")
            {
                foreach (var snapshotAffilation in snapshotAffiliations)
                {
                    var recAffilation =
                        recsAffiliations.FirstOrDefault(
                            _ => _.IncomeGroup == snapshotAffilation.IncomeGroup);
                    if (recAffilation == null)
                    {
                        continue;
                    }
                    else
                    {
                        listOfChanges.AddRange(FindAffiliationBaseChanges(recAffilation.Affiliations,
                            snapshotAffilation.Affiliations));
                    }
                }
            }
            else
            {
                foreach (var recAffilation in recsAffiliations)
                {
                    var snapshotAffilation =
                        snapshotAffiliations.FirstOrDefault(
                            _ => _.IncomeGroup == recAffilation.IncomeGroup);
                    if (snapshotAffilation == null)
                    {
                        continue;
                    }
                    else
                    {
                        listOfChanges.AddRange(FindAffiliationBaseChanges(recAffilation.Affiliations,
                            snapshotAffilation.Affiliations));
                    }
                }
            }

            return listOfChanges;
        }

        private List<RecsProductChanges> FindAffiliationDifferences(
            List<Snapshot_ComposerOriginalPublisherAffiliation> snapshotAffiliations,
            List<Affiliation> recsAffiliations)
        {
            var listOfChanges = new List<RecsProductChanges>();

            if (recsAffiliations.Count != snapshotAffiliations.Count)
            {
                //Get Writer Ids
                var snapshotOPAffilationIncomeGroup = snapshotAffiliations.Select(_ => _.IncomeGroup).ToList();
                var recsOPAffilationIncomeGroup = recsAffiliations.Select(_ => _.IncomeGroup).ToList();

                //find which writers were added and removed
                var originalPublisherRemovedIncomeGroups =
                    recsOPAffilationIncomeGroup.Except(snapshotOPAffilationIncomeGroup).ToList();
                var originalPublisherAddedIncomeGroups =
                    snapshotOPAffilationIncomeGroup.Except(recsOPAffilationIncomeGroup).ToList();

                //log added or removed writers
                listOfChanges.AddRange(_productChangeLogService.OriginalPublisherAffiliationAddedToRecs(
                    recsAffiliations, originalPublisherAddedIncomeGroups));
                listOfChanges.AddRange(
                    _productChangeLogService.OriginalPublisherAffiliationRemovedFromRecs(snapshotAffiliations,
                        originalPublisherRemovedIncomeGroups));

                //Clean added or removed writer
                //Remove added/removed writers from list
                recsAffiliations = CleanRecsOriginalPublishersAffiliations(recsAffiliations,
                    originalPublisherAddedIncomeGroups);
                snapshotAffiliations = CleanRecsOriginalPublishersAffiliations(snapshotAffiliations,
                    originalPublisherRemovedIncomeGroups);
            }

            //check nested affilationBase
            //Temp off
            //for (var i = 0; i < recsAffiliations.Count; i++)
            //{
            //    var recAffilation = recsAffiliations[i];
            //    var snapshotAffilation = snapshotAffiliations[i];

            //    //check affilation base
            //    listOfChanges.AddRange(FindAffiliationBaseChanges(recAffilation.Affiliations, snapshotAffilation.Affiliations));
            //}

            return listOfChanges;
        }

        private List<RecsProductChanges> FindAffiliationBaseChanges(
            List<AffiliationBase> recsOriginalPubAffiliationBases,
            List<Snapshot_OriginalPubAffiliationBase> snapshotOriginalPubAffiliationBases)
        {
            var listOfChanges = new List<RecsProductChanges>();

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


            snapshotOriginalPubAffiliationBases =
                snapshotOriginalPubAffiliationBases.OrderBy(_ => _.SocietyAcronym).ToList();
            recsOriginalPubAffiliationBases =
                recsOriginalPubAffiliationBases.OrderBy(_ => _.SocietyAcronym).ToList();



            var larger = "";
            if (snapshotOriginalPubAffiliationBases.Count > recsOriginalPubAffiliationBases.Count)
            {
                larger = "snapshot";
            }
            else
            {
                larger = "recs";
            }
            if (larger == "snapshot")
            {
                foreach (var snapshotOpAffBase in snapshotOriginalPubAffiliationBases)
                {
                    var recsOriginalPubBase =
                        recsOriginalPubAffiliationBases.FirstOrDefault(
                            _ => _.SocietyAcronym == snapshotOpAffBase.SocietyAcronym);
                    if (recsOriginalPubBase == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (snapshotOpAffBase.StartDate != recsOriginalPubBase.StartDate)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Controlled Affiliation Base Start Date was Changed ",
                                    "Original Publisher Affiliaton Base",
                                    "Original Publisher Affiliaton Base - Start Date ",
                                    snapshotOpAffBase.StartDate.Value.ToShortDateString(),
                                    recsOriginalPubBase.StartDate.Value.ToShortDateString()));
                        }

                        if (snapshotOpAffBase.EndDate != recsOriginalPubBase.EndDate)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Controlled Affiliation Base End Date was Changed ",
                                    "Original Publisher Affiliaton Base",
                                    "Original Publisher Affiliaton Base - End Date ",
                                    snapshotOpAffBase.EndDate.Value.ToShortDateString(),
                                    recsOriginalPubBase.EndDate.Value.ToShortDateString()));
                        }
                    }
                }
            }
            else
            {
                foreach (var recsOriginalPubBase in recsOriginalPubAffiliationBases)
                {
                    var snapshotOpAffBase =
                        snapshotOriginalPubAffiliationBases.FirstOrDefault(
                            _ => _.SocietyAcronym == recsOriginalPubBase.SocietyAcronym);
                    if (snapshotOpAffBase == null)
                    {
                        continue;
                    }
                    else
                    {

                        if (snapshotOpAffBase.StartDate != recsOriginalPubBase.StartDate)
                        {
                            var snapshotStartDate = "";
                            var recsStartDate = "";

                            if (snapshotOpAffBase.StartDate != null)
                            {
                                snapshotStartDate = snapshotOpAffBase.StartDate.Value.ToShortDateString();
                            }

                            if (recsOriginalPubBase.StartDate != null)
                            {
                                recsStartDate = recsOriginalPubBase.StartDate.Value.ToShortDateString();
                            }





                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Controlled Affiliation Base Start Date was Changed ",
                                    "Original Publisher Affiliaton Base",
                                    "Original Publisher Affiliaton Base - Start Date ", snapshotStartDate, recsStartDate));
                        }

                        if (snapshotOpAffBase.EndDate != recsOriginalPubBase.EndDate)
                        {
                            var snapshotEndDate = "";
                            var recsEndDate = "";



                            if (snapshotOpAffBase.EndDate != null)
                            {
                                snapshotEndDate = snapshotOpAffBase.EndDate.Value.ToShortDateString();
                            }

                            if (recsOriginalPubBase.EndDate != null)
                            {
                                recsEndDate = recsOriginalPubBase.EndDate.Value.ToShortDateString();
                            }




                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Original Publisher Controlled Affiliation Base End Date was Changed ",
                                    "Original Publisher Affiliaton Base",
                                    "Original Publisher Affiliaton Base - End Date ", snapshotEndDate, recsEndDate));
                        }
                    }
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
                snapshotOriginalPubAffiliationBases =
                    CleanRecsComposerAffiliationsBases(snapshotOriginalPubAffiliationBases,
                        originalPublisherRemovedSocietyAcronym);
            }

            for (var i = 0; i < snapshotOriginalPubAffiliationBases.Count; i++)
            {
                var snapshotOpAffBase = snapshotOriginalPubAffiliationBases[i];
                var recsOriginalPubBase = recsOriginalPubAffiliationBases[i];

                if (snapshotOpAffBase.StartDate != recsOriginalPubBase.StartDate)
                {
                    listOfChanges.Add(
                        LogRecsProductChanges("Original Publisher Controlled Affiliation Base Start Date was Changed ",
                            "Original Publisher Affiliaton Base", "Original Publisher Affiliaton Base - Start Date ",
                            snapshotOpAffBase.StartDate.ToString(), recsOriginalPubBase.StartDate.ToString()));
                }

                if (snapshotOpAffBase.EndDate != recsOriginalPubBase.EndDate)
                {
                    listOfChanges.Add(
                        LogRecsProductChanges("Original Publisher Controlled Affiliation Base End Date was Changed ",
                            "Original Publisher Affiliaton Base", "Original Publisher Affiliaton Base - End Date ",
                            snapshotOpAffBase.EndDate.ToString(), recsOriginalPubBase.EndDate.ToString()));
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



            var larger1 = "";
            if (mechsProductHeaders.Count > recsProductHeaders.Count)
            {
                larger1 = "recs";

            }
            else
            {
                larger1 = "snapshot";
            }
            if (larger1 == "snapshot")
            {
                foreach (var snapshotProductHeader in mechsProductHeaders)
                {
                    var recProductHeader =
                        recsProductHeaders.FirstOrDefault(
                            _ => Convert.ToInt32(_.Id) == snapshotProductHeader.CloneProductHeaderId);
                    if (recProductHeader == null)
                    {
                        continue;
                    }
                    else
                    {


                        if (snapshotProductHeader.Label == null)
                        {
                            var newLabel = new Snapshot_Label();
                            var newLabelGroup = new List<Snapshot_LabelGroup>();
                            newLabel.RecordLabelGroups = newLabelGroup;
                        }

                        if (recProductHeader.Label == null)
                        {
                            var newLabel = new Snapshot_Label();
                            var newLabelGroup = new List<Snapshot_LabelGroup>();
                            newLabel.RecordLabelGroups = newLabelGroup;
                        }

                        //Check for label differences
                        if (snapshotProductHeader.Label == null && recProductHeader.Label != null)
                        {

                            
                            listOfChanges.Add(
                                            LogRecsProductChanges("Label added to Recs on product " + snapshotProductHeader.Title,
                                                "Label Added", "Label added", "N/A",
                                                recProductHeader.Label.name));
                        }





                        //check for title difference
                        if (snapshotProductHeader.Title != recProductHeader.Title)
                        {

                            listOfChanges.Add(LogRecsProductChanges("Product Title was Changed", "Product Title",
                                "Product Title", snapshotProductHeader.Title, recProductHeader.Title));
                        }

                        //check album art URL
                        /*
                        if (snapshotProductHeader.AlbumArtUrl != recProductHeader.AlbumArtUrl)
                        {
                            listOfChanges.Add(LogRecsProductChanges("Product Header Album Art URL Changed", "Product Header Album Art URL", "Product Header AlbumArt URL", snapshotProductHeader.AlbumArtUrl, recProductHeader.AlbumArtUrl));
                        }
                        */
                        //Check nested Artist
                        if (snapshotProductHeader.Artist != null && recProductHeader.Artist != null)
                        {
                            if (snapshotProductHeader.Artist.Name != recProductHeader.Artist.name)
                            {
                                listOfChanges.Add(FindArtistDifferences(recProductHeader.Artist,
                                    snapshotProductHeader.Artist, snapshotProductHeader, recProductHeader));
                            }
                        }

                        //Check nested RecordLabel
                        if (snapshotProductHeader.Label != null && recProductHeader.Label == null)
                        {
                            listOfChanges.Add(FindRecordLabelDifferences(recProductHeader.Label,
                                    snapshotProductHeader.Label));

                        }


                        if (snapshotProductHeader.Label != null && recProductHeader.Label != null)
                        {
                            if (snapshotProductHeader.Label.Name != recProductHeader.Label.name)
                            {
                                listOfChanges.Add(FindRecordLabelDifferences(recProductHeader.Label,
                                    snapshotProductHeader.Label));
                            }

                            if (snapshotProductHeader.Label.RecordLabelGroups != null &&
                                recProductHeader.Label.recordLabelGroups != null &&
                                snapshotProductHeader.Label.RecordLabelGroups.Count >= 1 &&
                                recProductHeader.Label.recordLabelGroups.Count >= 1)
                            {
                                if (snapshotProductHeader.Label.RecordLabelGroups[0].Name !=
                                    recProductHeader.Label.recordLabelGroups[0].Name)
                                {
                                    listOfChanges.Add(
                                        FindRecordLabelGroupDifferences(recProductHeader.Label.recordLabelGroups[0],
                                            snapshotProductHeader.Label.RecordLabelGroups[0]));
                                }
                            }
                        }

                        //Check nested Configuratuions
                        if (snapshotProductHeader.Configurations != null && recProductHeader.Configurations != null)
                        {
                            //Check for added or removed configurations
                            var snapshotConfigIds =
                                snapshotProductHeader.Configurations.Select(_ => _.Configuration.CloneConfigId).ToList();
                            var recConfigIds =
                                recProductHeader.Configurations.Select(_ => Convert.ToInt32(_.Configuration.ConfigId))
                                    .ToList();

                            var configurationIdsAddedToRecs = recConfigIds.Except(snapshotConfigIds).ToList();
                            var configurationIdsRemovedFromRecs = snapshotConfigIds.Except(recConfigIds).ToList();

                            listOfChanges.AddRange(
                                _productChangeLogService.ConfigurationAddedToRecs(recProductHeader.Configurations,
                                    configurationIdsAddedToRecs));
                            listOfChanges.AddRange(
                                _productChangeLogService.ConfigurationRemovedFromRecs(
                                    snapshotProductHeader.Configurations, configurationIdsRemovedFromRecs));

                            //Clean (remove added or removed configurations)
                            var recsConfigurtaions = CleanRecsConfigurations(recProductHeader.Configurations,
                                configurationIdsAddedToRecs);
                            var snapshotConfigurations =
                                CleanSnapshotRecsConfigurations(snapshotProductHeader.Configurations,
                                    configurationIdsRemovedFromRecs);

                            var larger = "";
                            if (recsConfigurtaions.Count > snapshotConfigurations.Count)
                            {
                                larger = "recs";
                            }
                            else
                            {
                                larger = "snapshot";
                            }

                            if (larger == "snapshot")
                            {
                                foreach (var snapshotConfiguration in snapshotConfigurations)
                                {
                                    var recsConfiguration =
                                        recsConfigurtaions.FirstOrDefault(
                                            _ =>
                                                Convert.ToInt32(_.configuration_id) ==
                                                snapshotConfiguration.CloneRecsConfigurationId);
                                    if (recsConfiguration == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (recsConfiguration.UPC == null)
                                        {
                                            recsConfiguration.UPC = "";
                                        }

                                        if (snapshotConfiguration.UPC == null)
                                        {
                                            snapshotConfiguration.UPC = "";
                                        }
                                        //check UPC
                                        if (recsConfiguration.UPC != snapshotConfiguration.UPC)
                                        {
                                            listOfChanges.Add(
                                                LogRecsProductChanges("UPC Code was changed on configuration on product "+ recProductHeader.Title,
                                                    "UPC Code has been changed", "UPC Code", snapshotConfiguration.UPC,
                                                    recsConfiguration.UPC));
                                        }

                                        //Check Config Realease Date
                                        if (recsConfiguration.ReleaseDate != snapshotConfiguration.ReleaseDate)
                                        {
                                            listOfChanges.Add(
                                                LogRecsProductChanges("Release Date was changed on configuration.",
                                                    "Release Date has been changed", "Config Release Date",
                                                    Convert.ToDateTime(snapshotConfiguration.ReleaseDate)
                                                        .ToShortDateString(),
                                                    Convert.ToDateTime(recsConfiguration.ReleaseDate)
                                                        .ToShortDateString()));

                                        }

                                        //check nested Configuration type
                                        if (recsConfiguration.Configuration != null &&
                                            snapshotConfiguration.Configuration != null)
                                        {
                                            if ((recsConfiguration.Configuration.name !=
                                                 snapshotConfiguration.Configuration.Name) ||
                                                (recsConfiguration.Configuration.type !=
                                                 snapshotConfiguration.Configuration.Type))
                                            {
                                                listOfChanges.Add(
                                                    LogRecsProductChanges(
                                                        "Configuration type has changed on UPC: " +
                                                        recsConfiguration.UPC, "Configuration type has been changed",
                                                        "Configuration Type",
                                                        snapshotConfiguration.Configuration.Name + " " +
                                                        snapshotConfiguration.Configuration.Type,
                                                        recsConfiguration.Configuration.name + " " +
                                                        recsConfiguration.Configuration.type));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var recsConfiguration in recsConfigurtaions)
                                {
                                    var snapshotConfiguration =
                                        snapshotConfigurations.FirstOrDefault(
                                            _ =>
                                                Convert.ToInt32(_.CloneRecsConfigurationId) ==
                                                recsConfiguration.configuration_id);
                                    if (snapshotConfiguration == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        //Check for label differences
                                        if (snapshotProductHeader.Label == null && recProductHeader.Label != null)
                                        {
                                            listOfChanges.Add(
                                                            LogRecsProductChanges("Label added to Recs on product " + recProductHeader.Title,
                                                                "Label Added", "Label added", "N/A",
                                                                recProductHeader.Label.name));
                                        }

                                        if (recsConfiguration.UPC == null)
                                        {
                                            recsConfiguration.UPC = "";
                                        }

                                        if (snapshotConfiguration.UPC == null)
                                        {
                                            snapshotConfiguration.UPC = "";
                                        }
                                        //check UPC
                                        if (recsConfiguration.UPC != snapshotConfiguration.UPC)
                                        {
                                            listOfChanges.Add(
                                                LogRecsProductChanges("UPC Code was changed on configuration on product " + recProductHeader.Title,
                                                    "UPC Code has been changed", "UPC Code", snapshotConfiguration.UPC,
                                                    recsConfiguration.UPC));
                                        }

                                        //Check Config Realease Date
                                        if (recsConfiguration.ReleaseDate != snapshotConfiguration.ReleaseDate)
                                        {
                                            listOfChanges.Add(
                                                LogRecsProductChanges("Release Date was changed on configuration.",
                                                    "Release Date has been changed", "Config Release Date",
                                                    Convert.ToDateTime(snapshotConfiguration.ReleaseDate)
                                                        .ToShortDateString(),
                                                    Convert.ToDateTime(recsConfiguration.ReleaseDate.ToString())
                                                        .ToShortDateString()));

                                        }

                                        //check nested Configuration type
                                        if (recsConfiguration.Configuration != null &&
                                            snapshotConfiguration.Configuration != null)
                                        {
                                            if ((recsConfiguration.Configuration.name !=
                                                 snapshotConfiguration.Configuration.Name) ||
                                                (recsConfiguration.Configuration.type !=
                                                 snapshotConfiguration.Configuration.Type))
                                            {
                                                listOfChanges.Add(
                                                    LogRecsProductChanges(
                                                        "Configuration type has changed on UPC: " +
                                                        recsConfiguration.UPC, "Configuration type has been changed",
                                                        "Configuration Type",
                                                        snapshotConfiguration.Configuration.Name + " " +
                                                        snapshotConfiguration.Configuration.Type,
                                                        recsConfiguration.Configuration.name + " " +
                                                        recsConfiguration.Configuration.type));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var recProductHeader in recsProductHeaders)
                {
                    var snapshotProductHeader =
                        mechsProductHeaders.FirstOrDefault(
                            _ => Convert.ToInt32(_.CloneProductHeaderId) == recProductHeader.Id);
                    if (recProductHeader == null)
                    {
                        continue;
                    }
                    else
                    {
                        //check for title difference
                        if (snapshotProductHeader.Title != recProductHeader.Title)
                        {

                            listOfChanges.Add(LogRecsProductChanges("Product Title was Changed", "Product Title",
                                "Product Title", snapshotProductHeader.Title, recProductHeader.Title));
                        }

                        //check album art URL
                        /*
                        if (snapshotProductHeader.AlbumArtUrl != recProductHeader.AlbumArtUrl)
                        {
                            listOfChanges.Add(LogRecsProductChanges("Product Header Album Art URL Changed", "Product Header Album Art URL", "Product Header AlbumArt URL", snapshotProductHeader.AlbumArtUrl, recProductHeader.AlbumArtUrl));
                        }
                        */
                        //Check nested Artist
                        if (snapshotProductHeader.Artist != null && recProductHeader.Artist != null)
                        {
                            if (snapshotProductHeader.Artist.Name != recProductHeader.Artist.name)
                            {
                                listOfChanges.Add(FindArtistDifferences(recProductHeader.Artist,
                                    snapshotProductHeader.Artist, snapshotProductHeader, recProductHeader));
                            }
                        }

                        //Check nested RecordLabel
                        if (snapshotProductHeader.Label == null)
                        {
                            var newLabel = new Snapshot_Label();
                            var newLabelGroup = new List<Snapshot_LabelGroup>();
                            newLabel.RecordLabelGroups = newLabelGroup;
                        }

                        if (recProductHeader.Label == null)
                        {
                            var newLabel = new Snapshot_Label();
                            var newLabelGroup = new List<Snapshot_LabelGroup>();
                            newLabel.RecordLabelGroups = newLabelGroup;
                        }

                        if (snapshotProductHeader.Label != null && recProductHeader.Label != null)
                        {
                            if (snapshotProductHeader.Label.Name != recProductHeader.Label.name)
                            {
                                listOfChanges.Add(FindRecordLabelDifferences(recProductHeader.Label,
                                    snapshotProductHeader.Label));
                            }

                            if (snapshotProductHeader.Label.RecordLabelGroups != null &&
                                recProductHeader.Label.recordLabelGroups != null &&
                                snapshotProductHeader.Label.RecordLabelGroups.Count >= 1 &&
                                recProductHeader.Label.recordLabelGroups.Count >= 1)
                            {
                                if (snapshotProductHeader.Label.RecordLabelGroups[0].Name !=
                                    recProductHeader.Label.recordLabelGroups[0].Name)
                                {
                                    listOfChanges.Add(
                                        FindRecordLabelGroupDifferences(recProductHeader.Label.recordLabelGroups[0],
                                            snapshotProductHeader.Label.RecordLabelGroups[0]));
                                }
                            }
                        }

                        //Check nested Configuratuions
                        if (snapshotProductHeader.Configurations != null && recProductHeader.Configurations != null)
                        {
                            //Check for added or removed configurations
                            var snapshotConfigIds =
                                snapshotProductHeader.Configurations.Select(_ => _.Configuration.CloneConfigId).ToList();
                            var recConfigIds =
                                recProductHeader.Configurations.Select(_ => Convert.ToInt32(_.Configuration.ConfigId))
                                    .ToList();

                            var configurationIdsAddedToRecs = recConfigIds.Except(snapshotConfigIds).ToList();
                            var configurationIdsRemovedFromRecs = snapshotConfigIds.Except(recConfigIds).ToList();

                            listOfChanges.AddRange(
                                _productChangeLogService.ConfigurationAddedToRecs(recProductHeader.Configurations,
                                    configurationIdsAddedToRecs));
                            listOfChanges.AddRange(
                                _productChangeLogService.ConfigurationRemovedFromRecs(
                                    snapshotProductHeader.Configurations, configurationIdsRemovedFromRecs));

                            //Clean (remove added or removed configurations)
                            var recsConfigurtaions = CleanRecsConfigurations(recProductHeader.Configurations,
                                configurationIdsAddedToRecs);
                            var snapshotConfigurations =
                                CleanSnapshotRecsConfigurations(snapshotProductHeader.Configurations,
                                    configurationIdsRemovedFromRecs);

                            var larger = "";
                            if (recsConfigurtaions.Count > snapshotConfigurations.Count)
                            {
                                larger = "recs";
                            }
                            else
                            {
                                larger = "snapshot";
                            }

                            if (larger == "snapshot")
                            {
                                foreach (var snapshotConfiguration in snapshotConfigurations)
                                {
                                    var recsConfiguration =
                                        recsConfigurtaions.FirstOrDefault(
                                            _ =>
                                                Convert.ToInt32(_.Configuration.ConfigId) ==
                                                snapshotConfiguration.Configuration.CloneConfigId);
                                    if (recsConfiguration == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (recsConfiguration.UPC == null)
                                        {
                                            recsConfiguration.UPC = "";
                                        }

                                        if (snapshotConfiguration.UPC == null)
                                        {
                                            snapshotConfiguration.UPC = "";
                                        }

                                        //check UPC
                                        if (recsConfiguration.UPC != snapshotConfiguration.UPC)
                                        {
                                            listOfChanges.Add(
                                                LogRecsProductChanges("UPC Code was changed on configuration for product " + recProductHeader.Title,
                                                    "UPC Code has been changed", "UPC Code", snapshotConfiguration.UPC,
                                                    recsConfiguration.UPC));
                                        }

                                        //Check Config Realease Date
                                        if (recsConfiguration.ReleaseDate != snapshotConfiguration.ReleaseDate)
                                        {
                                            listOfChanges.Add(
                                                LogRecsProductChanges("Release Date was changed on configuration.",
                                                    "Release Date has been changed", "Config Release Date",
                                                    Convert.ToDateTime(snapshotConfiguration.ReleaseDate)
                                                        .ToShortDateString(),
                                                    Convert.ToDateTime(recsConfiguration.ReleaseDate.ToString())
                                                        .ToShortDateString()));

                                        }

                                        //check nested Configuration type
                                        if (recsConfiguration.Configuration != null &&
                                            snapshotConfiguration.Configuration != null)
                                        {
                                            if ((recsConfiguration.Configuration.name !=
                                                 snapshotConfiguration.Configuration.Name) ||
                                                (recsConfiguration.Configuration.type !=
                                                 snapshotConfiguration.Configuration.Type))
                                            {
                                                listOfChanges.Add(
                                                    LogRecsProductChanges(
                                                        "Configuration type has changed on UPC: " +
                                                        recsConfiguration.UPC, "Configuration type has been changed",
                                                        "Configuration Type",
                                                        snapshotConfiguration.Configuration.Name + " " +
                                                        snapshotConfiguration.Configuration.Type,
                                                        recsConfiguration.Configuration.name + " " +
                                                        recsConfiguration.Configuration.type));
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var recsConfiguration in recsConfigurtaions)
                                {
                                    var snapshotConfiguration =
                                        recsConfigurtaions.FirstOrDefault(
                                            _ =>
                                                Convert.ToInt32(_.configuration_id) ==
                                                recsConfiguration.configuration_id);
                                    if (snapshotConfiguration == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (recsConfiguration.UPC == null)
                                        {
                                            recsConfiguration.UPC = "";
                                        }

                                        if (snapshotConfiguration.UPC == null)
                                        {
                                            snapshotConfiguration.UPC = "";
                                        }
                                        //check UPC
                                        if (recsConfiguration.UPC != snapshotConfiguration.UPC)
                                        {
                                            listOfChanges.Add(
                                                LogRecsProductChanges("UPC Code was changed on configuration on product " + recProductHeader.Title,
                                                    "UPC Code has been changed", "UPC Code", snapshotConfiguration.UPC,
                                                    recsConfiguration.UPC));
                                        }

                                        //Check Config Realease Date
                                        if (recsConfiguration.ReleaseDate != snapshotConfiguration.ReleaseDate)
                                        {
                                            listOfChanges.Add(
                                                LogRecsProductChanges("Release Date was changed on configuration.",
                                                    "Release Date has been changed", "Config Release Date",
                                                    Convert.ToDateTime(snapshotConfiguration.ReleaseDate)
                                                        .ToShortDateString(),
                                                    Convert.ToDateTime(recsConfiguration.ReleaseDate.ToString())
                                                        .ToShortDateString()));

                                        }

                                        //check nested Configuration type
                                        if (recsConfiguration.Configuration != null &&
                                            snapshotConfiguration.Configuration != null)
                                        {
                                            if ((recsConfiguration.Configuration.name !=
                                                 snapshotConfiguration.Configuration.name) ||
                                                (recsConfiguration.Configuration.type !=
                                                 snapshotConfiguration.Configuration.type))
                                            {
                                                listOfChanges.Add(
                                                    LogRecsProductChanges(
                                                        "Configuration type has changed on UPC: " +
                                                        recsConfiguration.UPC, "Configuration type has been changed",
                                                        "Configuration Type",
                                                        snapshotConfiguration.Configuration.name + " " +
                                                        snapshotConfiguration.Configuration.type,
                                                        recsConfiguration.Configuration.name + " " +
                                                        recsConfiguration.Configuration.type));
                                            }
                                        }
                                    }
                                }
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
            var count = 0;
            var larger = "";
            var listOfChanges = new List<RecsProductChanges>();
            snapshotWorksTracks = snapshotWorksTracks.OrderByDescending(_ => _.CloneWorksTrackId).ToList();
            recsTracks = recsTracks.OrderByDescending(_ => _.Id).ToList();

            if (recsTracks.Count > snapshotWorksTracks.Count)
            {
                count = recsTracks.Count;
                larger = "recs";
            }
            else
            {
                count = snapshotWorksTracks.Count;
                larger = "snapshot";
            }

            if (larger == "snapshot")
            {
                foreach (var snapshotTrack in snapshotWorksTracks)
                {
                    var track = snapshotTrack;
                    var recsTrack = recsTracks.FirstOrDefault(_ => _.Id == track.CloneWorksTrackId);
                    if (recsTrack == null)
                    {
                        continue;
                    }
                    else
                    {
                        //Find artist differences
                        if (recsTrack.Artists.name != snapshotTrack.Artist.Name)
                        {
                            listOfChanges.Add(FindTrackArtistDifferences(recsTrack.Artists, snapshotTrack.Artist,
                                snapshotTrack, recsTrack));
                        }

                        //Check for track title change
                        if (recsTrack.Title.Trim() != snapshotTrack.Title.Trim())
                        {
                            listOfChanges.Add(LogRecsProductChanges(
                                "Track Title has been changed to " + recsTrack.Title, "Track Title Changed",
                                "Track Title", snapshotTrack.Title, recsTrack.Title));
                        }

                        //check for track duration change
                        if (recsTrack.Duration != snapshotTrack.Duration)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Track Duration has been changed to " + recsTrack.Duration + " on " +
                                    recsTrack.Title, "Track Duration Changed", "Duration", snapshotTrack.Duration,
                                    recsTrack.Duration));
                        }

                        //check for isrc change
                        if (string.IsNullOrEmpty(recsTrack.Isrc))
                        {
                            recsTrack.Isrc = null;
                        }

                        if (string.IsNullOrEmpty(snapshotTrack.Isrc))
                        {
                            snapshotTrack.Isrc = null;
                        }

                        if (recsTrack.Isrc != snapshotTrack.Isrc)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Track Isrc has been changed to " + recsTrack.Isrc + " on " + recsTrack.Title,
                                    "Track Isrc Changed", "Isrc", snapshotTrack.Isrc, recsTrack.Isrc));
                        }







                        //Dive into Copyrights and check for changes
                        if (recsTrack.Copyrights != null && snapshotTrack.Copyrights != null)
                        {
                            listOfChanges.AddRange(FindCopyRightDifferences(recsTrack.Copyrights,
                                snapshotTrack.Copyrights, snapshotTrack));
                        }
                    }
                }
            }
            else
            {
                foreach (var recsTrack in recsTracks)
                {
                    var track = recsTrack;
                    var snapshotTrack = snapshotWorksTracks.FirstOrDefault(_ => _.CloneWorksTrackId == track.Id);
                    if (snapshotTrack == null)
                    {
                        continue;
                    }
                    else
                    {
                        //Find artist differences
                        if (recsTrack.Artists.name != snapshotTrack.Artist.Name)
                        {
                            listOfChanges.Add(FindTrackArtistDifferences(recsTrack.Artists, snapshotTrack.Artist,
                                snapshotTrack, recsTrack));
                        }

                        //Check for track title change
                        if (recsTrack.Title != snapshotTrack.Title)
                        {
                            listOfChanges.Add(LogRecsProductChanges(
                                "Track Title has been changed to " + recsTrack.Title, "Track Title Changed",
                                "Track Title", snapshotTrack.Title, recsTrack.Title));
                        }

                        //check for track duration change
                        if (recsTrack.Duration != snapshotTrack.Duration)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges("Track Duration has been changed to " + recsTrack.Duration,
                                    "Track Duration Changed", "Track Duration", snapshotTrack.Duration,
                                    recsTrack.Duration));
                        }

                        //check for isrc change
                        if (string.IsNullOrEmpty(recsTrack.Isrc))
                        {
                            recsTrack.Isrc = null;
                        }

                        if (string.IsNullOrEmpty(snapshotTrack.Isrc))
                        {
                            snapshotTrack.Isrc = null;
                        }

                        if (recsTrack.Isrc != snapshotTrack.Isrc)
                        {
                            listOfChanges.Add(LogRecsProductChanges("Track Isrc has been changed to " + recsTrack.Isrc,
                                "Track Isrc Changed", "Track Isrc", snapshotTrack.Isrc, recsTrack.Isrc));
                        }






                        //Dive into Copyrights and check for changes
                        if (recsTrack.Copyrights != null && snapshotTrack.Copyrights != null)
                        {
                            listOfChanges.AddRange(FindCopyRightDifferences(recsTrack.Copyrights,
                                snapshotTrack.Copyrights, snapshotTrack));
                        }
                    }
                }
            }

            //for (var i = 0; i < recsTracks.Count; i++)
            //{
            //    var snapshotTrack = snapshotWorksTracks[i];
            //    var recsTrack = recsTracks[i];

            //    //Find artist differences
            //    if (recsTrack.Artists.name != snapshotTrack.Artist.Name)
            //    {
            //        listOfChanges.Add(FindTrackArtistDifferences(recsTrack.Artists, snapshotTrack.Artist, snapshotTrack, recsTrack ));
            //    }

            //    //Check for track title change
            //    if (recsTrack.Title != snapshotTrack.Title)
            //    {
            //        listOfChanges.Add(LogRecsProductChanges("Track Title has been changed to " + recsTrack.Title, "Track Title Changed", "Track Title", snapshotTrack.Title, recsTrack.Title));
            //    }

            //    //check for track duration change
            //    if (recsTrack.Duration != snapshotTrack.Duration)
            //    {
            //        listOfChanges.Add(LogRecsProductChanges("Track Duration has been changed to " + recsTrack.Duration, "Track Duration Changed", "Track Duration", snapshotTrack.Duration, recsTrack.Duration));
            //    }

            //    //check for isrc change
            //    if (string.IsNullOrEmpty(recsTrack.Isrc))
            //    {
            //        recsTrack.Isrc = null;
            //    }

            //    if (string.IsNullOrEmpty(snapshotTrack.Isrc))
            //    {
            //        snapshotTrack.Isrc = null;
            //    }

            //    if (recsTrack.Isrc != snapshotTrack.Isrc)
            //    {
            //        listOfChanges.Add(LogRecsProductChanges("Track Isrc has been changed to " + recsTrack.Isrc, "Track Isrc Changed", "Track Isrc", snapshotTrack.Isrc, recsTrack.Isrc));
            //    }






            //    //Dive into Copyrights and check for changes
            //    if (recsTrack.Copyrights != null && snapshotTrack.Copyrights != null)
            //    {
            //     //   listOfChanges.AddRange(FindCopyRightDifferences(recsTrack.Copyrights, snapshotTrack.Copyrights));   //(Turned Off)
            //    }
            //}



            return listOfChanges;
        }

        private List<RecsProductChanges> FindCopyRightDifferences(List<RecsCopyrights> recsCopyrights,
            List<Snapshot_RecsCopyright> snapshotRecsCopyrights, Snapshot_WorksTrack snapshotTrack)
        {
            var listOfChanges = new List<RecsProductChanges>();

            var snapshotProductHeader =
                _snapshotLicenseProductManager.GetProductForTrackId(snapshotTrack.SnapshotWorkTrackId);
            var info = _snapshotWorksRecordingManager.GetRecordingInfoForSnapshotTrackId(snapshotTrack.SnapshotWorkTrackId);


            //Check for added or removed copyrights
            var snapshotCopyrightsWorkCodes = recsCopyrights.Select(_ => _.WorkCode).ToList();
            var recCopyrightsWorkCodes = snapshotRecsCopyrights.Select(_ => _.WorkCode).ToList();

            var copyrightWorkcodesRemovedFromRecs =
                recCopyrightsWorkCodes.Except(snapshotCopyrightsWorkCodes).ToList();
            var copyrightWorkcodesAddedToRecs = snapshotCopyrightsWorkCodes.Except(recCopyrightsWorkCodes).ToList();

            listOfChanges.AddRange(_productChangeLogService.CopyrightsAddedToRecs(recsCopyrights,
                copyrightWorkcodesAddedToRecs, snapshotProductHeader, snapshotTrack));
            listOfChanges.AddRange(_productChangeLogService.CopyrightsRemovedFromRecs(snapshotRecsCopyrights,
                copyrightWorkcodesRemovedFromRecs, snapshotProductHeader, snapshotTrack));

            //Clean (remove added or removed copyrights)
            recsCopyrights = CleanCopyrights(recsCopyrights, copyrightWorkcodesAddedToRecs);
            snapshotRecsCopyrights = CleanSnapshotsCopyrights(snapshotRecsCopyrights,
                copyrightWorkcodesRemovedFromRecs);

            snapshotRecsCopyrights = snapshotRecsCopyrights.OrderBy(_ => _.WorkCode).ToList();
            recsCopyrights = recsCopyrights.OrderBy(_ => _.WorkCode).ToList();

            var larger = "";
            if (recsCopyrights.Count > snapshotRecsCopyrights.Count)
            {
                larger = "recs";
            }
            else
            {
                larger = "snapshot";
            }
            if (larger == "snapshot")
            {
                foreach (var snapshotCopyright in snapshotRecsCopyrights)
                {
                    var recsCopyright = recsCopyrights.FirstOrDefault(_ => _.WorkCode == snapshotCopyright.WorkCode);
                    if (recsCopyright == null)
                    {
                        continue;
                    }
                    else
                    {
                        //check title
                        if (recsCopyright.Title != snapshotCopyright.Title)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges("Copyright Title has been changed to " + recsCopyright.Title,
                                    "Copyright Title Changed", "Copyright Title", snapshotCopyright.Title,
                                    recsCopyright.Title));
                        }
                        //check principle artists
                        if (recsCopyright.PrincipalArtist != snapshotCopyright.PrincipalArtist)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Copyright PrincipalArtist has been changed to " + recsCopyright.PrincipalArtist,
                                    "Copyright PrincipalArtist Changed", "Copyright PrincipalArtist",
                                    snapshotCopyright.PrincipalArtist, recsCopyright.PrincipalArtist));
                        }
                        //check writers
                        if (recsCopyright.Writers != snapshotCopyright.Writers)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges("Copyright Writers has been changed to " + recsCopyright.Writers,
                                    "Copyright Writers Changed", "Copyright Writers", snapshotCopyright.Writers,
                                    recsCopyright.Writers));
                        }

                        //check writer string
                        if (recsCopyright.WriteString != snapshotCopyright.WriteString)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Copyright Writer String has been changed to " + recsCopyright.WriteString,
                                    "Copyright Writer String Changed", "Copyright Writer String",
                                    snapshotCopyright.WriteString,
                                    recsCopyright.WriteString));
                        }


                        if (Math.Floor(recsCopyright.MechanicalCollectablePercentage) !=
                            snapshotCopyright.MechanicalCollectablePercentage)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Copyright Mechanical Collectable Percentage has been changed to " +
                                    recsCopyright.MechanicalCollectablePercentage.ToString() + "% on track " +
                                    info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title,
                                    "Copyright Mechanical Collectable Percentage Changed",
                                    "Copyright Mechanical Collectable Percentage Changed",
                                    snapshotCopyright.MechanicalCollectablePercentage.ToString() + "%",
                                    recsCopyright.MechanicalCollectablePercentage.ToString() + "%"));
                        }


                        if (Math.Floor(recsCopyright.MechanicalOwnershipPercentage) !=
                            snapshotCopyright.MechanicalOwnershipPercentage)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Copyright Mechanical Ownership Percentage has been changed to " +
                                    recsCopyright.MechanicalOwnershipPercentage.ToString() + "% on track " +
                                    info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title,
                                    "Copyright Mechanical Ownership Percentage Changed",
                                    "Copyright Mechanical Ownership Percentage Changed",
                                    snapshotCopyright.MechanicalOwnershipPercentage.ToString() + "%",
                                    recsCopyright.MechanicalOwnershipPercentage.ToString() + "%"));
                        }
                        //chek sampled works (TODO:not implemented)




                        //-------------
                        //check local Clients

                        if (recsCopyright.LocalClients != null && snapshotCopyright.LocalClients != null)
                        {
                            listOfChanges.AddRange(CheckForLocalClientChanges(recsCopyright.LocalClients,
                                snapshotCopyright.LocalClients));
                        }
                        if (recsCopyright.AquisitionLocationCode != null &&
                            snapshotCopyright.AquisitionLocationCodes != null)
                        {
                            //check deal owning locations
                            listOfChanges.AddRange(
                                CheckForDealOwningLocaitonChanges(recsCopyright.AquisitionLocationCode,
                                    snapshotCopyright.AquisitionLocationCodes));
                        }
                        //cechk composers
                        // listOfChanges.AddRange(CheckForComposerChanges(recsCopyright.Composers, snapshotCopyright.Composers));


                        //Turned off, we use workcode as an identifier, if it changes, then its an entirely diff workcode completely.
                        //if (snapshotCopyright.WorkCode != recsCopyright.WorkCode)
                        //{
                        //    listOfChanges.Add(
                        //        LogRecsProductChanges(
                        //            "Work Code has been changed on track " + snapshotTrack.Title,
                        //            "Work Code Changed ", "Work Code Changed",
                        //            snapshotCopyright.WorkCode + " (" + snapshotCopyright.Title + ")",
                        //            recsCopyright.WorkCode + " (" + recsCopyright.Title + ")"));
                        //}
                        //cechk composers
                        //   listOfChanges.AddRange(CheckForComposerChanges(recsCopyright.Composers, snapshotCopyright.Composers));  //Turned off

                    }

                }
            }
            else
            {
                foreach (var recsCopyright in recsCopyrights)
                {
                    var snapshotCopyright =
                        snapshotRecsCopyrights.FirstOrDefault(_ => _.WorkCode == recsCopyright.WorkCode);
                    if (snapshotCopyright == null)
                    {
                        continue;
                    }
                    else
                    {
                        //check title
                        if (recsCopyright.Title != snapshotCopyright.Title)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges("Copyright Title has been changed to " + recsCopyright.Title,
                                    "Copyright Title Changed", "Copyright Title", snapshotCopyright.Title,
                                    recsCopyright.Title));
                        }
                        //check principle artists
                        if (recsCopyright.PrincipalArtist != snapshotCopyright.PrincipalArtist)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Copyright PrincipalArtist has been changed to " + recsCopyright.PrincipalArtist,
                                    "Copyright PrincipalArtist Changed", "Copyright PrincipalArtist",
                                    snapshotCopyright.PrincipalArtist, recsCopyright.PrincipalArtist));
                        }
                        //check writers
                        if (recsCopyright.Writers != snapshotCopyright.Writers)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges("Copyright Writers has been changed to " + recsCopyright.Writers,
                                    "Copyright Writers Changed", "Copyright Writers", snapshotCopyright.Writers,
                                    recsCopyright.Writers));
                        }

                        //check writer string
                        if (recsCopyright.WriteString != snapshotCopyright.WriteString)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Copyright WriteString has been changed to " + recsCopyright.WriteString,
                                    "Copyright WriteString Changed", "Copyright WriteString",
                                    snapshotCopyright.WriteString,
                                    recsCopyright.WriteString));
                        }


                        if (recsCopyright.MechanicalCollectablePercentage !=
                            snapshotCopyright.MechanicalCollectablePercentage)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Copyright MechanicalCollectablePercentage has been changed to " +
                                    recsCopyright.MechanicalCollectablePercentage.ToString() + "% on track " +
                                    snapshotTrack.Title,
                                    "Copyright MechanicalCollectablePercentage Changed",
                                    "Copyright Mechanical Collectable Percentage Changed",
                                    snapshotCopyright.MechanicalCollectablePercentage.ToString() + "%",
                                    recsCopyright.MechanicalCollectablePercentage.ToString() + "%"));
                        }


                        if (recsCopyright.MechanicalOwnershipPercentage !=
                            snapshotCopyright.MechanicalOwnershipPercentage)
                        {
                            listOfChanges.Add(
                                LogRecsProductChanges(
                                    "Copyright Mechanical Ownership Percentage has been changed to " +
                                    recsCopyright.MechanicalOwnershipPercentage.ToString() + "% on track " +
                                    snapshotTrack.Title,
                                    "Copyright Mechanical Ownership Percentage Changed",
                                    "Copyright Mechanical Ownership Percentage Changed",
                                    snapshotCopyright.MechanicalOwnershipPercentage.ToString() + "%",
                                    recsCopyright.MechanicalOwnershipPercentage.ToString() + "%"));
                        }


                        //chek sampled works (TODO:not implemented)




                        //-------------
                        //check local Clients
                        if (recsCopyright.LocalClients != null && snapshotCopyright.LocalClients != null)
                        {
                            listOfChanges.AddRange(CheckForLocalClientChanges(recsCopyright.LocalClients,
                                snapshotCopyright.LocalClients));
                        }
                        if (recsCopyright.AquisitionLocationCode != null &&
                            snapshotCopyright.AquisitionLocationCodes != null)
                        {
                            //check deal owning locations
                            listOfChanges.AddRange(
                                CheckForDealOwningLocaitonChanges(recsCopyright.AquisitionLocationCode,
                                    snapshotCopyright.AquisitionLocationCodes));
                        }
                        //cechk composers
                        //listOfChanges.AddRange(CheckForComposerChanges(recsCopyright.Composers, snapshotCopyright.Composers));

                        //Turned off, we use workcode as an identifier, if it changes, then its an entirely diff workcode completely.
                        //if (snapshotCopyright.WorkCode != recsCopyright.WorkCode)
                        //{
                        //    listOfChanges.Add(
                        //        LogRecsProductChanges(
                        //            "Work Code has been changed on track " + snapshotTrack.Title,
                        //            "Work Code Changed ", "Work Code Changed",
                        //            snapshotCopyright.WorkCode + " (" + snapshotCopyright.Title + ")",
                        //            recsCopyright.WorkCode + " (" + recsCopyright.Title + ")"));
                        //}
                        //cechk composers
                        //   listOfChanges.AddRange(CheckForComposerChanges(recsCopyright.Composers, snapshotCopyright.Composers));  //Turned off

                    }
                }
            }

            return listOfChanges;
        }

        /* Composers not currenly used
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
                //TODO
                /*
                if (snapshotComposer.CloneCaeNumber == recComposer.CaeNumber)
                {
                    //check name
                    if (recComposer.FullName != snapshotComposer.FullName)
                    {
                        listOfChanges.Add(LogRecsProductChanges("Composer Name was Changed " + recComposer.FullName,
                            "Composer", "Composer Name", snapshotComposer.FullName, recComposer.FullName));
                    }

                    //check CapacityCode
                    if (recComposer.CapacityCode != snapshotComposer.CapacityCode)
                    {
                        listOfChanges.Add(
                            LogRecsProductChanges("Composer CapacityCode was Changed " + recComposer.CapacityCode,
                                "Composer", "Composer CapacityCode", snapshotComposer.CapacityCode,
                                recComposer.CapacityCode));
                    }
                    //check capacity
                    if (recComposer.Capacity != snapshotComposer.Capacity)
                    {
                        listOfChanges.Add(LogRecsProductChanges(
                            "Composer Capacity was Changed " + recComposer.Capacity, "Composer", "Composer Capacity",
                            snapshotComposer.Capacity, recComposer.Capacity));
                    }
                    //check controlled
                    if (recComposer.Controlled != snapshotComposer.Controlled)
                    {
                        listOfChanges.Add(
                            LogRecsProductChanges(
                                "Composer Controlled Status was Changed " + recComposer.Controlled.ToString(),
                                "Composer", "Composer Controlled Status ", snapshotComposer.Controlled.ToString(),
                                recComposer.Controlled.ToString()));
                    }
                }
               
                //check original publisers
                listOfChanges.AddRange(FindOrigainlPublisherDifferences(recComposer.OriginalPublishers, snapshotComposer.OriginalPublishers));
                //check affilaitons
            }

            return listOfChanges;
        }
        */

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

            if (recsLocalClients.Count > 0 && copyrightWorkcodesAddedToRecs.Count > 0)
            {
                listOfChanges.AddRange(_productChangeLogService.LocalClientAddedToRecs(recsLocalClients,
                    copyrightWorkcodesAddedToRecs));
            }
            if (snapshotLocalClients.Count > 0 && copyrightWorkcodesRemovedFromRecs.Count > 0)
            {
                listOfChanges.AddRange(_productChangeLogService.LocalClientRemovedFromRecs(snapshotLocalClients,
                    copyrightWorkcodesRemovedFromRecs));
            }

            //Clean (remove added or removed copyrights)
            if (copyrightWorkcodesAddedToRecs.Count > 0)
            {
                recsLocalClients = CleanLocalClient(recsLocalClients, copyrightWorkcodesAddedToRecs);
            }
            if (copyrightWorkcodesRemovedFromRecs.Count > 0)
            {
                snapshotLocalClients = CleanLocalClient(snapshotLocalClients,
                    copyrightWorkcodesRemovedFromRecs);
            }

            if (recsLocalClients.Count == snapshotLocalClients.Count)
            {
                for (int i = 0; i < recsLocalClients.Count; i++)
                {
                    var recLocalClient = recsLocalClients[i];
                    var snapshotLocalClient = snapshotLocalClients[i];

                    if (recLocalClient.ClientName != snapshotLocalClient.ClientName)
                    {
                        listOfChanges.Add(
                            LogRecsProductChanges(
                                "Local ClientName Local ClientName Status was Changed " +
                                recLocalClient.ClientName.ToString(), "Local ClientName",
                                "Local ClientName Controlled Status ", snapshotLocalClient.ClientName.ToString(),
                                recLocalClient.ClientName.ToString()));
                    }
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

        private RecsProductChanges FindTrackArtistDifferences(ArtistRecs recsArtist, Snapshot_ArtistRecs snapshotArtist,
            Snapshot_WorksTrack snapshotWorksTrack, WorksTrack recsWorksTrack)
        {
            var newChange = new RecsProductChanges();

            newChange.ChangeMessage = "The Artist Name has been changed on track " + recsWorksTrack.Title;
            newChange.PropertyLocation = "Artist Name";
            newChange.OriginalValue = snapshotArtist.Name;
            newChange.ChangedValue = recsArtist.name;
            newChange.PropertyChanged = "Track Artist Name";

            return newChange;
        }

        private RecsProductChanges FindArtistDifferences(ArtistRecs recsArtist, Snapshot_ArtistRecs snapshotArtist,
            Snapshot_ProductHeader snapshotProductHeader, ProductHeader recsProductHeader)
        {
            var newChange = new RecsProductChanges();

            newChange.ChangeMessage = "The Artist Name has been changed on product " + recsProductHeader.Title;
            newChange.PropertyLocation = "Product Artist Name";
            newChange.OriginalValue = snapshotArtist.Name;
            newChange.ChangedValue = recsArtist.name;
            newChange.PropertyChanged = "Product Artist Name";

            return newChange;
        }

        private RecsProductChanges FindRecordLabelDifferences(Label recsLabel, Snapshot_Label snapshotLabel)
        {
            var newChange = new RecsProductChanges();
            var productHeaderInfo =
                _snapshotProductHeaderManager.GetSnapshotProductHeaderForLabelSnapshotId(snapshotLabel.SnapshotLabelId);
            if (productHeaderInfo == null)
            {
                newChange.ChangeMessage = "The Record Label Name has been changed.";
            }
            else
            {
                newChange.ChangeMessage = "The Record Label Name has been changed on product " + productHeaderInfo.Title;
            }
            if (recsLabel == null)
            {
                recsLabel = new Label
                {
                    name = "N/A"
                };
            }
            if (snapshotLabel == null)
            {
                snapshotLabel = new Snapshot_Label
                {
                    Name = "N/A"
                };
            }
      
            
    
            newChange.PropertyLocation = "Record Label";
            newChange.OriginalValue = snapshotLabel.Name;
            newChange.ChangedValue = recsLabel.name;
            newChange.PropertyChanged = "Record Label";

            return newChange;
        }

        private RecsProductChanges FindRecordLabelGroupDifferences(LabelGroup recsLabelGroup,
            Snapshot_LabelGroup snapshotLabelGroup)
        {
            var newChange = new RecsProductChanges();

            newChange.ChangeMessage = "The Record Label Group Name has been changed";
            newChange.PropertyLocation = "Record Label Group";
            newChange.OriginalValue = snapshotLabelGroup.Name;
            newChange.ChangedValue = recsLabelGroup.Name;
            newChange.PropertyChanged = "Record Label Group";

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

        private List<Snapshot_ProductHeader> GetSnapshotProductHeaders(
            List<Snapshot_LicenseProduct> snapshotLicenseProducts)
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
                if (ids.Contains((int) list[i].ProductId))
                {
                    list.Remove(list[i]);
                }
            }

            return list;
        }

        private List<Snapshot_WorksRecording> CleanWorksRecordingSnapshots(List<Snapshot_WorksRecording> list,
            List<int> ids)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (ids.Contains((int) list[i].CloneTrackId))
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
                if (ids.Contains((int) list[i].CloneCaeNumber))
                {
                    list.Remove(list[i]);
                }
            }

            return list;
        }

        //Not Used Currently
        //private List<RecsProductChanges> FindAffiliationBaseChanges(
        //          List<AffiliationBase> recsOriginalPubAffiliationBases,
        //          List<Snapshot_ComposerOriginalPublisherAffiliationBase> snapshotOriginalPubAffiliationBases)
        //{
        //    var listOfChanges = new List<RecsProductChanges>();
        //    if (snapshotOriginalPubAffiliationBases.Count != recsOriginalPubAffiliationBases.Count)
        //    {
        //        //Get Writer Ids
        //        var snapshotOPAffilationSociertAcronym =
        //            snapshotOriginalPubAffiliationBases.Select(_ => _.SocietyAcronym).ToList();
        //        var recsOPAffilationSocietyAcronym =
        //            recsOriginalPubAffiliationBases.Select(_ => _.SocietyAcronym).ToList();

        //        //find which writers were added and removed
        //        var originalPublisherRemovedSocietyAcronym =
        //            recsOPAffilationSocietyAcronym.Except(snapshotOPAffilationSociertAcronym).ToList();
        //        var originalPublisherAddedSocietyAcronym =
        //            snapshotOPAffilationSociertAcronym.Except(recsOPAffilationSocietyAcronym).ToList();

        //        //log added or removed writers
        //        listOfChanges.AddRange(
        //            _productChangeLogService.AffiliationBaseAddedToRecs(recsOriginalPubAffiliationBases,
        //                originalPublisherAddedSocietyAcronym));
        //        listOfChanges.AddRange(
        //            _productChangeLogService.AffiliationBaseRemovedFromRecs(snapshotOriginalPubAffiliationBases,
        //                originalPublisherRemovedSocietyAcronym));

        //        //Clean added or removed writer
        //        //Remove added/removed writers from list
        //        recsOriginalPubAffiliationBases = CleanRecsAffiliationsBases(recsOriginalPubAffiliationBases,
        //            originalPublisherAddedSocietyAcronym);
        //        snapshotOriginalPubAffiliationBases = CleanRecsComposerAffiliationsBases(snapshotOriginalPubAffiliationBases,
        //            originalPublisherRemovedSocietyAcronym);
        //    }

        //    for (var i = 0; i < snapshotOriginalPubAffiliationBases.Count; i++)
        //    {
        //        var snapshotOpAffBase = snapshotOriginalPubAffiliationBases[i];
        //        var recsOriginalPubBase = recsOriginalPubAffiliationBases[i];

        //        if (snapshotOpAffBase.StartDate != recsOriginalPubBase.StartDate)
        //        {
        //            listOfChanges.Add(LogRecsProductChanges("Original Publisher Controlled Affiliation Base Start Date was Changed ", "Original Publisher Affiliaton Base", "Original Publisher Affiliaton Base - Start Date ", snapshotOpAffBase.StartDate.ToString(), recsOriginalPubBase.StartDate.ToString()));
        //        }

        //        if (snapshotOpAffBase.EndDate != recsOriginalPubBase.EndDate)
        //        {
        //            listOfChanges.Add(LogRecsProductChanges("Original Publisher Controlled Affiliation Base End Date was Changed ", "Original Publisher Affiliaton Base", "Original Publisher Affiliaton Base - End Date ", snapshotOpAffBase.EndDate.ToString(), recsOriginalPubBase.EndDate.ToString()));
        //        }
        //    }

        //    return listOfChanges;
        //}

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

        private List<Snapshot_OriginalPubAffiliationBase> CleanSnapshotAffiliationsBases(
            List<Snapshot_OriginalPubAffiliationBase> list, List<string> ids)
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

        private List<Snapshot_ComposerOriginalPublisherAffiliationBase> CleanRecsComposerAffiliationsBases(
            List<Snapshot_ComposerOriginalPublisherAffiliationBase> list, List<string> ids)
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

        private List<Snapshot_OriginalPublisherAffiliation> CleanRecsOriginalPublishersAffiliations(
            List<Snapshot_OriginalPublisherAffiliation> list, List<string> ids)
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

        private List<Snapshot_ComposerOriginalPublisherAffiliation> CleanRecsOriginalPublishersAffiliations(
            List<Snapshot_ComposerOriginalPublisherAffiliation> list, List<string> ids)
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

        private List<Snapshot_RecsCopyright> CleanSnapshotsCopyrights(List<Snapshot_RecsCopyright> list,
            List<string> ids)
        {
            //var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            //if (ids.Count > 0)
            //{
            //    for (int i = list.Count - 1; i >= 0; i--)
            //    {
            //        if (listOfIds.Contains(Convert.ToInt32(list[i].WorkCode)))
            //        {
            //            list.Remove(list[i]);
            //        }
            //    }
            //}
            return list;
        }

        private List<RecsCopyrights> CleanCopyrights(List<RecsCopyrights> list, List<string> ids)
        {
            //var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            //if (ids.Count > 0)
            //{
            //    if (ids.Count > 0)
            //    {
            //        for (int i = list.Count - 1; i >= 0; i--)
            //        {
            //            if (listOfIds.Contains(Convert.ToInt32(list[i].WorkCode)))
            //            {
            //                list.Remove(list[i]);
            //            }
            //        }
            //    }
            //}
            return list;
        }

        private List<LocalClientCopyright> CleanLocalClient(List<LocalClientCopyright> list, List<string> ids)
        {
            //int num;
            //var result = Int32.TryParse(ids[0], out num);
            //if (result)
            //{
            //    var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            //    if (ids.Count > 0)
            //    {
            //        for (int i = list.Count - 1; i >= 0; i--)
            //        {
            //            if (listOfIds.Contains(Convert.ToInt32(list[i])))
            //            {
            //                list.Remove(list[i]);
            //            }
            //        }
            //    }
            //}
            //  else
            //{
            var listOfIds = ids.Select(_ => _).ToList();
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (listOfIds.Contains(list[i].ToString()))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            //}
            return list;
        }

        private List<Snapshot_LocalClientCopyright> CleanLocalClient(List<Snapshot_LocalClientCopyright> list,
            List<string> ids)
        {
            //int num;
            //var result = Int32.TryParse(ids[0], out num);
            //if (result)
            //{
            //    var listOfIds = ids.Select(_ => Convert.ToInt32(_)).ToList();
            //    if (ids.Count > 0)
            //    {
            //        for (int i = list.Count - 1; i >= 0; i--)
            //        {
            //            if (listOfIds.Contains(Convert.ToInt32(list[i])))
            //            {
            //                list.Remove(list[i]);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            var listOfIds = ids.Select(_ => _).ToList();
            if (ids.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (listOfIds.Contains(list[i].ToString()))
                    {
                        list.Remove(list[i]);
                    }
                }
            }
            //}
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

        private List<Snapshot_OriginalPublisher> CleanSnapshotsOriginalPublishers(List<Snapshot_OriginalPublisher> list,
            List<string> ids)
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

        private List<Snapshot_ComposerOriginalPublisher> CleanSnapshotsComposerOriginalPublishers(
            List<Snapshot_ComposerOriginalPublisher> list, List<string> ids)
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

        private List<Snapshot_RecsConfiguration> CleanSnapshotRecsConfigurations(List<Snapshot_RecsConfiguration> list,
            List<int> ids)
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

        private string ControlledHelper(bool controlled)
        {
            if (controlled)
            {
                return "Controlled";
            }
            return "Uncontrolled";
        }

        
    }
}