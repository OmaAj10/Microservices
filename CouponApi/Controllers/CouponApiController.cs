using AutoMapper;
using CouponApi.Data;
using CouponApi.Models;
using CouponApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CouponApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponApiController : Controller
{
    private readonly AppDbContext _db;
    private ResponseDto _response;
    private IMapper _mapper;
    
    public CouponApiController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _response = new ResponseDto();
    }

    [HttpGet]
    public async Task<ResponseDto> Get()
    {
        try
        {
            IEnumerable<Coupon> objList = await _db.Coupons.ToListAsync();
            _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ResponseDto> GetById(int id)
    {
        try
        {
            Coupon obj = await _db.Coupons.FirstOrDefaultAsync(u => u.CouponId == id);
           _response.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    [HttpGet]
    [Route("GetByCode/{code}")]
    public async Task<ResponseDto> GetByCode(string code)
    {
        try
        {
            Coupon obj = await _db.Coupons.FirstOrDefaultAsync(u => u.CouponCode.ToLower() == code.ToLower());

            if (obj == null)
            {
                _response.isSuccess = false;
            } 
            _response.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
     
    [HttpPost]
    public async Task<ResponseDto> Post([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Add(obj);
            await _db.SaveChangesAsync();

            _response.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.Message = e.Message;
        }
        
        return _response;
    }
    
    [HttpPut]
    public async Task<ResponseDto> Put([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Update(obj);
            await _db.SaveChangesAsync();

            _response.Result = _mapper.Map<CouponDto>(obj);
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
    
    [HttpDelete]
    public async Task<ResponseDto> Delete(int id)
    {
        try
        {
          Coupon obj = await _db.Coupons.FirstOrDefaultAsync(u => u.CouponId == id);
          _db.Coupons.Remove(obj);
          await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _response.isSuccess = false;
            _response.Message = e.Message;
        }

        return _response;
    }
}
