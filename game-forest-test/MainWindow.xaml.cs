using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using game_forest_test.Scenes;
using game_forest_test.Views;

namespace game_forest_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static SceneManager _sceneManager;
        public MainWindow()
        {
            InitializeComponent();
            
            Frame mainFrame = (Frame)FindName("MainFrame");
            
            _sceneManager = new SceneManager(mainFrame);
            
            SceneManager.LoadScene(new MainMenu());
        }
    }
}