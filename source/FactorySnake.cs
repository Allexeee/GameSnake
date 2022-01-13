public class FactorySnake
{
  public Snake Spawn()
  {
    var storage = new Snake.StorageBody();
    storage.Add(new Coordinate(5, 5));
    storage.Add(new Coordinate(4, 5));
    var snake = new Snake(storage);

    return snake;
  }
}