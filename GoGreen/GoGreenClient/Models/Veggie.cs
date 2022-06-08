

using System;
using System.ComponentModel.DataAnnotations;

namespace GoGreenClient.Models
{
    public class Veggie
    {
        public  string Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Price should be between 0 and 1000")]
        public double Price { get; set; }

        public DateTime CreatedTimeStamp { get; set; }
    }

}
