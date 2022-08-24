using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.App.Models.ViewModels.Shared;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Infrastructure.Utils.RefitInterfaces
{
    public interface IApiMethods
    {
        [Get("/api/Countries/v1?Id={id}&IpAddressClient={ipAddress}&Token={token}")]
        Task<List<CountryInfoDTO>> GetCountries(string id, string ipAddress, string token);

        [Get("/api/Countries/v1/Country?Code={countryCode}&Id={id}&IpAddressClient={ipAddress}&Token={token}")]
        Task<AppResponse<EAppResponse, CountryDTO>> GetCountry(string countryCode, string id, string ipAddress, string token);

        [Get("/api/Countries/v1/Region?Name={regionName}&Id={id}&IpAddressClient={ipAddress}&Token={token}")]
        Task<AppResponse<EAppResponse, RegionDTO>> GetRegion(string regionName, string id, string ipAddress, string token);

        [Get("/api/Countries/v1/SubRegion?Name={regionName}&Id={id}&IpAddressClient={ipAddress}&Token={token}")]
        Task<AppResponse<EAppResponse, SubRegionDTO>> GetSubRegion(string regionName, string id, string ipAddress, string token);
    }
}
