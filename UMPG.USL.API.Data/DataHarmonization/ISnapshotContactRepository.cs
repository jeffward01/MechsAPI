﻿using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotContactRepository
    {
        Snapshot_Contact SaveSnapshotContact(Snapshot_Contact contactSnapshot);

        Snapshot_Contact GetSanSnapshotContactByContactId(int contactId);

        bool DeleteContactBySnapshotContactId(int snapshotContactId);

        int GetRoleIdForCOntactId(int contactId);
        int GetCloneContactIdForContactId(int contactId);
    }
}