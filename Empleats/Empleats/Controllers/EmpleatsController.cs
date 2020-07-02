using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmpleatsASPNET.Controllers
{
    [Route("api/Empleat")]
    [ApiController]
    public class EmpleatsController : ControllerBase
    {
        // GET: api/<EmpleatsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            return new string[] { "Bon dia, mon!" };
        }

        // GET api/<EmpleatsController>/5
        [HttpGet("{id}")]
        //public string Get(int id)
        public string Get(string id)
        {
            //return "value";
            return  $"Bon dia, {id} " ;

        }

        // POST api/<EmpleatsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmpleatsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmpleatsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
