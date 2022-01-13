#pragma warning disable CS0660
#pragma warning disable CS0661
public class Coordinate
{
  public Coordinate(int x, int y)
  {
    this.X = x;
    this.Y = y;
  }

  public int X { get; }
  public int Y { get; }

  public static bool operator ==(Coordinate coord1, Coordinate coord2)
  {
    return coord1.X == coord2.X && coord1.Y == coord2.Y;
  }
  public static bool operator !=(Coordinate coord1, Coordinate coord2)
  {
    return coord1.X != coord2.X || coord1.Y != coord2.Y;
  }

  public bool EqualsValues(int x, int y) => X == x && Y == y;
}