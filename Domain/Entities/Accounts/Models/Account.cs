using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Profiles.Models;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Accounts.Models
{
    public class Account
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }
    }
}
