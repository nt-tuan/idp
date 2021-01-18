using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ThanhTuan.IDP.Hydra.Models
{
  // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
  public class Jwks : Dictionary<string, string>
  {
  }

  public class Metadata : Dictionary<string, string>
  {
  }

  public class Client
  {
    [JsonPropertyName("allowed_cors_origins")]
    public List<string> AllowedCorsOrigins { get; set; }

    [JsonPropertyName("audience")]
    public List<string> Audience { get; set; }

    [JsonPropertyName("backchannel_logout_session_required")]
    public bool BackchannelLogoutSessionRequired { get; set; }

    [JsonPropertyName("backchannel_logout_uri")]
    public string BackchannelLogoutUri { get; set; }

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }

    [JsonPropertyName("client_name")]
    public string ClientName { get; set; }

    [JsonPropertyName("client_secret_expires_at")]
    public int ClientSecretExpiresAt { get; set; }

    [JsonPropertyName("client_uri")]
    public string ClientUri { get; set; }

    [JsonPropertyName("contacts")]
    public List<string> Contacts { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("frontchannel_logout_session_required")]
    public bool FrontchannelLogoutSessionRequired { get; set; }

    [JsonPropertyName("frontchannel_logout_uri")]
    public string FrontchannelLogoutUri { get; set; }

    [JsonPropertyName("grant_types")]
    public List<string> GrantTypes { get; set; }


    [JsonPropertyName("logo_uri")]
    public string LogoUri { get; set; }

    [JsonPropertyName("metadata")]
    public Metadata Metadata { get; set; }

    [JsonPropertyName("owner")]
    public string Owner { get; set; }

    [JsonPropertyName("policy_uri")]
    public string PolicyUri { get; set; }

    [JsonPropertyName("post_logout_redirect_uris")]
    public List<string> PostLogoutRedirectUris { get; set; }

    [JsonPropertyName("redirect_uris")]
    public List<string> RedirectUris { get; set; }

    [JsonPropertyName("request_object_signing_alg")]
    public string RequestObjectSigningAlg { get; set; }

    [JsonPropertyName("request_uris")]
    public List<string> RequestUris { get; set; }

    [JsonPropertyName("response_types")]
    public List<string> ResponseTypes { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    [JsonPropertyName("sector_identifier_uri")]
    public string SectorIdentifierUri { get; set; }

    [JsonPropertyName("subject_type")]
    public string SubjectType { get; set; }

    [JsonPropertyName("token_endpoint_auth_method")]
    public string TokenEndpointAuthMethod { get; set; }

    [JsonPropertyName("token_endpoint_auth_signing_alg")]
    public string TokenEndpointAuthSigningAlg { get; set; }

    [JsonPropertyName("tos_uri")]
    public string TosUri { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

  }

  public class Context
  {
  }

  public class IdTokenHintClaims
  {
  }

  public class OidcContext
  {
    [JsonPropertyName("acr_values")]
    public List<string> AcrValues { get; set; }

    [JsonPropertyName("display")]
    public string Display { get; set; }

    [JsonPropertyName("id_token_hint_claims")]
    public IdTokenHintClaims IdTokenHintClaims { get; set; }

    [JsonPropertyName("login_hint")]
    public string LoginHint { get; set; }

    [JsonPropertyName("ui_locales")]
    public List<string> UiLocales { get; set; }
  }

  public class ConsentRequest
  {
    [JsonPropertyName("acr")]
    public string Acr { get; set; }

    [JsonPropertyName("challenge")]
    public string Challenge { get; set; }

    [JsonPropertyName("client")]
    public Client Client { get; set; }

    [JsonPropertyName("context")]
    public Context Context { get; set; }

    [JsonPropertyName("login_challenge")]
    public string LoginChallenge { get; set; }

    [JsonPropertyName("login_session_id")]
    public string LoginSessionId { get; set; }

    [JsonPropertyName("oidc_context")]
    public OidcContext OidcContext { get; set; }

    [JsonPropertyName("request_url")]
    public string RequestUrl { get; set; }

    [JsonPropertyName("requested_access_token_audience")]
    public List<string> RequestedAccessTokenAudience { get; set; }

    [JsonPropertyName("requested_scope")]
    public List<string> RequestedScope { get; set; }

    [JsonPropertyName("skip")]
    public bool Skip { get; set; }

    [JsonPropertyName("subject")]
    public string Subject { get; set; }
  }
  public class Session
  {
    [JsonPropertyName("access_token")]
    public ConsentAcceptSessionBody AccessToken { get; set; }

    [JsonPropertyName("id_token")]
    public ConsentAcceptIDTokenBody IdToken { get; set; }
  }

  public class ConsentSection
  {
    [JsonPropertyName("consent_request")]
    public ConsentRequest ConsentRequest { get; set; }

    [JsonPropertyName("grant_access_token_audience")]
    public List<string> GrantAccessTokenAudience { get; set; }

    [JsonPropertyName("grant_scope")]
    public List<string> GrantScope { get; set; }

    [JsonPropertyName("handled_at")]
    public DateTime HandledAt { get; set; }

    [JsonPropertyName("remember")]
    public bool Remember { get; set; }

    [JsonPropertyName("remember_for")]
    public int RememberFor { get; set; }

    [JsonPropertyName("session")]
    public Session Session { get; set; }
  }
}