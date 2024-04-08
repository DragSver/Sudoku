using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.DataAccess;
using Sudoku.Domain.Entities;
using Sudoku.Domain.Models.SudokuBoardsModels;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;

public class GetSudokuBoardRequest : IRequest<SudokuBoard>
{
    public Guid Id { get; set; }
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
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return sudokuBoard;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
