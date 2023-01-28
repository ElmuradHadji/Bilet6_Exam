using FluentValidation;
using WebApplication1.VMs.AppUserVMs;

namespace WebApplication1.Validations.FluentValidator.UserValidations
{
    public class LoginVMValidation:AbstractValidator<LoginVM>
    {
        public LoginVMValidation()
        {
            RuleFor(p => p.UserName).NotEmpty().NotNull();
            RuleFor(p => p.Password).NotEmpty().NotNull();
        }
    }
}
