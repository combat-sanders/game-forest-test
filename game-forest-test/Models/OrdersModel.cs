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
    private int _maxCount = 1;
    
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
            if (_count >= _maxCount) OnMaxCountAchieved?.Invoke();
        }
    }

    private int _count = 0;

    /// <summary>
    /// Contains data about available elements to request
    /// </summary>
    public Dictionary<Slots, PlayboardElementModel> Data { get; private set; } = new()
    {
        { Slots.First, new PlayboardElementModel() },
        { Slots.Second, new PlayboardElementModel() }
    };
    
    public OrdersModel(PlayboardModel model)
    {
        _model = model;

        Data[Slots.First] = model.GetRandomElement();
        Data[Slots.Second] = model.GetRandomElement();
    }

    /// <summary>
    /// Make a request logic.
    /// If method called, element in associated slot removed from playborard (if contains).
    /// </summary>
    /// <param name="slot"></param>
    public void RequestOrder(Slots slot)
    {
        // get element in slot
        PlayboardElementModel elementInSlot = Data[slot];
        
        // get the list of same elements
        List<KeyValuePair<Vector2, PlayboardElementModel>> sameElements = 
            _model.Data.Where(x => 
                x.Value.Color == elementInSlot.Color && x.Value.Level == elementInSlot.Level).ToList();

        if (sameElements.Count > 0)
        {
            // get one of same elements
            Random random = new Random();
            Vector2 position = sameElements[random.Next(0, sameElements.Count)].Key;

            // and make it empty
            _model.Data[position].State = PlayboardElementModel.States.Empty;

            Count++;
            OnOrderCompleted?.Invoke(slot);
            Data[slot] = _model.GetRandomElement();
        }
    }

    public delegate void OrderCompleted(Slots slot);
    public event OrderCompleted? OnOrderCompleted;

    public delegate void MaxCountAchieved();
    public event MaxCountAchieved? OnMaxCountAchieved;
}