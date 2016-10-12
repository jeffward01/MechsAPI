using System.Collections.Generic;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface IAttachmentTypeRepository
    {
        List<LU_AttachmentType> GetAll();
        LU_AttachmentType GetAttachmentTypeById(int attachmentTypeId);
        LU_AttachmentType Add(LU_AttachmentType attachmentType);
        List<LU_AttachmentType> Search(string query);

        void Update(LU_AttachmentType attachmentType);
    }
}