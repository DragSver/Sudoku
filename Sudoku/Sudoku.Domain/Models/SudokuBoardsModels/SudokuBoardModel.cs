namespace Sudoku.Domain.Models.SudokuBoardsModels;

public class SudokuBoardModel
{
    public ICollection<SudokuCell> Cells { get; set; }

    public SudokuBoardModel() { }
    public SudokuBoardModel(ICollection<SudokuCell> cells) 
    {
        Cells = cells;
    }
}
