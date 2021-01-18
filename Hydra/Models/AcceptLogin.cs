using System.Text.Json.Serialization;

namespace ThanhTuan.IDP.Hydra.Models
{
  public class AcceptLoginRequest
  {
    [JsonPropertyName("acr")]
    public string Acr { get; set; }
    [JsonPropertyName("force_subject_identifier")]
    public string ForceSubjectIdentifier { get; set; }
    [JsonPropertyName("remember")]
    public bool Remember { get; set; }
    [JsonPropertyName("remember_for")]
    public long RememberFor { get; set; }
    [JsonPropertyName("subject")]
    public string Subject { get; set; }

    public AcceptLoginRequest(string subject)
    {
      Subject = subject;
      ForceSubjectIdentifier = subject;
      Acr = "user-password";
      Remember = true;
      RememberFor = (long)Constant.GetRememberDuration();
    }
  }


}