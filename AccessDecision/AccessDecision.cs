using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text.Json;
using System;
using System.IO;
using ThanhTuan.IDP;
using Microsoft.Extensions.Logging;

namespace ThanhTuan.IDP.AccessDecision
{
  public class AccessDecision
  {
    // private readonly List<Rule> Rules;
    private readonly Dictionary<string, List<ParsedRule>> RuleSet;
    private readonly ILogger<AccessDecision> _logger;
    public AccessDecision(ILogger<AccessDecision> logger)
    {
      if (string.IsNullOrEmpty(Constant.RULES_FILEPATH))
      {
        throw new Exception();
      }
      _logger = logger;
      using var reader = new StreamReader(Constant.RULES_FILEPATH);
      var content = reader.ReadToEnd();
      var rules = JsonSerializer.Deserialize<List<Rule>>(content);
      RuleSet = new Dictionary<string, List<ParsedRule>>();
      foreach (var rule in rules)
      {
        var regex = new Regex(rule.Path);
        var p = new ParsedRule
        {
          Pattern = regex,
          Scope = rule.Role
        };
        foreach (var method in rule.Methods)
        {
          if (!RuleSet.ContainsKey(method))
          {
            RuleSet[method] = new List<ParsedRule>();
          }
          RuleSet[method].Add(p);
        }
      }
    }

    public ParsedRule GetFirstMatchedRule(string path, string method)
    {
      if (string.IsNullOrEmpty(method)) return null;
      if (!RuleSet.ContainsKey(method)) return null;
      foreach (var rule in RuleSet[method])
      {
        if (rule.Pattern.Match(path).Success)
        {
          _logger.LogInformation("{0} {1} {2}", rule.Pattern.ToString(), path, rule.Pattern.Match(path).Success);
          return rule;
        }
      }
      return null;
    }
  }
}
