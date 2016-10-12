using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.LookUps
{
    public class AttachmentTypeManager : IAttachmentTypeManager
    {
        private readonly IAttachmentTypeRepository _attachmentTypeRepository;

        public AttachmentTypeManager(IAttachmentTypeRepository attachmentTypeRepository)
        {
            _attachmentTypeRepository = attachmentTypeRepository;
        }

        public List<LU_AttachmentType> GetAll()
        {
           return _attachmentTypeRepository.GetAll();
        }

        public LU_AttachmentType GetByAttachmentTypeId(int id)
        {
            return _attachmentTypeRepository.GetAttachmentTypeById(id);
        }

        public List<LU_AttachmentType> Search(string query)
        {
            return _attachmentTypeRepository.Search(query);
        }

        public LU_AttachmentType Add(LU_AttachmentType attachmentType)
        {
            return _attachmentTypeRepository.Add(attachmentType);
        }

        public void Update(LU_AttachmentType attachmentType)
        {
            _attachmentTypeRepository.Update(attachmentType);
        }
    }
}


