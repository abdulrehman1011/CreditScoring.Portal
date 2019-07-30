using CreditScoring.Portal.Helpers;
using CreditScoring.Portal.Identity;
using CreditScoring.Portal.Models;
using CreditScoring.Portal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;
        private readonly IAppAdminService _appAdminService;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(IConfiguration configuration, IAppAdminService appAdminService, UserManager<ApplicationUser> userManager)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _appAdminService = appAdminService;
            _userManager = userManager;
        }
        [Route("test")]
        public IActionResult Index()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    ViewBag.DbConnectionTest = "Connected";
                }
            }
            catch(Exception ex)
            {
                ViewBag.DbConnectionTest = "Connection Fail \n"+ ex.Message;
            }

             return View();
        }
        [Authorize(Roles ="Admin")]
        [Route("users")]
        public async Task<IActionResult> UserListing()
        {
            var userList = await _appAdminService.GetUserList();
            if(userList == null)
            {
                userList = new List<UserListingViewModel>();
            }
            ViewBag.ScoreBandList = await _appAdminService.GetScores();
            return View(userList);
        }
        [ServiceFilter(typeof(UserRoleAuthorizeFilter))]
        [Route("",Name ="Home")]
        public async Task<IActionResult> Home()
        {
            if(User.Identity.IsAuthenticated == true)
            {
                var applicationUser = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserScoreBands = await _appAdminService.GetUserScores(applicationUser.Id);
                
            }
            return View();
        }


    }
}
