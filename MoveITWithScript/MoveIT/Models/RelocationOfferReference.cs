using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace MoveITWeb.Models
{
    [Table("RelocationOfferReferences")]
    public class RelocationOfferReference
    {
        public int ID { get; set; }
        public int RelocationOfferId { get; set; }
        public Guid Reference { get; set; }
    }
}
