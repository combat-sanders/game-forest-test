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
        State = color == Colors.None && Level == Levels.None ? States.Empty : States.Busy;
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

    public States State { get; set; } = States.Empty;

    /// <summary>
    /// Property defines the color of the cell
    /// </summary>
    public Colors Color
    {
        get;
        set;
    }
    
    /// <summary>
    /// Property defines the level of the cell
    /// </summary>
    public Levels Level
    {
        get;
        set;
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