using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ThanhTuan.IDP.Hydra.Models
{
  public class TokenInstropectRequest
  {
    [JsonPropertyName("token")]
    public string Token { get; set; }
    [JsonPropertyName("scope")]
    public string Scope { get; set; }
  }

  public class TokenIntrospectResponse
  {
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("aud")]
    public List<string> Aud { get; set; }

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }

    [JsonPropertyName("exp")]
    public int Exp { get; set; }

    [JsonPropertyName("ext")]
    public ConsentAcceptSessionBody Ext { get; set; }

    [JsonPropertyName("iat")]
    public int Iat { get; set; }

    [JsonPropertyName("iss")]
    public string Iss { get; set; }

    [JsonPropertyName("nbf")]
    public int Nbf { get; set; }

    [JsonPropertyName("obfuscated_subject")]
    public string ObfuscatedSubject { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    [JsonPropertyName("sub")]
    public string Sub { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }
  }
}