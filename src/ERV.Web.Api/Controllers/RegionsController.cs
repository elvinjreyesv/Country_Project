using ERV.App.Infrastructure.Utils;
using ERV.App.Infrastructure.Utils.Constants;
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

namespace ERV.Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(PublicSecurityActionFilter), Order = 1)]
    [TypeFilter(typeof(PublicPostSecurityActionFilter), Order = 2)]
    public class RegionsController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly WebApiAppSettings _settings;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions cacheExpiryOptions;

        public RegionsController(ICountryService countryService, IOptions<WebApiAppSettings> options, IMemoryCache memoryCache)
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

        [HttpGet("{Name}")]
        [Produces(typeof(AppResponse<EAppResponse, RegionDTO>))]
        public async Task<ActionResult<AppResponse<EAppResponse, RegionDTO>>> Region([FromRoute] string Name, [FromQuery] ClientInputDTO input)
        {
            var cacheKey = $"{CacheKeyConstants.Region}_{Name}";
            if (!_memoryCache.TryGetValue(cacheKey, out AppResponse<EAppResponse, RegionDTO> content))
            {
                content = await _countryService.RegionInformation(Name);
                _memoryCache.Set(cacheKey, content, cacheExpiryOptions);
            }
            return Ok(content);
        }

        [HttpGet("{Region}/SubRegion/{Name}")]
        [Produces(typeof(AppResponse<EAppResponse, SubRegionDTO>))]
        public async Task<ActionResult<AppResponse<EAppResponse, SubRegionDTO>>> SubRegion([FromRoute] string Name, [FromQuery] ClientInputDTO input)
        {
            var cacheKey = $"{CacheKeyConstants.SubRegion}_{Name}";
            if (!_memoryCache.TryGetValue(cacheKey, out AppResponse<EAppResponse, SubRegionDTO> content))
            {
                content = await _countryService.SubRegionInformation(Name);
                _memoryCache.Set(cacheKey, content, cacheExpiryOptions);
            }

            return Ok(content);
        }
    }
}
