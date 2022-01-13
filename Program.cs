using System.Diagnostics;
using Raylib_cs;

static class Program
{
  static GameRender _render;
  static Snake _snake;
  static FactoryApple _factoryApples;
  static FactorySnake _factorySnake;
  static CollisionsRecords _records;
  static PhysicsRouter _physicsRouter;
  static GamePhysics _physics;
  static bool gameEnded;
  static int _score;

  static Program()
  {
    var board = new Board(Globals.BOARD_WEIGHT, Globals.BOARD_HEIGHT);
    _factorySnake = new FactorySnake();
    _snake = _factorySnake.Spawn();
    _factoryApples = new FactoryApple(_snake, board);
    _render = new GameRender(board, _snake);
    _records = new CollisionsRecords();
    _physicsRouter = new PhysicsRouter(_records.Values);
    _physics = new GamePhysics(_snake, board, _physicsRouter);
    _records.GameEnd += () =>
    {
      // Сделать поле нестатичным
      gameEnded = true;

    };
  }

  static void Main(string[] args)
  {
    Task.Run(UpdateFixed);
    _render.OpenWindow();
    SpawnApple();
    while (_render.IsWorking())
    {
      if (gameEnded)
        _render.DrawGameOver(_score);
      else
      {
        _render.DrawScore(_score);
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

  static void SpawnApple()
  {
    var apple = _factoryApples.Spawn();
    apple.OnSwallowed += apple =>
    {
      _score += 10;
      SpawnApple();
    };
  }

  static async void UpdateFixed()
  {
    while (true)
    {
      _snake.Move();
      _physics.Step();
      // TODO: вынести задержку в динамическую переменную. 
      // Сделать возможность регулировать при помощи скорости.
      await Task.Delay(500);
    }
  }
}