using System;
using game_forest_test.Helpers;

namespace game_forest_test.Models;

/// <summary>
/// Contains data and logic about current Grid state
/// </summary>
public class GameGrid
{
    /// <summary>
    /// Container, that contains data about cells.
    /// If container's element is null - empty cell
    /// </summary>
    private GameGridCell?[][] _data { get; }
    
    public GameGrid()
    {
        _data = new GameGridCell?[8][];
        for (int i = 0; i < 8; i++)
        {
            _data[i] = new GameGridCell?[8];
        }
    }

    public GameGridCell GenerateElement()
    {
        GameGridCell.Color color;
        GameGridCell.Level level; 
        
        color = Helper.GetRandomEnumValue<GameGridCell.Color>();
        level = Helper.GetRandomEnumValue<GameGridCell.Level>();
        
        return new GameGridCell(color, level);
    }

    /// <summary>
    /// Generate element for start stage of game
    /// </summary>
    /// <param name="elementsToGenerate"></param>
    public void StartupGrid(int elementsToGenerate)
    {
        Random random = new Random();

        for (int i = 0; i < elementsToGenerate; i++)
        {
            int row = random.Next(0, _data.Length - 1);
            int column = random.Next(0, _data[i].Length - 1);

            if (_data[row][column] == null)
            {
                GameGridCell.Color color = Helper.GetRandomEnumValue<GameGridCell.Color>();
                GameGridCell.Level level = Helper.GetRandomEnumValue<GameGridCell.Level>();
                
                GameGridCell cell = new GameGridCell(color, level);

                _data[row][column] = cell;
            }
            else
            {
                i--;
            }
        }
    }
}