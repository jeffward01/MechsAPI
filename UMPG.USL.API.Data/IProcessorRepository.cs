namespace UMPG.USL.API.Data
{
    public interface IProcessorRepository
    {
        int GetUnProcessedLicenseProcessorCount();
        int GetUnProcessedDataHarmonizationCount();
        int GetUnProcessedSolrProcessorCount();
        bool IsLicenseProcessorProcessing();
        bool IsSolrProcessorProcessing();
        bool IsDataHarmonizingProcessorProcessing();
        int GetUnProcessedDataHarmonizationErrorCount();
        int GetUnProcessedSolrProcessorErrorCount();
        int GetUnProcessedLicenseProcessorErrorCount();
    }
}