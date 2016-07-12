using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using UMPG.USL.API.Data.LicenseData;

namespace UMPG.USL.API.Business.Licenses
{
    public class LicenseSequenceIntercepror : IInterceptor
    {
        private readonly ILicenseSequenceRepository _licenseSequenceRepository;
        public LicenseSequenceIntercepror(ILicenseSequenceRepository licenseSequenceRepository)
        {
            _licenseSequenceRepository = licenseSequenceRepository;
        }


        public void Intercept(IInvocation invocation)
        {
            switch (invocation.MethodInvocationTarget.Name)
            {
                case "Add":
                    UpdateSequence();
                    break;
            }
            invocation.Proceed();
        }

        private void UpdateSequence()
        {
            var sequence = _licenseSequenceRepository.Get();
            sequence.LicenseNumber = sequence.LicenseNumber + 1;
            _licenseSequenceRepository.Update(sequence);
        }
    }
}
