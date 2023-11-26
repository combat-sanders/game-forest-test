using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace game_forest_test.Models;

public class OrdersModel
{
    public enum Slots
    {
        First,
        Second
    }
    
    private PlayboardModel _model;

    public int MaxCount
    {
        get => _maxCount;
        set
        {
            if (value < 1) _maxCount = 1;
            _maxCount = value;
        }
    }
    private int _maxCount;
    
    public int Count
    {
        get => _count;
        private set
        {
            if (value < 0) _count = 0;
            _count = value;
            if (_count >= _maxCount) OnMaxCountAchieved.Invoke();
        }
    }

    private int _count = 1;

    public Dictionary<Slots, PlayboardElementModel> Data;
    
    public OrdersModel(PlayboardModel model)
    {
        _model = model;
        
        Data[Slots.First] = _model.GetRandomCell();
        Data[Slots.Second] = _model.GetRandomCell();
    }

    public void RequestOrder(Slots slot)
    {
        PlayboardElementModel elementInSlot = Data[slot];

        var sameElements = _model.Data.Where(
            elementInPlayboard => elementInPlayboard.Value.Equals(elementInSlot));

        sameElements = sameElements.ToDictionary(x => x.Key, x => x.Value);
        
        if (sameElements.Any())
        {
            Random random = new Random();
            var key = sameElements.ElementAt(random.Next(0, sameElements.Count() - 1)).Key;
            _model.Data[key].State = PlayboardElementModel.States.Empty;
        }

        Count++;
        OnOrderCompleted.Invoke(slot);
    }

    public delegate void OrderCompleted(Slots slot);
    public event OrderCompleted OnOrderCompleted;

    public delegate void MaxCountAchieved();
    public event MaxCountAchieved OnMaxCountAchieved;
}