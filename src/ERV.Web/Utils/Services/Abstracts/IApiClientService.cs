using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Public.Inputs;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.App.Models.ViewModels.Shared;

namespace ERV.Web.Utils.Services.Abstracts
{
    public interface IApiClientService
    {
        Task<List<CountryDTO>> GetCountries(ClientInputDTO dto, string secretKey);
    }
}
