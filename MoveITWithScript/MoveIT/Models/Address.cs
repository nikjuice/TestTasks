using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoveIT.Models
{
    public class Address
    {
        public int ID { get; set; }
        [JsonPropertyName("addressCity")]
        public string City { get; set; }
        [JsonPropertyName("addressLine1")]
        public string AddressLine { get; set; }
        [JsonPropertyName("addressLine2")]
        public string AddressLine2 { get; set; }

    }
}
