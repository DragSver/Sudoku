using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.DataAccess;
using Sudoku.Domain.Models;

namespace Sudoku.BL.Workflow.User;

public class GetSudokuUserRequest : IRequest<SudokuUserModel>
{
    public Guid Id { get; set; }
}

public class GetSudokuUserRequestHandler : IRequestHandler<GetSudokuUserRequest, SudokuUserModel>
{
    private readonly AppDbContext _appDbContext;

    public GetSudokuUserRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<SudokuUserModel> Handle(GetSudokuUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sudokuUser = await _appDbContext.SudokuUsers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var getSudokuUserResponse = new SudokuUserModel { Login = sudokuUser?.Login, Password = sudokuUser?.Password };

            return getSudokuUserResponse;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
