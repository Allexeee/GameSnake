public class Snake : IReadOnlySnake
{
    SnakeHead _head;
    SnakeBody _body;
    SnakeStomach _stomach;

    // List<Coordinate> _sequence => _body.All;
    public Coordinate headCoord => _body.Head;
    public IEnumerable<Coordinate> bodyCoords => _body.Body;
    public IEnumerable<Coordinate> sequenceCoords => _body.All;
    public SnakeHead Head => _head;
    public SnakeBody Body => _body;

    Direction moveDirection;
    Direction moveDirectionLast;

    public Snake()
    {
        _head = new SnakeHead();
        _body = new SnakeBody();
        _stomach = new SnakeStomach(() => _body.Tail);

        _body.Add(new Coordinate(5, 5));
        _body.Add(new Coordinate(4, 5));
        _body.Add(new Coordinate(3, 5));
        _body.Add(new Coordinate(2, 5));
        moveDirection = Direction.Right;
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

    public void Swallow(Apple apple)
    {
        apple.Hide();
        _stomach.Apples.Add(apple);
    }

    public void Move()
    {
        _body.Move(moveDirection);
        moveDirectionLast = moveDirection;
        if (_stomach.CanExtend(out var coord))
        {
            _stomach.Extend();
            _body.Add(coord);
        }
    }

}

public enum Direction
{
    Left,
    Right,
    Top,
    Down
}

public interface IReadOnlySnake
{
    public Coordinate headCoord { get; }
    public IEnumerable<Coordinate> bodyCoords { get; }
    public IEnumerable<Coordinate> sequenceCoords { get; }
}

public class SnakeHead
{
}

public class SnakeBody
{
    List<Coordinate> _all { get; } = new List<Coordinate>();
    List<Coordinate> _body { get; } = new List<Coordinate>();

    public Coordinate Head => _all[0];

    public Coordinate Tail => _all[_all.Count - 1];

    public IReadOnlyList<Coordinate> All => _all;
    public IEnumerable<Coordinate> Body => GetBody();

    public void Add(Coordinate coordinate)
    {
        _all.Add(coordinate);
        if (_all.Count > 1)
            _body.Add(coordinate);
    }

    public void Move(Direction direction)
    {
        MoveBody();
        MoveHead(direction);
    }

    void MoveHead(Direction direction)
    {
        var prev = Head;
        var next = Globals.GetClamped(GetNext(direction, prev));
        _all[0] = next;
    }

    void MoveBody()
    {
        for (int i = _all.Count - 1; i >= 1; i--)
        {
            var next = _all[i - 1];
            _all[i] = next;
        }
    }


    Coordinate GetNext(Direction direction, Coordinate prev)
    {
        switch (direction)
        {
            case Direction.Left:
                return new Coordinate(prev.x - 1, prev.y);
            case Direction.Right:
                return new Coordinate(prev.x + 1, prev.y);
            case Direction.Top:
                return new Coordinate(prev.x, prev.y - 1);
            case Direction.Down:
                return new Coordinate(prev.x, prev.y + 1);
            default:
                return new Coordinate(0, 0);
        }
    }

    IEnumerable<Coordinate> GetBody(){
        var enumerator = _all.GetEnumerator();
        enumerator.MoveNext();
        while (enumerator.MoveNext())
        {
             yield return enumerator.Current;
        }
    }
}

public class SnakeStomach
{
    public List<Apple> Apples { get; }

    Func<Coordinate> _tail;

    Apple _cashe;

    public SnakeStomach(Func<Coordinate> tail)
    {
        Apples = new List<Apple>();
        _tail = tail;
        _cashe = null;
    }

    public bool CanExtend(out Coordinate coordinate)
    {
        var tail = _tail();
        foreach (var apple in Apples)
        {
            if (tail.x == apple.coordinate.x && tail.y == apple.coordinate.y)
            {
                _cashe = apple;
                coordinate = apple.coordinate;
                return true;
            }
        }
        coordinate = default;
        return false;
    }

    public void Extend()
    {
        Apples.Remove(_cashe);
        _cashe.Digest();
    }


}