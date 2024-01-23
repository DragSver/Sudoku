namespace Sudoku.Domain.Entities;

public class SudokuUser
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public List<SudokuBoard> SudokuBoards { get; set; }
}
