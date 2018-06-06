using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace api_seed.Controllers
{

    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {

        [HttpPost]
        public IActionResult Login( [FromBody]object rawData )
        {
            return Ok(rawData);
        }   


    }



}