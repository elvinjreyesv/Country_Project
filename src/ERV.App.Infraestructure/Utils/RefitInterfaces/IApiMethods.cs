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
        [Get("/api/v1/Countries/?Id={id}&Token={token}")]
        Task<List<CountryDTO>> GetCountries(string id, string token);
    }
}
