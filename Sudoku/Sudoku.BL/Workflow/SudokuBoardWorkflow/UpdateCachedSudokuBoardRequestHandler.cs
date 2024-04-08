using MediatR;
using Microsoft.Extensions.Logging;
using Sudoku.Domain;
using Sudoku.Domain.Models.SudokuBoardsModels;
using Sudoku.SudokuProcessor.Interfaces;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;

public class UpdateCachedSudokuBoardRequest : IRequest<SudokuActionResult>
{
    public Guid SudokuId { get; set; }
    public SudokuBoardModel SudokuBoardModel { get; set; }
}

public class UpdateCachedSudokuBoardRequestHandler : IRequestHandler<UpdateCachedSudokuBoardRequest, SudokuActionResult>
{
    private readonly ICachedSudokuBoardService _cachedSudokuBoardService;

    public UpdateCachedSudokuBoardRequestHandler(ICachedSudokuBoardService cachedSudokuBoardService)
    {
        _cachedSudokuBoardService = cachedSudokuBoardService;
    }

    public async Task<SudokuActionResult> Handle(UpdateCachedSudokuBoardRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _cachedSudokuBoardService.CreateSudokuBoard(request.SudokuId, request.SudokuBoardModel);

            return new SudokuActionResult { Success = true, Message = "" };
        }
        catch (Exception ex)
        {
            return new SudokuActionResult { Success = false, Message = ex.Message };
        }
    }
}