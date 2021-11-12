using Application._Common.Exceptions.NotFoundExceptions;
using Application.Entities.Accounts.Vms;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Entities.Accounts.Cmds.RegisterAccountCommand
{
    public class RegisterAccountCommand : IRequest<RegisterAccountResponseVm>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
