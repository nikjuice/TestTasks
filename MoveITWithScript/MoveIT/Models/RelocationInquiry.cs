using System.Text.Json.Serialization;


namespace MoveIT.Models
{
    public class RelocationInquiry
    {
        public int ID { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("addressFrom")]
        public Address AddressFrom { get; set; }
        [JsonPropertyName("addressTo")]
        public Address AddressTo { get; set; }
        [JsonPropertyName("distance")]
        public string Distance { get; set; }
        [JsonPropertyName("area")]
        public string Area { get; set; }
        [JsonPropertyName("specialArea")]
        public string SpecialArea { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("options")]
        public string Options { get; set; }
    }
}
