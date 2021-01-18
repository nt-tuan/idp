using System;

namespace ThanhTuan.IDP.Entities
{
  public class SignInLog
  {
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTimeOffset AcceptedLoginAt { get; set; }
    public DateTimeOffset? AcceptedConsentAt { get; set; }

    public string LoginChallenge { get; set; }
    public string ConsentChallenge { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public string RequestedScope { get; set; }
    public string GrantedScope { get; set; }
  }
}