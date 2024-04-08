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

    [HttpGet("favorite/{userId:guid}&{sudokuBoardId:guid}")]
    public async Task<ActionResult<SudokuBoard>> Get(Guid userId, Guid sudokuBoardId)
    {
        var sudokuBoard = await _mediator.Send(new GetSudokuBoardRequest { UserId = userId, SudokuModelId = sudokuBoardId });

        return sudokuBoard is not null ? Ok(sudokuBoard) : NotFound();
    }

    [HttpGet("user/{id:guid}")]
    public async Task<ActionResult<List<SudokuBoard>>> GetAll(Guid id)
    {
        var sudokuBoards = await _mediator.Send(new GetSudokuBoardsOfUserRequest { Id = id });

        return sudokuBoards;
    }

    [HttpPost("favourite/add")]
    public async Task<ActionResult<string>> AddFavorite([FromBody] AddFavoriteSudokuBoardModel model)
    {
        var result = await _mediator.Send(new AddFavoriteSudokuBoardRequest { UserId = model.UserId, SudokuBoardModel = model.SudokuBoardModel, SudokuBoardId = model.SudokuBoardId });

        return result.Success ? Ok() : NotFound(result.Message);
    }

    [HttpPost("favourite/delete")]
    public async Task<ActionResult<string>> DeleteFavorite([FromBody] DeleteFavoriteSudokuBoardModel model)
    {
        var result = await _mediator.Send(new DeleteFavoriteSudokuBoardRequest { UserId = model.UserId, SudokuBoardId = model.SudokuBoardId });

        return result.Success ? Ok() : NotFound(result.Message);
    }

    [HttpPost("favourite/update")]
    public async Task<ActionResult<string>> UpdateFavorite([FromBody] AddFavoriteSudokuBoardModel model)
    {
        var result = await _mediator.Send(new UpdateFavoriteSudokuBoardRequest { UserId = model.UserId, SudokuBoardId = model.SudokuBoardId });

        return result.Success ? Ok() : NotFound(result.Message);
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


    [HttpPut("update")]
    public async Task<ActionResult<string>> Update([FromBody] CachedSudokuBoard model)
    {
        var result = await _mediator.Send(new UpdateCachedSudokuBoardRequest { SudokuId = model.SudokuId, SudokuBoardModel = model.SudokuBoard });

        return result.Success ? Ok() : NotFound(result.Message);
    }
}
