using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UMPG.USL.API.Data;
using UMPG.USL.Models;

namespace UMPG.USL.API.Business
{
    public class AuthManager:IAuthManager
    {
        private readonly IAuthRepository _authRepository;

        public AuthManager(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            return _authRepository.RegisterUser(userModel);
        }

        public Task<IdentityUser> FindUser(string userName, string password)
        {
            return _authRepository.FindUser(userName, password);
        }

        public Task<bool> AddRefreshToken(RefreshToken token)
        {
            return _authRepository.AddRefreshToken(token);
        }

        public Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            return _authRepository.RemoveRefreshToken(refreshTokenId);
        }

        public Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            return _authRepository.RemoveRefreshToken(refreshToken);
        }

        public Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            return _authRepository.FindRefreshToken(refreshTokenId);
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _authRepository.GetAllRefreshTokens();
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            return _authRepository.FindAsync(loginInfo);
        }

        public Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            return _authRepository.CreateAsync(user);
        }

        public Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            return _authRepository.AddLoginAsync(userId, login);
        }
        public void Dispose()
        {
            _authRepository.Dispose();

        }

        public Client FindClient(string clientId)
        {
            return _authRepository.FindClient(clientId);
        }
    }
}
