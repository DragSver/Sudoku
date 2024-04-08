using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sudoku.BL.Workflow.SudokuBoardWorkflow;
using Sudoku.Domain.Entities;
using Sudoku.Domain.Models.SudokuBoardsModels;

namespace Sudoku.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SudokuBoardsController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<SudokuBoardsController> _logger;

    public SudokuBoardsController(IMediator mediator, ILogger<SudokuBoardsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SudokuBoard>> Get(Guid id)
    {
        var sudokuBoard = await _mediator.Send(new GetSudokuBoardRequest { Id = id });

        return sudokuBoard is not null ? Ok(sudokuBoard) : NotFound();
    }

    [HttpGet("user/{id:guid}")]
    public async Task<ActionResult<List<SudokuBoard>>> GetAll(Guid id)
    {
        var sudokuBoards = await _mediator.Send(new GetSudokuBoardsOfUserRequest { Id = id });

        return sudokuBoards;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Add([FromBody] AddSudokuBoardModel model)
    {
        var id = await _mediator.Send(new AddSudokuBoardRequest { UserId = model.UserId, SudokuBoardModel = model.SudokuBoardModel });

        return id is not null ? Ok(id) : NotFound();
    }

    [HttpPost("new/create/{id:guid}")]
    public async Task<ActionResult<bool>> Create(Guid id)
    {
        var result = await _mediator.Send(new CreateSudokuBoardRequest { SudokuId = id });

        return result is true ? Ok() : NotFound();
    }

    [HttpGet("new/get/{id:guid}")]
    public async Task<ActionResult<SudokuBoardModel>> GetNew(Guid id)
    {
        var sudokuBoardModel = await _mediator.Send(new GetNewSudokuBoardRequest { SudokuId = id });

        return sudokuBoardModel is not null? Ok(sudokuBoardModel) : NotFound();
    }


    /*[HttpPost("newTest")]
    public async Task<ActionResult<string>> CreateTest()
    {
        var newBoard = _sudokuBoardService.GenerateSudokuBoard();
        return newBoard is not null ? Ok(newBoard.Test()) : NotFound();
    }*/
}
