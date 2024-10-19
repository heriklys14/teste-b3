using FluentValidation;
using TesteB3.Domain.ViewModels;

namespace TesteB3.Domain.Validators
{
    public class CdbViewModelValidator : AbstractValidator<CdbViewModel>
    {
        public CdbViewModelValidator()
        {
            RuleFor(x => x.Value).GreaterThanOrEqualTo(0.01).WithMessage("O valor informado deve ser maior que R$ 0,01.");
            RuleFor(x => x.Interval).GreaterThanOrEqualTo(2).WithMessage("O intervalo informado deve ser maior que 1 (um).");
        }
    }
}
