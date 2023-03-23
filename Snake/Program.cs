using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool flag_move = false;
            bool game_over = false;
            char[,] Game_space = new char[20, 30];
            

            int height = Game_space.GetLength(0);
            int width = Game_space.GetLength(1);
            string[] Game = new string[height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((i == 0 || i == height - 1) && (j != width - 1 && j != 0))
                    {
                        Game_space[i, j] = '-';
                    }
                    if ((j == 0 || j == width - 1) && (i != height - 1 && i != 0))
                    {
                        Game_space[i, j] = '|';
                    }
                    if ((i == 0 && j == 0) || (i == height - 1 && j == width - 1))
                    {
                        Game_space[i, j] = '/';
                    }
                    if ((i == 0 && j == width - 1) || (i == height - 1 && j == 0))
                    {
                        Game_space[i, j] = '\\';
                    }
                }
            }    
                

            Snake snake = new Snake();

            while (!game_over)
            {
                Thread.Sleep(1000/60);
                ConsoleKeyInfo temp_move = snake.Move;

                if (Console.KeyAvailable)
                {
                    snake.Move = Console.ReadKey();
                    flag_move = true;
                }

                if (flag_move)
                {
                    if ((temp_move.Key == ConsoleKey.LeftArrow && snake.Move.Key == ConsoleKey.RightArrow)
                    || (temp_move.Key == ConsoleKey.RightArrow && snake.Move.Key == ConsoleKey.LeftArrow)
                    || (temp_move.Key == ConsoleKey.UpArrow && snake.Move.Key == ConsoleKey.DownArrow)
                    || (temp_move.Key == ConsoleKey.DownArrow && snake.Move.Key == ConsoleKey.UpArrow))
                    {
                        snake.Move = temp_move;
                    }
                    Move_snake(Game_space,snake,ref game_over); 
                }
                Update_screen(Game, Game_space, snake);
            }

            Console.WriteLine("Game over");
        }

        static public void Update_screen(string[] Game, char[,] Game_space, Snake snake)
        {
            Console.Clear();

            for (int i = 1; i < Game_space.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < Game_space.GetLength(1) - 1; j++)
                {
                    if (Game_space[i, j] != '*')
                    {
                        Game_space[i, j] = ' ';
                    }
                }
            }

            Draw_snake(Game_space, snake);
            Add_point(Game_space);

            //for (int i = 0; i < Game_space.GetLength(0); i++)
            //{
            //    for (int j = 0; j < Game_space.GetLength(1); j++)
            //    {
            //        Console.Write(Game_space[i, j]);
            //    }
            //    Console.WriteLine();
            //}

            for (int i = 0; i < Game_space.GetLength(0); i++)
            {
                string temp = ""; 
                for (int j = 0; j < Game_space.GetLength(1); j++)
                {
                    temp += Game_space[i, j];
                }
                Game[i] = temp;
            }

            for (int i = 0; i < Game_space.GetLength(0); i++)
            {
                Console.WriteLine(Game[i]);
            }
        }

        static public void Add_point(char[,] Game_space)
        {
            var rand = new Random();
            bool dot_not_in = true;
            int _i;
            int _j;

            for (int i = 0; i < Game_space.GetLength(0); i++)
                for (int j = 0; j < Game_space.GetLength(1); j++)
                {
                    if ('*' == Game_space[i, j])
                    {
                        dot_not_in = false;
                        break;
                    }
                }

            if (dot_not_in)
            {
                do
                {
                    _i = rand.Next(1, Game_space.GetLength(0) - 2);
                    _j = rand.Next(1, Game_space.GetLength(1) - 2);
                } while (Game_space[_i, _j] == 'ж');

                Game_space[_i, _j] = '*';
            }

        }

        static public void Draw_snake(char[,] Game_space, Snake snake)
        {
            //for (int i = 0; i < Game_space.GetLength(0); i++)
            //    for (int j = 0; j < Game_space.GetLength(1); j++)
            //    {
            //        for (int k = 0; k < snake.Parts.Count; k++)
            //        {
            //            if (snake.Parts[k].I == i && snake.Parts[k].J ==j)
            //            {
            //                Game_space[i, j] = 'ж';
            //                break;
            //            }
            //        }
            //    }
            // Test below

            for (int k = 0; k < snake.Parts.Count; k++)
            {
                Game_space[snake.Parts[k].I, snake.Parts[k].J] = 'ж';
            }
        }

        static public void Move_snake(char[,] Game_space, Snake snake,ref bool game_over)
        {
            Snake_part_pos New_pos_snake_head = new Snake_part_pos();
            
            switch (snake.Move.Key)
            {
                case ConsoleKey.RightArrow:
                    {
                        New_pos_snake_head = new Snake_part_pos(snake.Parts[snake.Parts.Count - 1].I, snake.Parts[snake.Parts.Count - 1].J + 1);
                        break;
                    }

                case ConsoleKey.LeftArrow:
                    {
                        New_pos_snake_head = new Snake_part_pos(snake.Parts[snake.Parts.Count - 1].I, snake.Parts[snake.Parts.Count - 1].J - 1);
                        break;
                    }

                case ConsoleKey.UpArrow:
                    {
                        New_pos_snake_head = new Snake_part_pos(snake.Parts[snake.Parts.Count - 1].I - 1, snake.Parts[snake.Parts.Count - 1].J);
                        break;
                    }

                case ConsoleKey.DownArrow:
                    {
                        New_pos_snake_head = new Snake_part_pos(snake.Parts[snake.Parts.Count - 1].I + 1, snake.Parts[snake.Parts.Count - 1].J);
                        break;
                    }

                default:
                    break; 
            }

            switch (Game_space[New_pos_snake_head.I, New_pos_snake_head.J])
            {
                case 'ж':
                    {
                        game_over = true;
                        break;
                    }

                case '|':
                    {
                        game_over = true;
                        break;
                    }

                case '-':
                    {
                        game_over = true;
                        break;
                    }

                case '*':
                    {
                        snake.Parts.Add(New_pos_snake_head);
                        break;
                    }

                case ' ':
                    {
                        for (int k = 0; k < snake.Parts.Count - 1; k++)
                        {
                            snake.Parts[k] = snake.Parts[k + 1];
                        }

                        snake.Parts[snake.Parts.Count - 1] = New_pos_snake_head;
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
    }
}
