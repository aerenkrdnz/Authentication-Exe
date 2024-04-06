using AuthenticationExe.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationExe.Data.Context
{
    public class AuthenticationExeContext : DbContext
    {
        public AuthenticationExeContext(DbContextOptions<AuthenticationExeContext> options) : base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserEntity> Users => Set<UserEntity>();
    }
}
