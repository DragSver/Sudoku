using MediatR;
using Sudoku.BL.Services;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;

public class CreateSudokuBoardRequest : IRequest<bool>
{
    public Guid SudokuId { get; set; }
}
public class CreateNewSudokuBoardRequestHandler : IRequestHandler<CreateSudokuBoardRequest, bool> 
{
    private readonly ISudokuBoardsService _sudokuBoardsService;

    public CreateNewSudokuBoardRequestHandler(ISudokuBoardsService sudokuBoardsService)
    {
        _sudokuBoardsService = sudokuBoardsService;
    }

    public async Task<bool> Handle(CreateSudokuBoardRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _sudokuBoardsService.CreateSudokuBoard(request.SudokuId);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
