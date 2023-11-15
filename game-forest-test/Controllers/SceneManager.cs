using System.Windows.Controls;

namespace game_forest_test.Views;

/// <summary>
/// Global singleton, that contains scenes behavior
/// </summary>
public class SceneManager
{
    /// <summary>
    /// Element, that contains a current game scene
    /// </summary>
    private static Frame _mainFrame;
    
    public SceneManager(Frame mainFrame)
    {
        _mainFrame = mainFrame;
    }
    
    /// <summary>
    /// Load game scene by name
    /// </summary>
    /// <param name="scene">The Page element, that contains scene</param>
    /// <returns>true if navigation is not canceled; otherwise, false</returns>
    public static bool LoadScene(Page scene) => _mainFrame.Navigate(scene);
}