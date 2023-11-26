using System;
using System.Collections.Generic;
using System.Windows.Media;
using game_forest_test.Helpers;

namespace game_forest_test.Models;

/// <summary>
/// Contains data and logic about current Playboard state
/// </summary>
public class PlayboardModel
{
    /// <summary>
    /// Container, that contains data about cells.
    /// </summary>
    
    public Dictionary<Vector2, PlayboardElementModel> Data { get; set; }
    
    public int Size { get; private set; }

    public PlayboardModel(int size)
    {
        InitPlayboard(size);
        Size = size;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source">source position</param>
    /// <param name="target">target position</param>
    /// <returns></returns>
    public bool MoveCell(Vector2 source, Vector2 target)
    {
        if (Data[source].State == PlayboardElementModel.States.Empty) return false;
        if (Equals(source, target)) return false;

        // swap cells if color and level are not same or target cell have max level 
        if (Data[target].Color != Data[source].Color ||
            Data[target].Level != Data[source].Level ||
            Data[target].Level == PlayboardElementModel.MaxLevel ||
            Data[target].State == PlayboardElementModel.States.Empty)
        {
            (Data[source], Data[target]) = (Data[target], Data[source]);
        }

        // improve level if color and level are same
        if (Data[target].Color == Data[source].Color &&
            Data[target].Level == Data[source].Level &&
            Data[source].Level != PlayboardElementModel.MaxLevel)
        {
            // improve level on target cell
            Data[target].Level++;
            // make source cell empty
            Data[source].State = PlayboardElementModel.States.Empty;
        }
        
        return true;
    }
    
    /// <summary>
    /// Returns list of empty cells indexes
    /// </summary>
    /// <returns>indexes of empty cells</returns>
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
    /// Spawns element on random position, depending cell properties
    /// </summary>
    /// <param name="emittedCell">cell, that requests spawn</param>
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

    public PlayboardElementModel GetRandomCell()
    {
        PlayboardElementModel.Colors color = Helper.GetRandomEnumValue<PlayboardElementModel.Colors>();
        PlayboardElementModel.Levels level = Helper.GetRandomEnumValue<PlayboardElementModel.Levels>();
        return new PlayboardElementModel(color, level);
    }

    /// <summary>
    /// Creates playboard with empty cells
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