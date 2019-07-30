using CreditScoring.Portal.Services.AdminService.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Services.AdminService
{
    public interface IUserService
    {
        Task<List<User>> GetUserList();
        Task<int> InsertUserToken(UserToken userToken);
        Task<List<ScoreBand>> GetScores();

        Task<List<UserScoreBand>> GetUserScores(string userId);
        Task<int> InsertUserScoreBand(UserScoreBand userScoreBand);
    }
}
