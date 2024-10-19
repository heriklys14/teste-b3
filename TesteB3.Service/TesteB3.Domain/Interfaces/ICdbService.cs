using TesteB3.Domain.ResponseModels;
using TesteB3.Domain.ViewModels;

namespace TesteB3.Domain.Interfaces
{
    public interface ICdbService
    {
        CdbResponseModel ComputeCdbValue(CdbViewModel model);
    }
}
