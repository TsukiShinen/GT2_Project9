using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridDirection
{
    public readonly Vector2Int Vector;

    private GridDirection(int x, int y)
    {
        Vector = new Vector2Int(x, y);
    }

    public static implicit operator Vector2Int(GridDirection direction)
    {
        return direction.Vector;
    }

    public static GridDirection GetDirectionFromV2I(Vector2Int vector)
    {
        return CardinalAndInterCardinalDirections.DefaultIfEmpty(None).FirstOrDefault(direction => direction == vector);
    }

    public static readonly GridDirection None = new GridDirection(0, 0);
    public static readonly GridDirection North = new GridDirection(0, 1);
    public static readonly GridDirection South = new GridDirection(0, -1);
    public static readonly GridDirection East = new GridDirection(1, 0);
    public static readonly GridDirection West = new GridDirection(-1, 0);
    public static readonly GridDirection NorthEast = new GridDirection(1, 1);
    public static readonly GridDirection NorthWest = new GridDirection(-1, 1);
    public static readonly GridDirection SouthEast = new GridDirection(1, -1);
    public static readonly GridDirection SouthWest = new GridDirection(-1, -1);

    public static readonly List<GridDirection> CardinalDirections = new List<GridDirection>
    {
        North, South, East, West
    };

    public static readonly List<GridDirection> CardinalAndInterCardinalDirections = new List<GridDirection>
    {
        North, South, East, West, NorthEast, NorthWest, SouthEast, SouthWest
    };

    public static readonly List<GridDirection> AllDirection = new List<GridDirection>
    {
        None, North, South, East, West, NorthEast, NorthWest, SouthEast, SouthWest
    };
}
