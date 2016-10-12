using System.Collections.Generic;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.LookUps
{
    public interface IAttachmentTypeManager
    {
        List<LU_AttachmentType> GetAll();
        LU_AttachmentType GetByAttachmentTypeId(int id);
        List<LU_AttachmentType> Search(string query);
        LU_AttachmentType Add(LU_AttachmentType attachmentType);

        void Update(LU_AttachmentType attachmentType);
    }
}