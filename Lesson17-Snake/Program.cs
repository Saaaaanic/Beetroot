using System.Diagnostics;
using Lesson17_Snake;

class Program
{
    public static int Wide = 40;
    public static int Height = 20;
    static void Main(string[] args)
    {
        Start();
        Console.ReadKey(true);
    }

    static void Start()
    {
        DrawBorders();
        Console.CursorVisible = false;
        Player player = new Player(10, 10, '$', '~');
        player.Draw();

        ConsoleKey key = ConsoleKey.D;
        Food food = EventFood(player);

        while (true)
        {
            if (player.Head.X == food.pixel.X && player.Head.Y == food.pixel.Y ||
                player.Body.Any(part => part.X == food.pixel.X && part.Y == food.pixel.Y))
            {
                player.Grow();
                food = EventFood(player);
            }
            if (Console.KeyAvailable)
            {
                key = ReadDirection(key);
            }

            player.ChangeDirection(key).Invoke();

            if (player.Head.X == 0 || player.Head.X == Wide ||
                player.Head.Y == 0 || player.Head.Y == Height ||
                player.Body.Any(part => part.X == player.Head.X && part.Y == player.Head.Y))
            {
                break;
            }

            
            Thread.Sleep(150);
        }
        player.Death();
    }

    static Food EventFood(Player player)
    {
        Food food = new Food();
        do
        {
            food.pixel.Draw();
        } while (player.Head.X == food.pixel.X && player.Head.Y == food.pixel.Y || 
                 player.Body.Any(part => part.X == food.pixel.X && part.Y == food.pixel.Y));

        return food;
    }
    
    static ConsoleKey ReadDirection(ConsoleKey key)
    {
        ConsoleKey temp = Console.ReadKey(true).Key;
        switch (temp)
        {
            case ConsoleKey.W when key == ConsoleKey.S:
            case ConsoleKey.S when key == ConsoleKey.W:
            case ConsoleKey.A when key == ConsoleKey.D:
            case ConsoleKey.D when key == ConsoleKey.A:
                return key;
            case ConsoleKey.W:
            case ConsoleKey.S:
            case ConsoleKey.A:
            case ConsoleKey.D:
                key = temp;
                break;
        }

        return key;
    }
    static void DrawBorders()
    {
        for (int i = 1; i < Wide; i++)
        {
            new Pixel(i, 0, '-').Draw();
            new Pixel(i, Height, '-').Draw();
        }

        for (int i = 1; i < Height; i++)
        {
            new Pixel(0, i, '|').Draw();
            new Pixel(Wide, i, '|').Draw();
        }
    }
}
//checked
