using Microsoft.AspNetCore.Identity;

namespace ThanhTuan.IDP.Entities
{
  public class ApplicationUser : IdentityUser<string>
  {
    public string Image { get; set; }
    public string Fullname { get; set; }
    public string Nickname { get; set; }
  }
}
