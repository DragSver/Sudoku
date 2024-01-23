using Sudoku.Domain.Entities;

namespace Sudoku.Domain.Response;

public class GetSudokuBoardsOfUser
{
    public List<SudokuBoard> SudokuBoards { get; set; }
}
