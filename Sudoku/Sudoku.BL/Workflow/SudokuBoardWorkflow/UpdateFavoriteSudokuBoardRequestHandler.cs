using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.DataAccess;
using Sudoku.Domain;
using Sudoku.Domain.Models.SudokuBoardsModels;
using System.Text.Json;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;

public class UpdateFavoriteSudokuBoardRequest : IRequest<SudokuActionResult>
{
    public SudokuBoardModel SudokuBoardModel { get; set; }
    public Guid UserId { get; set; }
    public Guid SudokuBoardId { get; set; }
}

public class UpdateFavoriteSudokuBoardRequestHandler : IRequestHandler<UpdateFavoriteSudokuBoardRequest, SudokuActionResult>
{
    private readonly AppDbContext _appDbContext;

    public UpdateFavoriteSudokuBoardRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<SudokuActionResult> Handle(UpdateFavoriteSudokuBoardRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sudokuBoard = await _appDbContext.SudokuBoards
                .FirstOrDefaultAsync(x => (x.Id == request.SudokuBoardId && x.UserId == request.UserId), cancellationToken);

            if (sudokuBoard.SudokuBoardModelJson.Equals(JsonSerializer.Serialize(request.SudokuBoardModel)))
            {
                sudokuBoard.SudokuBoardModel = request.SudokuBoardModel;

                _appDbContext.Update(sudokuBoard);
                await _appDbContext.SaveChangesAsync();

                return new SudokuActionResult { Message = "", Success = true };
            }
            else
                return new SudokuActionResult { Message = "Между судоку нет отличий.", Success = false };


        }
        catch (Exception ex)
        {
            return new SudokuActionResult { Message = ex.Message, Success = false };
        }
    }
}
