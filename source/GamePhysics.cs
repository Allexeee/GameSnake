public class GamePhysics
{
  Snake _snake;
  Board _board;

  PhysicsRouter _physicsRouter;

  public GamePhysics(Snake snake, Board conteiner, PhysicsRouter physicsRouter)
  {
    _snake = snake;
    _board = conteiner;
    _physicsRouter = physicsRouter;
  }

  public void Step()
  {
    foreach (var body in _snake.Body.Coordinates)
    {
      if (body == _snake.Head.Coordinate)
      {
        _physicsRouter.TryAddCollision(_snake.Head, _snake.Body);
        break;
      }
    }

    foreach (var apple in _board.Apples)
    {
      if (apple.Coordinate == _snake.Head.Coordinate)
        _physicsRouter.TryAddCollision(_snake, apple);
    }

    _physicsRouter.Step();
  }
}