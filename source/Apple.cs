public class Apple
{
  public event Action<Apple>? OnSwallowed;

  public Apple(Coordinate coordinate)
  {
    this.Coordinate = coordinate;
  }

  public Coordinate Coordinate { get; }

  public void OnSwallow()
  {
    OnSwallowed?.Invoke(this);
  }
}