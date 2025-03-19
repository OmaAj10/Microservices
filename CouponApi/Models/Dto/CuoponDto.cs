namespace CouponApi.Models.Dto;

public class CuoponDto
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; }
    public double DiscountAmount { get; set; }
    public int MinAmount { get; set; }
}