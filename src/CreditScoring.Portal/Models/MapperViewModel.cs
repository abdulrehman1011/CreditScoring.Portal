using AutoMapper;
using CreditScoring.Portal.Services.AdminService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Models
{
   
    class MapperViewModel : Profile
    {
        public MapperViewModel()
        {
            CreateMap<User, UserListingViewModel>();
            CreateMap<ScoreBand, ScoreBandViewModel>();
            CreateMap<UserScoreBand, UserScoreBandModel>();
        }
       
    }
}
