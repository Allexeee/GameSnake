public static class Globals
{
  public const int BOARD_WEIGHT = 10;
  public const int BOARD_HEIGHT = 10;
  public const int SIZE_CELL = 25;
  public const string TITLE_WINDOW = "Snake";

  public static Coordinate GetClamped(Coordinate coordinate)
  {
    var x = coordinate.X;
    var y = coordinate.Y;
    if (x < 0)
      x = BOARD_WEIGHT - 1;
    if (y < 0)
      y = BOARD_HEIGHT - 1;
    if (x > BOARD_WEIGHT - 1)
      x = 0;
    if (y > BOARD_HEIGHT - 1)
      y = 0;

    return new Coordinate(x, y);
  }
}