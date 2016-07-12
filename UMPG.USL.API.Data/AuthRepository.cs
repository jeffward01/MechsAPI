using UMPG.USL.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMPG.USL.API.Data
{

    public class AuthRepository : IAuthRepository
    {
        private AuthContext2 _ctx;
        private UserManager<IdentityUser> _userManager;
        private IdentityResult result;


        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                IdentityUser user = new IdentityUser
                {
                    UserName = userModel.UserName
                };
                try
                {
                    var result = await _userManager.CreateAsync(user, userModel.Password);
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inner :" + ex.InnerException + " Message: " + ex.Message);
                    return result;
                }
            }
            

        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                IdentityUser user = await _userManager.FindAsync(userName, password);

                return user;
            }
        }

        public Client FindClient(string clientId)
        {
            using (_ctx = new AuthContext2())
            {
                var client = _ctx.Clients.Find(clientId);

                return client;
            }
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                var existingToken =
                    _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId)
                        .SingleOrDefault();

                if (existingToken != null)
                {
                    var result = await RemoveRefreshToken(existingToken);
                }

                _ctx.RefreshTokens.Add(token);

                return await _ctx.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

                if (refreshToken != null)
                {
                    _ctx.RefreshTokens.Remove(refreshToken);
                    return await _ctx.SaveChangesAsync() > 0;
                }

                return false;
            }
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

                return refreshToken;
            }
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                return _ctx.RefreshTokens.ToList();
            }
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                IdentityUser user = await _userManager.FindAsync(loginInfo);

                return user;
            }
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
           using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                var result = await _userManager.CreateAsync(user);

                return result;
            }
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                var result = await _userManager.AddLoginAsync(userId, login);

                return result;
            }
        }

        public void Dispose()
        {
            using (_ctx = new AuthContext2())
            {
                _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
                _ctx.Dispose();
                _userManager.Dispose();
            }

        }
    }
}