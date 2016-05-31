/**
 * Board.cs
 * A model for the board in the game "Connect Four"
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
    public class Board
    {
        //dimentions
        public const int ROWS = 6;
        public const int COLS = 7;

        //  The Board:
        //
        //   --> columns
        //  |       
        //  V       | 0 0 0 0 0 0 0 |
        //  rows    | 0 0 0 0 0 0 0 |
        //          | 0 0 0 0 0 0 0 |
        //          | 0 0 0 0 1 0 2 |
        //          | 0 2 0 0 2 1 2 |
        //          | 0 1 2 1 1 2 1 |
        //
        //  1 - Player One (red)
        //  2 - Player Two (yellow)
        //  0 - Empty 

        // Contains the board's state
        public int[,] State { set; get; } 

        //inits the board with all zeros
        public Board()
        {
            State = new int[ROWS, COLS];
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    State[i, j] = 0;
                }
            }
        }

        //gets a string for printing a board
        public override string ToString()
        {
            string s = "\n";
            for (int i = 0; i < ROWS; i++)
            {
                s += "|";
                for (int j = 0; j < COLS; j++)
                {
                    int state = State[i, j];

                    if (state == 0)
                    {
                        s += " ";
                    }
                    if (state == 1)
                    {
                        s += "R";
                    }
                    if (state == 2)
                    {
                        s += "Y";
                    }
                    
                    s += "|";
                }
                s += "\n";
            }
            s += "\n";
            return s;
        }

        //adds a piece of a given side (1 or 2) to a certain position (row, col)
        public void Add(int row, int col, int side)
        {
            State[row, col] = side;
        }

        //returns a row to drop on a given column, -1 if there is no space
        public int GetDropRow(int col)
        {
            for (int i = ROWS - 1; i >= 0; i--)
            {
                if (State[i, col] == 0)
                {
                    return i;
                }
            }

            return -1;
        }
        
    }
}
