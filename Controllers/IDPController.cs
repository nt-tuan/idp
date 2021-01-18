using System;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThanhTuan.IDP.Entities;
using ThanhTuan.IDP.Controllers.Models;
using ThanhTuan.IDP.Hydra;
using ThanhTuan.IDP.Hydra.Models;
using ThanhTuan.IDP.Data;
using Microsoft.EntityFrameworkCore;

namespace ThanhTuan.IDP.Controllers
{
  [Route("api")]
  [ApiController]
  public class IDPController : ControllerBase
  {
    readonly UserManager<ApplicationUser> _userManager;
    readonly IHydra _hydra;
    readonly ApplicationDbContext _db;
    public IDPController(UserManager<ApplicationUser> userManager,
    IHydra hydra, ApplicationDbContext db)
    {
      _userManager = userManager;
      _hydra = hydra;
      _db = db;
    }

    [HttpGet]
    [Route("health")]
    public ActionResult Health()
    {
      return Ok();
    }

    [HttpGet]
    [Route("login")]
    public async Task<ActionResult<LoginInfo>> GetLoginInfo(string login_challenge)
    {
      var loginInfo = await _hydra.GetLoginInfo(login_challenge);
      if (loginInfo.Skip)
      {
        var signInLog = new SignInLog
        {
          UserName = loginInfo.Subject,
          IpAddress = Request.Headers["X-Real-IP"],
          UserAgent = Request.Headers["User-Agent"],
          AcceptedLoginAt = DateTimeOffset.Now,
          LoginChallenge = login_challenge
        };
        _db.Add(signInLog);
        await _db.SaveChangesAsync();
        var appuser = await _userManager.FindByNameAsync(loginInfo.Subject);
        if (appuser == null)
        {
          var response = await _hydra.RejectLogin(new RejectRequest
          {
            Error = "user not found",
            ErrorDescription = $"subject {loginInfo.Subject} not found"
          }, login_challenge);
          return BadRequest(response);
        }
        return Ok(loginInfo);
      }
      return Ok(loginInfo);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<RedirectResponse>> Login(Login model)
    {
      var loginInfo = await _hydra.GetLoginInfo(model.LoginChallenge);
      if (loginInfo.Skip)
      {
        return await _hydra.AcceptLogin(new AcceptLoginRequest(loginInfo.Subject), model.LoginChallenge);
      }
      var appuser = await _userManager.FindByEmailAsync(model.Username);
      if (appuser == null)
      {
        appuser = await _userManager.FindByNameAsync(model.Username);
      }
      if (appuser == null)
      {
        return NotFound();
      }
      var valid = await _userManager.CheckPasswordAsync(appuser, model.Password);
      if (!valid)
      {
        return BadRequest(IDPErrors.InvalidCredential);
      }
      var response = await _hydra.AcceptLogin(new AcceptLoginRequest(appuser.UserName), model.LoginChallenge);
      var signInLog = new SignInLog
      {
        UserName = appuser.UserName,
        IpAddress = Request.Headers["X-Real-IP"],
        UserAgent = Request.Headers["User-Agent"],
        AcceptedLoginAt = DateTimeOffset.Now,
        LoginChallenge = model.LoginChallenge
      };
      _db.Add(signInLog);
      await _db.SaveChangesAsync();
      return response;
    }


    [HttpPost]
    [Route("consent/accept")]
    public async Task<ActionResult<RedirectResponse>> Consent(string consent_challenge)
    {
      var consent = await _hydra.GetConsentInfo(consent_challenge);
      var user = await _userManager.FindByNameAsync(consent.Subject);
      if (user == null)
      {
        var response = await _hydra.RejectConsent(new RejectRequest
        {
          Error = "user-not-found",
          ErrorDescription = "no user match this subject",
          ErrorDebug = $"Subject {consent.Subject} is not found",
          StatusCode = 404,
        }, consent_challenge);
        return BadRequest(response);
      }
      var roles = await _userManager.GetRolesAsync(user);
      var requestContent = new AcceptConsentRequest(consent, roles.ToArray(), user);
      var acceptResponse = await _hydra.AcceptConsent(requestContent, consent_challenge);
      var signInLog = await _db.SignInLogs.FirstAsync(u => u.LoginChallenge == consent.LoginChallenge);
      signInLog.ConsentChallenge = consent_challenge;
      signInLog.RequestedScope = string.Join(",", consent.RequestedScope);
      signInLog.GrantedScope = string.Join(",", requestContent.GrantScope);
      signInLog.AcceptedConsentAt = DateTimeOffset.Now;
      _db.Update(signInLog);
      await _db.SaveChangesAsync();
      return acceptResponse;
    }


    [HttpPost]
    [Route("logout/accept")]
    public async Task<ActionResult<AcceptLogoutResponse>> Logout(string logout_challenge)
    {
      var acceptResponse = await _hydra.AcceptLogout(logout_challenge);
      return acceptResponse;
    }

    [HttpGet]
    [Route("userInfo/{name}")]
    public async Task<ActionResult<PublicUserInfo>> GetUser(string name)
    {
      var user = await _userManager.FindByNameAsync(name);
      if (user == null) return NotFound();
      return new PublicUserInfo(user);
    }
  }
}