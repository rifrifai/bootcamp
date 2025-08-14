using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            Batteries_V2.Init();
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // List<IdentityRole> roles = new List<IdentityRole>
            // {
            //     new IdentityRole {
            //         Name = "Admin",
            //         NormalizedName = "ADMIN"
            //     },
            //     new IdentityRole {
            //         Name = "User",
            //         NormalizedName = "USER"
            //     },
            // };
            // builder.Entity<IdentityRole>().HasData(roles);
            SeedData(builder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "a1b2c3d4-e5f6-7890-abcd-123456789abc",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "role-admin-stamp"
                },
                new IdentityRole
                {
                    Id = "b2c3d4e5-f6g7-8901-bcde-234567890def",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "role-user-stamp"
                }
            );
        }
    }
}