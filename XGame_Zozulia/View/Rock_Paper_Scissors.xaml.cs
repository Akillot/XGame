using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace XGame
{
    public partial class Rock_Paper_Scissors : Page
    {
        private enum Move
        {
            Rock,
            Paper,
            Scissors
        }

        private Random random = new Random();

        public Rock_Paper_Scissors()
        {
            InitializeComponent();
        }

        private void PlayMove(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            Move userMove = (Move)Enum.Parse(typeof(Move), clickedButton.Tag.ToString());
            Move computerMove = (Move)random.Next(0, 3);

            string result;
            if (userMove == computerMove)
            {
                result = "It's a tie!";
            }
            else if ((userMove == Move.Rock && computerMove == Move.Scissors) ||
                     (userMove == Move.Paper && computerMove == Move.Rock) ||
                     (userMove == Move.Scissors && computerMove == Move.Paper))
            {
                result = "You win!";
            }
            else
            {
                result = "Computer wins!";
            }

            MessageBox.Show($"You chose {userMove}. Computer chose {computerMove}. {result}", "Result");
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("History:\n\nRock paper scissors (also known by several other names and word orders, see § Names) is an intransitive hand game, usually played between two people,\n" +
                "in which each player simultaneously forms one of three shapes with an outstretched hand.");
        }
    }
}
