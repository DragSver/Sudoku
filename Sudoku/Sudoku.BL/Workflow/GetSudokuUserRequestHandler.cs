using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.DataAccess;
using Sudoku.Domain.Response;

namespace Sudoku.BL.Workflow;

public class GetSudokuUserRequest : IRequest<GetSudokuUserResponse>
{
    public Guid Id { get; set; }
}

public class GetSudokuUserRequestHandler : IRequestHandler<GetSudokuUserRequest, GetSudokuUserResponse>
{
    private readonly AppDbContext _appDbContext;

    public GetSudokuUserRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<GetSudokuUserResponse> Handle(GetSudokuUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sudokuUser = await _appDbContext.SudokuUsers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var getSudokuUserResponse = new GetSudokuUserResponse { Login = sudokuUser?.Login, Password = sudokuUser?.Password };

            return getSudokuUserResponse;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
