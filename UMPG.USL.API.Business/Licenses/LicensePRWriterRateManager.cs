using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Licenses
{
    public class LicensePRWriterRateManager : ILicensePRWriterRateManager
    {

        private readonly ILicensePRWriterRateRepository _licenseWriterRateRepository;
        private readonly ILicenseProductRecordingRepository _licenseProductRecordingRepository;
        private readonly ILicensePRWriterRepository _licensePRWriterRepository;


        public LicensePRWriterRateManager(ILicensePRWriterRateRepository licenseWriterRateRepository, ILicenseProductRecordingRepository licenseProductRecordingRepository, ILicensePRWriterRepository licensePRWriterRepository)
        {
            _licenseWriterRateRepository = licenseWriterRateRepository;
            _licenseProductRecordingRepository = licenseProductRecordingRepository;
            _licensePRWriterRepository = licensePRWriterRepository;

        }

        public LicenseProductRecordingWriterRate Get(int id)
        {
            return _licenseWriterRateRepository.Get(id);
        }

        public LicenseProductRecordingWriterRate GetLicenseProductRecordingWriterRates(int id)
        {
            return _licenseWriterRateRepository.Get(id);
        }
        public List<LicenseProductRecordingWriterRate> GetAll()
        {
            return _licenseWriterRateRepository.GetAll();
        }

        public LicenseProductRecordingWriterRate Add(LicenseProductRecordingWriterRate licensenote)
        {
            return _licenseWriterRateRepository.Add(licensenote);
        }

        public List<LicenseProductRecordingWriterRate> Search(string query)
        {
            return _licenseWriterRateRepository.Search(query);

        }


        public List<LicenseProductRecordingWriterRate> GetEditWriterRates(GetWritersRatesRequest request)
        {
            // Edit Rates Modal - display rates based on filters passed in

            List<int> licprodids = request.LicenseProductIds;

            // Check first if writers selected... if so, you can skip all the others

            if (request.LicenseWriterIds.Count > 0)
            {
                //pass in licensewriterids
                var writerrates = _licenseWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(request.LicenseWriterIds, request.LicenseConfigIds);

                return writerrates;

            }
            else
            { 

            // get list licenseRecordingids per product licenseproductid (if Product selected)
            var recordingsIds = _licenseProductRecordingRepository.GetLicenseProductRecordingsFromList(licprodids)
             .Select(x=> x.LicenseRecordingId)
             .DefaultIfEmpty(0)
             .ToList();

            // check to see if any filters for tracks, if so remove from list
            if (request.LicenseRecordingIds.Count > 0)
            {
                recordingsIds.RemoveAll(item => !request.LicenseRecordingIds.Contains(item));

            };
            
            // from the Recordings get the WriterIds
            var writerIds = _licensePRWriterRepository.GetLicenseRecordingWriterIds(recordingsIds);
            
            //from the writers, get the writer rates
            //check to see if a config value is selected
            var writerrates = _licenseWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(writerIds, request.LicenseConfigIds);

            return writerrates;

            };

 
            

        }


    }
}
