using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.DataAccess;
using Sudoku.Domain;
using Sudoku.Domain.Entities;
using Sudoku.Domain.Models.SudokuBoardsModels;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;

public class AddFavoriteSudokuBoardRequest : IRequest<SudokuActionResult>
{
    public SudokuBoardModel SudokuBoardModel { get; set; }
    public Guid UserId { get; set; }
    public Guid SudokuBoardId { get; set; }
}

public class AddFavoriteSudokuBoardRequestHandler : IRequestHandler<AddFavoriteSudokuBoardRequest, SudokuActionResult>
{
    private readonly AppDbContext _appDbContext;

    public AddFavoriteSudokuBoardRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<SudokuActionResult> Handle(AddFavoriteSudokuBoardRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sudokuBoard = await _appDbContext.SudokuBoards
                .FirstOrDefaultAsync(x => (x.Id == request.SudokuBoardId && x.UserId == request.UserId), cancellationToken);

            if (sudokuBoard == null)
            {
                var entity = new SudokuBoard { SudokuBoardModel = request.SudokuBoardModel, UserId = request.UserId, Id = request.SudokuBoardId };

                await _appDbContext.AddAsync(entity, cancellationToken);
                await _appDbContext.SaveChangesAsync();

                return new SudokuActionResult { Message = "", Success = true };
            }
            else
                return new SudokuActionResult { Message = "Это судоку уже находится в избранном игрока.", Success = false };


        }
        catch (Exception ex)
        {
            return new SudokuActionResult { Message = ex.Message, Success = false };
        }
    }
}
