using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZIdentityDbContext : IdentityDbContext
    {
        public NZIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
