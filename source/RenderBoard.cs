using Raylib_cs;
using static Globals;

public class RenderBoard
{
  public Cell[,] cells { get; }

  public RenderBoard(int weight, int height)
  {
    cells = new Cell[weight, height];
    for (int x = 0; x < weight; x++)
    {
      for (int y = 0; y < height; y++)
      {
        var cell = new Cell();
        cells[x, y] = cell;
      }
    }
  }

  public void Clear()
  {
    for (int y = 0; y < cells.GetLength(1); y++)
    {
      for (int x = 0; x < cells.GetLength(0); x++)
      {
        var cell = cells[x, y];
        cell.color = Color.WHITE;
      }
    }
  }

  public void Draw()
  {
    for (int y = 0; y < cells.GetLength(1); y++)
    {
      for (int x = 0; x < cells.GetLength(0); x++)
      {
        var cell = cells[x, y];
        Raylib.DrawRectangle(x * SIZE_CELL, y * SIZE_CELL, SIZE_CELL, SIZE_CELL, cell.color);
      }
    }
  }

  public class Cell
  {
    public Color color { get; set; }
  }
}