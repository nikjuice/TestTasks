using Microsoft.AspNetCore.Identity;
using MoveITWeb.Models;
using System.Collections.Generic;


namespace MoveIT.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string userName) : base(userName) { }
        public ApplicationUser()  { }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public  IList<RelocationOffer> RelocationOffers { get; set; }

    }
}
