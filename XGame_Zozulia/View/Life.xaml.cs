using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace XGame
{
    public partial class Life : Page
    {
        private const int Rows = 50;
        private const int Columns = 50;
        private const int CellSize = 10;
        private bool[,] cells = new bool[Rows, Columns];
        private Rectangle[,] rectangles = new Rectangle[Rows, Columns];
        private DispatcherTimer timer;

        public Life()
        {
            InitializeComponent();
            InitializeGameGrid();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += Timer_Tick;
        }

        private void InitializeGameGrid()
        {
            GameCanvas.Width = Columns * CellSize;
            GameCanvas.Height = Rows * CellSize;

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = CellSize,
                        Height = CellSize,
                        Stroke = Brushes.Black,
                        Fill = Brushes.White
                    };
                    Canvas.SetTop(rect, row * CellSize);
                    Canvas.SetLeft(rect, col * CellSize);
                    GameCanvas.Children.Add(rect);
                    rectangles[row, col] = rect;

                    rect.MouseLeftButtonDown += (sender, args) =>
                    {
                        int r = (int)(Canvas.GetTop(rect) / CellSize);
                        int c = (int)(Canvas.GetLeft(rect) / CellSize);
                        cells[r, c] = !cells[r, c];
                        UpdateCellVisual(r, c);
                    };
                }
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            bool[,] newCells = new bool[Rows, Columns];

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    int aliveNeighbors = CountAliveNeighbors(row, col);

                    if (cells[row, col])
                    {
                        newCells[row, col] = aliveNeighbors == 2 || aliveNeighbors == 3;
                    }
                    else
                    {
                        newCells[row, col] = aliveNeighbors == 3;
                    }
                }
            }

            cells = newCells;
            UpdateGridVisual();
        }

        private int CountAliveNeighbors(int row, int col)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int r = row + i;
                    int c = col + j;

                    if (r >= 0 && r < Rows && c >= 0 && c < Columns && cells[r, c])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private void UpdateGridVisual()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    UpdateCellVisual(row, col);
                }
            }
        }

        private void UpdateCellVisual(int row, int col)
        {
            rectangles[row, col].Fill = cells[row, col] ? Brushes.Black : Brushes.White;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            cells = new bool[Rows, Columns];
            UpdateGridVisual();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Instruction:\n\n" +
                "--- Put some dots and press 'START' ---\n" +
                "\nHistory:\n\n" +
                "The Game of Life, also known simply as Life, is a cellular automaton devised by the British mathematician John Horton Conway in 1970.\n\n" +
                "It is a zero-player game, meaning that its evolution is determined by its initial state, requiring no further input.\n\n" +
                "One interacts with the Game of Life by creating an initial configuration and observing how it evolves.\n\n" +
                "It is Turing complete and can simulate a universal constructor or any other Turing machine.");
        }
    }
}
