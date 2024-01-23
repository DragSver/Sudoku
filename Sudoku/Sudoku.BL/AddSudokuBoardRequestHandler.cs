using MediatR;
using Sudoku.DataAccess;
using Sudoku.Domain.Entities;

namespace Sudoku.BL;

public class AddSudokuBoardRequest : IRequest<Guid?>
{
    public string SudokuBoardData { get; set; }
    public SudokuUser User { get; set; }
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
        var entity = new SudokuBoard { SudokuBoardData = request.SudokuBoardData, User = request.User };

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
