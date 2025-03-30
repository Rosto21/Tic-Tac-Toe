using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private bool isPlayerX;// true means the player is using X, false means the player is using O
        private int difficulty = 1; //1=random ;2=first available position
        private bool isPlayerTurn = true;
        private byte turnCount = 0;
        private bool gameStarted = false;

        private Button[,] buttons = new Button[3, 3];

        private void ConfigureButtons()
        {
            //assigning buttons to a button matrix for ease of use
            buttons[0, 0] = button1;
            buttons[0, 1] = button2;
            buttons[0, 2] = button3;

            buttons[1, 0] = button4;
            buttons[1, 1] = button5;
            buttons[1, 2] = button6;

            buttons[2, 0] = button7;
            buttons[2, 1] = button8;
            buttons[2, 2] = button9;

            //assinging the same function to all of the buttons
            button1.Click += Button_Click;
            button2.Click += Button_Click;
            button3.Click += Button_Click;

            button4.Click += Button_Click;
            button5.Click += Button_Click;
            button6.Click += Button_Click;

            button7.Click += Button_Click;
            button8.Click += Button_Click;
            button9.Click += Button_Click;
        }

        public Form1()
        {
            InitializeComponent();
            
            ConfigureButtons();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private bool SquareIsFree(byte i, byte j)
        {
            return buttons[i, j].Text == "";
        }


        private void displayMatrix()
        {
            for(byte i=0; i<3;i++)
            {
                for(byte j=0;j<3;j++)
                {
                    if (buttons[i, j].Text == "")
                        Console.Write("# ");
                    Console.Write(buttons[i,j].Text + " ");
                }
                Console.WriteLine();
            }
        }
        private string checkVictory()
        {
            for (byte i = 0; i < 3; i++)
            {
                // Check rows
                if (buttons[i, 0].Text != "" &&
                    buttons[i, 0].Text == buttons[i, 1].Text &&
                    buttons[i, 1].Text == buttons[i, 2].Text)
                {
                    return buttons[i, 0].Text;
                }

                // Check columns
                if (buttons[0, i].Text != "" &&
                    buttons[0, i].Text == buttons[1, i].Text &&
                    buttons[1, i].Text == buttons[2, i].Text)
                {
                    return buttons[0, i].Text;
                }
            }

            // Check diagonals
            if (buttons[0, 0].Text != "" &&
                buttons[0, 0].Text == buttons[1, 1].Text &&
                buttons[1, 1].Text == buttons[2, 2].Text)
            {
                return buttons[0, 0].Text;
            }

            if (buttons[0, 2].Text != "" &&
                buttons[0, 2].Text == buttons[1, 1].Text &&
                buttons[1, 1].Text == buttons[2, 0].Text)
            {
                return buttons[0, 2].Text;
            }

            // Check for draw
            if (turnCount == 9)
            {
                return "D"; // Draw
            }

            return "L"; // No win yet
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (buttons[i, j].Text == "") 
                        return false;
                }
            }
            return true;
        }

        private void clearBoard()
        {
            for (byte i = 0; i < 3; i++)
                for (byte j = 0; j < 3; j++)
                {
                    buttons[i, j].Enabled = true;
                    buttons[i, j].Text = "";
                }
            for (byte i = 0; i < 3; i++)
                for (byte j = 0; j < 3; j++)
                {
                    buttons[i, j].Text = "";
                }
        }
        private int Minimax(bool isMaximizing, int alpha, int beta)
        {
            string winner = checkVictory();

            if (winner == "X") return isPlayerX ? -10 : 10; // Player X wins → bad for AI
            if (winner == "O") return isPlayerX ? 10 : -10; // AI wins → good for AI
            if (IsBoardFull()) return 0; // Draw

            int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (buttons[i, j].Text == "")
                    {
                        buttons[i, j].Text = isMaximizing ? getSymbol(!isPlayerX) : getSymbol(isPlayerX);
                        int score = Minimax(!isMaximizing, alpha, beta);
                        buttons[i, j].Text = ""; // Undo move

                        if (isMaximizing)
                        {
                            bestScore = Math.Max(bestScore, score);
                            alpha = Math.Max(alpha, score);
                        }
                        else
                        {
                            bestScore = Math.Min(bestScore, score);
                            beta = Math.Min(beta, score);
                        }

                        // Alpha-Beta Pruning
                        if (beta <= alpha) return bestScore;
                    }
                }
            }
            return bestScore;
        }


        private Tuple<byte, byte> ComputerTurn(int difficulty)
        {
            Tuple<byte, byte> move;

            switch (difficulty)
            {
                case 1: //the first level of difficulty just searches for the first available space
                    List<Tuple<byte, byte>> zeroPositions = new List<Tuple<byte, byte>>();

                    // Iterate through the matrix to 
                    for (byte i = 0; i < 3; i++)
                    {
                        for (byte j = 0; j < 3; j++)
                        {
                            if (buttons[i, j].Text == "")
                                zeroPositions.Add(new Tuple<byte, byte>(i, j));
                        }
                    }

                    // If there are no empty spaces in the game, return null, probably won't happen
                    if (zeroPositions.Count == 0)
                        return null;

                    // Randomly select a position from the zeroPositions list
                    Random rand = new Random();
                    byte randomIndex = Convert.ToByte(rand.Next(zeroPositions.Count));

                    move=zeroPositions[randomIndex];
                    return move;
                 break;

                case 2: // the second level picks a position at random
                    for (byte i=0;i<3;i++)
                    {
                        for (byte j = 0; j < 3; j++)
                        {
                            if (buttons[i, j].Text == "")
                            {
                                move = new Tuple<byte, byte>(i, j);
                                return move;
                            }
                        }
                    }
                break;
                case 3: // optimal game

                    if (turnCount==0)
                    {
                        return new Tuple<byte, byte>(0, 0);
                    }


                    int bestScore = int.MinValue;
                    Tuple<byte, byte> bestMove = null;

                    for (byte i = 0; i < 3; i++)
                    {
                        for (byte j = 0; j < 3; j++)
                        {
                            if (buttons[i, j].Text == "")
                            {
                                buttons[i, j].Text = getSymbol(!isPlayerX); ; // Simulate AI move
                                int moveScore = Minimax(false, int.MinValue, int.MaxValue);
                                buttons[i, j].Text = ""; // Undo move

                                if (moveScore > bestScore)
                                {
                                    bestScore = moveScore;
                                    bestMove = new Tuple<byte, byte>(i, j);
                                }
                            }
                        }
                    }
                    //buttons[bestMove.Item1, bestMove.Item2].Text = getSymbol(!isPlayerX); ;
                    return bestMove;
                break;
                default:
                    return new Tuple<byte, byte>(1, 1);
                break;
            }

            return null;

        }

        private String getSymbol(bool isX)
        {
            if (isX)
                return "X";
            return "O";
        }

        private void Victory()
        {
            char victoryCase = Convert.ToChar(checkVictory());
            if (victoryCase != 'L')
            {
                Form3 newForm = new Form3(victoryCase);
                newForm.Show();
                gameStarted = false;
                XorOBox.SelectedIndex = -1;
                clearBoard();
                turnCount = 0;
                isPlayerX = true;
                return;  
            }  
        }

        private void executeComputerTurn()
        {

            Tuple<byte, byte> position = ComputerTurn(difficulty);
            displayMatrix();
            Console.WriteLine(position);
            turnCount++;

            int index2 = position.Item1 * 3 + position.Item2;
            buttons[position.Item1, position.Item2].Text = getSymbol(!isPlayerX); // computer symbol
            isPlayerTurn = true;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (XorOBox.SelectedIndex == -1) // will not press a button if no symbol is selected
                return;
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                if (clickedButton.Text != "")
                    return;
                clickedButton.Text = getSymbol(isPlayerX);
                byte index = Convert.ToByte(clickedButton.Tag);

                turnCount++;

                Victory();
                isPlayerTurn = false;

                //Computer's turn:
                if(gameStarted)
                    executeComputerTurn();
                Victory();            
            }         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void difficultyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            difficulty = difficultyBox.SelectedIndex + 1;
            Console.WriteLine(difficulty);
        }

        private void XorOBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (XorOBox.SelectedIndex == -1)
                return;
            if (gameStarted) // if a symbol has been selected it can't be changed
            {
                if(isPlayerX)
                   XorOBox.SelectedIndex = 0;
                else
                    XorOBox.SelectedIndex = 1;
                return;
            }

            if (XorOBox.SelectedIndex == 0)
            {
                isPlayerX = true;
            }
            else
            {
                isPlayerX = false;
                executeComputerTurn();
                turnCount++;
            }
            gameStarted = true;
        }

        private void helpLabel_Click(object sender, EventArgs e)
        {
            HelpForm newForm = new HelpForm();
            newForm.Show();
            return;
        }
    }
}
