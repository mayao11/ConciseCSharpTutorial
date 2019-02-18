using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    struct Pos
    {
        public int x;
        public int y;
        public Pos(int x, int y)
        {
            this.x = x; this.y = y;
        }

        public Pos(Pos p)
        {
            this.x = p.x; this.y = p.y;
        }
    }

    class Snake
    {
        public LinkedList<Pos> bodies;
        public int dir;

        public Snake()
        {
            bodies = new LinkedList<Pos>();
            bodies.AddLast(new Pos(2, 0));
            bodies.AddLast(new Pos(1, 0));
            bodies.AddLast(new Pos(0, 0));

            dir = 0;
        }
    }

    class Apple
    {
        public Pos pos;
    }

    class Program
    {
        static Snake snake;
        static Apple apple;
        static bool gameOver;

        static Random random;

        static int W = 3;
        static int H = 3;

        static string[,] buffer;

        static void Init()
        {
            random = new Random();

            snake = new Snake();
            apple = new Apple();
            apple.pos = new Pos(1, 1);

            gameOver = false;
            buffer = new string[H + 2, W + 2];
            Console.CursorVisible = false;
        }

        static int Input()
        {
            ConsoleKey key = ConsoleKey.Spacebar;
            //while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true).Key;
            }

            switch (key)
            {
                case ConsoleKey.W:
                    return 1;
                case ConsoleKey.S:
                    return 2;
                case ConsoleKey.A:
                    return 3;
                case ConsoleKey.D:
                    return 4;
                default:
                    return 0;
            }
        }

        static void ChangeSnakeDir(int dir)
        {
            if (dir == 0) { return; }
            if (dir == snake.dir) { return; }
            if (dir==1 && snake.dir==2) { return; }
            if (dir==2 && snake.dir==1) { return; }
            if (dir==3 && snake.dir==4) { return; }
            if (dir==4 && snake.dir==3) { return; }

            snake.dir = dir;
        }

        static void GameLogic(int dir)
        {
            if (gameOver)
            {
                return;
            }

            ChangeSnakeDir(dir);
            if (snake.dir == 0)
            {
                return;
            }

            int x = snake.bodies.First.Value.x;
            int y = snake.bodies.First.Value.y;

            switch (snake.dir)
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

            if (x < 0 || x >= W || y < 0 || y >= H)
            {
                gameOver = true;
                return;
            }

            // 链表移动
            snake.bodies.RemoveLast();
            snake.bodies.AddFirst(new Pos(x, y));

            if (snake.bodies.First.Value.Equals(apple.pos))
            {
                // 
                apple = new Apple();
                //apple.pos = new Pos(random.Next(0, W), random.Next(0, H));
                int n = W * H - snake.bodies.Count;
                int rand = random.Next(0, n);
                bool[,] temp = new bool[H, W];

                LinkedListNode<Pos> p = snake.bodies.First;
                while (p != null)
                {
                    temp[p.Value.y, p.Value.x] = true;
                    p = p.Next;
                }

                for (int i=0; i<H; i++)
                {
                    bool flag = false;
                    for (int j=0; j<W; j++)
                    {
                        if (!temp[i,j])
                        {
                            if (rand == 0)
                            {
                                apple.pos = new Pos(j, i);
                                flag = true;
                                break;
                            }
                            rand -= 1;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }


                // 蛇变长
                var tail = snake.bodies.Last.Value;
                snake.bodies.AddLast(tail);
            }

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

            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                buffer[i, 0] = "##";
            }
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                buffer[i, W + 1] = "##";
            }
            for (int j = 0; j < buffer.GetLength(1); j++)
            {
                buffer[0, j] = "##";
            }
            for (int j = 0; j < buffer.GetLength(1); j++)
            {
                buffer[H + 1, j] = "##";
            }

            // 画蛇
            LinkedListNode<Pos> p = snake.bodies.First;
            while (p != null)
            {
                buffer[p.Value.y + 1, p.Value.x + 1] = "■";
                p = p.Next;
            }

            // 画苹果
            if (apple != null)
            {
                buffer[apple.pos.y+1, apple.pos.x+1] = "●";
            }

            Refresh();
        }

        static void Refresh()
        {
            Console.Clear();
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    Console.Write(buffer[i, j]);
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
                Thread.Sleep(400);
            }
        }
    }
}
