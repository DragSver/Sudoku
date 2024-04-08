using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.DataAccess;
using Sudoku.Domain;
using Sudoku.Domain.Entities;
using Sudoku.Domain.Models.SudokuBoardsModels;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;
public class DeleteFavoriteSudokuBoardRequest : IRequest<SudokuActionResult>
{
    public Guid SudokuBoardId { get; set; }
    public Guid UserId { get; set; }
}

public class DeleteFavoriteSudokuBoardRequestHandler : IRequestHandler<DeleteFavoriteSudokuBoardRequest, SudokuActionResult>
{
    private readonly AppDbContext _appDbContext;

    public DeleteFavoriteSudokuBoardRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<SudokuActionResult> Handle(DeleteFavoriteSudokuBoardRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sudokuBoard = await _appDbContext.SudokuBoards
                .FirstOrDefaultAsync(x => (x.Id == request.SudokuBoardId && x.UserId == request.UserId), cancellationToken);
            if (sudokuBoard is not null)
            {
                _appDbContext.SudokuBoards.Remove(sudokuBoard);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return new SudokuActionResult { Message = "", Success = true };
            }
            else
            {
                return new SudokuActionResult { Message = "Это судоку не найдено в избранном игрока.", Success = false };
            }

        }
        catch (Exception ex)
        {
            return new SudokuActionResult { Message = ex.Message, Success = false };
        }
    }
}
