using Application._Common.Services;
using Application.Entities.Accounts.Vms;
using AutoMapper;
using DataAccess;
using Domain.Entities.Accounts.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Entities.Accounts.Cmds.RegisterAccountCommand
{
    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, RegisterAccountResponseVm>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public RegisterAccountCommandHandler(ApplicationDbContext context, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<RegisterAccountResponseVm> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            var newAccount = _mapper.Map<Account>(request);

            if (await IsAccountExist(newAccount))
            {
                throw new ApplicationException("Account is already exsist.");
            }

            var pwdHasher = new PasswordHasher<Account>();
            newAccount.PasswordHash = pwdHasher.HashPassword(newAccount, request.Password);

            await _context.AddAsync<Account>(newAccount);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<RegisterAccountResponseVm>(newAccount);
            result.AuthenticationToken = await _tokenService.GetTokenForAccount(newAccount);

            return result;
        }

        private async Task<bool> IsAccountExist(Account account)
        {
            var existingAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == account.Email);

            if (existingAccount is not null)
            {
                return true;
            }

            return false;
        }
    }

}
