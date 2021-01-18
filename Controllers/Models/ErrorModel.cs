using System.Collections.Generic;

namespace ThanhTuan.IDP.Controllers.Models
{
  public class ErrorResponse
  {
    public string Message { get; set; }
    public List<string> Messages { get; set; }
    public ErrorResponse()
    {
      Message = "unexpected-error";
      Messages = new List<string>();

    }
    public ErrorResponse(string message)
    {
      Message = message;
      Messages = new List<string>
      {
        message
      };
    }
  }

  public static class IDPErrors
  {
    public static ErrorResponse InvalidCredential = new ErrorResponse("invalid-credential");
    public static ErrorResponse UserNotFound = new ErrorResponse("user-not-found");
  }
}