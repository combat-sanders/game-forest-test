using System;
using System.Data.SqlTypes;
using System.Linq;

namespace game_forest_test.Models;
/// <summary>
/// Defines Behavior of elements of playboard
/// </summary>
public class PlayboardElementModel 
{
    public PlayboardElementModel(Colors color = Colors.None, Levels level = Levels.None)
    {
        Color = color;
        Level = level;
        State = color == Colors.None && Level == Levels.None ? States.Empty : States.Busy;
    }

    /// <summary>
    /// States of element
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

    public States State
    {
        get => _state;
        set
        {
            if (value == States.Empty)
            {
                _color = Colors.None;
                _level = Levels.None;
            }
            _state = value;
        }
    }

    private States _state = States.Empty;

    /// <summary>
    /// Property defines the color of the element
    /// </summary>
    public Colors Color
    {
        get => _color;
        set
        {
            if (value == Colors.None) State = States.Empty;
            State = States.Busy;
            _color = value;
        }
    }

    private Colors _color = Colors.None;

    /// <summary>
    /// Property defines the level of the element
    /// </summary>
    public Levels Level
    {
        get => _level;
        set
        {
            if (value == Levels.None) State = States.Empty;
            State = States.Busy;
            _level = value;
        }
    }

    private Levels _level = Levels.None;

    /// <summary>
    /// Max level of the element
    /// </summary>
    public static readonly Levels MaxLevel = Enum.GetValues(typeof(Levels)).Cast<Levels>().Max();

    /// <summary>
    /// Can element allow generation
    /// </summary>
    /// <returns>true if element can generate elements</returns>
    public bool AllowGeneration() => Level == MaxLevel;
}