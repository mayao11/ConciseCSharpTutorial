using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First2048
{
    class Program
    {
        static char[,] buffer;
        static ConsoleColor[,] colorBuffer;
        static Random random = new Random();

        static int[,] nums;

        static void Refresh()
        {
            Console.Clear();
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    Console.ForegroundColor = colorBuffer[i, j];
                    Console.Write(buffer[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void DrawAll()
        {
            // 画棋盘格
            string grid =
                  "+-------------------+"
                + "|    |    |    |    |"
                + "+-------------------+"
                + "|    |    |    |    |"
                + "+-------------------+"
                + "|    |    |    |    |"
                + "+-------------------+"
                + "|    |    |    |    |"
                + "+-------------------+";

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    buffer[i, j] = grid[i * 21 + j];
                    colorBuffer[i, j] = ConsoleColor.White;
                }
            }

            // 画数字
            for (int i = 0; i < nums.GetLength(1); i++)
            {
                for (int j = 0; j < nums.GetLength(0); j++)
                {
                    int x = j * 5 + 1;
                    int y = i * 2 + 1;

                    string s = "    ";
                    if (nums[i, j] != 0)
                    {
                        s = nums[i, j].ToString();
                        switch (s.Length)
                        {
                            case 1:
                                s = "  " + s + " ";
                                break;
                            case 2:
                                s = " " + s + " ";
                                break;
                            case 3:
                                s = " " + s;
                                break;
                            default:
                                break;
                        }
                    }

                    for (int k = 0; k < s.Length; k++)
                    {
                        buffer[y, x + k] = s[k];
                        ConsoleColor c = ConsoleColor.Gray;
                        switch (nums[i, j])
                        {
                            case 2: c = ConsoleColor.White; break;
                            case 4: c = ConsoleColor.DarkBlue; break;
                            case 8: c = ConsoleColor.Blue; break;
                            case 16: c = ConsoleColor.DarkGreen; break;
                            case 32: c = ConsoleColor.Green; break;
                            case 64: c = ConsoleColor.DarkMagenta; break;
                            case 128: c = ConsoleColor.Magenta; break;
                            case 256: c = ConsoleColor.DarkRed; break;
                            case 512: c = ConsoleColor.Red; break;
                            case 1024: c = ConsoleColor.DarkYellow; break;
                            case 2048:
                            default:
                                c = ConsoleColor.Yellow; break;

                        }
                        colorBuffer[y, x + k] = c;
                    }
                }
            }
        }

        static void RandNewNum()
        {
            int num0 = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (nums[i, j] == 0)
                    {
                        num0++;
                    }
                }
            }
            Debug.WriteLine("num0 " + num0);
            if (num0 == 0)
            {
                return;
            }
            int r = random.Next(num0);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (nums[i, j] == 0)
                    {
                        if (r == 0)
                        {
                            nums[i, j] = 2;
                            return;
                        }
                        r--;
                    }
                }
            }
            return;
        }

        static int Input()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            int dir = 0;
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    dir = 1;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    dir = 2;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    dir = 3;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    dir = 4;
                    break;
            }
            return dir;
        }

        static void GameLogic()
        {
            // 向着dir方向移动
            bool moved = false;
            int dir = Input();

            switch (dir)
            {
                //case 1:
                //    for (int i=1; i<4; i++)
                //    {
                //        for (int j=0; j<4; j++)
                //        {
                //            if (nums[i,j] != 0)
                //            {
                //                if (nums[i-1,j] == 0)
                //                {
                //                    nums[i - 1, j] = nums[i, j];
                //                    nums[i, j] = 0;
                //                }
                //            }
                //        }
                //    }
                //    break;
                //case 2:
                //    for (int i=2; i>=0; i--)
                //    {
                //        for (int j=0; j<4; j++)
                //        {
                //            if (nums[i,j] != 0)
                //            {
                //                if (nums[i+1,j] == 0)
                //                {
                //                    nums[i + 1, j] = nums[i, j];
                //                    nums[i, j] = 0;
                //                }
                //            }
                //        }
                //    }
                //    break;
                //case 3:
                //    for (int j = 1; j <4; j++)
                //    {
                //        for (int i = 0; i < 4; i++)
                //        {
                //            if (nums[i, j] != 0)
                //            {
                //                if (nums[i, j-1] == 0)
                //                {
                //                    nums[i, j-1] = nums[i, j];
                //                    nums[i, j] = 0;
                //                }
                //            }
                //        }
                //    }
                //    break;
                //case 4:
                //    for (int j = 2; j >=0; j--)
                //    {
                //        for (int i = 0; i < 4; i++)
                //        {
                //            if (nums[i, j] != 0)
                //            {
                //                if (nums[i, j+1] == 0)
                //                {
                //                    nums[i, j+1] = nums[i, j];
                //                    nums[i, j] = 0;
                //                }
                //            }
                //        }
                //    }
                //    break;
            }

            switch (dir)
            {
                case 1:
                    for (int i = 1; i <= 3; i++)
                    {
                        for (int j = 0; j <= 3; j++)
                        {
                            if (nums[i, j] == 0)
                            {
                                continue;
                            }
                            int t = 0;     // t 代表找到的目标位置
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (nums[k, j] != 0)
                                {
                                    t = k;
                                    if (nums[k, j] != nums[i, j])
                                    {
                                        t = k + 1;
                                    }
                                    break;
                                }
                            }
                            if (t != i)
                            {
                                moved = true;
                                if (nums[t, j] == 0)
                                {
                                    nums[t, j] = nums[i, j];
                                    nums[i, j] = 0;
                                }
                                else
                                {
                                    nums[t, j] = nums[t, j] * 2;
                                    nums[i, j] = 0;
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    for (int i = 2; i >= 0; i--)
                    {
                        for (int j = 0; j <= 3; j++)
                        {
                            if (nums[i, j] == 0)
                            {
                                continue;
                            }
                            int t = 3;     // t 代表找到的目标位置
                            for (int k = i + 1; k <= 3; k++)
                            {
                                if (nums[k, j] != 0)
                                {
                                    t = k;
                                    if (nums[k, j] != nums[i, j])
                                    {
                                        t = k - 1;
                                    }
                                    break;
                                }
                            }
                            if (t != i)
                            {
                                moved = true;
                                if (nums[t, j] == 0)
                                {
                                    nums[t, j] = nums[i, j];
                                    nums[i, j] = 0;
                                }
                                else
                                {
                                    nums[t, j] = nums[t, j] * 2;
                                    nums[i, j] = 0;
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    for (int j = 1; j <= 3; j++)
                    {
                        for (int i = 0; i <= 3; i++)
                        {
                            if (nums[i, j] == 0)
                            {
                                continue;
                            }
                            int t = 0;
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (nums[i, k] != 0)
                                {
                                    t = k;
                                    if (nums[i, k] != nums[i, j])
                                    {
                                        t = k + 1;
                                    }
                                    break;
                                }
                            }
                            if (t != j)
                            {
                                moved = true;
                                if (nums[i, t] == 0)
                                {
                                    nums[i, t] = nums[i, j];
                                    nums[i, j] = 0;
                                }
                                else
                                {
                                    nums[i, t] = nums[i, t] * 2;
                                    nums[i, j] = 0;
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    for (int j = 2; j >= 0; j--)
                    {
                        for (int i = 0; i <= 3; i++)
                        {
                            if (nums[i, j] == 0)
                            {
                                continue;
                            }
                            int t = 3;
                            for (int k = j + 1; k <= 3; k++)
                            {
                                if (nums[i, k] != 0)
                                {
                                    t = k;
                                    if (nums[i, k] != nums[i, j])
                                    {
                                        t = k - 1;
                                    }
                                    break;
                                }
                            }
                            if (t != j)
                            {
                                moved = true;
                                if (nums[i, t] == 0)
                                {
                                    nums[i, t] = nums[i, j];
                                    nums[i, j] = 0;
                                }
                                else
                                {
                                    nums[i, t] = nums[i, t] * 2;
                                    nums[i, j] = 0;
                                }
                            }
                        }
                    }
                    break;
            }

            // 如果没有成功移动，不生成新数字
            // 生成一个新数字
            if (moved)
            {
                RandNewNum();
            }
        }

        static void Main(string[] args)
        {
            buffer = new char[9, 21];
            colorBuffer = new ConsoleColor[9, 21];

            nums = new int[4, 4];
            RandNewNum();

            // 先刷新一次界面
            DrawAll();
            Refresh();

            while (true)
            {
                GameLogic();

                DrawAll();
                Refresh();
            }


        }
    }
}
