using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Models
{
    public class UserListingViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public decimal Scores { get; set; }
        public int ScoreId { get; set; }
        public bool IsDisable { get; set; }
    }
}
