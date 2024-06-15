using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace XGame
{
    public partial class TicTacToe : Page
    {
        private string currentPlayer;
        private Button[,] buttons;
        private int moves;

        public TicTacToe()
        {
            InitializeComponent();
            InitializeGameGrid();
            StartNewGame();
        }

        private void InitializeGameGrid()
        {
            buttons = new Button[3, 3];

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Button button = new Button
                    {
                        FontSize = 36,
                        Background = Brushes.White
                    };
                    button.Click += Button_Click;
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    GameGrid.Children.Add(button);
                    buttons[row, col] = button;
                }
            }
        }

        private void StartNewGame()
        {
            currentPlayer = "X";
            moves = 0;
            StatusTextBlock.Text = "Player X's turn";

            foreach (Button button in buttons)
            {
                button.Content = null;
                button.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && string.IsNullOrEmpty(button.Content?.ToString()))
            {
                button.Content = currentPlayer;
                moves++;
                if (CheckForWinner())
                {
                    StatusTextBlock.Text = $"Player {currentPlayer} wins!";
                    DisableButtons();
                }
                else if (moves == 9)
                {
                    StatusTextBlock.Text = "It's a draw!";
                }
                else
                {
                    currentPlayer = currentPlayer == "X" ? "O" : "X";
                    StatusTextBlock.Text = $"Player {currentPlayer}'s turn";
                }
            }
        }

        private bool CheckForWinner()
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (buttons[row, 0].Content == buttons[row, 1].Content &&
                    buttons[row, 1].Content == buttons[row, 2].Content &&
                    !string.IsNullOrEmpty(buttons[row, 0].Content?.ToString()))
                {
                    return true;
                }
            }

            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (buttons[0, col].Content == buttons[1, col].Content &&
                    buttons[1, col].Content == buttons[2, col].Content &&
                    !string.IsNullOrEmpty(buttons[0, col].Content?.ToString()))
                {
                    return true;
                }
            }

            // Check diagonals
            if (buttons[0, 0].Content == buttons[1, 1].Content &&
                buttons[1, 1].Content == buttons[2, 2].Content &&
                !string.IsNullOrEmpty(buttons[0, 0].Content?.ToString()))
            {
                return true;
            }

            if (buttons[0, 2].Content == buttons[1, 1].Content &&
                buttons[1, 1].Content == buttons[2, 0].Content &&
                !string.IsNullOrEmpty(buttons[0, 2].Content?.ToString()))
            {
                return true;
            }

            return false;
        }

        private void DisableButtons()
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Content = null;
                button.IsEnabled = true;
            }
            StatusTextBlock.Text = string.Empty;
            moves = 0;
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("History:\n\nTic-tac-toe, or Xs and Os is a paper-and-pencil game for two players who take turns marking the spaces in a three-by-three grid with X or O.\n\n" +
                "The player who succeeds in placing three of their marks in a horizontal, vertical, or diagonal row is the winner.\n\n" +
                "It is a solved game, with a forced draw assuming best play from both players.");
        }
    }
}
