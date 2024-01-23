using MediatR;
using Sudoku.DataAccess;
using Sudoku.Domain.Entities;

namespace Sudoku.BL;

public class AddSudokuUserRequest : IRequest<Guid?>
{
    public string Login { get; set; }
    public string Password { get; set; }
}

public class AddSudokuUserRequestHandler : IRequestHandler<AddSudokuUserRequest, Guid?>
{
    private readonly AppDbContext _appDbContext;

    public AddSudokuUserRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Guid?> Handle(AddSudokuUserRequest request, CancellationToken cancellationToken)
    {
        var entity = new SudokuUser { Login = request.Login, Password = request.Password };

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
