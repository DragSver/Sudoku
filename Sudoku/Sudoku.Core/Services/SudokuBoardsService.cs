using Sudoku.Core.Models;
using Sudoku.Core.RabbitMq;
using System.Text.Json;

namespace Sudoku.BL.Services;

public class SudokuBoardsService : ISudokuBoardsService
{
    private readonly IRabbitService _rabbitService;

    public SudokuBoardsService(IRabbitService rabbitService)
    {
        _rabbitService = rabbitService;
    }

    public Task CreateSudokuBoard(Guid boardId)
    {
        _rabbitService.Publish("sudokucreate",
            JsonSerializer.Serialize(new SudokuBoardMessage(MessageAction.CreateSudokuBoard, boardId)));

        throw new NotImplementedException();
    }
}
