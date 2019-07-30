using AutoMapper;
using CreditScoring.Portal.Models;
using CreditScoring.Portal.Services.AdminService;
using CreditScoring.Portal.Services.AdminService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Services
{
    public class AppAdminService: IAppAdminService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AppAdminService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<List<UserListingViewModel>> GetUserList()
        {
           
            var userList =  await _userService.GetUserList();

            return _mapper.Map<List<User>, List<UserListingViewModel>>(userList);
        }
        public async Task<bool> InsertUserToken(string userId, string token)
        {
            var result = await _userService.InsertUserToken(new UserToken() { Id =userId, Token = token});
            return result > 0;
        }
        public async Task<List<ScoreBandViewModel>> GetScores()
        {
            var scoreBandList = await _userService.GetScores();
            return _mapper.Map<List<ScoreBand>, List<ScoreBandViewModel>>(scoreBandList);
            
        }
        public async Task<bool> InsertUserScoreBand(UserScoreBandModel userScoreBandViewModel)
        {
            var userBand = new UserScoreBand() {
                UserId = userScoreBandViewModel.UserId,
                ScoreList = userScoreBandViewModel.ScoreList
            };
            var result = await _userService.InsertUserScoreBand(userBand);
            return result > 0;

        }
        public async Task<List<UserScoreBandModel>> GetUserScores(string userId)
        {
            var scoreBandList = await _userService.GetUserScores(userId);
            return _mapper.Map<List<UserScoreBand>, List<UserScoreBandModel>>(scoreBandList);

        }
    }
}
