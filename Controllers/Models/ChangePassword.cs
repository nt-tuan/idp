using System.Collections.Generic;
using ThanhTuan.IDP.Entities;

namespace ThanhTuan.IDP.Controllers.Models
{
  public class ChangePasswordModel
  {
    public string NewPassword { get; set; }
    public string OldPassword { get; set; }
  }
}