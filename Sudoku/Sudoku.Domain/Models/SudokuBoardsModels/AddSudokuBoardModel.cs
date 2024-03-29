﻿using System.ComponentModel.DataAnnotations;

namespace Sudoku.Domain.Models.SudokuBoardsModels;

public class AddSudokuBoardModel
{
    [Required]
    public Guid UserId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string SudokuBoardData { get; set; }
}
