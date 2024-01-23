namespace Sudoku.Domain.Entities;

public class SudokuBoard
{
    public Guid Id { get; set; }
    public string SudokuBoardData { get; set; }

    public Guid UserId { get; set; }
    public SudokuUser User { get; set; }
}
