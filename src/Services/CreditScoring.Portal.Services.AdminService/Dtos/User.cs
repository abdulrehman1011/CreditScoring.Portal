using System;
using System.Collections.Generic;
using System.Text;

namespace CreditScoring.Portal.Services.AdminService.Dtos
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public decimal Scores { get; set; }
        public int ScoreId { get; set; }
        public bool IsDisable { get; set; }
    }
}
