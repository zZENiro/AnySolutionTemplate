using Application.Entities.Accounts.Cmds.RegisterAccountCommand;
using Application.Entities.Accounts.Vms;
using AutoMapper;
using Domain.Entities.Accounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application._Common.MappingsProfiles
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<RegisterAccountCommand, Account>();
            CreateMap<Account, RegisterAccountResponseVm>();
        }
    }
}
