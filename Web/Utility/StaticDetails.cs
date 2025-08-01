namespace Web.Utility;

public class StaticDetails
{
    public static string CouponAPIBase { get; set; }
    public static string AuthAPIBase { get; set; }
    public const string RoleAdmin = "Admin";
    public const string RoleCustomer = "Customer";
    public enum ApiType
    {
        GET,
        POST,
        PUt,
        DELETE,
    }
}