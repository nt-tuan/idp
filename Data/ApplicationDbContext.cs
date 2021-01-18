using ThanhTuan.IDP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThanhTuan.IDP.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, String>
  {
    public DbSet<SignInLog> SignInLogs { get; set; }
    public ApplicationDbContext(
        DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<SignInLog>().HasIndex(e => e.LoginChallenge);
      modelBuilder.Entity<SignInLog>().HasIndex(e => e.ConsentChallenge);
      modelBuilder.Entity<ApplicationUser>().Property(e => e.Id).ValueGeneratedOnAdd();
    }

    public async Task<List<SignInLog>> GetAccessLogs(string userName)
    {
      return await SignInLogs.Where(u => u.UserName == userName).OrderByDescending(u => u.Id).ToListAsync();
    }
  }
}
