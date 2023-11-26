using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        _playboardModel = new PlayboardModel(8);
        _playboardView = new PlayboardView(_playboardParent, 8);
        
        // Startup game
        GameController.InitGame(_playboardModel, 8);
        InitHandlers(_playboardModel, _playboardView);
        
        // sync state between model and view
        GameController.SyncPlayboardWithModel(_playboardModel, _playboardView);
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

    private void InitHandlers(PlayboardModel model, PlayboardView view)
    {
        foreach (var item in view.Data)
        {
            item.Value.MouseDoubleClick += (sender, args) =>
            {
                var key = item.Key;
                model.SpawnElement(model.Data[key]);
                GameController.SyncPlayboardWithModel(model, view);
            };

            item.Value.MouseMove += (sender, args) =>
            {
                if (args.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(item.Value, item.Key,
                        DragDropEffects.Move | DragDropEffects.Copy);
                }
            };

            item.Value.Drop += (sender, args) =>
            {
                dynamic data = args.Data.GetData(typeof(Vector2));
                Vector2 source = new Vector2(data.X, data.Y);
                model.MoveCell(source, item.Key);
                GameController.SyncPlayboardWithModel(model, view);
            };
        }
    }
}