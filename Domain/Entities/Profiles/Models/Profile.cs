using Domain.Entities.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Profiles.Models
{
    public class Profile
    {
        public long Id { get; set; }
        public string Nickname { get; set; }
        public string About { get; set; }
        
        public long? AccountId { get; set; }
        public Account Account { get; set; }
    }
}
