using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLabelGroupRepository : ISnapshotLabelGroupRepository
    {
        public Snapshot_LabelGroup SaveSnapshotLabelGroup(Snapshot_LabelGroup snapshotLabelGroup)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_LabelGroups.Add(snapshotLabelGroup);
                context.SaveChanges();
                return snapshotLabelGroup;
            }
        }

        public Snapshot_LabelGroup GetSaSnapshotLabelGroupByLabelGroupId(int labelGroupId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LabelGroups.Find(labelGroupId);
            }
        }

        public List<Snapshot_LabelGroup> GetAllLabelGroupsForProductHeaderSnapshotId(int productHeaderSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var productHeader =
                    context.Snapshot_ProductHeaders.Include("Label")
                        .Include("Label.RecordLabelGroups")
                        .FirstOrDefault(_ => _.SnapshotProductHeaderId == productHeaderSnapshotId);
                return productHeader.Label.RecordLabelGroups.ToList();
            }

        }

        public List<Snapshot_LabelGroup> GetAllALabelGroupsForLabelId(int labelId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LabelGroups.Where(_ => _.CloneLabelId == labelId).ToList();
            }
        }

        public bool DeleteLabelGroupByLabelGroupSnapshotId(int labelGroupSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var licenseProduct =
                    context.Snapshot_LabelGroups.Find(labelGroupSnapshotId);
                context.Snapshot_LabelGroups.Attach(licenseProduct);
                context.Snapshot_LabelGroups.Remove(licenseProduct);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
    }
}