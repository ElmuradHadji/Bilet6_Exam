using FluentValidation;
using WebApplication1.VMs.AppUserVMs;

namespace WebApplication1.Validations.FluentValidator.UserValidations
{
    public class RegisterVMValidation:AbstractValidator<RegisterVM>
    {
        public RegisterVMValidation()
        {
            RuleFor(p => p.UserName).NotEmpty().NotNull();
            RuleFor(p => p.Password).NotEmpty().NotNull();
            RuleFor(p => p.ConfirmedPassword).NotEmpty().NotNull();
        }
    }
}
