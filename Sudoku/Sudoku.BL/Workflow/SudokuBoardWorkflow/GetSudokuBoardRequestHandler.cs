using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.DataAccess;
using Sudoku.Domain.Models.SudokuBoardsModels;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;

public class GetSudokuBoardRequest : IRequest<SudokuBoardModel>
{
    public Guid Id { get; set; }
}

public class GetSudokuBoardRequestHandler : IRequestHandler<GetSudokuBoardRequest, SudokuBoardModel>
{
    private readonly AppDbContext _appDbContext;

    public GetSudokuBoardRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<SudokuBoardModel> Handle(GetSudokuBoardRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sudokuBoard = await _appDbContext.SudokuBoards
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


            var sudokuBoardModel = SudokuBoardModel.ToBoard(sudokuBoard?.SudokuBoardData);

            return sudokuBoardModel;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
