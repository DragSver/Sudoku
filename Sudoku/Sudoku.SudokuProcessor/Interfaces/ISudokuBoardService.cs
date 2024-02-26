namespace Sudoku.SudokuProcessor.Interfaces;

public interface ISudokuBoardService
{
    public Task CreateBoard(Guid sudokuId);
}
