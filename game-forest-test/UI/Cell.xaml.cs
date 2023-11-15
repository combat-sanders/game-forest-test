using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;

namespace game_forest_test.UI;

public partial class Cell : UserControl
{
    private TextBox? _appearence;
    public Cell()
    {
        InitializeComponent();

        _appearence = (TextBox)FindName("CellAppearence");
    }

    public void SetColor(Brush color)
    {
        if (_appearence == null)
        {
            return;
        }
        _appearence.Background = color;
    }

    public void SetLevel(int level)
    {
        if (_appearence == null)
        {
            return;
        }
        _appearence.Text = level.ToString();
    }
}