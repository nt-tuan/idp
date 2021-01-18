
using System.Linq;
using System.Threading.Tasks;
using ThanhTuan.IDP;
using ThanhTuan.IDP.Controllers.Models;
using ThanhTuan.IDP.Entities;
using ThanhTuan.IDP.Hydra;
using ThanhTuan.IDP.Hydra.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using ThanhTuan.IDP.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Web.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class MeController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly IHydra _hydra;
    public MeController(UserManager<ApplicationUser> userManager, IHydra hydra, ApplicationDbContext db)
    {
      _userManager = userManager;
      _hydra = hydra;
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<UserResponse>> Get()
    {
      var appUser = await GetUser();
      if (appUser == null) return NotFound();
      var roles = await _userManager.GetRolesAsync(appUser);
      return new UserResponse(appUser, roles);
    }

    [HttpGet("logs")]
    public async Task<ActionResult<List<SignInLog>>> GetAccessLogs()
    {
      var appUser = await GetUser();
      if (appUser == null) return NotFound();
      return await _db.GetAccessLogs(appUser.UserName);
    }

    [HttpPost("changepassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
    {
      var user = await GetUser();
      if (user == null)
        return NotFound();
      var rs = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
      if (rs.Succeeded)
        return Ok();
      return ResponseIdentityResultError(rs);
    }

    [HttpGet("consentSessions")]
    public async Task<ActionResult<List<ConsentSection>>> GetListConsentSession()
    {
      var user = await GetUser();
      if (user == null)
        return NotFound();
      return await _hydra.ListAllConsentSessions(user.UserName);
    }

    [HttpDelete("revokeConsentSession")]
    public async Task<ActionResult> RevokeConsentSession(string client, bool? all)
    {
      var user = await GetUser();
      if (user == null)
        return NotFound();
      await _hydra.RevokeConsentSession(user.UserName, client, all);
      return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<UserResponse>> UpdateProfile(EditUserModel model)
    {
      var user = await GetUser();
      if (user == null) return NotFound();
      user.Email = model.Email;
      user.PhoneNumber = model.Phone;
      user.Fullname = model.Fullname;
      user.Nickname = model.Nickname;
      user.Image = model.Image;
      if (model.LockoutEnable != null)
      {
        user.LockoutEnabled = model.LockoutEnable.Value;
      }
      if (model.LockoutEnd != null)
      {
        user.LockoutEnd = model.LockoutEnd;
      }
      await _userManager.UpdateAsync(user);
      return new UserResponse(user);
    }
    private async Task<ApplicationUser> GetUser()
    {
      var subject = Request.Headers[Constant.USER_HEADER_KEY];
      if (string.IsNullOrEmpty(subject))
      {
        return null;
      }
      return await _userManager.FindByNameAsync(Request.Headers[Constant.USER_HEADER_KEY]);
    }

    private BadRequestObjectResult ResponseIdentityResultError(IdentityResult identityResult)
    {
      var messages = identityResult.Errors.Select(u => $"{u.Code}: {u.Description}").ToList();
      var errResponse = new ErrorResponse();
      if (messages.Count > 0)
        errResponse.Message = messages[0];
      errResponse.Messages = messages;
      return BadRequest(errResponse);
    }

  }
}