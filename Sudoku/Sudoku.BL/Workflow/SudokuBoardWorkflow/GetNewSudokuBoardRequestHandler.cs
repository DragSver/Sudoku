using MediatR;
using Newtonsoft.Json.Serialization;
using Sudoku.Domain.Models.SudokuBoardsModels;
using Sudoku.SudokuProcessor.Interfaces;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;

public class GetNewSudokuBoardRequest : IRequest<SudokuBoardModel>
{
    public Guid SudokuId { get; set; }
}

public class GetNewSudokuBoardRequestHandler : IRequestHandler<GetNewSudokuBoardRequest, SudokuBoardModel>
{
    private readonly ICachedSudokuBoardService _cachedSudokuBoardService;
    
    private const int TimeDelay = 1;
    private const int TimeToLive = 60;

    public GetNewSudokuBoardRequestHandler(ICachedSudokuBoardService cachedSudokuBoardService)
    {
        _cachedSudokuBoardService = cachedSudokuBoardService;
    }

    public async Task<SudokuBoardModel> Handle(GetNewSudokuBoardRequest request, CancellationToken cancellationToken)
    {
        SudokuBoardModel sudokuBoard = await FetchCachedCoroutine(request.SudokuId);

        return sudokuBoard;
    }

    private async Task<SudokuBoardModel> FetchCachedCoroutine(Guid sudokuId)
    {
        SudokuBoardModel sudokuBoard = null;
        DateTime startTime = DateTime.Now;

        while (sudokuBoard == null && DateTime.Now - startTime < TimeSpan.FromSeconds(TimeToLive))
        {
            await Task.Delay(TimeSpan.FromSeconds(TimeDelay));
            sudokuBoard = _cachedSudokuBoardService.GetSudokuBoard(sudokuId)?.Result;
        }

        return sudokuBoard;
    }
}
