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
    /// Contains appearance and user actions handlers
    /// </summary>
    private PlayboardView _playboardView;
    
    /// <summary>
    /// Container, that will be parent of playboard
    /// </summary>
    private Canvas _playboardParent;
    
    /// <summary>
    /// Container, that will be parent of orders block
    /// </summary>
    private Canvas _ordersParent;

    /// <summary>
    /// Contains all game data and methods about game logic
    /// </summary>
    private OrdersModel _ordersModel;

    /// <summary>
    /// Contains appearence and user actions handlers of orders mechanic
    /// </summary>
    private OrdersView _ordersView;
    public GameScene()
    {
        InitializeComponent();
        
        // Find static xaml container
        _playboardParent = (Canvas)FindName("PlayboardContainer");
        _ordersParent = (Canvas)FindName("OrdersContainer");
        
        // Init general actors
        _playboardModel = new PlayboardModel(8);
        _playboardView = new PlayboardView(_playboardParent, 8);
        
        _ordersModel = new OrdersModel(_playboardModel);
        _ordersView = new OrdersView();

        /// Setup orders view
        _ordersView.Width = _ordersParent.Width;
        _ordersView.Height = _ordersParent.Height;
        _ordersParent.Children.Add(_ordersView);
        
        // Startup game
        GameController.InitGame(_playboardModel, 8);
        InitPlayboardHandlers(_playboardModel, _playboardView);
        InitOrdersHandlers();
        
        // sync state between model and view
        GameController.SyncPlayboardWithModel(_playboardModel, _playboardView);
        GameController.SyncOrdersWithModel(_ordersModel, _ordersView);
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

    /// <summary>
    /// As fact, connects model and view in scene
    /// </summary>
    /// <param name="model">model layer of game</param>
    /// <param name="view"> view layer of game</param>
    private void InitPlayboardHandlers(PlayboardModel model, PlayboardView view)
    {
        foreach (var item in view.Data)
        {
            // generate element if it possible
            item.Value.MouseDoubleClick += (sender, args) =>
            {
                var key = item.Key;
                model.SpawnElement(model.Data[key]);
                GameController.SyncPlayboardWithModel(model, view);
            };
            
            // pack source element position in data object and drag it into another element
            item.Value.MouseMove += (sender, args) =>
            {
                if (args.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(item.Value, item.Key,
                        DragDropEffects.Move | DragDropEffects.Copy);
                }
            };

            // recieve a dragged data with source postion and requests to model
            item.Value.Drop += (sender, args) =>
            {
                dynamic data = args.Data.GetData(typeof(Vector2));
                Vector2 source = new Vector2(data.X, data.Y);
                model.MoveElement(source, item.Key);
                GameController.SyncPlayboardWithModel(model, view);
            };
        }
    }

    void InitOrdersHandlers()
    {
        _ordersView.Data[OrdersView.Slots.First].PreviewMouseDown += (sender, args) =>
        {
            if (args.LeftButton == MouseButtonState.Pressed)
            {
                _ordersModel.RequestOrder(OrdersModel.Slots.First);
                GameController.SyncOrdersWithModel(_ordersModel, _ordersView);
                GameController.SyncPlayboardWithModel(_playboardModel, _playboardView);
            }
        };
        
        _ordersView.Data[OrdersView.Slots.Second].PreviewMouseDown += (sender, args) =>
        {
            if (args.LeftButton == MouseButtonState.Pressed)
            {
                _ordersModel.RequestOrder(OrdersModel.Slots.Second);
                GameController.SyncOrdersWithModel(_ordersModel, _ordersView);
                GameController.SyncPlayboardWithModel(_playboardModel, _playboardView);
            }
        };
    }
}