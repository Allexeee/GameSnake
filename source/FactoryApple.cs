
public class FactoryApple
{
  Snake _snake;

  List<Coordinate> _cache;

  Board _conteiner;

  public FactoryApple(Snake snake, Board conteiner)
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
    for (int x = 0; x < Config.BOARD_WEIGHT; x++)
    {
      for (int y = 0; y < Config.BOARD_HEIGHT; y++)
      {
        var result = true;
        foreach (var sn in _snake.All)
        {
          if (sn.EqualsValues(x, y))
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