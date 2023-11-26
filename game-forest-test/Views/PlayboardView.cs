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
    /// <summary>
    /// Parent container of all elements. Also element anchors inside it
    /// </summary>
    private Canvas _playboardContainer;
    
    /// <summary>
    /// Storage of elements representation
    /// </summary>
    public Dictionary<Vector2, PlayboardElementView> Data { get; set; }
    
    /// <summary>
    /// Count of rows and columns in grid
    /// </summary>
    public int Size { get; private set; } = 0;
    public PlayboardView(Canvas playboardContainer, int size)
    {
        _playboardContainer = playboardContainer;
        Size = size;
        Data = new Dictionary<Vector2, PlayboardElementView>();
        InitElements();
    }

    /// <summary>
    /// Initialise grid of empty elements
    /// </summary>
    private void InitElements()
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