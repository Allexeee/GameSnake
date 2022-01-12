public class Apple
{
    public Coordinate coordinate { get; }

    public event Action<Apple> OnDestroyed;

    public Apple(Coordinate coordinate)
    {
        this.coordinate = coordinate;
    }

    public void Hide()
    {
        OnDestroyed?.Invoke(this);
    }

    public void Digest(){

    }
}

public class FactoryApple
{
    IReadOnlySnake _snake;

    List<Coordinate> _cache;

    ConteinerApples _conteiner;

    public FactoryApple(IReadOnlySnake snake, ConteinerApples conteiner)
    {
        _snake = snake;
        _cache = new List<Coordinate>();
        _conteiner = conteiner;
    }

    public Apple Spawn()
    {
        var cell = GetFreeCells().GetRandomItem();
        var apple = new Apple(cell);
        _conteiner.Register(apple);
        return apple;
    }

    List<Coordinate> GetFreeCells()
    {
        _cache.Clear();
        for (int x = 0; x < Globals.BOARD_WEIGHT; x++)
        {
            for (int y = 0; y < Globals.BOARD_HEIGHT; y++)
            {
                var result = true;
                foreach (var sn in _snake.sequenceCoords)
                {
                    if (sn.x == x && sn.y == y)
                    {
                        result = false;
                        break;
                    }
                }
                if (result)
                    _cache.Add(new Coordinate(x, y));
            }
        }
        return _cache;
    }
}

public class ConteinerApples
{
    List<Apple> _apples;

    public IEnumerable<Apple> apples => _apples;

    public ConteinerApples()
    {
        _apples = new List<Apple>();
    }

    public void Register(Apple apple)
    {
        _apples.Add(apple);
        apple.OnDestroyed += UnRegister;
    }

    void UnRegister(Apple apple)
    {
        _apples.Remove(apple);
        apple.OnDestroyed -= UnRegister;
    }
}