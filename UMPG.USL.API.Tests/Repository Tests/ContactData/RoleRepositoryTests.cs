using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit;
using NUnit.Framework;
using UMPG.USL.API.Business.Audits;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.API.Controllers.LicenseCTRL;
using UMPG.USL.API.Business.Lookups;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.API.Controllers.LookUpCTRL;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Controllers.RECsCTRL;
using UMPG.USL.API.Business;
using System.Web.Http;
using UMPG.USL.API.Controllers;
 
using UMPG.USL.Security.Safe;
using System.Net;
using UMPG.USL.API.Data.AuditData;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.Models.Security;

namespace UMPG.USL.API.Tests.Repository_Tests.ContactData
{
    public class RoleRepositoryTests
    {
        public void GetRoleForContact_ReturnRole()
        {
            //Arrange
            var mockRoleRepository = A.Fake<IRoleRepository>();

            //Build Expected 
            Role expected = new Role { };

            A.CallTo(() => mockRoleRepository.GetRoleForContact(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockRoleRepository.GetRoleForContact(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockRoleRepository.GetRoleForContact(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        public void GetRole_ReturnRole()
        {
            //Arrange
            var mockRoleRepository = A.Fake<IRoleRepository>();

            //Build Expected 
            Role expected = new Role { };

            A.CallTo(() => mockRoleRepository.GetRole(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockRoleRepository.GetRole(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockRoleRepository.GetRole(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        public void GetAllRoles_ReturnListRole()
        {
            //Arrange
            var mockRoleRepository = A.Fake<IRoleRepository>();

            //Build Expected 
            List<Role> expected = new List<Role> { };

            A.CallTo(() => mockRoleRepository.GetAllRoles(A<bool>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockRoleRepository.GetAllRoles(A<bool>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockRoleRepository.GetAllRoles(A<bool>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        public void GetRoles_ReturnListRole()
        {
            //Arrange
            var mockRoleRepository = A.Fake<IRoleRepository>();

            //Build Expected 
            List<Role> expected = new List<Role> { };

            A.CallTo(() => mockRoleRepository.GetRoles(A<List<int>>.Ignored, A<bool>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockRoleRepository.GetRoles(A<List<int>>.Ignored, A<bool>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockRoleRepository.GetRoles(A<List<int>>.Ignored, A<bool>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
