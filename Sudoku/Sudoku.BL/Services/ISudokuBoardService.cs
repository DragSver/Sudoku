using Sudoku.Domain.Models;

namespace Sudoku.BL.Services;

public interface ISudokuBoardService
{
    public SudokuBoardModel GenerateSudokuBoard();
}
