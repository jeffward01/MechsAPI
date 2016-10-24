using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotContactManger
    {
        Snapshot_Contact SaveSnapshotContact(Snapshot_Contact snapshotContact);
        Snapshot_Contact GetSnapshotContactByContactId(int snapshotContactId);
    }
}