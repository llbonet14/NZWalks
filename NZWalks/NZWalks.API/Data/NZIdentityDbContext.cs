using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZIdentityDbContext : IdentityDbContext
    {
        public NZIdentityDbContext(DbContextOptions<NZIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleId = "809838d4-4252-4bf6-9fc6-231fa6142fe3";
            var writerRoleId = "2c2bd542-7674-47f2-a428-68358f1d5f3";

            var roles = new List<IdentityRole>
            {
                new() {Id = readerRoleId, ConcurrencyStamp = readerRoleId, Name = "Reader", NormalizedName = "READER"},
                new() {Id = writerRoleId, ConcurrencyStamp = writerRoleId, Name = "Writer", NormalizedName = "WRITER"}
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
