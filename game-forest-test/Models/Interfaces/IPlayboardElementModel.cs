namespace game_forest_test.Models;

public interface IPlayboardElementModel
{
    PlayboardElementModel.States State { get; set; }

    /// <summary>
    /// Property defines the color of the element
    /// </summary>
    PlayboardElementModel.Colors Color { get; set; }

    /// <summary>
    /// Property defines the level of the element
    /// </summary>
    PlayboardElementModel.Levels Level { get; set; }

    /// <summary>
    /// Can element allow generation
    /// </summary>
    /// <returns>true if element can generate elements</returns>
    bool AllowGeneration();
}