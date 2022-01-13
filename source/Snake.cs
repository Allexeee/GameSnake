public class Snake
{
  StorageBody _storageBody;
  SnakeMotion _motion;
  SnakeStomach _stomach;

  public Snake(StorageBody _storage)
  {
    _storageBody = _storage;
    _motion = new SnakeMotion(() => _storageBody.GetAll());
    _stomach = new SnakeStomach(() => _storageBody.GetTail());
    Head = new SnakeHead(() => _storageBody.GetHead());
    Body = new SnakeBody(() => _storageBody.GetTorso());
    Tail = new SnakeTail(() => _storageBody.GetTail());
  }

  public IEnumerable<Coordinate> All => _storageBody.GetAll();
  public SnakeHead Head { get; }
  public SnakeBody Body { get; }
  public SnakeTail Tail { get; }

  public bool TrySetDirection(Direction direction) => _motion.TrySetDirection(direction);

  public void Swallow(Apple apple)
  {
    apple.OnSwallow();
    _stomach.Apples.Add(apple);
  }

  public void Move()
  {
    if (_stomach.CanExtend(out var coord))
    {
      _stomach.Extend();
#pragma warning disable CS8604
      _storageBody.Add(coord);
    }
    _motion.Move();
  }
  public class StorageBody
  {
    List<Coordinate> _all = new List<Coordinate>();

    public void Add(Coordinate coordinate)
    {
      _all.Add(coordinate);
    }

    public List<Coordinate> GetAll() => _all;
    public Coordinate GetHead() => _all[0];
    public Coordinate GetTail() => _all[_all.Count - 1];

    public IEnumerable<Coordinate> GetTorso()
    {
      var enumerator = _all.GetEnumerator();
      enumerator.MoveNext();
      while (enumerator.MoveNext())
      {
        yield return enumerator.Current;
      }
    }
  }
}

public class SnakeMotion
{
  Func<List<Coordinate>> _all;

  Direction moveDirection;
  Direction moveDirectionLast;

  public SnakeMotion(Func<List<Coordinate>> all)
  {
    _all = all;
    moveDirection = Direction.Right;
    moveDirectionLast = moveDirection;
  }

  public bool TrySetDirection(Direction direction)
  {
    if (GetOpposite(moveDirectionLast) == direction) return false;
    moveDirection = direction;
    return true;
  }

  Direction GetOpposite(Direction direction)
  {
    switch (direction)
    {
      case Direction.Left:
        return Direction.Right;
      case Direction.Right:
        return Direction.Left;
      case Direction.Top:
        return Direction.Down;
      case Direction.Down:
        return Direction.Top;
      default:
        throw new IndexOutOfRangeException(nameof(direction));
    }
  }

  public void Move()
  {
    MoveTorso();
    MoveHead(moveDirection);
    moveDirectionLast = moveDirection;
  }

  void MoveHead(Direction direction)
  {
    var prev = _all()[0];
    var next = Globals.GetClamped(GetNext(direction, prev));
    _all()[0] = next;
  }

  void MoveTorso()
  {
    for (int i = _all().Count - 1; i >= 1; i--)
    {
      var next = _all()[i - 1];
      _all()[i] = next;
    }
  }

  Coordinate GetNext(Direction direction, Coordinate prev)
  {
    switch (direction)
    {
      case Direction.Left:
        return new Coordinate(prev.X - 1, prev.Y);
      case Direction.Right:
        return new Coordinate(prev.X + 1, prev.Y);
      case Direction.Top:
        return new Coordinate(prev.X, prev.Y - 1);
      case Direction.Down:
        return new Coordinate(prev.X, prev.Y + 1);
      default:
        return new Coordinate(0, 0);
    }
  }
}

public class SnakeHead
{
  Func<Coordinate> _coordinate;

  public SnakeHead(Func<Coordinate> coordinate)
  {
    _coordinate = coordinate;
  }

  public Coordinate Coordinate => _coordinate();
}

public class SnakeBody
{
  Func<IEnumerable<Coordinate>> _coordinates;
  public SnakeBody(Func<IEnumerable<Coordinate>> coordinates)
  {
    _coordinates = coordinates;
  }
  public IEnumerable<Coordinate> Coordinates => _coordinates();

}

public class SnakeTail
{
  Func<Coordinate> _coordinate;

  public SnakeTail(Func<Coordinate> coordinate)
  {
    _coordinate = coordinate;
  }

  public Coordinate Coordinate => _coordinate();
}