namespace Sudoku.Domain.Models.SudokuBoardsModels;

public class CachedSudokuBoard
{
    public Guid SudokuId { get; set; }
    public SudokuBoardModel SudokuBoard { get; set; }

    public CachedSudokuBoard(Guid sudokuId, SudokuBoardModel sudokuBoard)
    {
        SudokuId = sudokuId;
        SudokuBoard = sudokuBoard;
    }
}
