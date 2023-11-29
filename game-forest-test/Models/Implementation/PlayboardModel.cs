using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;
using game_forest_test.Helpers;

namespace game_forest_test.Models;

/// <summary>
/// Contains data and logic about current Playboard state
/// </summary>
public class PlayboardModel : IPlayboardModel
{
    /// <summary>
    /// Container, that contains data about elements.
    /// </summary>
    
    public Dictionary<Vector2, PlayboardElementModel> Data { get; set; }
    
    /// <summary>
    /// Count of rows and columns in grid
    /// </summary>
    public int Size { get; private set; }

    public PlayboardModel(int size)
    {
        InitPlayboard(size);
        Size = size;
    }
    
    /// <summary>
    /// Moves element from source to target postion, depending game rules
    /// </summary>
    /// <param name="source">source position</param>
    /// <param name="target">target position</param>
    /// <returns></returns>
    public bool MoveElement(Vector2 source, Vector2 target)
    {
        if (Data[source].State == PlayboardElementModel.States.Empty) return false;
        if (Equals(source, target)) return false;

        // swap cells if color and level are not same or target element have max level 
        if (Swap(source, target))
        {
            (Data[source], Data[target]) = (Data[target], Data[source]);
        }

        // improve level if color and level are same
        if (Merge(source, target))
        {
            // improve level on target element
            Data[target].Level++;
            // make source element empty
            Data[source].State = PlayboardElementModel.States.Empty;
        }
        
        return true;
    }

    public bool Swap(Vector2 source, Vector2 target)
    {
        return Data[target].Color != Data[source].Color ||
               Data[target].Level != Data[source].Level ||
               Data[target].Level == PlayboardElementModel.MaxLevel ||
               Data[target].State == PlayboardElementModel.States.Empty;
    }

    public bool Merge(Vector2 source, Vector2 target)
    {
        return Data[target].Color == Data[source].Color &&
               Data[target].Level == Data[source].Level &&
               Data[source].Level != PlayboardElementModel.MaxLevel;
    }
    
    /// <summary>
    /// Returns list of empty element indexes
    /// </summary>
    /// <returns>indexes of empty elements</returns>
    public List<Vector2> GetEmptyCells()
    {
        var emptyCells = new List<Vector2>();

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Data[new Vector2(i, j)].State == PlayboardElementModel.States.Empty)
                {
                    emptyCells.Add(new Vector2(i, j));
                }
            }
        }
        
        return emptyCells;
    }

    /// <summary>
    /// Spawns element on random position, depending elements properties
    /// </summary>
    /// <param name="emittedCell">element, that requests spawn</param>
    public void SpawnElement(PlayboardElementModel? emittedCell = null)
    {
        // case of cell, that don't allow generation
        if (emittedCell?.State == PlayboardElementModel.States.Empty ||
            !emittedCell.AllowGeneration())
        {
            return;
        }
        
        // get list of empty cells
        var emptyCells = GetEmptyCells();
        
        // no spawn if board is full
        if (emptyCells.Count == 0)
        {
            return;
        }

        Random random = new Random();
        
        // determine spawn position
        var index = random.Next(0, emptyCells.Count - 1);
        var cellPosition = emptyCells[index];

        // determine color for cell
        var cellColor = emittedCell.Color switch
        {
            PlayboardElementModel.Colors.Blue => random.NextDouble() > 0.2f
                ? PlayboardElementModel.Colors.Green
                : PlayboardElementModel.Colors.Red,
            PlayboardElementModel.Colors.Green => random.NextDouble() > 0.2f
                ? PlayboardElementModel.Colors.Red
                : PlayboardElementModel.Colors.Blue,
            PlayboardElementModel.Colors.Red => random.NextDouble() > 0.2f
                ? PlayboardElementModel.Colors.Blue
                : PlayboardElementModel.Colors.Green,
            _ => PlayboardElementModel.Colors.None
        };
        
        // change element to target properties (spawn, in view
        Data[cellPosition].Color = cellColor;
        Data[cellPosition].Level = PlayboardElementModel.Levels.First;
    }

    public void SpawnElement(Vector2 position, PlayboardElementModel.Colors color, PlayboardElementModel.Levels level)
    {
        Data[position] = new PlayboardElementModel(color, level);
    }

    /// <summary>
    /// Returns random model element. Method have not generate empty cells
    /// </summary>
    /// <returns>element with random data</returns>
    public PlayboardElementModel GetRandomElement()
    {
        List<PlayboardElementModel.Colors> colors = 
            Enum.GetValues(typeof(PlayboardElementModel.Colors)).Cast<PlayboardElementModel.Colors>()
            .Where(x => x != PlayboardElementModel.Colors.None).ToList();
        List<PlayboardElementModel.Levels> levels = 
            Enum.GetValues(typeof(PlayboardElementModel.Levels)).Cast<PlayboardElementModel.Levels>()
                .Where(x => x != PlayboardElementModel.Levels.None).ToList();
        
        Random random = new Random();
        
        PlayboardElementModel.Colors color = colors[random.Next(0, colors.Count)];
        PlayboardElementModel.Levels level = levels[random.Next(0, levels.Count)];
        
        return new PlayboardElementModel(color, level);
    }

    /// <summary>
    /// Creates playboard with empty elements
    /// </summary>
    /// <param name="countOfRows">count of rows</param>
    /// <param name="countOfColumns">count of columns</param>
    private void InitPlayboard(int size)
    {
        Data = new Dictionary<Vector2, PlayboardElementModel>();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Data[new Vector2(i, j)] = new PlayboardElementModel();
            }
        }
    }
}