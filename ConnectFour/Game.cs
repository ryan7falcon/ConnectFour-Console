/**
 * Game.cs
 * A model for the game "Connect Four"
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
    class Game
    {
        public Board Board { set; get; }
        public int Turn { set; get; }
        int TurnsLeft { set; get; }

        public Game()
        {
            Board = new Board();
            TurnsLeft = 6 * 7;
        }

        //checks if someone wins (returns 1 or 2, depending on the winning side), or its a draw (returns 3) or the game continues (returns 0)
        public int CheckGameOver()
        {
            int w = CheckWin();

            if (w != 0)
            {
                return w;
            }

            return CheckDraw();
        }

        //Check if someone wins 
        // returns
        // 1 if player One wins, 
        // 2 if player Two wins, 
        // 0 otherwise
        int CheckWin()
        {

            int r = CheckEvery("row");
            if (r != 0)
            {
                return r;
            }

            r = CheckEvery("col");
            if (r != 0)
            {
                return r;
            }

            r = CheckEvery("diag");
            if (r != 0)
            {
                return r;
            }

            r = CheckEvery("antidiag");
            if (r != 0)
            {
                return r;
            }

            return 0;
        }

        //look for a winner in each row or column or diaganal (depending on what is passed as an element)
        int CheckEvery(string element)
        {
            int limit = 0;

            //get the number of elements (numner of rows, columns or diaganals)
            switch (element)
            {
                case "row":
                    limit = Board.ROWS;
                    break;
                case "col":
                    limit = Board.COLS;
                    break;
                case "diag":
                case "antidiag":
                    limit = 6;
                    break;
            }

            for (int i = 0; i < limit; i++)
            {
                int check = CheckOne(i, element);
                if (check != 0)
                {
                    return check;
                }
            }
            return 0;
        }

        //iterate through the column or a row or a diaganal and find a winner
        int CheckOne(int index, string element)
        {
            //the limit for looping
            int limit = 0;

            //starting points for looping in diaganals
            int startRow = 0;
            int startCol = 0;

            //variables for determining a winner
            int seq = 0;
            int prev = 0;
            int winner = 0;

            //get initial conditions depending on what we itarate through
            switch (element)
            {
                case "row":
                    limit = Board.COLS;
                    break;
                case "col":
                    limit = Board.ROWS ;
                    break;
                case "diag":
                    limit = DiagLen(index);                  
                    if (index < 3)
                    {
                        startRow = 3 + index;
                        startCol = 0;
                    }
                    else
                    {
                        startRow = 5;
                        startCol = index - 3;
                    }
                    break;
                case "antidiag":
                    limit = DiagLen(index);
                    if (index < 3)
                    {
                        startRow = 2 - index;
                        startCol = 0;
                    }
                    else
                    {
                        startRow = 0;
                        startCol = index - 2;
                    }
                    break;
            }
          
            //iterate through the column or a row or a diaganal and look for sequances of 4 disks
            for (int i = 0; i <limit; i++)
            {
                //current disk
                int current = 0;

                //get the current disk
                switch (element)
                {
                    case "row":
                        current = this.Board.State[index, i];
                        break;
                    case "col":
                        current = this.Board.State[i, index];
                        break;
                    case "diag":
                        current = this.Board.State[startRow - i, startCol + i];
                        break;
                    case "antidiag":
                        current = this.Board.State[startRow + i, startCol + i];
                        break;
                }         

                //if its the same as a previous disk, increase the sequance counter, store its color as a potemtial winner
                if (current == prev && current != 0)
                {
                    seq++;
                    winner = current;
                }
                //otherwise null the counter and the potential winner
                else
                {
                    seq = 0;
                    winner = 0;
                }

                //now the current disk becomes the prevous for the next loop iteration
                prev = current;

                //if the sequance registered 4 disks of the same color, return the winner
                if (seq == 3)
                {
                    return winner;
                }
            }
            return 0;
        }

        //returns the length of the diaganal
        int DiagLen(int diag)
        {
            switch (diag)
            {
                case 0:
                    return 4;
                case 1:
                    return 5;
                case 2:
                    return 6;
                case 3:
                    return 6;
                case 4:
                    return 5;
                case 5:
                    return 4;
                default:
                    return 0;

            }
        }

        //returns 3 if its a draw, 0 if not
        int CheckDraw()
        {
            if (TurnsLeft == 0)
            {
                return 3;
            }
            return 0;
        }

        //switches turns
        public int SwitchTurn()
        {
            if (Turn == 1)
            {
                Turn = 2;
            }
            else
            {
                Turn = 1;
            }

            return Turn;
        }

        //checks if there is a possibility to drop in a given column, returns a row or -1
        public int checkCol(int col)
        {
           return Board.GetDropRow(col);
        }

        //makes a turn (adds a piece to the board at (row, col)
        public void makeTurn(int row, int col)
        {
           TurnsLeft--;
           Board.Add(row, col, Turn);
        }
        
    }
}
