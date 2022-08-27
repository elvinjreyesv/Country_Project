using ERV.App.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.DataAccess.Repositories.Abstracts
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountries();
        Task<Country> GetCountryDetails(string code);
        Task<List<Country>> GetRegionDetails(string name);
        Task<List<Country>> GetSubRegions(string name);
    }
}
