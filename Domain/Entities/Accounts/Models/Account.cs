using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Profiles.Models;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Accounts.Models
{
    public class Account : IdentityUser
    {
        public long ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
