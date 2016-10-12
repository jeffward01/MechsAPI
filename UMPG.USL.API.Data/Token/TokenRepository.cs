using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.Authorization;

namespace UMPG.USL.API.Data.Token
{
    public class TokenRepository : ITokenRepository
    {
        public TokenEntity Add(TokenEntity tokenEntity)
        {
            using (var context = new AuthContext())
            {
                context.Tokens.Add(tokenEntity);
                context.SaveChanges();
                return tokenEntity;
            }
        }

        public TokenEntity Update(TokenEntity tokenEntity)
        {
            using (var context = new AuthContext())
            {
                context.Entry(tokenEntity).State = EntityState.Modified;
                context.SaveChanges();
                return tokenEntity;
            }
        }

        public TokenEntity Find(string tokenId)
        {
            using (var context = new AuthContext())
            {
                return context.Tokens.FirstOrDefault(t => t.AuthToken == tokenId);
            }

        }

        public void DeleteByUserId(int userId)
        {
            using (var context = new AuthContext())
            {
                var tokens = context.Tokens.Where(t => t.UserId == userId);
                foreach (var token in tokens)
                {
                    context.Tokens.Remove(token);
                }
            }
        }

        public void DeleteUserExpired(int userId)
        {
            using (var context = new AuthContext())
            {
                var tokens = context.Tokens.Where(t => t.UserId == userId && t.ExpiresOn < DateTime.Now);
                foreach (var token in tokens)
                {
                    context.Tokens.Remove(token);
                }
            }
        }
    }
}
