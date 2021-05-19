using UnityEngine;

public static class DirectionManager
{
    public static Direction Vector2ToDirection(Vector2 direction)
    {
        float x = direction.x;
        float y = direction.y;

        if (x > y)
        {
            if (x > 0)
            {
                return Direction.Right;
            }
            else
            {
                return Direction.Left;
            }
        }
        else if (y > x)
        {
            if (y > 0)
            {
                return Direction.Up;
            }
            else
            {
                return Direction.Down;
            }
        }
        else
        {
            return Direction.Zero;
        }
    }

    public static Vector2 DirectionToVector2(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector2.up;

            case Direction.Down:
                return Vector2.down;

            case Direction.Left:
                return Vector2.left;

            case Direction.Right:
                return Vector2.right;

            default:
                return Vector2.zero;
        }
    }
}
public enum Direction { Up, Down, Left, Right, Zero }