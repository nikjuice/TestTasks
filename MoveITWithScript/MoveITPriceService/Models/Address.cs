using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoveITPriceService.Models
{
    public class Address
    {
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("addressLine")]
        public string AddressLine { get; set; }
        [JsonPropertyName("AddressLine2")]
        public string AddressLine2 { get; set; }

    }
}
