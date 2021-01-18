using System.Threading.Tasks;
using ThanhTuan.IDP.AccessDecision;
using ThanhTuan.IDP.Hydra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CleanArchitecture.Web.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthorizeController : ControllerBase
  {
    private readonly AccessDecision _decision;
    private readonly ILogger<AuthorizeController> _logger;
    private readonly IHydra _hydra;
    public AuthorizeController(AccessDecision decision, ILogger<AuthorizeController> logger, IHydra hydra)
    {
      _decision = decision;
      _logger = logger;
      _hydra = hydra;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
      var bearertoken = Request.Headers["Authorization"].ToString();
      var path = Request.Headers["X-Request-Uri"];
      var method = Request.Headers["X-Request-Method"];
      var rule = _decision.GetFirstMatchedRule(path, method);
      if (rule == null) return Ok();
      if (string.IsNullOrEmpty(bearertoken)) return Unauthorized();
      if (bearertoken.Length <= "Bearer".Length) return Unauthorized();
      var accessToken = bearertoken["Bearer ".Length..];
      try
      {
        var result = await _hydra.IntrospectToken(accessToken, null);
        if (!result.Active)
          return Unauthorized();
        _logger.LogInformation("Subject {0}, Scope {1}", result.Sub, result.Scope);
        Response.Headers.Add("X-Subject", result.Sub);
        Response.Headers.Add("X-Scope", result.Scope);
        if (string.IsNullOrEmpty(rule.Scope)) return Ok();
        var scope = result.Scope;
        if (!result.Scope.Contains(rule.Scope))
          return StatusCode(403);
        return Ok();
      }
      catch (Exception e)
      {
        _logger.LogInformation(e.Message);
        _logger.LogInformation(e.StackTrace);
        return Unauthorized(e.Message);
      }
    }
  }
}