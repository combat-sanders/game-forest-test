using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using game_forest_test.Models;
using game_forest_test.Views;
using System.Windows.Media;
using System.Windows.Media.Animation;
using game_forest_test.Models.Interfaces;

namespace game_forest_test.Scenes;

/// <summary>
/// Scene with play field
/// </summary>
public partial class GameScene : Page
{
    /// <summary>
    /// Count of orders, when game ends
    /// </summary>
    private const int _maxOrdersCount = 10;
    
    /// <summary>
    /// Count of rows and columns of playboard
    /// </summary>
    private const int _playboardSize = 8;
    
    /// <summary>
    /// Contains playbord data and game methods
    /// </summary>
    private IPlayboardModel _playboardModel;
    
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
    /// Container, that will be parent of statistics block
    /// </summary>
    private Canvas _statisticsParent;

    /// <summary>
    /// Contains all game data and methods about game logic
    /// </summary>
    private IOrdersModel _ordersModel;

    /// <summary>
    /// Contains appearence and user actions handlers of orders mechanic
    /// </summary>
    private OrdersView _ordersView;
    
    /// <summary>
    /// Contains all game staticstics
    /// </summary>
    private IStatisticsModel _statisticsModel;

    /// <summary>
    /// Visualize data from statistics model
    /// </summary>
    private StatisticsView _statisticsView;
    
    /// <summary>
    /// Allow communication between view and model
    /// </summary>
    private PlayboardController _playboardController;

    /// <summary>
    /// Contains methods for animations on playboard elements
    /// </summary>
    private PlayboardAnimator _playboardAnimator;
    public GameScene()
    {
        InitializeComponent();
        
        // Find static xaml container
        _playboardParent = (Canvas)FindName("PlayboardContainer");
        _ordersParent = (Canvas)FindName("OrdersContainer");
        _statisticsParent = (Canvas)FindName("StatisticsContainer");
        
        // Init general actors
        _playboardModel = new PlayboardModel(_playboardSize);
        _playboardView = new PlayboardView(_playboardParent, _playboardSize);
        
        _ordersModel = new OrdersModel(_playboardModel);
        _ordersModel.MaxCount = _maxOrdersCount;
        _ordersView = new OrdersView();

        _statisticsModel = new StatisticsModel();
        _statisticsView = new StatisticsView();

        _playboardController = new PlayboardController();

        _playboardAnimator = new PlayboardAnimator();
        
        /// Setup orders view
        _ordersView.Width = _ordersParent.Width;
        _ordersView.Height = _ordersParent.Height;
        _ordersParent.Children.Add(_ordersView);
        
        // Setup statistics view
        _statisticsView.Width = _statisticsParent.Width;
        _statisticsView.Height = _statisticsParent.Height;
        _statisticsParent.Children.Add(_statisticsView);
        
        // Startup game
        _playboardController.InitGame(_playboardModel, 8);
        InitPlayboardHandlers(_playboardModel, _playboardView);
        InitOrdersHandlers();
        
        // sync state between model and view
        _playboardController.SyncPlayboardWithModel(_playboardModel, _playboardView);
        _playboardController.SyncOrdersWithModel(_ordersModel, _ordersView);
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
    private void InitPlayboardHandlers(IPlayboardModel model, PlayboardView view)
    {
        foreach (var item in view.Data)
        {
            // generate element if it possible
            item.Value.MouseDoubleClick += (sender, args) =>
            {
                var source = item.Key;
                var target = model.SpawnElement(model.Data[source]);
                
                _playboardController.SyncPlayboardWithModel(model, view);

                if (model.Data[source].AllowGeneration())
                {
                    _playboardAnimator.GenerationAnimation(source, target, _playboardView, 200);
                }
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

                if (_playboardModel.CanSwapElements(source, item.Key))
                {
                    _playboardAnimator.SwapAnimation(source, item.Key, _playboardView, 100);
                }
                model.MoveElement(source, item.Key);
                _playboardController.SyncPlayboardWithModel(_playboardModel, _playboardView);
            };
        }
    }

    void InitOrdersHandlers()
    {
        // handle click on first slot
        _ordersView.Data[OrdersView.Slots.First].PreviewMouseDown += (sender, args) =>
        {
            if (args.LeftButton == MouseButtonState.Pressed)
            {
                _ordersModel.RequestOrder(OrdersModel.Slots.First);
                _playboardController.SyncOrdersWithModel(_ordersModel, _ordersView);
                _playboardController.SyncPlayboardWithModel(_playboardModel, _playboardView);
            }
        };
        
        //handle click on second slot
        _ordersView.Data[OrdersView.Slots.Second].PreviewMouseDown += (sender, args) =>
        {
            if (args.LeftButton == MouseButtonState.Pressed)
            {
                _ordersModel.RequestOrder(OrdersModel.Slots.Second);
                _playboardController.SyncOrdersWithModel(_ordersModel, _ordersView);
                _playboardController.SyncPlayboardWithModel(_playboardModel, _playboardView);
            }
        };

        // setup statistics with orders data
        _ordersModel.OnOrderCompleted += (slot) =>
        {
            _statisticsModel.OrdersCount = _ordersModel.Count;
            _statisticsModel.PointsCount += _playboardController.GetPointsByLevel(_ordersModel.Data[slot].Level);
            _playboardController.SyncStatisticsWithModel(_statisticsModel, _statisticsView);
        };

        // end game if orders equal max orders
        _ordersModel.OnMaxCountAchieved += () =>
        {
            string messageBoxText = $"You result is: {_statisticsModel.PointsCount}";
            string caption = "End of game";
            
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                SceneManager.LoadScene(new MainMenuScene());
            }
        };
    }
}