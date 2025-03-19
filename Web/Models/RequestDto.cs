using static Web.Utility.StaticDetails;
namespace Web.Models;

public class RequestDto
{
    public ApiType ApiType { get; set; }
    public string Url { get; set; }
    public object Data { get; set; }
    public string AccessToken { get; set; }
}