using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.LookupData
{
    public class AttachmentTypeRepository : IAttachmentTypeRepository
    {
        public List<LU_AttachmentType> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.LU_AttachmentTypes.ToList();
            }
        }

        public LU_AttachmentType GetAttachmentTypeById(int attachmentTypeId)
        {
            using (var context = new AuthContext())
            {
                return context.LU_AttachmentTypes.FirstOrDefault(a => a.AttachmentTypeId == attachmentTypeId);
            }
        }

        public LU_AttachmentType Add(LU_AttachmentType attachmentType)
        {
            using (var context = new AuthContext())
            {
                return context.LU_AttachmentTypes.Add(attachmentType);
            }
        }


        public List<LU_AttachmentType> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var AttachmentTypes = context.LU_AttachmentTypes.Where(c => c.AttachmentType == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return AttachmentTypes.Where(c => c.AttachmentType.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return AttachmentTypes.ToList();
                }
            }
        }

        public void Update(LU_AttachmentType attachmentType)
        {
            using (var context = new AuthContext())
            {
                context.Entry(attachmentType).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }



    }
}
