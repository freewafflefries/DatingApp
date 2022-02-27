using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")] // {URL}/api/Buggy/auth
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")] // {URL}/api/Buggy/authnot-found
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);
            Console.WriteLine(thing);
            if (thing == null) return NotFound();

            return Ok(thing);
        }

        [HttpGet("server-error")] // {URL}/api/Buggy/server-error
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1);

            var thingToReturn = thing.ToString();

            return thingToReturn;
        }

        [HttpGet("bad-request")] // {URL}/api/Buggy/bad-request"
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was a bad request");
        }
    }
}