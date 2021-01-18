using System;
using System.Collections.Generic;
using System.Linq;

namespace ThanhTuan.IDP
{
  public class Constant
  {
    public static string CLIENT_ID
    {
      get
      {
        return Environment.GetEnvironmentVariable("CLIENT_ID");
      }
    }
    public static string INIT_ROLES_FILEPATH
    {
      get { return Environment.GetEnvironmentVariable("INIT_ROLES_FILEPATH"); }
    }
    public static string INIT_USERS_FILEPATH
    {
      get { return Environment.GetEnvironmentVariable("INIT_USERS_FILEPATH"); }
    }
    public static string RULES_FILEPATH
    {
      get { return Environment.GetEnvironmentVariable("RULES_FILEPATH"); }
    }
    public static string USER_HEADER_KEY = "X-Subject";

    public static string GetUserInfoURL()
    {
      return Environment.GetEnvironmentVariable("PUBLIC_AUTH_URL") + "/userinfo";
    }
    public static string GetAuthURL()
    {
      return Environment.GetEnvironmentVariable("AUTH_URL");
    }
    public static List<string> GetScopes()
    {
      var scopes = Environment.GetEnvironmentVariable("DEFAULT_SCOPE");
      return scopes.Split(",").Select(scope => scope.Trim()).ToList();
    }
    public static UInt64 GetRememberDuration()
    {
      UInt64 dur = 0;
      try
      {
        var str = Environment.GetEnvironmentVariable("REMEMBER_DURATION");
        dur = UInt64.Parse(str);
      }
      catch { }
      return dur;
    }
  }
}