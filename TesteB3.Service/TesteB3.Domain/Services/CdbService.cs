using FluentValidation;
using TesteB3.Domain.Interfaces;
using TesteB3.Domain.ResponseModels;
using TesteB3.Domain.ViewModels;

namespace TesteB3.Domain.Services
{
    public class CdbService(IValidator<CdbViewModel> validator) : ICdbService
    {
        private readonly IValidator<CdbViewModel> validator = validator;
        private const double CDI = 0.009;
        private const double TB = 1.08;

        public CdbResponseModel ComputeCdbValue(CdbViewModel model)
        {
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
                throw new InvalidOperationException(string.Join(';', validationResult.Errors.Select(x => x.ErrorMessage)));

            var grossValue = model.Value;

            for (int i = 1; i <= model.Interval; i++)
            {
                grossValue *= (1 + (CDI * TB));
            }

            var result = new CdbResponseModel(grossValue, CalculateNetValue(model, grossValue));

            return result;
        }

        private static double CalculateNetValue(CdbViewModel model, double grossValue)
        {
            return model.Value + ((grossValue - model.Value) * GetTax(model.Interval));
        }

        private static double GetTax(int interval)
        {
            return interval switch
            {
                <= 6 => 0.775,
                <= 12 => 0.8,
                <= 24 => 0.825,
                _ => 0.85,
            };
        }
    }
}
