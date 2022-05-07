using SnakeGame.Data.DBContext;
using SnakeGame.Data.Entities;
using SnakeGame.Models;
using SnakeGame.Models.Exceptions;
using System;
using System.Threading;
using System.Linq;


namespace SnakeGame
{
    class Program
    {

        #region GameLogic

        //Make Game board:
        //Two dimensinal matrix with borders:
        //row=0,col=0 => upper border
        //row=0,col=0 => left border
        //row=0,col=X => right border
        //row=X,col=0 => down border

        //Starting point X=rand, Y=rand

        //Movement: 
        //w-> Up matrix: row-1,col
        //s-> Down matrix: row+1,col
        //a-> Left matrix: row,col-1
        //d-> Right matrix: row, col+1

        //Snake Logic:
        //Snake will be represented by -> #
        //Food will be represented by -> o
        //When food position is reached food item is deleted and snake is +1->*
        //Shake bites itself: If * reaches another * The game is over.      

        //Food Logic:
        //Food position X=rand, Y=rand
        //When snake +1* => food is consumed => new rand X,Y position for food

        #endregion

        private Board Board;
        private Snake Snake;

        public UserScore StartGame(Board board, Snake snake, SnakeGameDbContext dbContext)
        {
            long score = 0;
            Program program = new Program();
            program.Board = board;
            program.Snake = snake;
            UserScore userScore = new UserScore();
            Console.CursorVisible = false;
            program.Board.Draw();

            Console.SetCursorPosition(program.Board.Y + 4, 0);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Your score:");
            Console.SetCursorPosition(program.Board.Y + 4, 1);

            var bestScore = dbContext.UserScores.Max(x => x.Score);
            Console.Write($"Best score: {bestScore}");

            Console.ResetColor();  

            while (true)
            {    
                try
                {
                    score = program.Snake.Move();
                    userScore.Score = score;
                }
                catch(SnakeException ex)
                {
                    Console.SetCursorPosition(program.Board.Y + 4, 3);
                    Console.WriteLine($"SnakeException:");
                    Console.SetCursorPosition(program.Board.Y + 4, 4);
                    Console.Write(ex.Message);
                    break;
                }               
                Console.SetCursorPosition(program.Board.Y + 16, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(score);
                Console.ResetColor();
            }
            return userScore;
        }

        static void Main(string[] args)
        {
            Console.Title = "S N A K E   G A M E -> Author: Jivko Spasov";
            #pragma warning disable CA1416 // Validate platform compatibility
            Console.WindowHeight = 32;
            Console.WindowWidth = 80;
            #pragma warning restore CA1416 // Validate platform compatibility

            Program program = new Program();
            Board board = new Board(30, 50);
            Snake snake = new Snake(board);
            char answ = 'n';

            try
            {
                using(var dbContext = new SnakeGameDbContext())
                {
                    do
                    {
                        var userScore = program.StartGame(board, snake, dbContext);
                        dbContext.UserScores.Add(userScore);
                        dbContext.SaveChangesAsync();
                        Console.Write("Do you want to play again?");
                        Thread.Sleep(3000);
                        snake.RestartSnake();
                        snake = new Snake(board);
                        Console.Clear();

                        if (Console.KeyAvailable)
                        {
                            if (Console.ReadKey(true).KeyChar == 'y') { answ = 'y'; }
                        }

                    } while (answ == 'y');
                }          
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }         
        }
    }
}
