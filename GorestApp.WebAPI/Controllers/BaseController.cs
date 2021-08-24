using GorestApp.Core.Utilities.Helpers;
using GorestApp.Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GorestApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public GenericResult result { get; }
        public BaseController()
        {
            result = new GenericResult();
            result.Version = AppConfigurationHelper.GetAppVersion();
        }
    }
}
