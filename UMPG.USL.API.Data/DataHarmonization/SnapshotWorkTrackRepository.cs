using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotWorkTrackRepository : ISnapshotWorkTrackRepository
    {
        public Snapshot_WorksTrack GetTrackForCloneTrackId(int cloneTrackId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Tracks.Include("Artist").FirstOrDefault(_ => _.CloneWorksTrackId == cloneTrackId);
            }
        }

        public Snapshot_WorksTrack GetTrackBySnapshotWorksTrackId(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Tracks.FirstOrDefault(_ => _.SnapshotWorkTrackId == id);
            }
        }

        public Snapshot_WorksTrack SaveWorksTrack(Snapshot_WorksTrack worksTrack)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Tracks.Add(worksTrack);
                context.SaveChanges();
                return worksTrack;
            }
        }

        public bool DeleteTrackBySnapshotTrackId(int snapshotTrackId)
        {
            using (var context = new AuthContext())
            {
                var address = context.Snapshot_Tracks.Find(snapshotTrackId);
                context.Snapshot_Tracks.Attach(address);
                context.Snapshot_Tracks.Remove(address);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}