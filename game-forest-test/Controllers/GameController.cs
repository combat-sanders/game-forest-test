using System;
using System.Collections.Generic;
using System.Windows.Media;
using game_forest_test.Helpers;
using game_forest_test.Models;

namespace game_forest_test.Views;

/// <summary>
/// Contains methods to provide a correct gameplay
/// </summary>
public static class GameController
{
    /// <summary>
    /// Bridge betweem model and view side color representations
    /// </summary>
    private static readonly Dictionary<PlayboardElementModel.Colors, SolidColorBrush> _colorAdapter = 
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
    public static void SyncPlayboardWithModel(PlayboardModel? model, PlayboardView? view)
    {
        if (model == null || view == null) return;
        if (model.Size != view.Size) return;

        for (int i = 0; i < model.Size; i++)
        {
            for (int j = 0; j < model.Size; j++)
            {
                view.Data[new Vector2(i, j)].Color = _colorAdapter[model.Data[new Vector2(i, j)].Color];
                view.Data[new Vector2(i, j)].Level = Convert.ToInt32(model.Data[new Vector2(i, j)].Level);
            }
        }
    }
    
    /// <summary>
    /// Makes initial state of game on model-side.
    /// Fills playboard of blue first level elements in random position
    /// </summary>
    /// <param name="model">data storage</param>
    /// <param name="countOfCells">count of cells to generate</param>
    public static void InitGame(PlayboardModel model, int countOfCells)
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
}