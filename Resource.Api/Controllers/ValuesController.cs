using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resource.Api.ViewModels;
using Microsoft.Extensions.Configuration;
using Resource.Api.Configs;

namespace Resource.Api.Controllers
{
    [Authorize(Policy = "ApiReader")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly IConfiguration Configuration;

        public ValuesController(IConfiguration configuration){
            Configuration = configuration;
        }

        // GET api/values
        [Authorize(Policy = "Consumer")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new JsonResult(User.Claims.Select(c => new { c.Type, c.Value }));
        }
        
        [HttpGet("jwt")]
        public ActionResult<IEnumerable<string>> GetJWT()
        {
            return new JsonResult(User.Claims.Select(c => new { c.Type, c.Value }));
        }
        
        [HttpGet("test")]
        [AllowAnonymous]
        public ActionResult<SwaggerOptions> Test()
        {
          var swaggerOptions = new SwaggerOptions();
          Configuration.Bind(nameof(SwaggerOptions), swaggerOptions);
          return new JsonResult(swaggerOptions);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<string> Post(ValuesRequest request)
        {
          return new JsonResult((request != null ? "Not Null" : "Null") + request?.Id + request?.Name);
        }
    }
}
