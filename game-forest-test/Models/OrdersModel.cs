using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace game_forest_test.Models;

public class OrdersModel
{
    /// <summary>
    /// Available slots to request
    /// </summary>
    public enum Slots
    {
        First,
        Second
    }
    
    /// <summary>
    /// Storage of elements and game logic
    /// </summary>
    private PlayboardModel _model;

    /// <summary>
    /// Max count of orders in current session
    /// </summary>
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
    
    /// <summary>
    /// Current count of orders in session
    /// </summary>
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

    /// <summary>
    /// Contains data about available elements to request
    /// </summary>
    public Dictionary<Slots, PlayboardElementModel> Data { get; private set; }
    
    public OrdersModel(PlayboardModel model)
    {
        _model = model;
        
        Data[Slots.First] = _model.GetRandomElement();
        Data[Slots.Second] = _model.GetRandomElement();
    }

    /// <summary>
    /// Make a request logic.
    /// If method called, element in associated slot removed from playborard (if contains).
    /// </summary>
    /// <param name="slot"></param>
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