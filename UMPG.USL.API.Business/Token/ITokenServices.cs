﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMPG.USL.Models.Authorization;


namespace UMPG.USL.API.Business.Token
{
    public interface ITokenServices
    {
        TokenEntity GenerateToken(int userId);
        bool ValidateToken(string tokenId);
        bool Kill(string tokenId);
        bool DeleteByUserId(int userId);
    }
}
