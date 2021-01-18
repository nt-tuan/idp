using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThanhTuan.IDP.Hydra.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using ThanhTuan.IDP;

namespace ThanhTuan.IDP.Hydra
{
  public interface IHydra
  {
    Task<LoginInfo> GetLoginInfo(string challenge);
    Task<ConsentInfo> GetConsentInfo(string challenge);
    Task<RedirectResponse> AcceptLogin(AcceptLoginRequest request, string challenge);
    Task<RedirectResponse> RejectLogin(RejectRequest payload, string challenge);
    Task<RedirectResponse> AcceptConsent(AcceptConsentRequest requestContent, string challenge);
    Task<RedirectResponse> RejectConsent(RejectRequest payload, string consent_challenge);
    Task<AcceptLogoutResponse> AcceptLogout(string challenge);
    Task<TokenIntrospectResponse> IntrospectToken(string token, string scope);
    Task<List<ConsentSection>> ListAllConsentSessions(string subject);
    Task RevokeConsentSession(string subject, string client, bool? all);
  }

  public class Hydra : IHydra
  {
    public async Task<RedirectResponse> AcceptConsent(AcceptConsentRequest requestContent, string challenge)
    {
      var authURL = $"{Constant.GetAuthURL()}/oauth2/auth/requests/consent/accept?consent_challenge={challenge}";
      var client = GetClient();
      var requestBodyString = JsonSerializer.Serialize(requestContent);
      var stringRequestContent = new StringContent(requestBodyString, Encoding.UTF8, "application/json");
      var response = await client.PutAsync(authURL, stringRequestContent);
      var stringContent = await response.Content.ReadAsStringAsync();
      if (response.StatusCode == HttpStatusCode.OK)
      {
        var acceptResponse = JsonSerializer.Deserialize<RedirectResponse>(stringContent);
        return acceptResponse;
      }
      throw new Exception(stringContent);
    }

    public async Task<RedirectResponse> AcceptLogin(AcceptLoginRequest requestContent, string challenge)
    {
      var client = GetClient();
      var acceptURL = $"{Constant.GetAuthURL()}/oauth2/auth/requests/login/accept?login_challenge={challenge}";
      var stringRequestContent = new StringContent(JsonSerializer.Serialize(requestContent), Encoding.UTF8, "application/json");
      var response = await client.PutAsync(acceptURL, stringRequestContent);
      var stringContent = await response.Content.ReadAsStringAsync();
      if (response.StatusCode == HttpStatusCode.OK)
      {
        var acceptResponse = JsonSerializer.Deserialize<RedirectResponse>(stringContent);
        return acceptResponse;
      }
      throw new Exception(stringContent);
    }

    public async Task<RedirectResponse> RejectLogin(RejectRequest payload, string challenge)
    {
      var client = GetClient();
      var stringRequestContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
      var url = $"{Constant.GetAuthURL()}/oauth2/auth/requests/login/reject?login_challenge={challenge}";
      var responseMessage = await client.PutAsync(url, stringRequestContent);
      var responseText = await responseMessage.Content.ReadAsStringAsync();
      if (responseMessage.StatusCode == HttpStatusCode.OK)
      {
        return JsonSerializer.Deserialize<RedirectResponse>(responseText);
      }
      throw new Exception(responseText);
    }

    public async Task<RedirectResponse> RejectConsent(RejectRequest payload, string consent_challenge)
    {
      var url = $"{Constant.GetAuthURL()}/oauth2/auth/requests/consent/reject?consent_challenge={consent_challenge}";
      var stringRequestContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
      var httpClient = GetClient();
      var responseMessage = await httpClient.PutAsync(url, stringRequestContent);
      var responseText = await responseMessage.Content.ReadAsStringAsync();
      if (responseMessage.StatusCode == HttpStatusCode.OK)
      {
        var rejectPayload = JsonSerializer.Deserialize<RedirectResponse>(responseText);
        return rejectPayload;
      }
      throw new Exception(responseText);
    }
    public async Task<AcceptLogoutResponse> AcceptLogout(string challenge)
    {
      var logoutURL = $"{Constant.GetAuthURL()}/oauth2/auth/requests/logout/accept?logout_challenge={challenge}";
      var client = GetClient();
      var stringRequestContent = new StringContent("", Encoding.UTF8, "application/json");
      var response = await client.PutAsync(logoutURL, stringRequestContent);
      var stringContent = await response.Content.ReadAsStringAsync();
      if (response.StatusCode == HttpStatusCode.OK)
      {
        var acceptResponse = JsonSerializer.Deserialize<AcceptLogoutResponse>(stringContent);
        return acceptResponse;
      }
      throw new Exception(stringContent);
    }

