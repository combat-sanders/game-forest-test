using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
namespace game_forest_test.Views;

/// <summary>
/// Visulalize game state
/// </summary>
public class Playboard
{
    /// <summary>
    /// Unit element of grid
    /// </summary>
    private Rectangle _cell { get; set; }
    
    /// <summary>
    /// Grid to layout elements
    /// </summary>
    private Grid _grid { get; set; }
    
    public Playboard(Grid grid, Rectangle cell)
    {
        _cell = cell;
        _grid = grid;
        
        CreateEmptyPlayboard();
    }
    
    void CreateEmptyPlayboard()
    {
        for (int i = 0; i < 8; i++)
        {
            _grid.RowDefinitions.Add(new RowDefinition());
            _grid.ColumnDefinitions.Add(new ColumnDefinition());
        }
        for (int row = 0; row < 8; row++)
        {
            for (int column = 0; column < 8; column++)
            {
                Rectangle rectangle = new Rectangle();
                
                rectangle.Stroke = Brushes.Black;
                rectangle.StrokeThickness = 1;

                Grid.SetRow(rectangle, row);
                Grid.SetColumn(rectangle, column);
                
                _grid.Children.Add(rectangle);
            }
        }
    }
}