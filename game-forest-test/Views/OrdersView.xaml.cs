using System;
using System.Collections.Generic;
using System.Windows.Controls;
using game_forest_test.Models;

namespace game_forest_test.Views;

public partial class OrdersView : UserControl
{
    public enum Slots
    {
        First,
        Second,
    }

    public Dictionary<Slots, TextBlock> Data;
    public OrdersView()
    {
        InitializeComponent();
        
        Data = new Dictionary<Slots, TextBlock>();
        
        Data.Add(Slots.First, (TextBlock)FindName("FirstSlot"));
        Data.Add(Slots.Second, (TextBlock)FindName("SecondSlot"));
    }
}