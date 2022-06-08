using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoveIT.Models;
using MoveITWeb.Builders;
using MoveITWeb.Services;
using MoveITWeb.ViewModel;

namespace MoveIT.Controllers
{

    [ApiController]
    public class ApplicationUserInfoController : ControllerBase
    {
        private readonly ApplicationUserInfoService _applicationUserInfoService;
        public ApplicationUserInfoController(ApplicationUserInfoService applicationUserInfoService)
        {
            _applicationUserInfoService = applicationUserInfoService;
        }

        [Route("api/user/info")]
        [HttpGet]
        public async Task<ApplicationUserInfoViewModel> GetUserInfo()
        {
            return await _applicationUserInfoService.GetCurrentUserInfo();
        }
    }
}