    public async Task<ConsentInfo> GetConsentInfo(string challenge)
    {
      var url = $"{Constant.GetAuthURL()}/oauth2/auth/requests/consent?consent_challenge={challenge}";
      var client = GetClient();
      var responseMessage = await client.GetAsync(url);
      var stringContent = await responseMessage.Content.ReadAsStringAsync();
      if (responseMessage.StatusCode == HttpStatusCode.OK)
      {
        var consentInfo = JsonSerializer.Deserialize<ConsentInfo>(stringContent);
        return consentInfo;
      }
      throw new Exception(stringContent);
    }

    public async Task<LoginInfo> GetLoginInfo(string challenge)
    {
      var url = $"{Constant.GetAuthURL()}/oauth2/auth/requests/login?login_challenge={challenge}";
      var httpClient = GetClient();
      var responseMessage = await httpClient.GetAsync(url);
      var responseText = await responseMessage.Content.ReadAsStringAsync();
      if (responseMessage.StatusCode != HttpStatusCode.OK)
      {
        throw new Exception(responseText);
      }
      var loginInfo = JsonSerializer.Deserialize<LoginInfo>(responseText);
      return loginInfo;
    }

    public async Task<TokenIntrospectResponse> IntrospectToken(string token, string scope)
    {
      var url = $"{Constant.GetAuthURL()}/oauth2/introspect";
      var httpClient = GetClient();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      var formVariables = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("token", token),
                new KeyValuePair<string, string>("scope", scope)
            };
      var formContent = new FormUrlEncodedContent(formVariables);
      var request = new HttpRequestMessage(HttpMethod.Post, url)
      {
        Content = formContent
      };
      var response = await httpClient.SendAsync(request);
      var responseText = await response.Content.ReadAsStringAsync();
      if (response.StatusCode == HttpStatusCode.OK)
      {
        return JsonSerializer.Deserialize<TokenIntrospectResponse>(responseText);
      }
      throw new Exception(responseText);
    }

    public async Task<List<ConsentSection>> ListAllConsentSessions(string subject)
    {
      var client = GetClient();
      var url = $"{Constant.GetAuthURL()}/oauth2/auth/sessions/consent?subject={subject}";
      var response = await client.GetAsync(url);
      var responseText = await response.Content.ReadAsStringAsync();
      if (response.StatusCode == HttpStatusCode.OK)
      {
        return JsonSerializer.Deserialize<List<ConsentSection>>(responseText);
      }
      throw new Exception(responseText);
    }
    public async Task RevokeConsentSession(string subject, string client, bool? all)
    {
      var url = $"{Constant.GetAuthURL()}/oauth2/auth/sessions/consent?subject={subject}";
      if (!string.IsNullOrEmpty(client))
        url += $"&client={client}";
      if (all != null)
        url += $"&all={all}";
      var httpClient = GetClient();
      var response = await httpClient.DeleteAsync(url);
      var responseText = await response.Content.ReadAsStringAsync();
      var statusCode = (int)response.StatusCode;
      if (statusCode >= 200 && statusCode <= 299)
        return;
      throw new Exception(responseText);
    }
    HttpClient GetClient()
    {
      var httpClientHandler = new HttpClientHandler
      {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // DEBUGGING ONLY
      };
      var client = new HttpClient(httpClientHandler);
      return client;
    }
  }
}