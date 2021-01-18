using System.Text.Json.Serialization;

namespace ThanhTuan.IDP.Hydra.Models
{
  public class RedirectResponse
  {
    [JsonPropertyName("redirect_to")]
    public string RedirectTo { get; set; }
  }
}