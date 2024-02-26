using MediatR;
using Sudoku.DataAccess;
using Sudoku.Domain.Entities;

namespace Sudoku.BL.Workflow.SudokuBoardWorkflow;

public class GetSudokuBoardsOfUserRequest : IRequest<List<SudokuBoard>>
{
    public Guid Id { get; set; }
}

public class GetSudokuBoardsOfUserRequestHandler : IRequestHandler<GetSudokuBoardsOfUserRequest, List<SudokuBoard>>
{
    private readonly AppDbContext _appDbContext;

    public GetSudokuBoardsOfUserRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<SudokuBoard>> Handle(GetSudokuBoardsOfUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sudokuBoards = _appDbContext.SudokuBoards
                .Where(x => x.UserId == request.Id).ToList();

            return sudokuBoards;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
