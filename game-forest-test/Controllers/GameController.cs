using System;
using System.Collections.Generic;
using game_forest_test.Helpers;
using game_forest_test.Models;

namespace game_forest_test.Views;

public static class GameController
{
    public static void InitGame(PlayboardModel model, int countOfCells)
    {
        var emptyCells = model.GetEmptyCells();
        Random random = new Random();

        while (countOfCells > 0)
        {
            int index = random.Next(0, emptyCells.Count - 1);
            model.SpawnElement(emptyCells[index].Item1,
                emptyCells[index].Item2,
                PlayboardModelCell.Colors.Blue,
                PlayboardModelCell.Levels.First);
            emptyCells.Remove(emptyCells[index]);
            countOfCells--;
        }
    }
}