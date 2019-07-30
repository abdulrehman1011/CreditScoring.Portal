using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Models
{
    public class UserDeactivateModel
    {
        [Required]
        public string Id { get; set; }
    }
}
