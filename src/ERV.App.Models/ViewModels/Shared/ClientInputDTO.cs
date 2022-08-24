using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.ViewModels.Shared
{
    public class ClientInputDTO
    {
        [Required, JsonProperty("IpAddressClient")]
        public string IpAddressClient { get; set; }

        [Required, JsonProperty("Token")]
        public string Token { get; set; }

        [JsonIgnore]
        public string IpAddressService { get; set; }

        [Required, JsonProperty("SiteId")]
        public string SiteId { get; set; }

        public virtual void SetDefaults()
        {
        }
    }

    public class ClientInternationalizationInputDTO : ClientInputDTO
    {
        public string Lang { get; set; }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Lang = Lang ?? "ENG";
        }
    }
}
