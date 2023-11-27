using System.Collections.Generic;

namespace game_forest_test.Models.Interfaces;

public interface IOrdersModel
{
    /// <summary>
    /// Max count of orders in current session
    /// </summary>
    int MaxCount { get; set; }

    /// <summary>
    /// Current count of orders in session
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Contains data about available elements to request
    /// </summary>
    Dictionary<OrdersModel.Slots, PlayboardElementModel> Data { get; }

    /// <summary>
    /// Make a request logic.
    /// If method called, element in associated slot removed from playborard (if contains).
    /// </summary>
    /// <param name="slot"></param>
    void RequestOrder(OrdersModel.Slots slot);

    event OrdersModel.OrderCompleted? OnOrderCompleted;
    event OrdersModel.MaxCountAchieved? OnMaxCountAchieved;
}