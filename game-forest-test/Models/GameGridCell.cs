using System;
using System.Data.SqlTypes;

namespace game_forest_test.Models;
/// <summary>
/// Defines Behavior of cell
/// </summary>
public class GameGridCell 
{
    /// <summary>
    /// Available colors
    /// </summary>
    public enum Color
    {
        Red,
        Green,
        Blue
    }
    /// <summary>
    /// Available levels
    /// </summary>
    public enum Level
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
    }

    /// <summary>
    /// Property defines the color of the cell
    /// </summary>
    private Color _color { get; set; }
    
    /// <summary>
    /// Property defines the level of the cell
    /// </summary>
    private Level _level { get; set; }

    /// <summary>
    /// Can element allow generation
    /// </summary>
    /// <returns>true if element can generate cells</returns>
    public bool AllowGeneration() => _level == Level.Fourth;
    public GameGridCell(Color color, Level level)
    {
        _color = color;
        _level = level;
    }
}