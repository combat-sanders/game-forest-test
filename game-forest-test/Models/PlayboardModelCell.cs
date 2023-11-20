using System;
using System.Data.SqlTypes;
using System.Linq;

namespace game_forest_test.Models;
/// <summary>
/// Defines Behavior of cell
/// </summary>
public class PlayboardModelCell 
{
    public PlayboardModelCell(Colors color = Colors.None, Levels level = Levels.None)
    {
        Color = color;
        Level = level;
    }

    /// <summary>
    /// States of cell
    /// </summary>
    public enum States
    {
        Empty,
        Busy
    }
    /// <summary>
    /// Available colors
    /// </summary>
    public enum Colors
    {
        None,
        Red,
        Green,
        Blue
    }
    /// <summary>
    /// Available levels
    /// </summary>
    public enum Levels
    {
        None = 0,
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
    }
    
    public States State { get; set; }

    /// <summary>
    /// Property defines the color of the cell
    /// </summary>
    public Colors Color 
    {
        get => (State == States.Empty) ? Colors.None : Color;
        set => Color = (State == States.Empty) ? Colors.None : value;
    }
    
    /// <summary>
    /// Property defines the level of the cell
    /// </summary>
    public Levels Level
    {
        get => (State == States.Empty) ? Levels.None : Level;
        set => Level = (State == States.Empty) ? Levels.None : value;
    }

    /// <summary>
    /// Max level of the cell
    /// </summary>
    public static readonly Levels MaxLevel = Enum.GetValues(typeof(Levels)).Cast<Levels>().Max();

    /// <summary>
    /// Can element allow generation
    /// </summary>
    /// <returns>true if element can generate cells</returns>
    public bool AllowGeneration() => Level == MaxLevel;
}