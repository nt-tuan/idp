using System;
using System.Collections.Generic;

namespace ThanhTuan.IDP.Controllers.Models
{
  public class CreateUserModel
  {
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public List<string> Roles { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool? LockoutEnable { get; set; }
  }
}