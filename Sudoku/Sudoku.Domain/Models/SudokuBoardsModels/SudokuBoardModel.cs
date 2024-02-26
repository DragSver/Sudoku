using System.Text;

namespace Sudoku.Domain.Models.SudokuBoardsModels;

public class SudokuBoardModel
{
    public SudokuCell[] Cells { get; set; }

    public SudokuBoardModel() { }

    public SudokuBoardModel(string boardData)
    {
        ToBoard(boardData);
    }

    public SudokuBoardModel(SudokuCell[] board)
    {
        Cells = board;
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        foreach (var i in Cells)
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
        Cells.OrderBy(x => x.Row + x.Column);
        for (var i = 1; i <= Cells.Length; i++)
        {
            stringBuilder.Append(Cells[i - 1].Content is null ? " " : Cells[i - 1].Content);

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

    public static SudokuBoardModel ToBoard(string boardData)
    {
        boardData.Split(" }, { ");
        //TODO
        return null;
    }
}
