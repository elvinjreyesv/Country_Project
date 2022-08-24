using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.ViewModels.Public.Outputs
{
    public class CountryDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<string> CapitalCities { get; set; }
        public int Population { get; set; }
        public List<CurrencyDTO> Currencies { get; set; }
        public List<LanguageDTO> Languages { get; set; }
        public List<string> BorderCountries { get; set; }
        public string Flag { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
    }

    public class CurrencyDTO
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class LanguageDTO
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
