using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditScoring.Portal.Helpers;
using CreditScoring.Portal.Identity;
using CreditScoring.Portal.Models;
using CreditScoring.Portal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CreditScoring.Portal.Controllers
{
    
    [ApiController]
    public class TestApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly AppSettings _serviceSettings;
        public TestApiController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IHttpClientHelper httpClientHelper, IOptions<AppSettings>  serviceSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpClientHelper = httpClientHelper;
            _serviceSettings = serviceSettings.Value;
        }
        [Route("test-api")]
        public IActionResult TestApi()
        {
            var url = _serviceSettings.API_URL;
            //var data = _httpClientHelper.GetSingleItemRequest(_serviceSettings.API_URL).Result;
            

            return new OkObjectResult(new { Test = url });
        }
        [Route("create-admin-login")]
        public IActionResult CreateAdminLogin()
        {
            //var role = new ApplicationRole() { Name = "Admin" };
            //var r2 = _roleManager.CreateAsync(role).Result;
            //var role2 = new ApplicationRole() { Name = "User" };
            //var r1 =_roleManager.CreateAsync(role2).Result;

            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.UserName = "admin@test.com";
            applicationUser.Email = "admin@test.com";
            //var applicationUser = _userManager.FindByNameAsync("admin@test.com").Result;
            //var isSuccess = _userManager.AddToRoleAsync(applicationUser, "Admin").Result;
            //return new OkObjectResult(new { Test = "User creation failed." });
            var result = _userManager.CreateAsync(applicationUser, "Admin@1234").Result;
            if (result.Succeeded)
            {
                var res = _signInManager.PasswordSignInAsync(applicationUser, "Admin@1234", true, true).Result;
                return new OkObjectResult(new { Test = "Created Successfully." });
            }
            return new OkObjectResult(new { Test = "User creation failed." });
        }
        //[Route("admin-login")]
        //[HttpPost]
        //public async Task<IActionResult> AdminLogin(LoginModel loginModel)
        //{
        //    var req = Request;
        //    if (ModelState.IsValid == true)
        //    {
        //        var appplicationUser = await _userManager.FindByNameAsync(loginModel.Username);
        //        var result = await _signInManager.PasswordSignInAsync(appplicationUser, loginModel.Password, isPersistent: true, lockoutOnFailure: true);
        //        if (result.Succeeded)
        //        {
        //            return new OkObjectResult(new { Status = true, Message = "Login Success" });
        //        }
        //        return new BadRequestObjectResult(new { Status = false, Message = "Invalid Credentials." });
        //    }
        //    return new BadRequestObjectResult(new { Status = false, ModelError = ModelState.GetErrors() });
        //}
    }
}