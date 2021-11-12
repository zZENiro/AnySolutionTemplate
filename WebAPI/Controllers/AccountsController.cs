using Application._Common.Services;
using Application.Entities.Accounts.Cmds.RegisterAccountCommand;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("registerAccount")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> RegisterAccount(RegisterAccountCommand cmd)
        {
            var res = await Mediator.Send(cmd);

            HttpContext.Response.Cookies.Append(Startup.AUTH_KEY, res.AuthenticationToken);

            return Ok(res);
        }
    }
}
