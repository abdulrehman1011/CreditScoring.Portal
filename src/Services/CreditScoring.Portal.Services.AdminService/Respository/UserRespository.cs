using CreditScoring.Portal.Core;
using CreditScoring.Portal.Services.AdminService.Dtos;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace CreditScoring.Portal.Services.AdminService.Respository
{
    public class UserRespository: IUserRespository
    {
        private readonly IDbRepository _dbRepository;
        public UserRespository(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        public async Task<List<User>> GetUserList()
        {
           
            var result = await _dbRepository.QueryAsync<User>("GetUserList", CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<List<ScoreBand>> GetScores()
        {

            var result = await _dbRepository.QueryAsync<ScoreBand>("GetScores", CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<List<UserScoreBand>> GetUserScores(string userId)
        {
            var spParams = new
            {
                UserId = userId
            };
            var result = await _dbRepository.QueryAsync<UserScoreBand>("GetUserScores", spParams, CommandType.StoredProcedure);
            return result.ToList();
        }
        //public async Task<List<UserScoreBand>> GetUserDetail(string userId)
        //{
        //    var spParams = new
        //    {
        //        UserId = userId
        //    };
        //    var result = await _dbRepository.QueryAsync<UserScoreBand>("GetUserDetail", spParams, CommandType.StoredProcedure);
        //    return result.ToList();
        //}
        public async Task<int> InsertUserScoreBand(UserScoreBand userScoreBand)
        {
            var spParams = new
            {
                UserId = userScoreBand.UserId,
                ScoreIds = userScoreBand.ScoreList
            };
            var result = await _dbRepository.ExecuteAsync("UserScoresInsert", spParams, CommandType.StoredProcedure);
            return result;
        }
        public async Task<int> InsertUserToken(UserToken userToken)
        {
           
            var spParams = new
            {
                UserId = userToken.Id,
                Token = userToken.Token
            };
            var result = await _dbRepository.ExecuteAsync("UserTokenInsert", spParams, CommandType.StoredProcedure);
            return result;
            
        }
    }
}
