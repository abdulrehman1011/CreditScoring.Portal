using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CreditScoring.Portal.Identity
{
    public class ApplicationUser :IdentityUser
    {
        public bool IsDisable { get; set; }
    }
    
}
