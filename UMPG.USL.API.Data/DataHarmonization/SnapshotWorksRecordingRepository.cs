﻿using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotWorksRecordingRepository : ISnapshotWorksRecordingRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public Snapshot_WorksRecording SaveSnapshotWorksRecording(Snapshot_WorksRecording snapshotWorksRecording)
        {
            using (var context = new AuthContext())
            {
                Logger.Info("CLONE TRACK ID: " + snapshotWorksRecording.CloneTrackId);
                Logger.Info("TRACK ID: " + snapshotWorksRecording.TrackId);
                context.Snapshot_WorksRecordings.Add(snapshotWorksRecording);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Logger.Debug(e.ToString());
                    throw new Exception(e.ToString());
                }
          
                return snapshotWorksRecording;
            }
        }

        public Snapshot_WorksRecording GetSnapshotWorksRecordingByWorksRecordingId(int? worksRecordingId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_WorksRecordings.Find(worksRecordingId);
            }
        }

        public Snapshot_WorksRecording GetWorksRecordingForSnapshotTrackId(int trackId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_WorksRecordings.FirstOrDefault(_ => _.SnapshotWorkTrackId == trackId);
            }
        }

        public List<Snapshot_WorksRecording> GetAllWorksRecordingsForProductId(int? productId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_WorksRecordings.Where(_ => _.ProductId == productId).ToList();
            }
        }
        public List<Snapshot_WorksRecording> GetAllWorksRecordingsForLicenseProductId(int? productId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_WorksRecordings.
                    
                    Include("Track").
                    Include("Track.Copyrights").
                    Include("Track.Copyrights.Composers").
                    Include("Track.Copyrights.Samples").
                    Include("Track.Copyrights.Samples.LocalClients").
                    Include("Track.Copyrights.Samples.AquisitionLocationCodes").
                    Include("Track.Copyrights.LocalClients").
                    Include("Track.Copyrights.AquisitionLocationCodes").
                    Include("Track.Artist").
                    Include("LicenseRecording").
                    Include("Writers").

                    Where(_ => _.LicenseProductId == productId).ToList();
            }
        }

        public Snapshot_WorksRecording GetSnapshotWorksRecordingForTrackId(int snapshotTrackId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_WorksRecordings.FirstOrDefault(_ => _.SnapshotWorkTrackId == snapshotTrackId);
            }
        }

        public bool DeleteWorkRecordingByRecordignSnapshotId(int recordingSnapshotIdea)
        {
            using (var context = new AuthContext())
            {
                var recording = context.Snapshot_WorksRecordings.Find(recordingSnapshotIdea);
                context.Snapshot_WorksRecordings.Attach(recording);
                context.Snapshot_WorksRecordings.Remove(recording);
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