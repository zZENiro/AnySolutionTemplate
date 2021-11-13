using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController, Route("api"), Authorize("Api")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator => HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;

        protected IMediator Mediator { get => _mediator; }
    }
}
