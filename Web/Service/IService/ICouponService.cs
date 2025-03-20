using Web.Models;

namespace Web.Service.IService;

public interface ICouponService
{
    Task<ResponseDto?> GetCouponAsync(string cuponCode); 
    Task<ResponseDto?> GetAllCouponsAsync(); 
    Task<ResponseDto?> GetCouponByIdAsync(int id); 
    Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto); 
    Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto); 
    Task<ResponseDto?> DeleteCouponAsync(int id); 
}