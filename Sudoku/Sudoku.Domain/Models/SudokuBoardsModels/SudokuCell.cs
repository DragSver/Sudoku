namespace Sudoku.Domain.Models.SudokuBoardsModels;

public class SudokuCell
{
    public byte Row { get; set; }
    public byte Column { get; set; }
    public byte? OriginalValue { get; set; }
    public byte? CurrentValue { get; set; }
    public bool Changeable { get; set; }
    public List<byte> PossibleValues { get; set; }

    public SudokuCell() { }
    public SudokuCell(byte row, byte column, byte? originalValue, byte? currentValue, bool changeable, List<byte> possibleValues)
    {
        Row = row;
        Column = column;
        OriginalValue = originalValue;
        CurrentValue = currentValue;
        Changeable = changeable;
        PossibleValues = possibleValues;
    } 
}
