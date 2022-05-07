using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame.Models
{
    public class Board
    {
        private List<List<string>> matrix;
        public int X { get; set; }
        public int Y { get; set; }

        //Creates the board
        private void InitMatrix()
        {
            for(int row=0; row<X; row++)
            {
                var newRow = new List<string>();

                for(int col=0; col<Y; col++)
                {
                    if(row == 0) //upper bounds
                    {
                        newRow.Add("*");      
                    }
                    else if(col == 0 && row < X-1) //left bounds
                    {
                        newRow.Add("*");
                    }
                    else if(col == Y-1 && row < X-1) //right bounds
                    {
                        newRow.Add("*");
                    }
                    else if(row == X-1) //down bounds
                    {
                        newRow.Add("*");
                    }
                    else { newRow.Add(" "); }
                }
                matrix.Add(newRow);
            }
        }

        public Board(int x, int y)
        {
            matrix = new List<List<string>>();
            X = x;
            Y = y;
            InitMatrix();
        }

        public void InsertAtPosition(int x, int y, string entity)
        {
            Console.SetCursorPosition(y, x);
            Console.Write(entity);
        }

        public void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int row = 0; row < matrix.Count; row++)
            {
                for (int col = 0; col < matrix[row].Count; col++)
                {
                    Console.Write(matrix[row][col]);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
