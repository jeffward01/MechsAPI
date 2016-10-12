using UMPG.USL.Models.Authorization;

namespace UMPG.USL.API.Data.Token
{
    public interface ITokenRepository
    {
        TokenEntity Add(TokenEntity tokenEntity);
        TokenEntity Update(TokenEntity tokenEntity);
        TokenEntity Find(string tokenId);
        void DeleteByUserId(int userId);
        void DeleteUserExpired(int userId);
    }
}