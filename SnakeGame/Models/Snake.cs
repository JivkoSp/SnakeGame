using SnakeGame.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame.Models
{
    public class Snake
    {
        private int SnakePosX, SnakePosY, FoodPosX, FoodPosY;
        private Queue<Tuple<int, int>> Snakebody;
        private Board Board;

        private Tuple<int, int> EntityStartingPosition()
        {
            Random random = new Random();

            int positionX = random.Next(1, Board.X - 1);
            int positionY = random.Next(1, Board.Y - 1);

            return new Tuple<int, int>(positionX, positionY);
        }

        private void UpdateBody()
        {
            Tuple<int, int> part = Snakebody.Dequeue();
            Console.SetCursorPosition(part.Item2, part.Item1);
            Console.Write(" ");
            Snakebody.Enqueue(new Tuple<int, int>(SnakePosX, SnakePosY));
        }

        private void DisplaySnake()
        {
            foreach (var bodyPart in Snakebody)
            {
                Console.SetCursorPosition(bodyPart.Item2, bodyPart.Item1);
                Console.ForegroundColor = ConsoleColor.Yellow;      
                Console.Write("#");
            }
            Console.ResetColor();
        }

        private void FoodReached()
        {
            if(SnakePosX == FoodPosX && SnakePosY == FoodPosY)
            {
                #pragma warning disable CA1416 // Validate platform compatibility
                Console.Beep(450, 100);
                #pragma warning restore CA1416 // Validate platform compatibility
                Console.SetCursorPosition(FoodPosY, FoodPosX);
                Console.WriteLine(" ");
                Snakebody.Enqueue(new Tuple<int, int>(FoodPosX, FoodPosY));
                Tuple<int, int> Foodposition = EntityStartingPosition();
                FoodPosX = Foodposition.Item1;
                FoodPosY = Foodposition.Item2;
            }
        }

        #region SnakeBite
        //Snake bites itself if the current location point
        //is equal to some of the location points of the body except the head
        //Head is in the queue back
        #endregion
        private bool SnakeBite()
        {
            if(Snakebody.Count > 1)
            {
                Tuple<int, int>[] copyBody = new Tuple<int, int>[Snakebody.Count];            
                Snakebody.CopyTo(copyBody, 0);

                for(int i=0; i<copyBody.Length-1;i++)
                {
                    if (SnakePosX == copyBody[i].Item1 && SnakePosY == copyBody[i].Item2) { return true; }
                }
            }
            return false;
        }
        
        public Snake(Board board)
        {
            Snakebody = new Queue<Tuple<int, int>>();
            Board = board;
            Tuple<int, int> Snakeposition = EntityStartingPosition();
            Tuple<int, int> Foodposition = EntityStartingPosition();
            SnakePosX = Snakeposition.Item1;
            SnakePosY = Snakeposition.Item2;
            FoodPosX = Foodposition.Item1;
            FoodPosY = Foodposition.Item2;
            Snakebody.Enqueue(new Tuple<int, int>(SnakePosX, SnakePosY));
        }

        public int Move()
        {           
            bool invalidKey = false, invalidMove = false;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Board.InsertAtPosition(FoodPosX, FoodPosY, "o");
            Console.ResetColor();
            DisplaySnake();

            switch (Console.ReadKey(true).KeyChar)
            {
                #pragma warning disable CA1416 // Validate platform compatibility
                case ('w'):
                    {
                        if (SnakePosX > 1) { SnakePosX--; }
                        else { Console.Beep(110, 100); invalidMove = true; }
                        break;
                    }
                    case ('s'):
                    {
                        if(SnakePosX < Board.X - 2) { SnakePosX++; }
                        else { Console.Beep(110, 100); invalidMove = true; }
                        break;
                    }
                    case ('a'):
                    {
                        if(SnakePosY > 1) { SnakePosY--; }
                        else { Console.Beep(110, 100); invalidMove = true; }
                        break;
                    }
                    case ('d'):
                    {
                        if(SnakePosY < Board.Y - 2) { SnakePosY++; }
                        else { Console.Beep(110, 100); invalidMove = true; }
                        break;
                    }
                #pragma warning restore CA1416 // Validate platform compatibility
                default: invalidKey = true; break;
            }

            if(!invalidKey && !invalidMove)
            {
                #pragma warning disable CA1416 // Validate platform compatibility
                Console.Beep(200, 100);
                #pragma warning restore CA1416 // Validate platform compatibility
                UpdateBody();
                if (SnakeBite()) { throw new SnakeException("Snake bite itself!"); }
                FoodReached();
                DisplaySnake();        
            }

            Thread.Sleep(10);

            return Snakebody.Count;
        }


        #region RestartSnake
        //Deletes the body parts of the snake by their location on the grid
        #endregion
        public void RestartSnake()
        {
            foreach (var bodyPart in Snakebody)
            {
                Console.SetCursorPosition(bodyPart.Item2, bodyPart.Item1);
                Console.Write(" ");
            }
        }
    }
}
