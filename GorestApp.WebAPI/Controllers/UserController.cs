using GorestApp.Business.Abstract.Users;
using GorestApp.Core.Infrastructure;
using GorestApp.Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GorestApp.WebAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserWriteService _userWriteService;

        public UserController(IUserWriteService userWriteService)
        {
            _userWriteService = userWriteService;
        }

        [HttpGet]
        public async Task<ActionResult<GenericResult>> Get()
        {
            result.Data = await _userWriteService.UpdateGorestUserSourceAsync();
            result.Message = GorestAppMessages.SuccessfullyListed;
            return result;
        }
    }
}
