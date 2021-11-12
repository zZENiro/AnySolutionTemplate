using FluentValidation;

namespace Application.Entities.Accounts.Cmds.RegisterAccountCommand
{
    public class RegisterAccountValidator : AbstractValidator<RegisterAccountCommand>
    {
        public RegisterAccountValidator()
        {
            RuleFor<string>(prop => prop.Email).NotEmpty().NotNull().WithMessage("Email is required!");
            RuleFor<string>(prop => prop.Password).NotEmpty().NotNull().WithMessage("Password is required!");
            RuleFor<string>(prop => prop.UserName).NotEmpty().NotNull().WithMessage("UserName is required!");
        }
    }

}
