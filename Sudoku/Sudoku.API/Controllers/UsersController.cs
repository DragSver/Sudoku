using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sudoku.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //[HttpGet("{id:guid}")]
        //public async Task<ActionResult<string>> Get(Guid id)
        //{
        //    var user = await _mediator.Send(new )
        //}
    }
}
