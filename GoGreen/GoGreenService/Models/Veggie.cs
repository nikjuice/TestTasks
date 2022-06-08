using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGreenService.Models
{
    public class Veggie
    {
        public  string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
    }
}
