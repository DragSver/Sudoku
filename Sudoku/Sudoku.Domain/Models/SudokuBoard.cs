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
        foreach (var i in _board)
        {
            stringBuilder.Append("{ ");

            stringBuilder.Append("row : ");
            stringBuilder.Append($"{i.Row}, ");

            stringBuilder.Append("column : ");
            stringBuilder.Append($"{i.Column}, ");

            stringBuilder.Append("content : ");
            stringBuilder.Append($"{(i.Content is null ? "null" : i.Content)} ");

            stringBuilder.Append("}, ");
        }
        stringBuilder.Remove(stringBuilder.Length - 2, 1);
        return stringBuilder.ToString();
    }

    public string Test()
    {
        var stringBuilder = new StringBuilder();
        _board.OrderBy(x => x.Row + x.Column);
        for (var i = 1; i <= _board.Length; i++)
        {
            stringBuilder.Append((_board[i-1].Content is null? " " : _board[i - 1].Content));

            if (i % 27 == 0)
                stringBuilder.Append("\n-----------------------------------\n");
            else if (i % 9 == 0)
                stringBuilder.Append("\n\n");
            else if (i % 3 == 0)
                stringBuilder.Append(" | ");
            else
                stringBuilder.Append("   ");
        }
        return stringBuilder.ToString();
    }

    public SudokuCell[] ToBoard(string boardData)
    {
        boardData.Split(" }, { ");
        //TODO
        return null;
    }

    public static SudokuBoard CreateSudokuBoard()
    {
        var random = new Random();
        var board = CreateStartBoard();

        if (random.Next(0,2) == 1) Transposition(board);

        var n = random.Next(10, 16);
        while (n > 0)
        {
            SwapRowSmall(board);
            n--;
        }

        n = random.Next(10, 16);
        while (n > 0)
        {
            SwapColumnSmall(board);
            n--;
        }

        n = random.Next(10, 16);
        while (n > 0)
        {
            SwapRowBig(board);
            n--;
        }

        n = random.Next(10, 16);
        while (n > 0)
        {
            SwapColumnBig(board);
            n--;
        }

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
            var row = (byte)random.Next(1, 10);
            var col = (byte)random.Next(1, 10);
            board.Where(c => c.Row == row).Where(c => c.Column == col).FirstOrDefault().Content = null;
        }
    }
    public static void Transposition(SudokuCell[] board)
    {
        byte i;
        foreach (var cell in board)
        {
            i = cell.Column;
            cell.Column = cell.Row;
            cell.Row = i;
        }
    }
    public static void SwapRowSmall(SudokuCell[] board)
    {
        var random = new Random();
        var area = random.Next(0, 3); 
        var row1 = random.Next(1, 4);
        var row2 = random.Next(1, 4);
        while (row1==row2)
            row2 = random.Next(1, 4);
        row1 = row1 + area * 3;
        row2 = row2 + area * 3;

        byte? j;
        for (var i = 1; i < 10; i++)
        {
            var cell1 = board.Where(x => x.Row == row1).Where(x => x.Column == i).FirstOrDefault();
            var cell2 = board.Where(x => x.Row == row2).Where(x => x.Column == i).FirstOrDefault();

            j = cell1.Content;
            cell1.Content = cell2.Content;
            cell2.Content = j;
        }
    }
    public static void SwapColumnSmall(SudokuCell[] board)
    {
        var random = new Random();
        var area = random.Next(0, 3);
        var col1 = random.Next(1, 4);
        var col2 = random.Next(1, 4);
        while (col1 == col2)
            col2 = random.Next(1, 4);
        col1 = col1 + area * 3;
        col2 = col2 + area * 3;

        byte? j;
        for (var i = 1; i < 10; i++)
        {
            var cell1 = board.Where(x => x.Column == col1).Where(x => x.Row == i).FirstOrDefault();
            var cell2 = board.Where(x => x.Column == col2).Where(x => x.Row == i).FirstOrDefault();

            j = cell1.Content;
            cell1.Content = cell2.Content;
            cell2.Content = j;
        }
    }
    public static void SwapRowBig(SudokuCell[] board)
    {
        var random = new Random();
        var area1 = random.Next(0, 3);
        var area2 = random.Next(0, 3);
        while (area1 == area2)
            area2 = random.Next(0, 3);


        byte? i;

        for (var row = 1; row < 4; row++)
            for (var col = 1; col < 10; col++)
            {
                var rowCell1 = row + area1 * 3;
                var rowCell2 = row + area2 * 3;
                var cell1 = board.Where(x => x.Row == rowCell1).Where(x => x.Column == col).FirstOrDefault();
                var cell2 = board.Where(x => x.Row == rowCell2).Where(x => x.Column == col).FirstOrDefault();

                i = cell1.Content;
                cell1.Content = cell2.Content;
                cell2.Content = i;
            }
    }
    public static void SwapColumnBig(SudokuCell[] board)
    {
        var random = new Random();
        var area1 = random.Next(0, 3);
        var area2 = random.Next(0, 3);
        while (area1 == area2)
            area2 = random.Next(0, 3);


        byte? i;

        for (var col = 1; col < 4; col++)
            for (var row = 1; row < 10; row++)
            {
                var colCell1 = col + area1 * 3;
                var colCell2 = col + area2 * 3;
                var cell1 = board.Where(x => x.Row == row).Where(x => x.Column == colCell1).FirstOrDefault();
                var cell2 = board.Where(x => x.Row == row).Where(x => x.Column == colCell2).FirstOrDefault();

                i = cell1.Content;
                cell1.Content = cell2.Content;
                cell2.Content = i;
            }
    }
}
