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
public partial class Game : Page
{
    private GameGrid _gameGrid;
    private Grid? _grid;
    private Playboard _playboard;
    public Game()
    {
        InitializeComponent();

        _gameGrid = new GameGrid();
        _gameGrid.StartupGrid(8);
        _grid = (Grid)FindName("Grid");
        
        Rectangle cell = new Rectangle();
        cell.Stroke = Brushes.Black;
        cell.StrokeThickness = 1;
        
        _playboard = new Playboard(_grid, cell);
    }

    private void OnExitButtonPressed(object sender, RoutedEventArgs e)
    {
        SceneManager.LoadScene(new MainMenu());
    }
}