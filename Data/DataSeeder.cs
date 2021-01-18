using System;
using System.Collections.Generic;
using System.IO;
using ThanhTuan.IDP.Controllers.Models;
using ThanhTuan.IDP.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace ThanhTuan.IDP.Data
{
  public class DataSeeder
  {
    readonly UserManager<ApplicationUser> userManager;
    readonly RoleManager<ApplicationRole> roleManager;
    public DataSeeder(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
      this.userManager = userManager;
      this.roleManager = roleManager;
    }

    public void Seed()
    {
      SeedRoles();
      SeedUsers();
    }

    private string ReadFile(string filepath)
    {
      if (string.IsNullOrEmpty(filepath))
      {
        throw new Exception();
      }
      using var reader = new StreamReader(filepath);
      var content = reader.ReadToEnd();
      return content;
    }

    private void SeedRoles()
    {
      var content = ReadFile(Constant.INIT_ROLES_FILEPATH);
      var roles = JsonConvert.DeserializeObject<List<ApplicationRole>>(content);
      foreach (var role in roles)
      {
        var entity = roleManager.FindByNameAsync(role.Name).Result;
        if (entity == null)
        {
          roleManager.CreateAsync(role).Wait();
        }
      }

    }
    private void SeedUsers()
    {
      var content = ReadFile(Constant.INIT_USERS_FILEPATH);
      var users = JsonConvert.DeserializeObject<List<CreateUserModel>>(content);
      foreach (var user in users)
      {
        var entity = userManager.FindByNameAsync(user.Username).Result;
        if (entity == null)
        {
          var result = userManager.CreateAsync(new ApplicationUser { UserName = user.Username, Email = user.Email }, user.Password).Result;
          if (result.Succeeded)
          {
            entity = userManager.FindByNameAsync(user.Username).Result;
          }
        }
        userManager.AddToRolesAsync(entity, user.Roles).Wait();
      }
    }
  }
}