using System;
using System.Collections.Generic;
using System.Text;

namespace CreditScoring.Portal.Services.AdminService.Dtos
{
    public class UserScoreBand
    {
        public string UserId { get; set; }
        public int ScoreId { get; set; }
        public decimal Score { get; set; }
        public string ScoreList { get; set; }
    }
}
