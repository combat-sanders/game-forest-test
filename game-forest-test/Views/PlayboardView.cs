using System.Collections.Generic;
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
    public Dictionary<Vector2, Rectangle> Anchors { get; private set; }
    
    public Dictionary<Vector2, PlayboardElementView> Data { get; set; }
    
    public int Size { get; private set; } = 0;
    public PlayboardView(Canvas playboardContainer, int size)
    {
        _playboardContainer = playboardContainer;
        Size = size;
        Anchors = new Dictionary<Vector2, Rectangle>();
        Data = new Dictionary<Vector2, PlayboardElementView>();
        InitAnchors();
        InitCells();
    }

    private void InitAnchors()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Fill = Brushes.Transparent;
                rectangle.Stroke = Brushes.Black;
                rectangle.StrokeThickness = 1;
                rectangle.Width = _playboardContainer.Width / Size;
                rectangle.Height = _playboardContainer.Height / Size;
                _playboardContainer.Children.Add(rectangle);
                Anchors[new Vector2(i, j)] = rectangle;
                Canvas.SetTop(rectangle, i * rectangle.Height);
                Canvas.SetLeft(rectangle, j * rectangle.Width);
            }
        }
    }

    private void InitCells()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Vector2 position = new Vector2(i, j);
                Data[position] = new PlayboardElementView();
                Data[position].Width = _playboardContainer.Width / Size;
                Data[position].Height = _playboardContainer.Height / Size;
                _playboardContainer.Children.Add(Data[position]);
                Canvas.SetTop(Data[position], i * Data[position].Height);
                Canvas.SetLeft(Data[position], j * Data[position].Width);
            }
        }
    }
}