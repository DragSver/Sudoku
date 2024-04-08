using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.DataAccess;
using Sudoku.Domain.Entities;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;

public class GetSudokuBoardRequest : IRequest<SudokuBoard>
{
    public Guid UserId { get; set; }
    public Guid SudokuModelId { get; set; }
}

public class GetSudokuBoardRequestHandler : IRequestHandler<GetSudokuBoardRequest, SudokuBoard>
{
    private readonly AppDbContext _appDbContext;

    public GetSudokuBoardRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<SudokuBoard> Handle(GetSudokuBoardRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sudokuBoard = await _appDbContext.SudokuBoards
                .FirstOrDefaultAsync(x => (x.Id == request.SudokuModelId && x.UserId == request.UserId), cancellationToken);

            return sudokuBoard;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
