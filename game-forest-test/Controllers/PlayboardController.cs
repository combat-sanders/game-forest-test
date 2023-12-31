﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using game_forest_test.Helpers;
using game_forest_test.Models;
using game_forest_test.Models.Interfaces;

namespace game_forest_test.Views;

/// <summary>
/// Contains methods to provide a correct gameplay
/// </summary>
public class PlayboardController
{
    /// <summary>
    /// Bridge betweem model and view side color representations
    /// </summary>
    private readonly Dictionary<PlayboardElementModel.Colors, SolidColorBrush> _colorAdapter = 
        new()
        { 
            { PlayboardElementModel.Colors.None, Brushes.White},
            { PlayboardElementModel.Colors.Red, Brushes.Red},
            { PlayboardElementModel.Colors.Green, Brushes.Green},
            { PlayboardElementModel.Colors.Blue, Brushes.Blue},
        };
    
    /// <summary>
    /// Sync model and view size playboard. Need to call after all view-size methods
    /// </summary>
    /// <param name="model"></param>
    /// <param name="view"></param>
    public void SyncPlayboardWithModel(IPlayboardModel model, PlayboardView view)
    {
        if (model.Size != view.Size) return;

        for (int i = 0; i < model.Size; i++)
        {
            for (int j = 0; j < model.Size; j++)
            {
                view.Data[new Vector2(i, j)].Color = _colorAdapter[model.Data[new Vector2(i, j)].Color];
                view.Data[new Vector2(i, j)].Level = Convert.ToInt32(model.Data[new Vector2(i, j)].Level);
                Canvas.SetZIndex(view.Data[new Vector2(i, j)], 0);
            }
        }
    }

    public void SyncOrdersWithModel(IOrdersModel model, OrdersView view)
    {
        view.Data[OrdersView.Slots.First].Background = _colorAdapter[model.Data[OrdersModel.Slots.First].Color];
        view.Data[OrdersView.Slots.First].Text = Convert.ToInt32(model.Data[OrdersModel.Slots.First].Level).ToString();
        
        view.Data[OrdersView.Slots.Second].Background = _colorAdapter[model.Data[OrdersModel.Slots.Second].Color];
        view.Data[OrdersView.Slots.Second].Text = Convert.ToInt32(model.Data[OrdersModel.Slots.Second].Level).ToString();
    }
    
    /// <summary>
    /// Makes initial state of game on model-side.
    /// Fills playboard of blue first level elements in random position
    /// </summary>
    /// <param name="model">data storage</param>
    /// <param name="countOfCells">count of cells to generate</param>
    public void InitGame(IPlayboardModel model, int countOfCells)
    {
        var emptyElementsKeys = model.GetEmptyCells();
        Random random = new Random();

        while (countOfCells > 0)
        {
            int index = random.Next(0, emptyElementsKeys.Count - 1);
            model.SpawnElement(emptyElementsKeys[index],
                PlayboardElementModel.Colors.Blue,
                PlayboardElementModel.Levels.First);
            emptyElementsKeys.Remove(emptyElementsKeys[index]);
            countOfCells--;
        }
    }

    public void SyncStatisticsWithModel(IStatisticsModel model, StatisticsView view)
    {
        view.Orders = model.OrdersCount;
        view.Points = model.PointsCount;
    }

    public int GetPointsByLevel(PlayboardElementModel.Levels level)
    {
        switch (level)
        {
            case PlayboardElementModel.Levels.First:
                return 1;
            case PlayboardElementModel.Levels.Second:
                return 3;
            case PlayboardElementModel.Levels.Third:
                return 9;
            case PlayboardElementModel.Levels.Fourth:
                return 27;
            default: return 0;
        }
    }
}