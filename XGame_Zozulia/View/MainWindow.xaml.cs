using System.Windows;
using System.Windows.Controls;
using XGame;

namespace XGame_Zozulia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void LifeGameButton_Click(Object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Life();
            HideWelcomeText();
        }
        private void TicTacToeGameButton_Click(Object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TicTacToe();
            HideWelcomeText();
        }
        private void RPSGameButton_Click(Object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Rock_Paper_Scissors();
            HideWelcomeText();
        }

        private void HideWelcomeText()
        {
            TextBlockWelcome.Visibility = Visibility.Collapsed;
        }
    }
}

