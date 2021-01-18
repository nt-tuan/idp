using Microsoft.AspNetCore.Identity;
namespace ThanhTuan.IDP.Entities
{
  public class ApplicationRole : IdentityRole
  {
    public string Description { get; set; }
  }
}