namespace Sudoku.BL.Services;

public interface ISudokuBoardsService
{
    public Task CreateSudokuBoard(Guid boardId);
}
