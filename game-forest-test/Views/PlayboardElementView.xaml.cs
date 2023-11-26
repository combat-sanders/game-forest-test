
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace game_forest_test.Views;

/// <summary>
/// Representation of playboard element
/// </summary>
public partial class PlayboardElementView : UserControl
{
    /// <summary>
    /// Fill color of element
    /// </summary>
    public Brush Color
    {
        get => Color;
        set => _background.Fill = value;
    }

    /// <summary>
    /// Level of the element
    /// </summary>
    public int Level
    {
        get => Level;
        set => _text.Text = value != 0 ? value.ToString() : "";
    }

    /// <summary>
    /// Container, that provides color of element
    /// </summary>
    private Rectangle _background;
    
    /// <summary>
    /// Provides level appearance
    /// </summary>
    private TextBlock _text;
    
    public PlayboardElementView(int level = 0, Brush color = null)
    {
        InitializeComponent();
        _background = (Rectangle)FindName("Background");
        _text = (TextBlock)FindName("Text");
        Level = level;
        Color = color;
    }
}