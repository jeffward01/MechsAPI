using System.Data.Entity.ModelConfiguration.Conventions;
using UMPG.USL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Migrations;


namespace UMPG.USL.API.Data
{
    public class AuthContext2 : IdentityDbContext<IdentityUser>
    {
        public AuthContext2(): base("AuthContext2")
        {
            Database.SetInitializer<AuthContext>(null);
            
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
 
    }

}