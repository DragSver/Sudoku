using System;
using System.Text;
using System.Text.Json;

namespace Sudoku.Domain.Models;

public class SudokuBoard
{
    private SudokuCell[] _board;

    public SudokuBoard(string boardData)
    {
        ToBoard(boardData);
    }

    public SudokuBoard(SudokuCell[] board)
    {
        _board = board;
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("{\n    \"board\": {\n");
        foreach (var i in _board)
        {
            stringBuilder.Append("        \"row\": ");
            stringBuilder.Append($"{i.Row},\n");

            stringBuilder.Append($"        \"column\": ");
            stringBuilder.Append($"{i.Column},\n");

            stringBuilder.Append($"        \"content\": ");
            stringBuilder.Append($"{i.Content}\n");

            stringBuilder.Append("    },\n");
        }
        stringBuilder.Remove(stringBuilder.Length - 2, 1);
        stringBuilder.Append("}");
        return stringBuilder.ToString();
    }

    public SudokuCell[] ToBoard(string boardData)
    {
        //TODO
        return null;
    }

    public static SudokuBoard CreateSudokuBoard()
    {
        var board = CreateStartBoard();
        AddEmptyCells(board);
        return new SudokuBoard(board);
    }

    public static SudokuCell[] CreateStartBoard()
    {
        var board = new SudokuCell[81];
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                var m = (i * 3 + i / 3 + j) % 9 + 1;
                board[i * 9 + j] = new SudokuCell { Column = (byte)(j + 1), Row = (byte)(i + 1), Content = (byte)m };
            }
        }
        return board;
    }
    public static void AddEmptyCells(SudokuCell[] board)
    {
        var random = new Random();
        for (var i = 0; i < 50; i++)
        {
            var row = (byte)random.Next(1, 9);
            var col = (byte)random.Next(1, 9);
            board.Where(c => c.Row == row).Where(c => c.Column == col).FirstOrDefault().Content = null;
        }
    }
}

