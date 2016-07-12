﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Licenses
{
    public class LicenseAttachmentManager : ILicenseAttachmentManager
    {

        private readonly ILicenseAttachmentRepository _licenseAttachmentRepository;

        public LicenseAttachmentManager(ILicenseAttachmentRepository licenseAttachmentRepository)
        {
            _licenseAttachmentRepository = licenseAttachmentRepository;

        }

        public LicenseAttachment Get(int id)
        {
            return _licenseAttachmentRepository.Get(id);
        }

        
        public List<LicenseAttachment> GetAll()
        {
            return _licenseAttachmentRepository.GetAll();
        }

        public List<LicenseAttachment> GetAllAttachmentsByLicenseId(int licenseId)
        {
            return _licenseAttachmentRepository.GetAll().Where(c=>c.licenseId == licenseId).ToList();
        }

        public LicenseAttachment GetLicenseAttachement(int licenseAttachmentId)
        {
            return _licenseAttachmentRepository.Get(licenseAttachmentId);
        }
        public void AddLicenseAttachment(LicenseAttachment licenseAttachment)
        {
            LicenseAttachment existingAttachment = _licenseAttachmentRepository.Get(licenseAttachment.fileName, licenseAttachment.licenseId);
            if (existingAttachment != null)
            {
                existingAttachment.uploaddedDate = licenseAttachment.uploaddedDate;
                existingAttachment.ModifiedDate = DateTime.Now;
                existingAttachment.ModifiedBy = licenseAttachment.ModifiedBy;
                existingAttachment.CreatedBy = licenseAttachment.CreatedBy;
                _licenseAttachmentRepository.Update(existingAttachment);
            }
            else
            {
                _licenseAttachmentRepository.Add(licenseAttachment);
            }
        }

        public void RemoveLicenseAttachment(LicenseAttachment licenseAttachment)
        {
            LicenseAttachment existingAttachment = _licenseAttachmentRepository.Get(licenseAttachment.fileName, licenseAttachment.licenseId);
            if (existingAttachment != null)
            {
                existingAttachment.Deleted = DateTime.Now;
                existingAttachment.ModifiedDate = DateTime.Now;
                _licenseAttachmentRepository.Update(existingAttachment);
            }
        }

        //public Licensee Add(Licensee licensee)
        //{
        //    return _licenseeRepository.Add(licensee);
        //}

        public List<LicenseAttachment> Search(string query)
        {
            return _licenseAttachmentRepository.Search(query);
            
        }


    }
}