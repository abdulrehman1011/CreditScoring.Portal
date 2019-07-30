using CreditScoring.Portal.Services.AdminService.Dtos;
using CreditScoring.Portal.Services.AdminService.Respository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Services.AdminService
{
    public class UserService : IUserService
    {
        private readonly IUserRespository _userRespository;
        public UserService(IUserRespository userRespository)
        {
            _userRespository = userRespository;
        }
        public async Task<List<User>> GetUserList()
        {
            return await _userRespository.GetUserList();
        }
        public async Task<List<ScoreBand>> GetScores()
        {
            return await _userRespository.GetScores();
        }
        public async Task<List<UserScoreBand>> GetUserScores(string userId)
        {
            return await _userRespository.GetUserScores(userId);
        }
        public async Task<int> InsertUserScoreBand(UserScoreBand userScoreBand)
        {
            return await _userRespository.InsertUserScoreBand(userScoreBand);
        }
        public async Task<int> InsertUserToken(UserToken userToken)
        {
            return await _userRespository.InsertUserToken(userToken);
        }
    }
}
