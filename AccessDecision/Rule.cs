using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ThanhTuan.IDP.AccessDecision
{
  public class Rule
  {
    [JsonPropertyName("path")]
    public string Path { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; }
    [JsonPropertyName("methods")]
    public List<string> Methods { get; set; }
  }
  public class ParsedRule
  {
    public Regex Pattern { get; set; }
    public string Scope { get; set; }
  }
}