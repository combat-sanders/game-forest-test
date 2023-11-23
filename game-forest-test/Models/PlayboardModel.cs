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
    public PlayboardModelCell[,] Data { get; set; }
    
    public int Rows { get; private set; }
    public int Columns { get; private set; }

    public PlayboardModel(int rows, int columns)
    {
        InitPlayboard(rows, columns);
        Rows = rows;
        Columns = columns;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sourceX">first coordinate of moving cell</param>
    /// <param name="sourceY">second coordinate of moving cell</param>
    /// <param name="targetX">first coordinate of target cell</param>
    /// <param name="targetY">first coordinate of target cell</param>
    /// <returns></returns>
    public bool MoveCell(int sourceX, int sourceY, int targetX, int targetY)
    {
        if (Helper.InRange(sourceX, 0, Data.GetLength(0)) ||
            Helper.InRange(sourceY, 0, Data.GetLength(1)) ||
            Helper.InRange(targetX, 0, Data.GetLength(0)) ||
            Helper.InRange(targetY, 0, Data.GetLength(1)))
        {
            return false;
        }
        // if target cell is empty
        if (Data[targetX, targetY].State == PlayboardModelCell.States.Empty)
        {
            // just move cell
            Data[targetX, targetY] = Data[sourceX, sourceY];
            // and make source cell empty
            Data[sourceX, sourceY].State = PlayboardModelCell.States.Empty;
        }

        // swap cells if color and level are not same or target cell have max level 
        if (Data[targetX, targetY]?.Color != Data[sourceX, sourceY].Color ||
            Data[targetX, targetY]?.Level != Data[sourceX, sourceY].Level ||
            Data[targetX, targetY]?.Level == PlayboardModelCell.MaxLevel)
        {
            (Data[sourceX, sourceY], Data[targetX, targetY]) = (Data[targetX, targetY], Data[sourceX, sourceY]);
        }

        // improve level if color and level are same
        if (Data[targetX, targetY]?.Color == Data[sourceX, sourceY].Color &&
            Data[targetX, targetY]?.Level == Data[sourceX, sourceY].Level &&
            Data[sourceX, sourceY].Level != PlayboardModelCell.MaxLevel)
        {
            // improve level on target cell
            Data[targetX, targetY].Level++;
            // make source cell empty
            Data[sourceX, sourceY].State = PlayboardModelCell.States.Empty;
        }
        
        return true;
    }
    
    /// <summary>
    /// Returns list of empty cells indexes
    /// </summary>
    /// <returns>indexes of empty cells</returns>
    public List<Tuple<int, int>> GetEmptyCells()
    {
        var emptyCells = new List<Tuple<int, int>>();

        for (int i = 0; i < Data.GetLength(0); i++)
        {
            for (int j = 0; j < Data.GetLength(1); j++)
            {
                if (Data[i, j].State == PlayboardModelCell.States.Empty)
                {
                    emptyCells.Add(new Tuple<int, int>(i, j));
                }
            }
        }
        
        return emptyCells;
    }

    /// <summary>
    /// Spawns element on random position, depending cell properties
    /// </summary>
    /// <param name="emittedCell">cell, that requests spawn</param>
    public void SpawnElement(PlayboardModelCell? emittedCell = null)
    {
        // case of cell, that don't allow generation
        if (emittedCell?.State == PlayboardModelCell.States.Empty &&
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
            PlayboardModelCell.Colors.Blue => random.NextDouble() > 0.2f
                ? PlayboardModelCell.Colors.Green
                : PlayboardModelCell.Colors.Red,
            PlayboardModelCell.Colors.Green => random.NextDouble() > 0.2f
                ? PlayboardModelCell.Colors.Red
                : PlayboardModelCell.Colors.Blue,
            PlayboardModelCell.Colors.Red => random.NextDouble() > 0.2f
                ? PlayboardModelCell.Colors.Blue
                : PlayboardModelCell.Colors.Green,
            _ => PlayboardModelCell.Colors.None
        };
        
        // change element to target properties (spawn, in view
        Data[cellPosition.Item1, cellPosition.Item2].Color = cellColor;
        Data[cellPosition.Item1, cellPosition.Item2].Level = PlayboardModelCell.Levels.First;
    }

    public void SpawnElement(int xIndex, int yIndex, PlayboardModelCell.Colors color, PlayboardModelCell.Levels level)
    {
        Data[xIndex, yIndex] = new PlayboardModelCell(color, level);
    }

    /// <summary>
    /// Creates playboard with empty cells
    /// </summary>
    /// <param name="countOfRows">count of rows</param>
    /// <param name="countOfColumns">count of columns</param>
    private void InitPlayboard(int countOfRows, int countOfColumns)
    {
        Data = new PlayboardModelCell[countOfRows, countOfColumns];

        for (int i = 0; i < countOfRows; i++)
        {
            for (int j = 0; j < countOfColumns; j++)
            {
                Data[i, j] = new PlayboardModelCell();
            }
        }
    }
}