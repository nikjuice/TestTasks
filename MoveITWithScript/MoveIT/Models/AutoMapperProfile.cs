using AutoMapper;
using MoveIT.Models;
using MoveITWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITWeb.Models
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserInfoViewModel>();           
        }
    }
    
}
