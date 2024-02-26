namespace Sudoku.Domain.Models.SudokuBoardsModels;

public class SudokuCell
{
    public byte Row { get; set; }
    public byte Column { get; set; }
    public byte? Content { get; set; }
}
