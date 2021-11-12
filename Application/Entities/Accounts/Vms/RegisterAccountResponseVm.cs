using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Accounts.Vms
{
    public class RegisterAccountResponseVm
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string AuthenticationToken { get; set; }
    }
}
