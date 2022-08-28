using ERV.App.ConnectedServices.Countries;
using ERV.App.DataAccess.Repositories.Abstracts;
using ERV.App.Infrastructure.Utils;
using ERV.App.Infrastructure.Utils.Constants;
using ERV.App.Models.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.DataAccess.Repositories
{
    public class CountryRepository: ICountryRepository
    {
        private readonly WebApiAppSettings _settings;
        private readonly string ApiUrl;
        
        public CountryRepository(IOptions<WebApiAppSettings> options)
        {
            _settings = options.Value;
            ApiUrl = _settings.ExternalServices?.FirstOrDefault(row => row.Key == ApiKeyConstants.Country_Key)?.BaseUrl ?? string.Empty;
        }
        public IApiMethods Rest => RestService.For<IApiMethods>(ApiUrl);

        public async Task<List<Country>> GetCountries()
        {
            try
            {
                var countries = await Rest.GetCountries();
                return countries;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Country>().ToList();
            }
        }
        public async Task<Country> GetCountryDetails(string code)
        {
            try
            {
                var country = (await Rest.GetCountryDetails(code))?.FirstOrDefault();
                return country;
            }
            catch (Exception ex)
            {
                return default(Country);
            }
        }
        public async Task<List<Country>> GetRegionDetails(string name)
        {
            try
            {
                var regionDetails = await Rest.GetRegionDetails(name);
                return regionDetails;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Country>().ToList();
            }
        }
        public async Task<List<Country>> GetSubRegions(string name)
        {
            try
            {
                var subRegionDetails = await Rest.GetSubRegions(name);
                return subRegionDetails;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Country>().ToList();
            }
        }
    }
}
