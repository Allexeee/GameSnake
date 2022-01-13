using static PhysicsRouter;

public class CollisionsRecords
{
  public event Action? GameEnd;

  public IEnumerable<Record> Values()
  {
    yield return IfCollided((Snake snake, Apple apple) =>
    {
      snake.Swallow(apple);
    });

    yield return IfCollided((SnakeHead head, SnakeBody body) =>
    {
      GameEnd?.Invoke();
    });
  }

  private Record IfCollided<T1, T2>(Action<T1, T2> action)
  {
    return new Record<T1, T2>(action);
  }
}