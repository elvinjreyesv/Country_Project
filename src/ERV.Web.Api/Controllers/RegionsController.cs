using ERV.App.Models.Enums;
using ERV.App.Models.ViewModels.Public.Inputs;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.App.Models.ViewModels.Shared;
using ERV.App.Services.Abstracts;
using ERV.Web.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERV.Web.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(PublicSecurityActionFilter), Order = 1)]
    [TypeFilter(typeof(PublicPostSecurityActionFilter), Order = 2)]
    public class RegionsController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public RegionsController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("{Name}")]
        [Produces(typeof(AppResponse<EAppResponse, RegionDTO>))]
        public async Task<ActionResult<AppResponse<EAppResponse, RegionDTO>>> Region([FromRoute] string Name, [FromQuery] ClientInputDTO input)
        {
            var content = await _countryService.RegionInformation(Name);
            return Ok(content);
        }

        [HttpGet("{Region}/SubRegion/{Name}")]
        [Produces(typeof(AppResponse<EAppResponse, SubRegionDTO>))]
        public async Task<ActionResult<AppResponse<EAppResponse, SubRegionDTO>>> SubRegion([FromRoute] string Name, [FromQuery] ClientInputDTO input)
        {
            var content = await _countryService.SubRegionInformation(Name);
            return Ok(content);
        }
    }
}
