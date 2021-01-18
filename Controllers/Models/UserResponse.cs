using System;
using System.Collections.Generic;
using ThanhTuan.IDP.Entities;

namespace ThanhTuan.IDP.Controllers.Models
{
  public class PublicUserInfo
  {
    public string Username { get; set; }
    public string Image { get; set; }
    public string Nickname { get; set; }
    public string Fullname { get; set; }
    public PublicUserInfo(ApplicationUser user)
    {
      Username = user.UserName;
      Image = user.Image;
      Nickname = user.Nickname;
      Fullname = user.Fullname;
    }
  }
  public class UserResponse
  {
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Fullname { get; set; }
    public string Phone { get; set; }
    public string Nickname { get; set; }
    public string Image { get; set; }
    public IList<string> Roles { get; set; }
    public bool LockoutEnable { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public UserResponse(ApplicationUser entity)
    {
      Id = entity.Id;
      Username = entity.UserName;
      LockoutEnable = entity.LockoutEnabled;
      LockoutEnd = entity.LockoutEnd;
      Email = entity.Email;
      Fullname = entity.Fullname;
      Nickname = entity.Nickname;
      Image = entity.Image;
      Phone = entity.PhoneNumber;
    }
    public UserResponse(ApplicationUser entity, IList<string> roles) : this(entity)
    {
      Roles = roles;
    }
  }
}