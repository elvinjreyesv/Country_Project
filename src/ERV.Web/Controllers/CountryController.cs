using ERV.App.Infrastructure.Utils;
using ERV.App.Models.Enums;
using ERV.Web.Controllers.BaseController;
using ERV.Web.Utils.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using ERV.App.Models.ViewModels.Shared;
using ERV.App.Models.ViewModels.Public.Inputs;
using ERV.App.Models.ViewModels.Public.Outputs;
using ERV.Web.Utils.Extensions;
using ERV.App.Infrastructure.Utils.Constants;
using X.PagedList;

namespace ERV.Web.Controllers
{
    public class CountryController : PublicBaseController
    {
        private readonly ILogger<HomeController> _logger;
        public CountryController(IOptions<WebAppSettings> settings, IApiClientService apiClient, ILogger<HomeController> logger) : base(settings, apiClient)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await ApiClient.GetCountries(InputParameter, AppSettings.SecretKey);

                return View(result);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.HomeController, ex);
            }
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Country([FromRoute] string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                TempData.Put(TempDataKeyConstants.ErrorMessage, "There is no country selected to continue!");
                return View(new CountryDTO());
            }

            try
            {
                var input = new CountryInputDTO()
                {
                    Code = code,
                    Id = InputParameter.Id
                };

                var result = await ApiClient.GetCountry(input, AppSettings.SecretKey);
                if (result.Status != EAppResponse.Success)
                    TempData.Put(TempDataKeyConstants.ErrorMessage, "There are no results for the current search!");

                return View(result.Result);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.HomeController, ex);
            }
        }

        [HttpGet("Region/{name}")]
        public async Task<IActionResult> Region([FromRoute] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData.Put(TempDataKeyConstants.ErrorMessage, "There is no region selected to continue!");
                return View(new RegionDTO());
            }

            try
            {
                var input = new RegionInputDTO()
                {
                    Name = name,
                    Id = InputParameter.Id
                };

                var result = await ApiClient.GetRegion(input, AppSettings.SecretKey);

                if (result.Status != EAppResponse.Success)
                    TempData.Put(TempDataKeyConstants.ErrorMessage, "There are no results for the current search!");

                return View(result.Result);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.HomeController, ex);
            }
        }

        [HttpGet("SubRegion/{region}")]
        public async Task<IActionResult> SubRegion([FromRoute] string region)
        {
            if (string.IsNullOrWhiteSpace(region))
            {
                TempData.Put(TempDataKeyConstants.ErrorMessage, "There is no region selected to continue!");
                return View(new SubRegionDTO());
            }

            try
            {
                var input = new RegionInputDTO()
                {
                    Name = region,
                    Id = InputParameter.Id
                };

                var result = await ApiClient.GetSubRegion(input, AppSettings.SecretKey);
                if (result.Status != EAppResponse.Success)
                    TempData.Put(TempDataKeyConstants.ErrorMessage, "There are no results for the current search!");

                return View(result.Result);
            }
            catch (Exception ex)
            {
                return await Error(EErrorCodeClientApi.HomeController, ex);
            }
        }
    }
}