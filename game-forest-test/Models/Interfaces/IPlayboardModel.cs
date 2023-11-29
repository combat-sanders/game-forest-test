using System.Collections.Generic;

namespace game_forest_test.Models;

public interface IPlayboardModel
{
    /// <summary>
    /// Container, that contains data about elements.
    /// </summary>

    Dictionary<Vector2, PlayboardElementModel> Data { get; set; }

    /// <summary>
    /// Count of rows and columns in grid
    /// </summary>
    int Size { get; }

    /// <summary>
    /// Moves element from source to target postion, depending game rules
    /// </summary>
    /// <param name="source">source position</param>
    /// <param name="target">target position</param>
    /// <returns></returns>
    bool MoveElement(Vector2 source, Vector2 target);
    
    /// <summary>
    /// Returns true if rules allow swap cells
    /// </summary>
    /// <param name="source">source position of element</param>
    /// <param name="target"> target position of element</param>
    /// <returns></returns>
    bool CanSwapElements(Vector2 source, Vector2 target);

    /// <summary>
    /// Returns list of empty element indexes
    /// </summary>
    /// <returns>indexes of empty elements</returns>
    List<Vector2> GetEmptyCells();

    /// <summary>
    /// Spawns element on random position, depending elements properties
    /// </summary>
    /// <param name="emittedCell">element, that requests spawn</param>
    Vector2 SpawnElement(PlayboardElementModel? emittedCell = null);

    void SpawnElement(Vector2 position, PlayboardElementModel.Colors color, PlayboardElementModel.Levels level);

    /// <summary>
    /// Returns random model element. Method have not generate empty cells
    /// </summary>
    /// <returns>element with random data</returns>
    PlayboardElementModel GetRandomElement();
}