public class Physics
{
    Snake _snake;
    ConteinerApples _conteinerApples;

    PhysicsRouter _physicsRouter;

    public Physics(Snake snake, ConteinerApples conteiner, PhysicsRouter physicsRouter)
    {
        _snake = snake;
        _conteinerApples = conteiner;
        _physicsRouter = physicsRouter;
    }

    public void Step()
    {
        foreach (var body in _snake.bodyCoords)
        {
            if (body.x == _snake.headCoord.x && body.y == _snake.headCoord.y)
            {
                _physicsRouter.TryAddCollision(_snake.Head, _snake.Body);
                break;
            }
        }

        foreach (var apple in _conteinerApples.apples)
        {
            if (apple.coordinate.x == _snake.headCoord.x && apple.coordinate.y == _snake.headCoord.y)
                _physicsRouter.TryAddCollision(_snake, apple);
        }

        _physicsRouter.Step();
    }
}