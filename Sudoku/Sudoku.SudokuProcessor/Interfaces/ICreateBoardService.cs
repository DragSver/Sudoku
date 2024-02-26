using Sudoku.Domain.Models.SudokuBoardsModels;

namespace Sudoku.SudokuProcessor.Interfaces;

public interface ICreateBoardService
{
    public SudokuBoardModel GenerateSudokuBoard();
}
