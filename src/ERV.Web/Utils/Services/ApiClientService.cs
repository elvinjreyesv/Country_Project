using ERV.App.Infrastructure.Utils;
using ERV.App.Infrastructure.Utils.RefitInterfaces;
using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Public.Inputs;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.App.Models.ViewModels.Shared;
using ERV.Web.Utils.Services.Abstracts;
using Microsoft.Extensions.Options;
using Refit;

namespace ERV.Web.Utils.Services
{
    public class ApiClientService : IApiClientService
    {
        private readonly WebAppSettings _appSettings;

        public ApiClientService(IOptions<WebAppSettings> options)
        {
            _appSettings = options.Value;
        }
        public IApiMethods Rest => RestService.For<IApiMethods>(_appSettings.WebApiBaseUrl);

        #region Content
        public async Task<List<CountryDTO>> GetCountries(ClientInputDTO dto, string secretKey)
        {
            dto.Token = TokenEncrypter.Encrypt(dto, secretKey);
            return await Rest.GetCountries(dto.Id, dto.Token);
        }
        #endregion
    }
}
