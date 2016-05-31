/**
 * Program.cs
 * An entry point (and a controller) for the game "Connect Four"
 * Author: Ryan
 * Date: 17 May 2016
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Program
    {
        static void Main(string[] args)
        {
            //show welcome messgae
            ShowWelcome();

            //create a new game
            Game game = new Game();     
            int endGame = 0;

            ////Testing
            //TestRowsEndGame(game);
            //TestColsEndGame(game);
            //TestDiagsEndGame(game);
            //TestAntiDiagsEndGame(game);

            //make turns until the game is over
            while (endGame == 0)
            {
                MakeTurn(game);

                //check if the game is over
                endGame = game.CheckGameOver();
            }

            ShowBoard(game);


            //display the end game message
            ShowEndGameMessage(endGame);


            //wait for the user to press a key to exit
            Console.ReadLine();
        }

        static void TestRowsEndGame(Game game)
        {
            game.Board.State = new int[,] { { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0}, { 0, 0, 0, 2, 2, 2, 2 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 } };
            int endGame = game.CheckGameOver();
            Console.WriteLine("endGameRows = " + endGame);
        }

        static void TestColsEndGame(Game game)
        {
            game.Board.State = new int[,] { { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0 } };
            int endGame = game.CheckGameOver();
            Console.WriteLine("endGameCols = " + endGame);
        }


        static void TestDiagsEndGame(Game game)
        {
            game.Board.State = new int[,] { { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 1, 0, 0, 0 }, {1, 0, 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 0, 0, 1 } };
            int endGame = game.CheckGameOver();
            Console.WriteLine("endGameDiags = " + endGame);
        }

        static void TestAntiDiagsEndGame(Game game)
        {
            game.Board.State = new int[,] { { 0, 2, 0, 0, 0, 0, 0 }, { 0, 0, 2, 0, 0, 0, 0 }, { 0, 0, 0, 2, 0, 0, 0 }, { 0, 0, 0, 0, 2, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 } };
            int endGame = game.CheckGameOver();
            Console.WriteLine("endGameDiags = " + endGame);
        }

        //Shows welcome message (prints in the console)
        static void ShowWelcome()
        {
            Console.WriteLine("Welcome to Connect Four!");
        }

        //shows a board (prints in the console)
        static void ShowBoard(Game game)
        {
            Console.Write(game.Board);
        }

        //shows an end game message (prints in the console)
        static void ShowEndGameMessage(int endGame)
        {
            switch (endGame)
            {
                case 1:
                    Console.WriteLine("Player 1 Wins!");
                    break;
                case 2:
                    Console.WriteLine("Player 2 Wins!");
                    break;
                case 3:
                    Console.WriteLine("Its a draw!");
                    break;
                default:
                    Console.WriteLine("Something went wrong, restart the app");
                    break;
            }
        }

        //shows who's turn it is (prints in the console)
        static void ShowTurn(Game game)
        {
            switch (game.Turn)
            {
                case 1:
                    Console.WriteLine("Drop a Red disk at column (1-7): ");
                    break;
                case 2:
                    Console.WriteLine("Drop a Yellow disk at column (1-7): ");
                    break;
                default:
                    Console.WriteLine("Something went wrong, restart the app");
                    break;
            }
        }

        static void MakeTurn(Game game)
        {
            //switch the player
            game.SwitchTurn();      

            //prompt the user for a valid column and get a row
            int row;
            int col;
            getMove(game,  out row, out col);

            //make a turn
            game.makeTurn( row, col - 1);
        }

        static void getMove(Game game, out int row, out int col)
        {
            //initial prompt for a column
            col = promptForColumn(game, "");
            row = -1;

            //keep prompting until the move can be done
            bool valid = false;
            while (!valid)
            {
                //check for integer validity
                if (col > Board.COLS || col < 1)
                {               
                    col = promptForColumn(game, "You must enter a number between 1 and " + Board.COLS);
                }      
                else
                {
                    //check for column validity (try to drop)
                    row = game.checkCol(col - 1);

                    //if success, exit the loop
                    if (row != -1)
                    {
                        valid = true;
                    }
                    //if not, display an error and prompt again
                    else
                    {                        
                        col = promptForColumn(game, "This column has no free space, try another one.");
                    }
                }
            }

        }

        static int promptForColumn(Game game, string message)
        {
            ShowBoard(game);
            Console.WriteLine(message);
            ShowTurn(game);

            //ask for a valid column number
            int col;
            bool parsed = Int32.TryParse(Console.ReadLine(), out col);

            //if it cant be parsed as an int, return -1
            if (!parsed)
            {
                return -1;
            }

            return col;
        }
    }
}
