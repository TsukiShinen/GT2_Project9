using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2 WorldPosition;
    public Vector2Int GridIndex;
    public GridDirection BestDirection;

    public byte Cost;
    public ushort bestCost;

    public Cell(Vector2 worldPosition, Vector2Int gridIndex)
    {
        WorldPosition = worldPosition;
        GridIndex = gridIndex;
        Cost = 1;
        bestCost = ushort.MaxValue;
        BestDirection = GridDirection.None;
    }

    public void IncreaseCost(int amount)
    {
        if (Cost == byte.MaxValue) { return; }

        if (amount+Cost >= 255) 
            Cost = byte.MaxValue;
        else 
            Cost+= ((byte)amount);
    }
}
