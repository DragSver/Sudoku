using MediatR;
using Microsoft.EntityFrameworkCore;
using Sudoku.DataAccess;
using Sudoku.Domain.Response;

namespace Sudoku.BL;

public class GetSudokuBoardRequest : IRequest<GetSudokuBoardResponse>
{
    public Guid Id { get; set; }
}

public class GetSudokuBoardRequestHandler : IRequestHandler<GetSudokuBoardRequest, GetSudokuBoardResponse>
{
    private readonly AppDbContext _appDbContext;

    public GetSudokuBoardRequestHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<GetSudokuBoardResponse> Handle(GetSudokuBoardRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sudokuBoard = await _appDbContext.SudokuBoards
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var getSudokuBoardResponse = new GetSudokuBoardResponse { SudokuBoardData = sudokuBoard?.SudokuBoardData }

            return getSudokuBoardResponse;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
