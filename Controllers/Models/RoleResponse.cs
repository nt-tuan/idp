using System.Collections.Generic;
using ThanhTuan.IDP.Entities;

namespace ThanhTuan.IDP.Controllers.Models
{
  public class RoleResponse
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public RoleResponse(ApplicationRole entity)
    {
      Name = entity.Name;
      Description = entity.Description;
    }
  }
}