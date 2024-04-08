using Sudoku.Domain.Models.SudokuBoardsModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Sudoku.Domain.Entities;

public class SudokuBoard
{
    public Guid Id { get; set; }
    [NotMapped] public SudokuBoardModel SudokuBoardModel { get; set; }

    public string SudokuBoardModelJson
    {
        get { return JsonSerializer.Serialize(SudokuBoardModel); }
        set { this.SudokuBoardModel = JsonSerializer.Deserialize<SudokuBoardModel>(value); }
    }

    public Guid UserId { get; set; }
    public SudokuUser User { get; set; }
}
