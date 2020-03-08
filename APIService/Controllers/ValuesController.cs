using APIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIService.Controllers
{
    [RoutePrefix("api/Values")]
    public class ValuesController : ApiController
    {
        public UserDto users { get; set; }
        public ValuesController()
        {
            users = new UserDto();
        }
        [HttpPost, Route("set")]
        public IHttpActionResult SetValue([FromBody] UserDto user)
        {
            users = user;
            Notify();
            return Ok(true);
        }
        [HttpGet, Route("notify")]
         public IHttpActionResult Notify()
        {
            return Ok(users);
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
