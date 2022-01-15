public class Locals
{
  int _snakeSpeed;

  public bool GameEnded;
  public int Score;

  public int SnakeSpeed
  {
    get => _snakeSpeed; set
    {
      _snakeSpeed = Math.Clamp(value, _snakeSpeed, Config.MAX_SNAKE_SPEED);
    }
  }
}