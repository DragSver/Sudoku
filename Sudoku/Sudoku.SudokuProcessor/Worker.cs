using Sudoku.Core.Models;
using Sudoku.Core.RabbitMq;
using Sudoku.SudokuProcessor.Interfaces;
using System.Text.Json;

namespace Sudoku.SudokuProcessor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IRabbitService _rabbitService;
    private readonly ISudokuBoardService _sudokuBoardService;

    public Worker(ILogger<Worker> logger,
        IRabbitService rabbitService,
        ISudokuBoardService sudokuBoardService)
    {
        _logger = logger;
        _rabbitService = rabbitService;
        _sudokuBoardService = sudokuBoardService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _rabbitService.StartConnection("sudokucreate");
        _rabbitService.Consume("sudokucreate", ProcessItemMessage);

        _logger.LogInformation("Started");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(2000, stoppingToken);
        }
    }

    private void ProcessItemMessage(string json)
    {
        _logger.LogInformation("From RabiitMQ (queue = 'sudokucreate') recieved: " + json);

        var message = JsonSerializer.Deserialize<SudokuBoardMessage>(json);

        switch (message.Action)
        {
            case MessageAction.CreateSudokuBoard:
                _sudokuBoardService.CreateBoard(message.Model);
                break;
            default: throw new NotImplementedException();
        }
    }
}
