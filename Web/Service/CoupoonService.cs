using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service;

public class CoupoonService : ICouponService
{
    private readonly IBaseService _baseService;
    
    public async Task<ResponseDto?> GetCouponAsync(string cuponCode)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.CouponAPIBase + $"/api/coupon/GetByCode/+{cuponCode}",
        });
    }

    public async Task<ResponseDto?> GetAllCouponAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.CouponAPIBase + "/api/coupon",
        });
    }

    public async Task<ResponseDto?> GetCouponByIdAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.CouponAPIBase + $"/api/coupon/+{id}",
        });
    }

    public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = couponDto,
            Url = StaticDetails.CouponAPIBase + $"/api/coupon/",
        });
    }

    public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticDetails.ApiType.PUt,
            Data = couponDto,
            Url = StaticDetails.CouponAPIBase + $"/api/coupon/",
        });
    }

    public async Task<ResponseDto?> DeleteCouponAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticDetails.ApiType.DELETE,
            Url = StaticDetails.CouponAPIBase + $"/api/coupon/+{id}",
        });
    }
}