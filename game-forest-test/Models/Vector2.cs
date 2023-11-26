namespace game_forest_test.Models;

public class Vector2
{
    /// <summary>
    /// Horizontal position inside somebody
    /// </summary>
    public int X { get; set; } = -1;
    
    /// <summary>
    /// vertical position inside somebody
    /// </summary>
    public int Y { get; set; } = -1;
    
    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public override bool Equals(object obj)
    {
        if (obj is Vector2 vector)
        {
            return (vector.X == X && vector.Y == Y);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return X + Y;
    }
}