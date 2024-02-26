using Sudoku.SudokuProcessor.Interfaces;

namespace Sudoku.SudokuProcessor.Services;

public class SudokuBoardService : ISudokuBoardService
{
    private readonly ILogger<CreateBoardService> _logger;
    private readonly ICreateBoardService _createBoardService;
    private readonly ICachedSudokuBoardService _cachedSudokuBoardService;

    public SudokuBoardService(ILogger<CreateBoardService> logger,
        ICreateBoardService createBoardService,
        ICachedSudokuBoardService cachedSudokuBoardService)
    {
        _logger = logger;
        _createBoardService = createBoardService;
        _cachedSudokuBoardService = cachedSudokuBoardService;
    }
    
    public async Task CreateBoard(Guid sudokuId)
    {

        var sudokuBoard = _createBoardService.GenerateSudokuBoard();

        try
        {
            await _cachedSudokuBoardService.CreateSudokuBoard(sudokuId, sudokuBoard);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
