namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotAddressManager
    {
        bool DeleteAllAddressForCloneContactId(int cloneContactId);
    }
}