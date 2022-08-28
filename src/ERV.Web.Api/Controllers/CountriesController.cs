using ERV.App.Infrastructure.Utils;
using ERV.App.Infrastructure.Utils.Constants;
using ERV.App.Models.Entities;
using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Public.Inputs;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.App.Models.ViewModels.Shared;
using ERV.App.Services.Abstracts;
using ERV.Web.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Diagnostics.Metrics;

namespace ERV.Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(PublicSecurityActionFilter), Order = 1)]
    [TypeFilter(typeof(PublicPostSecurityActionFilter), Order = 2)]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly WebApiAppSettings _settings;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions cacheExpiryOptions;

        public CountriesController(ICountryService countryService, IOptions<WebApiAppSettings> options, IMemoryCache memoryCache)
        {
            _countryService = countryService;
            _settings = options.Value;
            _memoryCache = memoryCache;
            cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(_settings.CacheConfiguration.AbsoluteExpirationSeconds),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromSeconds(_settings.CacheConfiguration.SlidingExpirationSeconds)
            };
        }

        [HttpGet]
        [Produces(typeof(List<CountryDTO>))]
        public async Task<ActionResult<List<CountryDTO>>> Get([FromQuery] ClientInputDTO dto)
        {
            if (!_memoryCache.TryGetValue(CacheKeyConstants.Countries, out List<CountryDTO> content))
            {
                content = await _countryService.Countries();
                _memoryCache.Set(CacheKeyConstants.Countries, content, cacheExpiryOptions);
            }
            
            return Ok(content);
        }

        [HttpGet("{Code}")]
        [Produces(typeof(AppResponse<EAppResponse, CountryDTO>))]
        public async Task<ActionResult<AppResponse<EAppResponse, CountryDTO>>> Country([FromRoute] string Code, [FromQuery] ClientInputDTO dto)
        {
            var cacheKey = $"{CacheKeyConstants.Country}_{Code}";
            if (!_memoryCache.TryGetValue(cacheKey, out AppResponse<EAppResponse, CountryDTO> content))
            {
                content = await _countryService.CountryInformation(Code);
                _memoryCache.Set(cacheKey, content, cacheExpiryOptions);
            }
            return Ok(content);
        }
    }
}
