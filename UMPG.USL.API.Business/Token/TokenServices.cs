using System;
using System.Configuration;
using System.Linq;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Business.Token;
using UMPG.USL.API.Data.Token;
using UMPG.USL.Common;
using UMPG.USL.Models.Authorization;

namespace BusinessServices
{
    public class TokenServices:ITokenServices
    {
         #region Private member variables.
        private readonly ITokenRepository _tokenRepository;
         #endregion

        #region Public constructor.
        /// <summary>
        /// Public constructor.
        /// </summary>
        public TokenServices(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }
        #endregion


        #region Public member methods.

        /// <summary>
        ///  Function to generate unique token with expiry against the provided userId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TokenEntity GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(
                                              Convert.ToDouble(ConfigHelper.GetAppSettingValue("AuthTokenExpiry", true)));
            var tokendomain = new TokenEntity
                                  {
                                      UserId = userId,
                                      AuthToken = token,
                                      IssuedOn = issuedOn,
                                      ExpiresOn = expiredOn
                                  };

            return _tokenRepository.Add(tokendomain);

        }


        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public bool ValidateToken(string tokenId)
        {
            var token = _tokenRepository.Find(tokenId);
            if (token != null && !(DateTime.Now > token.ExpiresOn))
            {
                token.ExpiresOn = DateTime.Now.AddSeconds(
                    Convert.ToDouble(ConfigHelper.GetAppSettingValue("AuthTokenExpiry", true)));
                _tokenRepository.Update(token);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId">true for successful delete</param>
        public bool Kill(string tokenId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete tokens for the specific deleted user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true for successful delete</returns>
        public bool DeleteByUserId(int userId)
        {
           
            _tokenRepository.DeleteUserExpired(userId);
            return true;
        }

        #endregion
    }
}
