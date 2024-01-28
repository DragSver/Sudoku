using MediatR;
using Sudoku.DataAccess;
using Sudoku.Domain.Response;

namespace Sudoku.BL.Workflow;

public class GetSudokuBoardsOfUserRequest : IRequest<GetSudokuBoardsOfUserResponse>
{
    public Guid Id { get; set; }
}

public class GetSudokuBoardsOfUserRequestHandler : IRequestHandler<GetSudokuBoardsOfUserRequest, GetSudokuBoardsOfUserResponse>
{
    private readonly AppDbContext _appDbContext;

    public GetSudokuBoardsOfUserRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<GetSudokuBoardsOfUserResponse> Handle(GetSudokuBoardsOfUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sudokuBoards = _appDbContext.SudokuBoards
                .Where(x => x.UserId == request.Id).ToList();

            var getSudokuBoardsOfUserResponse = new GetSudokuBoardsOfUserResponse { SudokuBoards = sudokuBoards };

            return getSudokuBoardsOfUserResponse;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
