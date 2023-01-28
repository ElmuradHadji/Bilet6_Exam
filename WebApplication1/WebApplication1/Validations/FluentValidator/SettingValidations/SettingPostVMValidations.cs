using FluentValidation;
using WebApplication1.VMs.PortfolioVMs;
using WebApplication1.VMs.SettingVMs;

namespace WebApplication1.Validations.FluentValidator.SettingValidations
{
    public class SettingPostVMValidations : AbstractValidator<SettingPostVM>
    {
        public SettingPostVMValidations()
        {
            RuleFor(p => p.Adress).NotEmpty().NotNull().MinimumLength(3);
            RuleFor(p => p.Logo).NotEmpty().NotNull().MinimumLength(3).MaximumLength(25);
            RuleFor(p => p.Year).NotEmpty().NotNull().LessThanOrEqualTo(2025).GreaterThanOrEqualTo(1990);
        }
    }
}
