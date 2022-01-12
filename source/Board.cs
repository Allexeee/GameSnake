public class Board
{
  public int width { get; }
  public int height { get; }

  public Board(int width, int height)
  {
      if (width <= 0)
          throw new ArgumentOutOfRangeException(nameof(width));
      if (height <= 0)
          throw new ArgumentOutOfRangeException(nameof(height));

      this.width = width;
      this.height = height;
  }
}