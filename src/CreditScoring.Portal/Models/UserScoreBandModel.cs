using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Models
{
    public class UserScoreBandModel
    {
        public string UserId { get; set; }
        public int ScoreId { get; set; }
        public decimal Score { get; set; }
        public string ScoreList { get; set; }
    }
}
