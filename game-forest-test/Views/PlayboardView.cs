using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using game_forest_test.Models;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace game_forest_test.Views;

/// <summary>
/// Visulalize game state
/// </summary>
public class PlayboardView
{
    private Canvas _playboardContainer;
    public Rectangle[,] Anchors { get; private set; }
    
    public PlayboardCellView[,] Cells { get; set; }
    public int Rows { get; private set; } = 0;
    public int Columns { get; private set; } = 0;
    public PlayboardView(Canvas playboardContainer, int rows, int columns)
    {
        _playboardContainer = playboardContainer;
        Rows = rows;
        Columns = columns;
        Anchors = new Rectangle[Rows, Columns];
        Cells = new PlayboardCellView[Rows, Columns];
        InitAnchors();
        InitCells();
    }

    private void InitAnchors()
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
                Anchors[i, j] = rectangle;
                Canvas.SetTop(rectangle, i * rectangle.Height);
                Canvas.SetLeft(rectangle, j * rectangle.Width);
            }
        }
    }

    private void InitCells()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Cells[i, j] = new PlayboardCellView();
                Cells[i, j].Width = _playboardContainer.Width / Columns;
                Cells[i, j].Height = _playboardContainer.Height / Rows;
                _playboardContainer.Children.Add(Cells[i, j]);
                Canvas.SetTop(Cells[i, j], i * Cells[i, j].Height);
                Canvas.SetLeft(Cells[i, j], j * Cells[i, j].Width);
            }
        }
    }
}