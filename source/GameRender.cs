using Raylib_cs;
using static Globals;

public class GameRender
{

  Board _board;
  Snake _snake;
  RenderBoard _renderMap;

  public GameRender(Board board, Snake snake)
  {
    _board = board;
    _snake = snake;
    _renderMap = new RenderBoard(board.Width + 1, board.Width + 1);
  }

  public void OpenWindow()
  {
    Raylib.InitWindow(_board.Width * SIZE_CELL, _board.Height * SIZE_CELL, TITLE_WINDOW);
    Raylib.SetTargetFPS(30);
  }

  public void CloseWindow()
  {
    Raylib.CloseWindow();
  }

  public bool IsWorking() => !Raylib.WindowShouldClose();

  public void DrawGame()
  {
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    _renderMap.Clear();
    Prepare(_snake);
    Prepare(_board.Apples);
    _renderMap.Draw();
    Raylib.EndDrawing();
  }

  public void DrawGameOver(int score)
  {
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    var width = Raylib.GetScreenWidth();
    var height = Raylib.GetScreenHeight();
    Raylib.DrawText($"Game Over\r\nScore: {score}", width / 2 - 50, height / 2 - 50, 20, Color.BLACK);
    Raylib.EndDrawing();
  }

  public void DrawScore(int score)
  {
    Raylib.SetWindowTitle($"{TITLE_WINDOW} ({score})");
  }

  void Prepare(Snake snake)
  {
    _renderMap.cells[snake.Head.Coordinate.X, snake.Head.Coordinate.Y].color = Color.RED;

    foreach (var body in snake.Body.Coordinates)
    {
      _renderMap.cells[body.X, body.Y].color = Color.BLACK;
    }
  }

  void Prepare(IEnumerable<Apple> apples)
  {
    foreach (var apple in apples)
    {
      _renderMap.cells[apple.Coordinate.X, apple.Coordinate.Y].color = Color.GREEN;
    }
  }
}