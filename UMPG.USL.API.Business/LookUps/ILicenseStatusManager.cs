﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.Lookups
{
    public interface ILicenseStatusManager
    {
        LU_LicenseStatus Add(LU_LicenseStatus licensemethod);

        LU_LicenseStatus Get(int id);

        List<LU_LicenseStatus> GetAll();


    }
}
