using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using game_forest_test.Models;
using game_forest_test.Views;
using System.Windows.Media;

namespace game_forest_test.Scenes;

/// <summary>
/// Scene with play field
/// </summary>
public partial class GameScene : Page
{
    private PlayboardModel _playboardModel;
    private Grid? _grid;
    public GameScene()
    {
        InitializeComponent();

        _playboardModel = new PlayboardModel();
        
        _grid = (Grid)FindName("Grid");
        
        Rectangle cell = new Rectangle();
        
        cell.Stroke = Brushes.Black;
        cell.StrokeThickness = 1;
    }

    private void OnExitButtonPressed(object sender, RoutedEventArgs e)
    {
        SceneManager.LoadScene(new MainMenuScene());
    }
}