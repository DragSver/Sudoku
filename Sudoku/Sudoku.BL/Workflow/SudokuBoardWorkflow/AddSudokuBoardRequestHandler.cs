using MediatR;
using Sudoku.DataAccess;
using Sudoku.Domain.Entities;
using Sudoku.Domain.Models.SudokuBoardsModels;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;

public class AddSudokuBoardRequest : IRequest<Guid?>
{
    public SudokuBoardModel SudokuBoardModel { get; set; }
    public Guid UserId { get; set; }
}

public class AddSudokuBoardRequestHandler : IRequestHandler<AddSudokuBoardRequest, Guid?>
{
    private readonly AppDbContext _appDbContext;

    public AddSudokuBoardRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Guid?> Handle(AddSudokuBoardRequest request, CancellationToken cancellationToken)
    {
        var entity = new SudokuBoard { SudokuBoardModel = request.SudokuBoardModel, UserId = request.UserId };

        try
        {
            await _appDbContext.AddAsync(entity, cancellationToken);
            await _appDbContext.SaveChangesAsync();

            return entity.Id;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
