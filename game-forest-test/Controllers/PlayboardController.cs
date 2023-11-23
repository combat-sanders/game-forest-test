using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using game_forest_test.Models;

namespace game_forest_test.Views;

public static class PlayboardController
{
    private static readonly Dictionary<PlayboardModelCell.Colors, SolidColorBrush> _colorAdapter = 
        new Dictionary<PlayboardModelCell.Colors, SolidColorBrush>
        { 
            { PlayboardModelCell.Colors.None, Brushes.White},
            { PlayboardModelCell.Colors.Red, Brushes.Red},
            { PlayboardModelCell.Colors.Green, Brushes.Green},
            { PlayboardModelCell.Colors.Blue, Brushes.Blue},
    };
    public static void SyncWithModel(PlayboardModel? model, PlayboardView? view)
    {
        if (model == null || view == null)
        {
            return;
        }

        if (model.Rows != view.Rows || model.Columns != view.Columns)
        {
            return;
        }

        for (int i = 0; i < model.Rows; i++)
        {
            for (int j = 0; j < model.Columns; j++)
            {
                view.Cells[i, j].Color = _colorAdapter[model.Data[i, j].Color];
                view.Cells[i, j].Level = Convert.ToInt32(model.Data[i, j].Level);
            }
        }
    }
}