using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ThanhTuan.IDP.Hydra.Models
{
  public class LoginInfo
  {
    [JsonPropertyName("challenge")]
    public string Challenge { get; set; }

    [JsonPropertyName("client")]
    public Client Client { get; set; }

    [JsonPropertyName("oidc_context")]
    public OidcContext OidcContext { get; set; }

    [JsonPropertyName("request_url")]
    public string RequestUrl { get; set; }

    [JsonPropertyName("requested_access_token_audience")]
    public List<string> RequestedAccessTokenAudience { get; set; }

    [JsonPropertyName("requested_scope")]
    public List<string> RequestedScope { get; set; }

    [JsonPropertyName("session_id")]
    public string SessionId { get; set; }

    [JsonPropertyName("skip")]
    public bool Skip { get; set; }

    [JsonPropertyName("subject")]
    public string Subject { get; set; }
  }
}