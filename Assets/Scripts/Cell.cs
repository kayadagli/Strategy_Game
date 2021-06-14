using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private bool isEmpty;

    public int gCost;
    public int hCost;
    public Cell parent;

    public Vector2Int index;

    public bool IsEmpty
    {
        get
        {
            return isEmpty;
        }
        set
        {
            isEmpty = value;
        }
    }

    public Cell(Vector2Int index)
    {
        isEmpty = true;
        this.index = index;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Vector2Int Index
    {
        get
        {
            return index;
        }
    }

}
