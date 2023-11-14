using System.Windows;
using System.Windows.Controls;
using game_forest_test.Views;

namespace game_forest_test.Scenes;

/// <summary>
/// Scene with play field
/// </summary>
public partial class PlayField : Page
{
    public PlayField()
    {
        InitializeComponent();
    }

    private void OnExitButtonPressed(object sender, RoutedEventArgs e)
    {
        SceneManager.LoadScene(new MainMenu());
    }
}