using System.Diagnostics;
using Raylib_cs;

public class Render
{
    public const int SIZE_CELL = 25;

    Board _board;
    Snake _snake;
    RenderMap _renderMap;
    ConteinerApples _conteinerApples;

    public Render(Board board, Snake snake, ConteinerApples conteiner)
    {
        _board = board;
        _snake = snake;
        _renderMap = new RenderMap(board.width + 1, board.width + 1);
        _conteinerApples = conteiner;
    }

    public void OpenWindow()
    {
        Raylib.InitWindow(_board.width * SIZE_CELL, _board.height * SIZE_CELL, "Snake");
        Raylib.SetTargetFPS(30);
    }

    public void CloseWindow()
    {
        Raylib.CloseWindow();
    }

    public bool TryExecute()
    {
        if (Raylib.WindowShouldClose()) return false;
        Raylib.BeginDrawing();
        _renderMap.Clear();
        if (Program.gameEnded)
        {
            var width = Raylib.GetScreenWidth();
            var height = Raylib.GetScreenHeight();
            Raylib.DrawText("Game Over", width / 2 - 50, height / 2, 20, Color.BLACK);
        }
        else
        {
            Prepare(_snake);
            Prepare(_conteinerApples.apples);
            _renderMap.Draw();
        }

        Raylib.EndDrawing();

        return true;
    }

    // public void Execute()
    // {
    //     Clear();
    //     Prepare(_board);
    //     Prepare(_snake);
    //     Display();
    // }

    // void Clear()
    // {
    //     Console.Clear();
    // }

    // void Prepare(Board board)
    // {
    //     for (int x = 0; x < board.width; x++)
    //     {
    //         for (int y = 0; y < board.height; y++)
    //         {
    //             _renderMap.cells[x, y].symbol = '0';
    //             // _renderMap.cells[x, y].color = Color.BLACK;
    //         }
    //     }
    // }

    void Display()
    {
        for (int y = 0; y < _renderMap.cells.GetLength(1); y++)
        {
            for (int x = 0; x < _renderMap.cells.GetLength(0); x++)
            {
                // _renderMap.cells[x, y].color = Color.BLACK;
                // Console.Write(_renderMap.cells[x, y].symbol);
            }
            // Console.WriteLine();
        }
    }

    void Prepare(IReadOnlySnake snake)
    {
        _renderMap.cells[snake.headCoord.x, snake.headCoord.y].color = Color.RED;

        foreach (var body in snake.bodyCoords)
        {
            // Console.WriteLine($"x:{body.x} y:{body.y}");
            _renderMap.cells[body.x, body.y].color = Color.BLACK;

        }
    }

    void Prepare(IEnumerable<Apple> apples)
    {
        foreach (var apple in apples)
        {
            _renderMap.cells[apple.coordinate.x, apple.coordinate.y].color = Color.GREEN;
        }
    }
}

public class RenderMap
{
    public Cell[,] cells { get; }

    public RenderMap(int weight, int height)
    {
        cells = new Cell[weight, height];
        for (int x = 0; x < weight; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cell = new Cell();
                cells[x, y] = cell;
            }
        }
    }

    public void Clear()
    {
        Raylib.ClearBackground(Color.WHITE);
        for (int y = 0; y < cells.GetLength(1); y++)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                var cell = cells[x, y];
                cell.color = Color.WHITE;
            }
        }
    }

    public void Draw()
    {
        for (int y = 0; y < cells.GetLength(1); y++)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                var cell = cells[x, y];
                Raylib.DrawRectangle(x * Render.SIZE_CELL, y * Render.SIZE_CELL, Render.SIZE_CELL, Render.SIZE_CELL, cell.color);
            }
        }
    }
}

public class Cell
{
    public char symbol { get; set; }
    public Color color { get; set; }

    public Cell() { }
}