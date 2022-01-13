public class SnakeStomach
{
  public List<Apple> Apples { get; }

  Func<Coordinate> _tail;

  Apple? _cache;

  public SnakeStomach(Func<Coordinate> tail)
  {
    Apples = new List<Apple>();
    _tail = tail;
  }

  public bool CanExtend(out Coordinate? coordinate)
  {
    var tail = _tail();
    foreach (var apple in Apples)
    {
      if (tail == apple.Coordinate)
      {
        _cache = apple;
        coordinate = apple.Coordinate;
        return true;
      }
    }
    coordinate = default;
    return false;
  }

  public void Extend()
  {
    if (_cache == default)
      throw new NullReferenceException(nameof(_cache));

    Apples.Remove(_cache);
  }
}