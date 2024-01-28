using Sudoku.Domain.Entities;

namespace Sudoku.Domain.Response;

public class GetSudokuBoardsOfUserResponse
{
    public List<SudokuBoard> SudokuBoards { get; set; }
}
