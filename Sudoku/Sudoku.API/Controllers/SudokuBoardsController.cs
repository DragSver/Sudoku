using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sudoku.BL.Services;
using Sudoku.BL.Workflow;
using Sudoku.Domain.Models;
using Sudoku.Domain.Response;

namespace Sudoku.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SudokuBoardsController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<SudokuBoardsController> _logger;
    private readonly ISudokuBoardService _sudokuBoardService;

    public SudokuBoardsController(IMediator mediator, ILogger<SudokuBoardsController> logger, ISudokuBoardService sudokuBoardService)
    {
        _mediator = mediator;
        _logger = logger;
        _sudokuBoardService = sudokuBoardService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetSudokuBoardResponse>> Get(Guid id)
    {
        var sudokuBoardData = await _mediator.Send(new GetSudokuBoardRequest { Id = id });

        return sudokuBoardData is not null ? Ok(sudokuBoardData) : NotFound();
    }

    [HttpGet("user/{id:guid}")]
    public async Task<ActionResult<GetSudokuBoardsOfUserResponse>> GetAll(Guid id)
    {
        var sudokuBoardsData = await _mediator.Send(new GetSudokuBoardsOfUserRequest { Id = id });

        return sudokuBoardsData;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Add([FromBody] AddSudokuBoardModel model)
    {
        var id = await _mediator.Send(new AddSudokuBoardRequest { UserId = model.SudokuUserId, SudokuBoardData = model.SudokuBoardData });

        return id is not null ? Ok(id) : NotFound();
    }

    [HttpPost("new")]
    public async Task<ActionResult<SudokuBoardModel>> Create()
    {
        var newBoard = _sudokuBoardService.GenerateSudokuBoard(); 
        return newBoard is not null ? Ok(newBoard) : NotFound();
    }

    [HttpPost("newTest")]
    public async Task<ActionResult<string>> CreateTest()
    {
        var newBoard = _sudokuBoardService.GenerateSudokuBoard();
        return newBoard is not null ? Ok(newBoard.Test()) : NotFound();
    }
}
