using Sudoku.Domain.Models.SudokuBoardsModels;

namespace Sudoku.SudokuProcessor.Interfaces;

public interface ICachedSudokuBoardService
{
    Task<bool> CreateSudokuBoard(Guid sudokuId, SudokuBoardModel sudokuBoard);

    Task<SudokuBoardModel> GetSudokuBoard(Guid sudokuId);
}
