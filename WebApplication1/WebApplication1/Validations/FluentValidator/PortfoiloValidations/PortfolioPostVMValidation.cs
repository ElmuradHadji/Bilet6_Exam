using FluentValidation;
using WebApplication1.VMs.PortfolioVMs;

namespace WebApplication1.Validations.FluentValidator.PortfoiloValidations
{
    public class PortfolioPostVMValidation:AbstractValidator<PortfolioPostVM>
    {
        public PortfolioPostVMValidation()
        {
            RuleFor(p => p.AlternativeText).NotEmpty().NotNull().MinimumLength(3).MaximumLength(25);
        }
    }
}
