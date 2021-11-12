using Domain.Entities.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application._Common.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenForAccount(Account account);
    }
}
