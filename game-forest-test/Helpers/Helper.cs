using System;
using System.Runtime.InteropServices.JavaScript;

namespace game_forest_test.Helpers;

/// <summary>
/// Contains different unclassified methods
/// </summary>
public static class Helper
{
    /// <summary>
    /// Get random value from enum
    /// </summary>
    /// <typeparam name="T">enum type</typeparam>
    /// <returns>enum value</returns>
    public static T? GetRandomEnumValue<T>()
    {
        Array enumValues = Enum.GetValues(typeof(T));
        Random random = new Random();
        int randomIndex = random.Next(enumValues.Length);
        return (T?)enumValues.GetValue(randomIndex);
    }

    /// <summary>
    /// Check if element enters in range
    /// </summary>
    /// <param name="target">element to compare</param>
    /// <param name="a">first element</param>
    /// <param name="b">second element</param>
    /// <returns>True if element in range</returns>
    public static bool InRange(int? target, int a, int b)
    {
        return target >= int.Min(a, b) && target <= int.Max(a, b);
    }
}
