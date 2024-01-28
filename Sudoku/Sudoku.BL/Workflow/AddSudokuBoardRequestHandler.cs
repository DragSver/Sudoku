using MediatR;
using Sudoku.DataAccess;
using Sudoku.Domain.Entities;

namespace Sudoku.BL.Workflow;

public class AddSudokuBoardRequest : IRequest<Guid?>
{
    public string SudokuBoardData { get; set; }
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
        var entity = new SudokuBoard { SudokuBoardData = request.SudokuBoardData, UserId = request.UserId };

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
