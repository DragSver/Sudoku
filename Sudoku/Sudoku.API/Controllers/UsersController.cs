using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sudoku.BL;
using Sudoku.Domain.Models;
using Sudoku.Domain.Response;

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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetSudokuUserResponse>> Get(Guid id)
        {
            var user = await _mediator.Send(new GetSudokuUserRequest { Id = id });

            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add([FromBody] AddSudokuUserModel model)
        {
            var id = await _mediator.Send(new AddSudokuUserRequest { Login = model.Login, Password = model.Password });

            return id is not null ? Ok(id) : NotFound();
        }
    }
}
