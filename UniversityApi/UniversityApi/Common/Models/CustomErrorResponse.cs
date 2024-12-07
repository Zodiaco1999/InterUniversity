namespace UniversityApi.Common.Models;

public class CustomErrorResponse
{
    public string TypeException { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public bool IsWarning { get; set; }
    public int StatusCode { get; set; }
    public bool CustomErrorWebApi { get { return true; } }
}
