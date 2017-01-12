using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class RecsProductChangeLogService : IRecsProductChangeLogService
    {

        private readonly ISnapshotWorksRecordingManager _snapshotWorksRecordingManager;

        public RecsProductChangeLogService(ISnapshotWorksRecordingManager snapshotWorksRecording)
        {
            _snapshotWorksRecordingManager = snapshotWorksRecording;
        }

        public List<RecsProductChanges> ProductAddedToRecs(List<LicenseProduct> licenseProducts,
            List<int> productIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            //foreach (var licenseProduct in licenseProducts)  //Turned off, not needed
            //{
            //    if (IsInList(licenseProduct.ProductId, productIdsAdded))
            //    {
            //        var newProductChange = new RecsProductChanges();
            //        newProductChange.PropertyLocation = "Product";
            //        newProductChange.PropertyChanged = "Product " + licenseProduct.ProductId + " has been added to Recs";
            //        if (licenseProduct.ProductHeader.Title != null)
            //        {
            //            newProductChange.ChangedValue = licenseProduct.ProductHeader.Title;
            //        }
            //        else
            //        {
            //            newProductChange.ChangedValue = licenseProduct.ProductId.ToString() + " Added";
            //        }
            //        newProductChange.ChangeMessage = "Product " + licenseProduct.ProductId + " has been added to Recs";
            //        listOfChanges.Add(newProductChange);
            //    }
            //}
            return listOfChanges;
        }

        public List<RecsProductChanges> AffiliationBaseAddedToRecs(List<AffiliationBase> licenseProducts,
            List<string> productIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseProduct in licenseProducts)
            {
                if (IsInListString(licenseProduct.SocietyAcronym, productIdsAdded))
                {
                    var newProductChange = new RecsProductChanges();
                    newProductChange.PropertyLocation = "Affiliation Base";
                    newProductChange.PropertyChanged = "Affiliation Base " + licenseProduct.SocietyAcronym +
                                                       " has been added to Recs";

                    newProductChange.ChangedValue = licenseProduct.SocietyAcronym;


                    newProductChange.ChangeMessage = "Affiliation Base " + licenseProduct.SocietyAcronym +
                                                     " has been added to Recs";
                    listOfChanges.Add(newProductChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> AffiliationBaseRemovedFromRecs(
            List<Snapshot_OriginalPubAffiliationBase> licenseProducts, List<string> productIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseProduct in licenseProducts)
            {
                if (IsInListString(licenseProduct.SocietyAcronym, productIdsAdded))
                {
                    var newProductChange = new RecsProductChanges();
                    newProductChange.PropertyLocation = "Product";
                    newProductChange.PropertyChanged = "Product " + licenseProduct.SocietyAcronym +
                                                       " has been removed from Recs";
                    newProductChange.ChangedValue = licenseProduct.SocietyAcronym;
                    newProductChange.ChangeMessage = "Product " + licenseProduct.SocietyAcronym +
                                                     " has been removed from Recs";
                    listOfChanges.Add(newProductChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> AffiliationBaseRemovedFromRecs(
            List<Snapshot_ComposerOriginalPublisherAffiliationBase> licenseProducts, List<string> productIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseProduct in licenseProducts)
            {
                if (IsInListString(licenseProduct.SocietyAcronym, productIdsAdded))
                {
                    var newProductChange = new RecsProductChanges();
                    newProductChange.PropertyLocation = "Product";
                    newProductChange.PropertyChanged = "Product " + licenseProduct.SocietyAcronym +
                                                       " has been removed from Recs";
                    newProductChange.ChangedValue = licenseProduct.SocietyAcronym;
                    newProductChange.ChangeMessage = "Product " + licenseProduct.SocietyAcronym +
                                                     " has been removed from Recs";
                    listOfChanges.Add(newProductChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> ProductRemovedFromRecs(List<Snapshot_LicenseProduct> licenseProducts,
            List<int> productIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            //foreach (var licenseProduct in licenseProducts)  //Turned off, not needed
            //{
            //    if (IsInList((int) licenseProduct.ProductId, productIdsAdded))
            //    {
            //        var newProductChange = new RecsProductChanges();
            //        newProductChange.PropertyLocation = "Product";
            //        newProductChange.PropertyChanged = "Product " + licenseProduct.ProductId +
            //                                           " has been removed from Recs";
            //        if (licenseProduct.ProductHeader.Title != null)
            //        {
            //            newProductChange.ChangedValue = licenseProduct.ProductHeader.Title;
            //        }
            //        else
            //        {
            //            newProductChange.ChangedValue = licenseProduct.ProductId.ToString() + " Removed";
            //        }
            //        newProductChange.ChangeMessage = "Product " + licenseProduct.ProductId +
            //                                         " has been removed from Recs";
            //        listOfChanges.Add(newProductChange);
            //    }
            //}
            return listOfChanges;
        }

        public List<RecsProductChanges> TracksRemovedFromRecs(List<Snapshot_WorksTrack> licenseRecordings,
            List<int> RecordingIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInList((int) licenseRecording.CloneWorksTrackId, RecordingIdsAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Recording";
                    newRecordingChange.PropertyChanged = "Recording " + licenseRecording.CloneWorksTrackId +
                                                         " has been removed from Recs";
                    if (licenseRecording.Title != null)
                    {
                        newRecordingChange.ChangedValue = licenseRecording.Title;
                    }
                    else
                    {
                        newRecordingChange.ChangedValue = licenseRecording.CloneWorksTrackId.ToString() + " Removed";
                    }
                    newRecordingChange.ChangeMessage = "Recording " + licenseRecording.CloneWorksTrackId +
                                                       " has been removed from Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> TracksAddedToRecs(List<WorksTrack> licenseRecordings,
            List<int> RecordingIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInList(licenseRecording.Id, RecordingIdsAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Recording";
                    newRecordingChange.PropertyChanged = "Recording " + licenseRecording.Id + " has been added to Recs";
                    if (licenseRecording.Title != null)
                    {
                        newRecordingChange.ChangedValue = licenseRecording.Title;
                    }
                    else
                    {
                        newRecordingChange.ChangedValue = licenseRecording.Id.ToString() + " Added";
                    }
                    newRecordingChange.ChangeMessage = "Recording " + licenseRecording.Id + " has been added to Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> RecordingRemovedFromRecs(List<Snapshot_WorksRecording> licenseRecordings,
            List<int> RecordingIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInList((int) licenseRecording.CloneTrackId, RecordingIdsAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Recording";
                    newRecordingChange.PropertyChanged = "Recording " + licenseRecording.CloneTrackId +
                                                         " has been removed from Recs";
                    if (licenseRecording.Track.Title != null)
                    {
                        newRecordingChange.ChangedValue = licenseRecording.Track.Title;
                    }
                    else
                    {
                        newRecordingChange.ChangedValue = licenseRecording.CloneTrackId.ToString() + " Removed";
                    }
                    newRecordingChange.ChangeMessage = "Recording " + licenseRecording.CloneTrackId +
                                                       " has been removed from Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> RecordingAddedToRecs(List<WorksRecording> licenseRecordings,
            List<int> RecordingIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInList(licenseRecording.TrackId, RecordingIdsAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Recording";
                    newRecordingChange.PropertyChanged = "Recording " + licenseRecording.TrackId +
                                                         " has been added to Recs";
                    if (licenseRecording.Track.Title != null)
                    {
                        newRecordingChange.ChangedValue = licenseRecording.Track.Title;
                    }
                    else
                    {
                        newRecordingChange.ChangedValue = licenseRecording.TrackId.ToString() + " Added";
                    }
                    newRecordingChange.ChangeMessage = "Recording " + licenseRecording.TrackId +
                                                       " has been added to Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> OriginalPublisherAffiliationAddedToRecs(List<Affiliation> licenseRecordings,
            List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();

            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInListString(licenseRecording.IncomeGroup, originalPublisherIpCodes))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Original Publisher Affiliation";
                    newRecordingChange.PropertyChanged = "Original Publisher Affiliation added to Recs " +
                                                         licenseRecording.IncomeGroup + " has been added to Recs";

                    newRecordingChange.ChangedValue = licenseRecording.IncomeGroup + " Added";

                    newRecordingChange.ChangeMessage = "Original Publisher Affiliation" + licenseRecording.IncomeGroup +
                                                       " has been added to  Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> OriginalPublisherAffiliationRemovedFromRecs(
            List<Snapshot_OriginalPublisherAffiliation> licenseRecordings, List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();

            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInListString(licenseRecording.IncomeGroup, originalPublisherIpCodes))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Original Publisher Affiliation";
                    newRecordingChange.PropertyChanged = "Original Publisher Affiliation removed from Recs " +
                                                         licenseRecording.IncomeGroup + " has been removed from  Recs";

                    newRecordingChange.ChangedValue = licenseRecording.IncomeGroup + " Removed";

                    newRecordingChange.ChangeMessage = "Original Publisher Affiliation" + licenseRecording.IncomeGroup +
                                                       " has been removed from Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }


        public List<RecsProductChanges> OriginalPublisherAffiliationRemovedFromRecs(
            List<Snapshot_ComposerOriginalPublisherAffiliation> licenseRecordings, List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();

            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInListString(licenseRecording.IncomeGroup, originalPublisherIpCodes))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Original Publisher Affiliation";
                    newRecordingChange.PropertyChanged = "Original Publisher Affiliation removed from Recs " +
                                                         licenseRecording.IncomeGroup + " has been removed from  Recs";

                    newRecordingChange.ChangedValue = licenseRecording.IncomeGroup + " Removed";

                    newRecordingChange.ChangeMessage = "Original Publisher Affiliation" + licenseRecording.IncomeGroup +
                                                       " has been removed from Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }


        public List<RecsProductChanges> OriginalPublishersAddedToRecs(List<OriginalPublisher> licenseRecordings,
            List<string> originalPublisherIpCodes, Snapshot_WorksRecording snapshotWorksRecording)
        {
            var listOfChanges = new List<RecsProductChanges>();
            var info = _snapshotWorksRecordingManager.GetRecordingInfoForSnapshotRecordingId(snapshotWorksRecording);
            var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
            if (intList.Count > 0)
            {
                foreach (var licenseRecording in licenseRecordings)
                {
                    if (IsInList(Convert.ToInt32(licenseRecording.IpCode), intList))
                    {
                        var newRecordingChange = new RecsProductChanges();
                        newRecordingChange.PropertyLocation = "Original Publisher";
                        newRecordingChange.PropertyChanged = "Original Publisher added to Recs.";
                        newRecordingChange.OriginalValue = "N/A";
                        newRecordingChange.ChangedValue = licenseRecording.FullName;

                        newRecordingChange.ChangeMessage = "Original Publisher CAE (" + licenseRecording.CaeNumber +  ") "+
                                                           " has been added to Recs on track: " + info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title;
                        listOfChanges.Add(newRecordingChange);
                    }
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> OriginalPublishersRemovedFromRecs(
            List<Snapshot_OriginalPublisher> licenseRecordings, List<string> originalPublisherIpCodes, Snapshot_WorksRecording snapshotWorksRecording)
        {
            var info = _snapshotWorksRecordingManager.GetRecordingInfoForSnapshotRecordingId(snapshotWorksRecording);

            var listOfChanges = new List<RecsProductChanges>();
            var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
            if (intList.Count > 0)
            {
                foreach (var licenseRecording in licenseRecordings)
                {
                    if (IsInList(Convert.ToInt32(licenseRecording.IpCode), intList))
                    {
                        var newRecordingChange = new RecsProductChanges();
                        newRecordingChange.PropertyLocation = "Original Publisher";
                        newRecordingChange.PropertyChanged = "Original Publisher removed from Recs.";
                        newRecordingChange.OriginalValue = licenseRecording.FullName + " removed";
                        newRecordingChange.ChangedValue = "N/A";
                        newRecordingChange.ChangeMessage = "Original Publisher CAE (" + licenseRecording.CloneCaeNumber + ") " +
                                                           " has been removed from Recs on track: " + info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title;
                        listOfChanges.Add(newRecordingChange);
                    }
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> OriginalPublishersRemovedFromRecs(
            List<Snapshot_ComposerOriginalPublisher> licenseRecordings, List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();
            var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
            if (intList.Count > 0)
            {
                foreach (var licenseRecording in licenseRecordings)
                {
                    if (IsInList(Convert.ToInt32(licenseRecording.IpCode), intList))
                    {
                        var newRecordingChange = new RecsProductChanges();
                        newRecordingChange.PropertyLocation = "Original Publisher";
                        newRecordingChange.PropertyChanged = "Original Publisher removed from Recs " +
                                                             licenseRecording.IpCode + " has been removed from Recs";

                        newRecordingChange.ChangedValue = licenseRecording.FullName + " Added";

                        newRecordingChange.ChangeMessage = "Original Publisher " + licenseRecording.IpCode +
                                                           " has been removed from Recs";
                        listOfChanges.Add(newRecordingChange);
                    }
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> WritersAddedToRecs(List<WorksWriter> worksWriters,
            List<int> writerCaeCodesAdded, Snapshot_WorksRecording snapshotWorksRecording)
        {
            var info =
                _snapshotWorksRecordingManager.GetRecordingInfoForSnapshotRecordingId(
                    snapshotWorksRecording);

            var listOfChanges = new List<RecsProductChanges>();
            foreach (var worksWriter in worksWriters)
            {
                if (IsInList(worksWriter.CaeNumber, writerCaeCodesAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Writer";
                    newRecordingChange.PropertyChanged = "Writer added to Recs";

                    newRecordingChange.ChangedValue = worksWriter.FullName;
                    newRecordingChange.OriginalValue = "N/A";
                    newRecordingChange.ChangeMessage = "Writer " + worksWriter.FullName + " CAE: (" + worksWriter.CaeNumber+") " +  
                                                       " has been added to Recs on recording: " + info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title;
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> WritersRemovedFromRecs(List<Snapshot_WorksWriter> licenseRecordings,
            List<int> writerCaeCodesAdded, Snapshot_WorksRecording snapshotWorksRecording)
        {
            var info =
               _snapshotWorksRecordingManager.GetRecordingInfoForSnapshotRecordingId(
                   snapshotWorksRecording);
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var worksWriter in licenseRecordings)
            {
                if (IsInList(worksWriter.CloneCaeNumber, writerCaeCodesAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Writer";
                    newRecordingChange.PropertyChanged = "Writer removed from Recs";
                    newRecordingChange.OriginalValue = worksWriter.FullName;
                    newRecordingChange.ChangedValue = "N/A";

                    newRecordingChange.ChangeMessage = "Writer " + worksWriter.FullName + " CAE: (" + worksWriter.CloneCaeNumber + ") " +
                                                       " has been removed to Recs on recording: " + info.SnapshotWorksTrack.Title + " / product: " + info.SnapshotProductHeader.Title;
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> ConfigurationAddedToRecs(List<RecsConfiguration> recsConfigurations,
            List<int> RecordingIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var config in recsConfigurations)
            {
                if (IsInList((int) config.Configuration.ConfigId, RecordingIdsAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Configuration";
                    newRecordingChange.PropertyChanged = "Configuration";
                    newRecordingChange.OriginalValue = "N/A";
                    if (config.UPC != null)
                    {
                        newRecordingChange.ChangedValue = "Configuration \"" + config.Configuration.name + "\"  \n UPC: " +
                                                          config.UPC;
                    }
                    else
                    {
                        newRecordingChange.ChangedValue = "Configuration \"" + config.Configuration.name;
                    }
                    newRecordingChange.ChangeMessage = "Configuration \"" + config.Configuration.name + "\" has been added to Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }






        public List<RecsProductChanges> ComposerRemovedFromRecs(List<Snapshot_Composer> licenseRecordings,
            List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();
            var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
            if (intList.Count > 0)
            {
                foreach (var licenseRecording in licenseRecordings)
                {
                    if (IsInList(Convert.ToInt32(licenseRecording.IpCode), intList))
                    {
                        var newRecordingChange = new RecsProductChanges();
                        newRecordingChange.PropertyLocation = "Composer";
                        newRecordingChange.PropertyChanged = "Composer added to Recs " + licenseRecording.IpCode +
                                                             " has been added to Recs";

                        newRecordingChange.ChangedValue = licenseRecording.IpCode + " removed";

                        newRecordingChange.ChangeMessage = "Composer " + licenseRecording.IpCode +
                                                           " has been added to Recs";
                        listOfChanges.Add(newRecordingChange);
                    }
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> ComposersAddedToRecs(List<WorksWriter> licenseRecordings,
            List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();
            var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
            if (intList.Count > 0)
            {
                foreach (var licenseRecording in licenseRecordings)
                {
                    if (IsInList(Convert.ToInt32(licenseRecording.IpCode), intList))
                    {
                        var newRecordingChange = new RecsProductChanges();
                        newRecordingChange.PropertyLocation = "Composer";
                        newRecordingChange.PropertyChanged = "Composer Added to Recs " + licenseRecording.IpCode +
                                                             " has been Added to Recs";

                        newRecordingChange.ChangedValue = licenseRecording.IpCode + " added";

                        newRecordingChange.ChangeMessage = "Composer " + licenseRecording.IpCode +
                                                           " has been Added to Recs";
                        listOfChanges.Add(newRecordingChange);
                    }
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> LocationCodeAddedToRecs(List<string> licenseRecordings,
        List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();
            var intList = originalPublisherIpCodes.Select(_ => _).ToList();
            if (intList.Count > 0)
            {
                foreach (var licenseRecording in licenseRecordings)
                {
                    if (IsInListString(licenseRecording, intList))
                    {
                        var newRecordingChange = new RecsProductChanges();
                        newRecordingChange.PropertyLocation = "Location Code";
                        newRecordingChange.PropertyChanged = "Location Code Added to Recs " + licenseRecording +
                                                             " has been Added to Recs";

                        newRecordingChange.ChangedValue = licenseRecording + " added";

                        newRecordingChange.ChangeMessage = "Location Code " + licenseRecording +
                                                           " has been Added to Recs";
                        listOfChanges.Add(newRecordingChange);
                    }
                }
            }
            return listOfChanges;
        }
        //public List<RecsProductChanges> LocationCodeAddedToRecs(List<string> licenseRecordings,
        //    List<string> originalPublisherIpCodes)
        //{
        //    var listOfChanges = new List<RecsProductChanges>();
        //    var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
        //    if (intList.Count > 0)
        //    {
        //        foreach (var licenseRecording in licenseRecordings)
        //        {
        //            if (IsInList(Convert.ToInt32(licenseRecording), intList))
        //            {
        //                var newRecordingChange = new RecsProductChanges();
        //                newRecordingChange.PropertyLocation = "Location Code";
        //                newRecordingChange.PropertyChanged = "Location Code Added to Recs " + licenseRecording +
        //                                                     " has been Added to Recs";

        //                newRecordingChange.ChangedValue = licenseRecording + " added";

        //                newRecordingChange.ChangeMessage = "Location Code " + licenseRecording +
        //                                                   " has been Added to Recs";
        //                listOfChanges.Add(newRecordingChange);
        //            }
        //        }
        //    }
        //    return listOfChanges;
        //}

        //public List<RecsProductChanges> LocationCodeRemovedFromRecs(
        //    List<Snapshot_AquisitionLocationCode> licenseRecordings, List<string> originalPublisherIpCodes)
        //{
        //    var listOfChanges = new List<RecsProductChanges>();
        //    var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
        //    if (intList.Count > 0)
        //    {
        //        foreach (var licenseRecording in licenseRecordings)
        //        {
        //            if (IsInList(Convert.ToInt32(licenseRecording.AquisitionLocationCode), intList))
        //            {
        //                var newRecordingChange = new RecsProductChanges();
        //                newRecordingChange.PropertyLocation = "Location Code";
        //                newRecordingChange.PropertyChanged = "Location Code removed from Recs " +
        //                                                     licenseRecording.AquisitionLocationCode +
        //                                                     " has been removed from Recs";

        //                newRecordingChange.ChangedValue = licenseRecording.AquisitionLocationCode + " removed";

        //                newRecordingChange.ChangeMessage = "Location Code " + licenseRecording.AquisitionLocationCode +
        //                                                   " has been removed from Recs";
        //                listOfChanges.Add(newRecordingChange);
        //            }
        //        }
        //    }
        //    return listOfChanges;
        //}
        public List<RecsProductChanges> LocationCodeRemovedFromRecs(
          List<Snapshot_AquisitionLocationCode> licenseRecordings, List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();
            var intList = originalPublisherIpCodes.Select(_ => _).ToList();
            if (intList.Count > 0)
            {
                foreach (var licenseRecording in licenseRecordings)
                {
                    if (IsInListString(licenseRecording.AquisitionLocationCode, intList))
                    {
                        var newRecordingChange = new RecsProductChanges();
                        newRecordingChange.PropertyLocation = "Location Code";
                        newRecordingChange.PropertyChanged = "Location Code removed from Recs " +
                                                             licenseRecording.AquisitionLocationCode +
                                                             " has been removed from Recs";

                        newRecordingChange.ChangedValue = licenseRecording.AquisitionLocationCode + " removed";

                        newRecordingChange.ChangeMessage = "Location Code " + licenseRecording.AquisitionLocationCode +
                                                           " has been removed from Recs";
                        listOfChanges.Add(newRecordingChange);
                    }
                }
            }
            return listOfChanges;
        }
       public List<RecsProductChanges> LocalClientAddedToRecs(List<LocalClientCopyright> licenseRecordings,
           List<string> originalPublisherIpCodes)
       {
           var listOfChanges = new List<RecsProductChanges>();
           var intList = originalPublisherIpCodes.Select(_ => _).ToList();
           if (intList.Count > 0)
           {
               foreach (var licenseRecording in licenseRecordings)
               {
                   if (IsInListString(licenseRecording.ClientCode, intList))
                   {
                       var newRecordingChange = new RecsProductChanges();
                       newRecordingChange.PropertyLocation = "LocalClient";
                       newRecordingChange.PropertyChanged = "LocalClient Added to Recs " + licenseRecording.ClientCode +
                                                            " has been Added to Recs";

                       newRecordingChange.ChangedValue = licenseRecording.ClientCode + " added";

                       newRecordingChange.ChangeMessage = "LocalClient " + licenseRecording.ClientCode +
                                                          " has been Added to Recs";
                       listOfChanges.Add(newRecordingChange);
                   }
               }
           }
           return listOfChanges;
       }

        public List<RecsProductChanges> LocalClientRemovedFromRecs(
            List<Snapshot_LocalClientCopyright> licenseRecordings, List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();

            //int num;
            //var result = Int32.TryParse(originalPublisherIpCodes[0], out num);
            //if (result)
            //{
            //    var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
            //    if (intList.Count > 0)
            //    {
            //        foreach (var licenseRecording in licenseRecordings)
            //        {
            //            if (IsInList(Convert.ToInt32(licenseRecording.ClientCode), intList))
            //            {
            //                var newRecordingChange = new RecsProductChanges();
            //                newRecordingChange.PropertyLocation = "LocalClient";
            //                newRecordingChange.PropertyChanged = "LocalClient Removed From Recs " +
            //                                                     licenseRecording.ClientCode +
            //                                                     " has been Removed From Recs";

            //                newRecordingChange.ChangedValue = licenseRecording.ClientCode + " removed";

            //                newRecordingChange.ChangeMessage = "LocalClient " + licenseRecording.ClientCode +
            //                                                   " has been Removed From Recs";
            //                listOfChanges.Add(newRecordingChange);
            //            }
            //        }

            //    }

            //}
            //else
            //{
                var intList = originalPublisherIpCodes.Select(_ => _).ToList();
                if (intList.Count > 0)
                {
                    foreach (var licenseRecording in licenseRecordings)
                    {
                        if (IsInListString(licenseRecording.ClientCode, intList))
                        {
                            var newRecordingChange = new RecsProductChanges();
                            newRecordingChange.PropertyLocation = "LocalClient";
                            newRecordingChange.PropertyChanged = "LocalClient Removed From Recs " +
                                                                 licenseRecording.ClientCode +
                                                                 " has been Removed From Recs";

                            newRecordingChange.ChangedValue = licenseRecording.ClientCode + " removed";

                            newRecordingChange.ChangeMessage = "LocalClient " + licenseRecording.ClientCode +
                                                               " has been Removed From Recs";
                            listOfChanges.Add(newRecordingChange);
                        }
                    }

                }

           // }
            return listOfChanges;
        }



        public List<RecsProductChanges> CopyrightsAddedToRecs(List<RecsCopyrights> licenseRecordings, List<string> originalPublisherIpCodes, Snapshot_ProductHeader productHeader, Snapshot_WorksTrack track)
        {
            var listOfChanges = new List<RecsProductChanges>();
         //   var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
            if (originalPublisherIpCodes.Count > 0)
            {
                foreach (var licenseRecording in licenseRecordings)
                {
                    if (licenseRecording.WorkCode == "")
                    {
                        continue;
                    }
                    if (IsInListString(licenseRecording.WorkCode, originalPublisherIpCodes))
                    {
                        var newRecordingChange = new RecsProductChanges();
                        newRecordingChange.PropertyLocation = "Workcode";
                        newRecordingChange.PropertyChanged = "Workcode";
                        newRecordingChange.OriginalValue = "N/A";
                        newRecordingChange.ChangedValue = "Workcode: " + licenseRecording.WorkCode + " added";

                        newRecordingChange.ChangeMessage = "Workcode " + licenseRecording.WorkCode + " ("+ licenseRecording.Title+")"+
                                                           " has been added to Recs on track: "+track.Title+" on product " + productHeader.Title;
                        listOfChanges.Add(newRecordingChange);
                    }
                }
            }
            return listOfChanges;
        }
        public List<RecsProductChanges> CopyrightsRemovedFromRecs(List<Snapshot_RecsCopyright> licenseRecordings, List<string> originalPublisherIpCodes, Snapshot_ProductHeader productHeader, Snapshot_WorksTrack track)
        {
            var listOfChanges = new List<RecsProductChanges>();
        //    var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
            if (originalPublisherIpCodes.Count > 0)
            {
                foreach (var licenseRecording in licenseRecordings)
                {
                    if (licenseRecording.WorkCode == "")
                    {
                        continue;
                    }

                    if (IsInListString(licenseRecording.WorkCode, originalPublisherIpCodes))
                    {
                        var newRecordingChange = new RecsProductChanges();
                        newRecordingChange.PropertyLocation = "Workcode";
                        newRecordingChange.PropertyChanged = "Workcode";

                        newRecordingChange.OriginalValue = "Workcode: " + licenseRecording.WorkCode + " removed";
                        newRecordingChange.ChangedValue = "N/A";
                        if (licenseRecording.Title.Length > 2)
                        {
                            newRecordingChange.ChangeMessage = "Workcode " + licenseRecording.WorkCode + " (" +
                                                               licenseRecording.Title + ")" +
                                                               " has been removed from track \"" + track.Title +
                                                               "\" on product " + productHeader.Title;
                        }
                        else
                        {
                            newRecordingChange.ChangeMessage = "Workcode " + licenseRecording.WorkCode +
                                                           " has been removed from track \"" + track.Title +
                                                           "\" on product " + productHeader.Title;
                        }
                        listOfChanges.Add(newRecordingChange);
                    }
                }
            }
            return listOfChanges;
        }
        public List<RecsProductChanges> ConfigurationRemovedFromRecs(List<Snapshot_RecsConfiguration> recsConfigurations, List<int> RecordingIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var config in recsConfigurations)
            {
                if (IsInList((int)config.Configuration.CloneConfigId, RecordingIdsAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Configuration";
                    newRecordingChange.PropertyChanged = "Configuration";
                    if (config.UPC.Length > 2)
                    {
                        newRecordingChange.OriginalValue = "Config: "+config.Configuration.Name + " \n UPC: " + config.UPC;
                    }
                    else
                    {
                        newRecordingChange.OriginalValue = config.Configuration.Name;

                    }
                   
                    newRecordingChange.ChangedValue = "Config Removed";
                   
                    newRecordingChange.ChangeMessage = "Configuration \"" + config.Configuration.Name + "\" has been removed from Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        private bool IsInList(int control, List<int> list)
        {
            return list.IndexOf(control) != -1;
        }

        private bool IsInListString(string control, List<string> list)
        {
            return list.Contains(control);
        }
    }
}