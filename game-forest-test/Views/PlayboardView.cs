using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using game_forest_test.Models;

namespace game_forest_test.Views;

/// <summary>
/// Visulalize game state
/// </summary>
public class PlayboardView
{
    private Canvas _playboardContainer;
    public int Rows { get; private set; } = 0;
    public int Columns { get; private set; } = 0;
    public PlayboardView(Canvas playboardContainer, int rows, int columns)
    {
        _playboardContainer = playboardContainer;
        Rows = rows;
        Columns = columns;
        InitEmptyBoard();
    }

    private void InitEmptyBoard()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = Brushes.Transparent;
                rectangle.Stroke = Brushes.Black;
                rectangle.StrokeThickness = 1;
                rectangle.Width = _playboardContainer.Width / Columns;
                rectangle.Height = _playboardContainer.Height / Rows;
                _playboardContainer.Children.Add(rectangle);
                
                Canvas.SetTop(rectangle, i * rectangle.Height);
                Canvas.SetLeft(rectangle, j * rectangle.Width);
            }
        }
    }
}