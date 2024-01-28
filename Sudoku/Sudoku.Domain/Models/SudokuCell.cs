namespace Sudoku.Domain.Models;

public class SudokuCell
{
    public byte Row { get; set; }
    public byte Column { get; set; }
    public byte? Content { get; set; }
}
