using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.Licenses
{
    public class LicenseRecordingMedleyManager:ILicenseRecordingMedleyManager
    {
        private readonly ILicenseRecordingMedleyRepository _medleyRepository;

        public LicenseRecordingMedleyManager(ILicenseRecordingMedleyRepository medleyRepository)
        {
            _medleyRepository = medleyRepository;

        }

        public void AddMedleys(List<LicenseRecordingMedley> medleys)
        {
            //get existing and delete
            var first = medleys.FirstOrDefault();
            var existing = _medleyRepository.GetMedleysByTrackId(first.TrackId);
            foreach (var licenseRecordingMedley in existing)
            {
                licenseRecordingMedley.ModifiedBy = first.CreatedBy;
                licenseRecordingMedley.ModifiedDate = DateTime.Now;

                licenseRecordingMedley.Deleted = DateTime.Now;
                _medleyRepository.Update(licenseRecordingMedley);

            }
            foreach (var licenseRecordingMedley in medleys)
            {
                if (licenseRecordingMedley.MedleyTrackId!=0)
                {
                    licenseRecordingMedley.CreatedDate = DateTime.Now;
                    _medleyRepository.Add(licenseRecordingMedley);    
                }
                
            }
        }

        public List<LicenseRecordingMedley> GetMedleysByTrackId(long trackId)
        {
            return _medleyRepository.GetMedleysByTrackId(trackId);
        }
    }
}
