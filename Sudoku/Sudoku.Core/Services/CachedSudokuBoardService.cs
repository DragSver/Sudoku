using Microsoft.Extensions.Caching.Distributed;
using Sudoku.Core.Caching;
using Sudoku.Domain.Models.SudokuBoardsModels;
using Sudoku.SudokuProcessor.Interfaces;

namespace Sudoku.SudokuProcessor.Services;

public class CachedSudokuBoardService : ICachedSudokuBoardService
{
    private const int CacheTimeToLive = 300;

    private readonly ICacheProvider _cacheProvider;

    private static readonly SemaphoreSlim GetUrlsSemaphore = new(1, 1);

    public CachedSudokuBoardService(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public async Task<bool> CreateSudokuBoard(Guid sudokuId, SudokuBoardModel sudokuBoard)
    {
        var cachedCreate = await CachedSudokuBoard(sudokuId, sudokuBoard, GetUrlsSemaphore);
        return cachedCreate;
    }

    public async Task<SudokuBoardModel> GetSudokuBoard(Guid sudokuId)
    {
        var cachedSudokuBoard = await GetCachedResponse(sudokuId);
        return cachedSudokuBoard;
    }

    private async Task<bool> CachedSudokuBoard(Guid sudokuId, SudokuBoardModel sudokuBoard, SemaphoreSlim semaphore)
    {
        try
        {
            await semaphore.WaitAsync();

            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(CacheTimeToLive));

            var cachedSudokuBoard = new CachedSudokuBoard(sudokuId, sudokuBoard);

            await _cacheProvider.SetCache(sudokuId, cachedSudokuBoard, cacheEntryOptions);
        }
        finally
        {
            semaphore.Release();
        }

        return true;
    }

    private async Task<SudokuBoardModel> GetCachedResponse(Guid sudokuId)
    {
        var cachedSudokuBoard = await _cacheProvider.GetFromCache<CachedSudokuBoard>(sudokuId);

        return cachedSudokuBoard?.SudokuBoard;
    }
}
