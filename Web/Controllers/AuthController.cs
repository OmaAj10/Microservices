using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.Models;
using Web.Models.Dto;
using Web.Service.IService;
using Web.Utility;

namespace Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        LoginRequestDto loginRequestDto = new();
        return View(loginRequestDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        ResponseDto responseDto = await _authService.LoginAsync(loginRequestDto);
        
        if (responseDto != null && responseDto.IsSuccess)
        {
            LoginResponseDto loginResponseDto =
                JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
            
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError("CustomerError", responseDto.Message);
            return View(loginRequestDto);
        }
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem { Text = StaticDetails.RoleAdmin, Value = StaticDetails.RoleAdmin },
            new SelectListItem { Text = StaticDetails.RoleCustomer, Value = StaticDetails.RoleCustomer }
        };
        
        ViewBag.RoleList = roleList;
        return View();
    } 
    
    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
    {
       ResponseDto responseDto = await _authService.RegisterAsync(registrationRequestDto);
       ResponseDto assingRole;

       if (responseDto != null && responseDto.IsSuccess)
       {
           if (string.IsNullOrEmpty(registrationRequestDto.Role))
           {
               registrationRequestDto.Role = StaticDetails.RoleCustomer;
           }
           assingRole = await _authService.AssignRoleAsync(registrationRequestDto);

           if (assingRole != null && assingRole.IsSuccess)
           {
               TempData["success"] = "Registration Successful";
               return RedirectToAction(nameof(Login));
           }
       }
       
       var roleList = new List<SelectListItem>()
       {
           new SelectListItem { Text = StaticDetails.RoleAdmin, Value = StaticDetails.RoleAdmin },
           new SelectListItem { Text = StaticDetails.RoleCustomer, Value = StaticDetails.RoleCustomer }
       };
       
       ViewBag.RoleList = roleList;
       
        return View(registrationRequestDto);
    }
    
    public IActionResult Logout()
    {
        return View();
    }
}