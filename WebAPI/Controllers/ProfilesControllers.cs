using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("profiles")]
    public class ProfilesControllers : BaseController
    {
        [HttpGet("getId")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetId()
        {
            return Ok(200);
        }
    }
}
