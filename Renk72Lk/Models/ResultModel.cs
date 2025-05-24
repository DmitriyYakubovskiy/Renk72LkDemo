using Microsoft.AspNetCore.Mvc;

namespace Renk72Lk.Models;
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorContainer
{
    public Dictionary<string, string[]> errors { get; set; }
}

[ApiExplorerSettings(IgnoreApi = true)]
public class ResultModel
{
    public bool Success { get; set; }
    public Dictionary<string, string[]> Errors = new Dictionary<string, string[]>();

    public ResultModel(bool success)
    {
        Success = success;
    }

    public ResultModel AddErrors(string[] errors, string errorField = "mainError")
    {
        Errors.Add(errorField, errors);
        return this;
    }

    public static ErrorContainer GetErrors(string[] errors, string errorField = "mainError")
    {
        var dict = new Dictionary<string, string[]>();
        dict.Add(errorField, errors);
        return new ErrorContainer { errors = dict};
    }
}
