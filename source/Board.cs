public class Board
{
  List<Apple> _apples;

  public Board(int width, int height)
  {
    if (width <= 0)
      throw new ArgumentOutOfRangeException(nameof(width));
    if (height <= 0)
      throw new ArgumentOutOfRangeException(nameof(height));

    this.Width = width;
    this.Height = height;
    _apples = new List<Apple>();
  }

  public int Width { get; }
  public int Height { get; }
  public IEnumerable<Apple> Apples => _apples;

  public void Register(Apple apple)
  {
    _apples.Add(apple);
    apple.OnSwallowed += UnRegister;
  }

  void UnRegister(Apple apple)
  {
    _apples.Remove(apple);
    apple.OnSwallowed -= UnRegister;
  }
}