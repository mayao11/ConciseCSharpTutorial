using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    class Snake
    {
        public int x;
        public int y;

        public int dir;

        public Snake(int x, int y)
        {
            this.x = x; this.y = y;
        }
    }

    class Program
    {
        static Snake snake;
        static bool gameOver;

        static int W = 10;
        static int H = 10;

        static string[,] buffer;

        static void Init()
        {
            snake = new Snake(0,0);
            gameOver = false;
            buffer = new string[H+2, W+2];
        }

        static int Input()
        {
            ConsoleKey key = Console.ReadKey().Key;
            switch(key)
            {
                case ConsoleKey.W:
                    return 1;
                    break;
                case ConsoleKey.S:
                    return 2;
                    break;
                case ConsoleKey.A:
                    return 3;
                    break;
                case ConsoleKey.D:
                    return 4;
                    break;
                default:
                    return 0;
                    break;
            }
        }

        static void GameLogic(int dir)
        {
            if (gameOver)
            {
                return;
            }
            int x = snake.x;
            int y = snake.y;

            switch(dir)
            {
                case 1:
                    y -= 1;
                    break;
                case 2:
                    y += 1;
                    break;
                case 3:
                    x -= 1;
                    break;
                case 4:
                    x += 1;
                    break;
                default:
                    break;
            }

            if (x<0 || x>=W || y<0 || y>=H)
            {
                gameOver = true;
                return;
            }
            snake.x = x; snake.y = y;
            Debug.WriteLine("snake:{0} {1}", x, y);
        }

        static void DrawAll()
        {
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    buffer[i, j] = "  ";
                }
            }
            //DrawBorder();
            //DrawSnake();

            for (int i=0; i<buffer.GetLength(0); i++)
            {
                buffer[i, 0] = "##";
            }
            for (int i=0; i<buffer.GetLength(0); i++)
            {
                buffer[i, W+1] = "##";
            }
            for (int j=0; j<buffer.GetLength(1); j++)
            {
                buffer[0, j] = "##";
            }
            for (int j=0; j<buffer.GetLength(1); j++)
            {
                buffer[H+1, j] = "##";
            }

            buffer[snake.y+1, snake.x+1] = "o ";

            Refresh();
        }

        static void Refresh()
        {
            Console.Clear();
            for (int i=0; i<buffer.GetLength(0); i++)
            {
                for (int j=0; j<buffer.GetLength(1); j++)
                {
                    Console.Write(buffer[i,j]);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Init();
            DrawAll();
            while (true)
            {
                int dir = Input();
                GameLogic(dir);
                DrawAll();      // 清空buffer，写入buffer，清空屏幕，打印buffer
            }
        }
    }
}
