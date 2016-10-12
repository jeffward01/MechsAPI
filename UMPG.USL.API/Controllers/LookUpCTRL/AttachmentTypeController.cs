using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UMPG.USL.API.Business.LookUps;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Controllers.LookUpCTRL
{
    [RoutePrefix("api/attachmentTypeCTRL/attachmentTypes")]
    public class AttachmentTypeController : ApiController
    {
        private readonly IAttachmentTypeManager _attachmentTypeManager;

        public AttachmentTypeController(IAttachmentTypeManager attachmentTypeManager)
        {
            _attachmentTypeManager = attachmentTypeManager;
        }

        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_AttachmentType> GetAll()
        {
            return _attachmentTypeManager.GetAll();
        }


        [Route("GetAttachmentTypeById/{attachmentId}")]
        [HttpGet]
        public LU_AttachmentType GetAttachmentTypeById(int attachmentId)
        {
            return _attachmentTypeManager.GetByAttachmentTypeId(attachmentId);
        }

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<LU_AttachmentType> Search([FromBody] string query)
        {

            return _attachmentTypeManager.Search(query);
        }

        [Route("Add")]
        [HttpPost]
        public LU_AttachmentType Add(LU_AttachmentType attachmentType)
        {

            return _attachmentTypeManager.Add(attachmentType);
        }


        [Route("Update")]
        [HttpPost]
        public bool Update(LU_AttachmentType attachmentType)
        {
            try
            {
                _attachmentTypeManager.Update(attachmentType);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }


        #region Helpers



        #endregion
    }
}
