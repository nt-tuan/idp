using System;
using System.Collections.Generic;

namespace ThanhTuan.IDP.Controllers.Models
{
  public class EditUserModel
  {
    public string Fullname { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string Image { get; set; }
    public string Phone { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool? LockoutEnable { get; set; }
  }
}