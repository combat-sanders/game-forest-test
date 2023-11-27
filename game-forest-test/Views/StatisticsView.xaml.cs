using System.Net.Mime;
using System.Windows.Controls;

namespace game_forest_test.Views;

public partial class StatisticsView : UserControl
{
    /// <summary>
    /// Pointer to xaml element, that displays points
    /// </summary>
    private TextBlock _pointsTextBox;
    
    /// <summary>
    /// Pointer to xaml element, that displays orders
    /// </summary>
    private TextBlock _ordersTextBox;

    /// <summary>
    /// Count of points
    /// </summary>
    public int Points
    {
        get => _pointsCount;
        set
        {
            _pointsCount = value;
            _pointsTextBox.Text = _pointsCount.ToString();
        }
    }
    
    private int _pointsCount = 0;

    /// <summary>
    /// Count of orders
    /// </summary>
    public int Orders
    {
        get => _ordersCount;
        set
        {
            _ordersCount = value;
            _ordersTextBox.Text = _ordersCount.ToString();
        }
    }
    private int _ordersCount = 0;
    
    public StatisticsView()
    {
        InitializeComponent();

        _pointsTextBox = (TextBlock)FindName("PointsText");
        _ordersTextBox = (TextBlock)FindName("OrdersText");
        Points = _pointsCount;
        Orders = _ordersCount;
    }
}