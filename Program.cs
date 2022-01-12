using System.Diagnostics;
using Raylib_cs;

static class Program
{
    static Render _render;
    static Snake _snake;
    static ConteinerApples _conteinerApples;
    static FactoryApple _factoryApples;
    static CollisionsRecords _records;
    static PhysicsRouter _physicsRouter;
    static Physics _physics;
    static Task _taskUpdate;

    public static bool gameEnded;

    static Program()
    {
        var board = new Board(Globals.BOARD_WEIGHT, Globals.BOARD_HEIGHT);
        _snake = new Snake();
        _conteinerApples = new ConteinerApples();
        _factoryApples = new FactoryApple(_snake, _conteinerApples);
        _render = new Render(board, _snake, _conteinerApples);
        _records = new CollisionsRecords();
        _physicsRouter = new PhysicsRouter(_records.Values);
        _physics = new Physics(_snake, _conteinerApples, _physicsRouter);
        _records.GameEnd += () =>
        {
            gameEnded = true;

        };
    }

    static void Main(string[] args)
    {
        Task.Run(Update);
        _render.OpenWindow();
        SpawnApple();
        while (_render.TryExecute())
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
                _snake.TrySetDirection(Direction.Left);
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
                _snake.TrySetDirection(Direction.Right);
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                _snake.TrySetDirection(Direction.Top);
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
                _snake.TrySetDirection(Direction.Down);

        }

        _render.CloseWindow();

    }

    static void SpawnApple()
    {
        var apple = _factoryApples.Spawn();
        apple.OnDestroyed += apple => SpawnApple();
    }

    static async void Update()
    {
        while (true)
        {
            _snake.Move();
            _physics.Step();
            await Task.Delay(500);
        }
    }
}