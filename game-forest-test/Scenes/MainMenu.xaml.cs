using System.Windows;
using System.Windows.Controls;
using game_forest_test.Views;
namespace game_forest_test.Scenes;

/// <summary>
/// Scene with Main Menu
/// </summary>
public partial class MainMenu : Page
{
    public MainMenu()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Play button handler
    /// </summary>
    /// <param name="sender">event sender</param>
    /// <param name="e">routed event args</param>
    private void OnPlayButtonPressed(object sender, RoutedEventArgs e)
    {
        SceneManager.LoadScene(new PlayField());
    }
}