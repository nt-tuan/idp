using System.Text.Json.Serialization;

namespace ThanhTuan.IDP.Controllers.Models
{
  public class Login
  {
    public string Username { get; set; }
    public string Password { get; set; }
    [JsonPropertyName("login_challenge")]
    public string LoginChallenge { get; set; }
  }
}