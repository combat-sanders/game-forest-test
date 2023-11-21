using System;
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
    /// <summary>
    /// Contains playbord data and game methods
    /// </summary>
    private PlayboardModel _playboardModel;
    
    /// <summary>
    /// Contains appearence and user actions handlers
    /// </summary>
    private PlayboardView _playboardView;
    
    /// <summary>
    /// Container, that will be parent of playboard
    /// </summary>
    private Canvas _playboardParent;
    public GameScene()
    {
        InitializeComponent();
        
        // Find static xaml container
        _playboardParent = (Canvas)FindName("PlayboardContainer");
        
        // Init general actors
        _playboardModel = new PlayboardModel(8, 8);
        _playboardView = new PlayboardView(_playboardParent, 8, 8);
        Console.WriteLine(_playboardParent.Children.Count);
        
        // sync state between model and view
        PlayboardController.Sync(_playboardModel, _playboardView);
    }

    /// <summary>
    /// Exit button handler
    /// </summary>
    /// <param name="sender"> sender</param>
    /// <param name="e">event args</param>
    private void OnExitButtonPressed(object sender, RoutedEventArgs e)
    {
        SceneManager.LoadScene(new MainMenuScene());
    }
}