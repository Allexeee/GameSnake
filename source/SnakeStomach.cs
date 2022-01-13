public class SnakeStomach
{
  List<Apple> _apples;

  Func<Coordinate> _tail;

  Apple? _cache;

  public SnakeStomach(Func<Coordinate> tail)
  {
    _apples = new List<Apple>();
    _tail = tail;
  }

  public IEnumerable<Coordinate> GetApples()
  {
    foreach (var apple in _apples)
    {
      yield return apple.Coordinate;
    }
  }

  public void Add(Apple apple)
  {
    _apples.Add(apple);
  }

  public bool CanExtend(out Coordinate? coordinate)
  {
    var tail = _tail();
    foreach (var apple in _apples)
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

    _apples.Remove(_cache);
  }
}