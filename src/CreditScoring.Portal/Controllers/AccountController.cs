using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CreditScoring.Portal.Helpers;
using CreditScoring.Portal.Identity;
using CreditScoring.Portal.Models;
using CreditScoring.Portal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CreditScoring.Portal.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAppAdminService _appAdminService;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAppAdminService appAdminService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appAdminService = appAdminService;
        }
        [Route("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Route("logout")]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToRoute("Login");
        }
        [Route("login", Name ="Login")]
        public IActionResult AdminLogin()
        {
            if(User.Identity.IsAuthenticated == true)
            {
                return RedirectToRoute("Home");
            }
            return View();
        }
       
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> AdminLogin([FromBody]LoginModel loginModel)
        {
            
            if (ModelState.IsValid == true)
            {
                var appplicationUser = await _userManager.FindByNameAsync(loginModel.Username);
                if(appplicationUser != null)
                {
                    if(appplicationUser.IsDisable == true)
                    {
                        return new BadRequestObjectResult(new { Status = false, Message = "Failed to login." });
                    }
                    var result = await _signInManager.PasswordSignInAsync(appplicationUser, loginModel.Password, isPersistent: true, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        var role = await _userManager.GetRolesAsync(appplicationUser);
                        if(role != null && role.Count > 0)
                        {
                            if(role[0] == "Admin")
                            {
                                return new OkObjectResult(new { Status = true, Message = "Login Success.", RedirectUrl = "/users" });
                            }
                            if (role[0] == "User")
                            {
                                return new OkObjectResult(new { Status = true, Message = "Login Success.", RedirectUrl = "/" });
                            }
                        }
                        
                    }
                }
                return new BadRequestObjectResult(new { Status = false, Message = "Invalid Credentials." });
            }
            return new BadRequestObjectResult(new { Status = false, ModelError = ModelState.GetErrors()});
        }
        [Authorize(Roles = "Admin")]
        [Route("create-user")]
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            ViewBag.ScoreList = await _appAdminService.GetScores();
            return View();
        }
       
        
        [Authorize(Roles ="Admin")]
        [Route("create-user")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRegisterModel userRegisterModel)
        {
            if(ModelState.IsValid == true)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = userRegisterModel.Username;
                applicationUser.Email = userRegisterModel.Username + "@example.com";
                string userId = applicationUser.Id;
                var result = await _userManager.CreateAsync(applicationUser, userRegisterModel.Password);
               
                if (result.Succeeded)
                {
                    applicationUser.Id = userId;
                    var isRoleCreated =await _userManager.AddToRoleAsync(applicationUser, "User");
                    if(isRoleCreated.Succeeded)
                    {
                        var tokenInsertResult = await _appAdminService.InsertUserToken(userId, userRegisterModel.Token);
                        if (tokenInsertResult == true)
                        {
                            string scoreList = string.Join(",", userRegisterModel.ScoreBandList);
                            var userScoreBandModel = new UserScoreBandModel(){
                                UserId = userId,
                                ScoreList = scoreList
                            };
                            var scoreBandInsetResult = await _appAdminService.InsertUserScoreBand(userScoreBandModel);

                            return new OkObjectResult(new { Status = true, Message = "User created successfully." });
                        }
                    }
                }
                return new OkObjectResult(new { Status = false, Message = "User creation failed." });
            }
            return new BadRequestObjectResult(new { Status = false, ModelError = ModelState.GetErrors() });
        }
        [Authorize(Roles = "Admin")]
        [Route("update-user")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody]UserModel userModel)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(userModel.Id);
                if(applicationUser != null)
                {
                    applicationUser.UserName = userModel.Username;
                    //var test = await _userManager.RemovePasswordAsync(applicationUser);

                    //var isNewPwdSet = await _userManager.AddPasswordAsync(applicationUser, userModel.Password);
                    var isNewPwdSet = await _userManager.ChangePasswordAsync(applicationUser, userModel.CurrentPassword, userModel.Password);
                    if (isNewPwdSet.Succeeded)
                    {
                        var isUpdated = await _userManager.UpdateAsync(applicationUser);
                        if(isUpdated.Succeeded)
                        {
                            var tokenInsertResult = await _appAdminService.InsertUserToken(userModel.Id, userModel.Token);
                            if (tokenInsertResult == true)
                            {
                                string scoreList = string.Join(",", userModel.ScoreBands);
                                var userScoreBandModel = new UserScoreBandModel()
                                {
                                    UserId = userModel.Id,
                                    ScoreList = scoreList
                                };
                                var scoreBandInsetResult = await _appAdminService.InsertUserScoreBand(userScoreBandModel);
                                return new OkObjectResult(new { Status = true, Message = "User created successfully." });
                            }
                        }
                    }
                }
                return new OkObjectResult(new { Status = false, Message = "User update failed." });
            }
            return new BadRequestObjectResult(new { Status = false, ModelError = ModelState.GetErrors() });
        }
        [Authorize(Roles = "Admin")]
        [Route("deactivate-user")]
        [HttpPost]
        public async Task<IActionResult>DeactivateUser([FromBody]UserDeactivateModel userModel)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(userModel.Id);
                if (applicationUser != null)
                {
                    applicationUser.IsDisable = true;
                    var isDeactivated = await _userManager.UpdateAsync(applicationUser);
                    if (isDeactivated.Succeeded)
                    {
                        return new OkObjectResult(new { Status = true, Message = "User deactivated successfully." });
                    }
                }
                return new OkObjectResult(new { Status = false, Message = "User deactivate failed." });
            }
            return new BadRequestObjectResult(new { Status = false, ModelError = ModelState.GetErrors() });
        }

    }
}