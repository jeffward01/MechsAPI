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
        public List<RecsProductChanges> ProductAddedToRecs(List<LicenseProduct> licenseProducts, List<int> productIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseProduct in licenseProducts)
            {
                if (IsInList(licenseProduct.ProductId, productIdsAdded))
                {
                    var newProductChange = new RecsProductChanges();
                    newProductChange.PropertyLocation = "Product";
                    newProductChange.PropertyChanged = "Product " + licenseProduct.ProductId + " has been added to Recs";
                    if (licenseProduct.ProductHeader.Title != null)
                    {
                        newProductChange.ChangedValue = licenseProduct.ProductHeader.Title;
                    }
                    else
                    {
                        newProductChange.ChangedValue = licenseProduct.ProductId.ToString() + " Added";
                    }
                    newProductChange.ChangeMessage = "Product " + licenseProduct.ProductId + " has been added to Recs";
                    listOfChanges.Add(newProductChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> ProductRemovedFromRecs(List<Snapshot_LicenseProduct> licenseProducts, List<int> productIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseProduct in licenseProducts)
            {
                if (IsInList((int)licenseProduct.ProductId, productIdsAdded))
                {
                    var newProductChange = new RecsProductChanges();
                    newProductChange.PropertyLocation = "Product";
                    newProductChange.PropertyChanged = "Product " + licenseProduct.ProductId + " has been removed from Recs";
                    if (licenseProduct.ProductHeader.Title != null)
                    {
                        newProductChange.ChangedValue = licenseProduct.ProductHeader.Title;
                    }
                    else
                    {
                        newProductChange.ChangedValue = licenseProduct.ProductId.ToString() + " Removed";
                    }
                    newProductChange.ChangeMessage = "Product " + licenseProduct.ProductId + " has been removed from Recs";
                    listOfChanges.Add(newProductChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> RecordingRemovedFromRecs(List<Snapshot_WorksRecording> licenseRecordings, List<int> RecordingIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInList((int)licenseRecording.CloneTrackId, RecordingIdsAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Recording";
                    newRecordingChange.PropertyChanged = "Recording " + licenseRecording.CloneTrackId + " has been removed from Recs";
                    if (licenseRecording.Track.Title != null)
                    {
                        newRecordingChange.ChangedValue = licenseRecording.Track.Title;
                    }
                    else
                    {
                        newRecordingChange.ChangedValue = licenseRecording.CloneTrackId.ToString() + " Removed";
                    }
                    newRecordingChange.ChangeMessage = "Recording " + licenseRecording.CloneTrackId + " has been removed from Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> RecordingAddedToRecs(List<WorksRecording> licenseRecordings, List<int> RecordingIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInList(licenseRecording.TrackId, RecordingIdsAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Recording";
                    newRecordingChange.PropertyChanged = "Recording " + licenseRecording.TrackId + " has been added to Recs";
                    if (licenseRecording.Track.Title != null)
                    {
                        newRecordingChange.ChangedValue = licenseRecording.Track.Title;
                    }
                    else
                    {
                        newRecordingChange.ChangedValue = licenseRecording.TrackId.ToString() + " Added";
                    }
                    newRecordingChange.ChangeMessage = "Recording " + licenseRecording.TrackId + " has been added to Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> OriginalPublisherAffiliationAddedToRecs(List<Affiliation> licenseRecordings, List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();

            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInListString(licenseRecording.IncomeGroup, originalPublisherIpCodes))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Original Publisher Affiliation";
                    newRecordingChange.PropertyChanged = "Original Publisher Affiliation added to Recs " + licenseRecording.IncomeGroup + " has been added to Recs";

                    newRecordingChange.ChangedValue = licenseRecording.IncomeGroup + " Added";

                    newRecordingChange.ChangeMessage = "Original Publisher Affiliation" + licenseRecording.IncomeGroup + " has been added to  Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> OriginalPublisherAffiliationRemovedFromRecs(List<Snapshot_OriginalPublisherAffiliation> licenseRecordings, List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();
        
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInListString(licenseRecording.IncomeGroup, originalPublisherIpCodes))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Original Publisher Affiliation";
                    newRecordingChange.PropertyChanged = "Original Publisher Affiliation removed from Recs " + licenseRecording.IncomeGroup + " has been removed from  Recs";

                    newRecordingChange.ChangedValue = licenseRecording.IncomeGroup + " Removed";

                    newRecordingChange.ChangeMessage = "Original Publisher Affiliation" + licenseRecording.IncomeGroup + " has been removed from Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }


        public List<RecsProductChanges> OriginalPublishersAddedToRecs(List<OriginalPublisher> licenseRecordings, List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();
            var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInList(Convert.ToInt32(licenseRecording.IpCode), intList))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Original Publisher";
                    newRecordingChange.PropertyChanged = "Original Publisher added to Recs " + licenseRecording.IpCode + " has been added to  Recs";

                    newRecordingChange.ChangedValue = licenseRecording.FullName + " Added";

                    newRecordingChange.ChangeMessage = "Original Publisher " + licenseRecording.CaeNumber + " has been added to  Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> OriginalPublishersRemovedFromRecs(List<Snapshot_OriginalPublisher> licenseRecordings, List<string> originalPublisherIpCodes)
        {
            var listOfChanges = new List<RecsProductChanges>();
            var intList = originalPublisherIpCodes.Select(_ => Convert.ToInt32(_)).ToList();
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInList(Convert.ToInt32(licenseRecording.IpCode), intList))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Original Publisher";
                    newRecordingChange.PropertyChanged = "Original Publisher removed from Recs " + licenseRecording.IpCode + " has been removed from Recs";

                    newRecordingChange.ChangedValue = licenseRecording.FullName + " Added";

                    newRecordingChange.ChangeMessage = "Original Publisher " + licenseRecording.IpCode + " has been removed from Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> WritersAddedToRecs(List<WorksWriter> licenseRecordings, List<int> writerCaeCodesAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInList(licenseRecording.CaeNumber, writerCaeCodesAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Writer";
                    newRecordingChange.PropertyChanged = "Writer added to Recs " + licenseRecording.CaeNumber + " has been added to  Recs";

                    newRecordingChange.ChangedValue = licenseRecording.FullName + " Added";

                    newRecordingChange.ChangeMessage = "Writer " + licenseRecording.CaeNumber + " has been added to  Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> WritersRemovedFromRecs(List<Snapshot_WorksWriter> licenseRecordings, List<int> writerCaeCodesAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var licenseRecording in licenseRecordings)
            {
                if (IsInList(licenseRecording.CloneCaeNumber, writerCaeCodesAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Writer";
                    newRecordingChange.PropertyChanged = "Writer Removed from Recs " + licenseRecording.CloneCaeNumber + " has been removed from Recs";

                    newRecordingChange.ChangedValue = licenseRecording.FullName + " Removed";

                    newRecordingChange.ChangeMessage = "Writer " + licenseRecording.CloneCaeNumber + " has been removed from Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> ConfigurationAddedToRecs(List<RecsConfiguration> recsConfigurations, List<int> RecordingIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var config in recsConfigurations)
            {
                if (IsInList((int)config.configuration_id, RecordingIdsAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Configuration";
                    newRecordingChange.PropertyChanged = "Configuration " + config.configuration_id + " has been added to Recs";
                    if (config.UPC != null)
                    {
                        newRecordingChange.ChangedValue = config.UPC;
                    }
                    else
                    {
                        newRecordingChange.ChangedValue = "New Config Added";
                    }
                    newRecordingChange.ChangeMessage = "Configuration " + config.configuration_id + " has been added to Recs";
                    listOfChanges.Add(newRecordingChange);
                }
            }
            return listOfChanges;
        }

        public List<RecsProductChanges> ConfigurationRemovedFromRecs(List<Snapshot_RecsConfiguration> recsConfigurations, List<int> RecordingIdsAdded)
        {
            var listOfChanges = new List<RecsProductChanges>();
            foreach (var config in recsConfigurations)
            {
                if (IsInList((int)config.CloneRecsConfigurationId, RecordingIdsAdded))
                {
                    var newRecordingChange = new RecsProductChanges();
                    newRecordingChange.PropertyLocation = "Configuration";
                    newRecordingChange.PropertyChanged = "Configuration " + config.CloneRecsConfigurationId + " has been removed to Recs";
                    if (config.UPC != null)
                    {
                        newRecordingChange.ChangedValue = config.UPC;
                    }
                    else
                    {
                        newRecordingChange.ChangedValue = "Config Removed";
                    }
                    newRecordingChange.ChangeMessage = "Configuration " + config.CloneRecsConfigurationId + " has been removed to Recs";
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