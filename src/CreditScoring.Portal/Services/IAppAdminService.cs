using CreditScoring.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Services
{
    public interface IAppAdminService
    {
        Task<List<UserListingViewModel>> GetUserList();
        Task<bool> InsertUserToken(string userId, string token);
        Task<List<ScoreBandViewModel>> GetScores();
        Task<bool> InsertUserScoreBand(UserScoreBandModel userScoreBandViewModel);
        Task<List<UserScoreBandModel>> GetUserScores(string userId);
    }
}
