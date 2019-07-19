using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TicTacToe
{
    public partial class MainPage : ContentPage
    {
        private Button[] buttons = new Button[9];
        //private Droid.TicTacToeMain TTT = new Droid.TicTacToeMain();

        public MainPage()
        {
            InitializeComponent();

            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;
            buttons[6] = button7;
            buttons[7] = button8;
            buttons[8] = button9;

            var restartGameButtonClick = new TapGestureRecognizer();
            restartGameButtonClick.Tapped += (s, e) =>
            {
                ResetGame(buttons);
                buttonPlayAgain.IsVisible = false;
            };
            RestartFA.GestureRecognizers.Add(restartGameButtonClick);

            var exitGameButtonClick = new TapGestureRecognizer();
            exitGameButtonClick.Tapped += (s, e) =>
            {
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            };
            ExitFA.GestureRecognizers.Add(exitGameButtonClick);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SetIfZeroOrCross((Button)sender);
            if (CheckGameStatus(buttons))
            {
                buttonPlayAgain.IsVisible = true;
            }
        }

        private void PlayAgain_Clicked(Object sender, EventArgs e)
        {
            ResetGame(buttons);
            buttonPlayAgain.IsVisible = false;
        }

        /**
         * ***************************
         * Functionality for TicTacToe
         * ***************************
         * */
        private int Turn = 1;

        readonly int[,] PossibleWaysToWin = new int[,]
        {
            {0, 1, 2},
            {3, 4, 5},
            {6, 7, 8},
            {0, 3, 6},
            {1, 4, 7},
            {2, 5, 8},
            {0, 4, 8},
            {2, 4, 6}
        };

        private bool CheckGameStatus(Button[] buttons)
        {
            bool EndOfTheGame = false;
            for (int i = 0; i < 8; i++)
            {
                int rowOne = PossibleWaysToWin[i, 0], rowTwo = PossibleWaysToWin[i, 1], rowThree = PossibleWaysToWin[i, 2];
                Button b1 = buttons[rowOne], b2 = buttons[rowTwo], b3 = buttons[rowThree];

                if (b1.Text == "" || b2.Text == "" || b3.Text == "") continue;

                if (b1.Text == b2.Text && b2.Text == b3.Text)
                {
                    b1.BackgroundColor = b2.BackgroundColor = b3.BackgroundColor = Color.FromHex("#a3a3a3");
                    MainLabel.Text = b1.Text == "X" ? "Winner: Cross" : "Winner: Zero";
                    EndOfTheGame = true; break;
                }
            }

            if (!EndOfTheGame)
            {
                bool isTie = true;
                foreach (Button b in buttons) if (b.Text == "") { isTie = false; break; }
                if (isTie)
                {
                    EndOfTheGame = true;
                    MainLabel.Text = "Winner: Tie!";

                    foreach (Button b in buttons)
                        b.BackgroundColor = Color.FromHex("#FF0000");
                }
            }
            return EndOfTheGame;
        }

        public void SetIfZeroOrCross(Button b)
        {
            if (b.Text == "")
            {
                if (Turn == 1)
                {
                    b.Text = "X";
                    Turn = 2;
                    MainLabel.Text = "Turn: Zero";
                }
                else
                {
                    b.Text = "O";
                    Turn = 1;
                    MainLabel.Text = "Turn: Cross";
                }
            }
            else DisplayAlert("What the hell?", "This slot is already taken!\nMovement is illegal!", "Roger that :(");
        }

        public void ResetGame(Button[] buttons)
        {
            Turn = 1;
            foreach (Button b in buttons)
            {
                b.Text = "";
                b.BackgroundColor = Color.FromHex("#494949");
                MainLabel.Text = "Turn: Cross";
            }
        }
    }
}
