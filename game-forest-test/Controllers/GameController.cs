using System;
using System.Collections.Generic;
using System.Windows.Media;
using game_forest_test.Helpers;
using game_forest_test.Models;

namespace game_forest_test.Views;

public static class GameController
{
    private static readonly Dictionary<PlayboardElementModel.Colors, SolidColorBrush> _colorAdapter = 
        new()
        { 
            { PlayboardElementModel.Colors.None, Brushes.White},
            { PlayboardElementModel.Colors.Red, Brushes.Red},
            { PlayboardElementModel.Colors.Green, Brushes.Green},
            { PlayboardElementModel.Colors.Blue, Brushes.Blue},
        };
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
    public static void InitGame(PlayboardModel model, int countOfCells)
    {
        var emptyCells = model.GetEmptyCells();
        Random random = new Random();

        while (countOfCells > 0)
        {
            int index = random.Next(0, emptyCells.Count - 1);
            model.SpawnElement(emptyCells[index],
                PlayboardElementModel.Colors.Blue,
                PlayboardElementModel.Levels.First);
            emptyCells.Remove(emptyCells[index]);
            countOfCells--;
        }
    }
}