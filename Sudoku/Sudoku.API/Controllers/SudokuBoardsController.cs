using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sudoku.BL;
using Sudoku.Domain.Models;
using Sudoku.Domain.Response;
using System.Text.Json;

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
    public async Task<ActionResult<GetSudokuBoardResponse>> Get(Guid id)
    {
        var sudokuBoardData = await _mediator.Send(new GetSudokuBoardRequest { Id = id });

        return sudokuBoardData is not null ? Ok(sudokuBoardData) : NotFound();
    }

    [HttpGet("user={id:guid}")]
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
    public async Task<ActionResult<byte[,]>> Create()
    {
        var newBoard = SudokuBoard.CreateSudokuBoard();
        return newBoard is not null ? Ok(newBoard) : NotFound();
    }
}

public class SudokuBoard
{
    private byte[,] _board;

    public SudokuBoard(string boardData)
    {
        ToBoard(boardData);
    }

    public SudokuBoard(byte[,] board) 
    {
        _board = board;
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(_board);
    }

    public byte[,] ToBoard(string boardData) 
    {
        _board = JsonSerializer.Deserialize<byte[,]>(boardData);
        return _board;
    }

    public static SudokuBoard CreateSudokuBoard()
    {
        var random = new Random();
        var board = new byte[9,9];
        for (var i = 0;  i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                var m = j + i*3 + 1;
                if (m > 9) m = m % 9;
                board[i, j] = (byte)m;
            }
        }
        for (var i = 0; i < 50; i++)
        {
            board[random.Next(1, 9), random.Next(1, 9)] = 0;
        }
        return new SudokuBoard(board);
    }
}
