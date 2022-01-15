using Raylib_CsLo;

// Главный класс игры.
// Отвечает за саму змейку
public class Game
{
  GameRender _render;
  Snake _snake;
  FactoryApple _factoryApples;
  FactorySnake _factorySnake;
  CollisionsRecords _records;
  PhysicsRouter _physicsRouter;
  GamePhysics _physics;

  public Game()
  {
    Locals = new Locals();
    Locals.SnakeSpeed = 50;

    var board = new Board(Config.BOARD_WEIGHT, Config.BOARD_HEIGHT);
    _factorySnake = new FactorySnake();
    _snake = _factorySnake.Spawn();
    _factoryApples = new FactoryApple(_snake, board);
    _render = new GameRender(board, _snake);
    _records = new CollisionsRecords();
    _physicsRouter = new PhysicsRouter(_records.Values);
    _physics = new GamePhysics(_snake, board, _physicsRouter);
    _records.GameEnd += () =>
    {
      Locals.GameEnded = true;
    };
  }
  public Locals Locals { get; }

  public void Start()
  {
    Task.Run(UpdateFixed);
    _render.OpenWindow();
    SpawnApple();
    while (_render.IsWorking())
    {
      if (Locals.GameEnded)
        _render.DrawGameOver(Locals.Score);
      else
      {
        _render.DrawScore(Locals.Score);
        _render.DrawGame();
        // TODO: вынести инпуты в отдельный класс.
        // Сделать так, чтобы движения змейки были командой.
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
          _snake.TrySetDirection(Direction.Left);
        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
          _snake.TrySetDirection(Direction.Right);
        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
          _snake.TrySetDirection(Direction.Top);
        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
          _snake.TrySetDirection(Direction.Down);
      }
    }

    _render.CloseWindow();

  }

  void SpawnApple()
  {
    var apple = _factoryApples.Spawn();
    apple.OnSwallowed += apple =>
    {
      Locals.Score += 10;
      SpawnApple();
    };
  }

  async void UpdateFixed()
  {
    while (true)
    {
      if (Locals.SnakeSpeed > 0)
      {
        _snake.Move();
        _physics.Step();
      }

      const int DELAY = 1000;
      var step = DELAY / (Config.MAX_SNAKE_SPEED + 1);
      await Task.Delay(DELAY - step * Locals.SnakeSpeed);
    }
  }
}