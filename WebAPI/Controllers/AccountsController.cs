using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("accounts")]
    public class AccountsController : BaseController
    {
        [HttpGet]
        [Route("getById")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> GetUserById(long? id)
        {
            return Ok(id);
        }
    }
}
