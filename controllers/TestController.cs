using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

using api_seed.Models;


namespace api_seed.Controllers
{

    [Route("api/[controller]/[action]")]
    [EnableCors("CorsPolicy")]
    public class TestController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public TestController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // simple http request
        [HttpGet]
        public IEnumerable<string> HelloWorld()
        {
            return new string[] { "Hello", "World" };
        }

        // http with query values
        [HttpGet("{id}")]
        public string Id([FromRoute]string id)
        {
            return id;
        }

        // echo post request
        [HttpPost]
        public object Echo( [FromBody]object rawData )
        {
            return rawData;
        }
        
        // you can assign default values in the route
        [HttpGet("{name=World}")]
        public string Hello([FromRoute]string name)
        {
            return $"Hello {name}";
        }

        // you can have optional route values
        [HttpGet("{max:int?}/{min:int?}")]
        public int Random([FromRoute]int max, [FromRoute]int min )
        {
            return (new Random()).Next(min, max);
        }

        [HttpGet]
        public int Throw(){
            throw new Exception("Here's your exception.");
        }

        // Best to use IActionResults
        [HttpGet]
        public IActionResult GetOk()
        {
            return Ok("ok"); // 200
        }

        [HttpGet]
        public IActionResult GetBad()
        {
            return BadRequest("Something went wrong"); // 400
        }

        [HttpGet]
        public IActionResult GetBroken()
        {
            return StatusCode(500, "Something broke"); // 500
        }

        [HttpPost("adduser")]
        public IActionResult Mongo( [FromBody]User user)
        {
            // var client = new MongoClient(Settings.DB_connection_development);
            _userRepository.CreateUser(user);
            return Ok("ok");
        }



    }

}