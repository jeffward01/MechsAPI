﻿using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotKnownAsRepository
    {
        public List<Snapshot_KnownAs> GetAllKnownAsForWriterCaeCode(int caeCode)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_KnownAs.Where(_ => _.CloneWriterCaeCode == caeCode).ToList();
            }
        }

        public bool DeleteKnownAsBySnapshotId(int snapshotId)
        {
            using (var context = new AuthContext())
            {
                var knownAs = context.Snapshot_KnownAs.Find(snapshotId);
                context.Snapshot_KnownAs.Attach(knownAs);
                context.Snapshot_KnownAs.Remove(knownAs);
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