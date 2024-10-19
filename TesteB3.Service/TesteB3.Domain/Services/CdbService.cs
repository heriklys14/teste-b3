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

            var baseValue = model.Value;

            for (int i = 1; i <= model.Interval; i++)
            {
                baseValue *= (1 + (CDI * TB));
            }

            var result = new CdbResponseModel(baseValue, CalculateNetValue(model.Interval, baseValue));

            return result;
        }

        private static double CalculateNetValue(int interval, double grossValue)
        {
            return interval switch
            {
                <= 6 => grossValue * 0.775,
                <= 12 => grossValue * 0.8,
                <= 24 => grossValue * 0.825,
                _ => grossValue * 0.85,
            };
        }
    }
}
