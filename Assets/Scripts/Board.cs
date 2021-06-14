using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    private Grid grid;

    public Building buildingPrefab;

    // Determining the edges of the game area
    public int gridXStart = -8;
    public int gridYStart = -8;

    public int gridXEnd = 7;
    public int gridYEnd = 5;

    private Dictionary<Vector2Int, Cell> _board = new Dictionary<Vector2Int, Cell>();

    public Dictionary<Vector2Int, Cell> board
    {
        get
        {
            return _board;
        }
    }

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Start()
    {
        // Representing the game area by cells
        for (int x = gridXStart; x <= gridXEnd; x++)
        {
            for (int y = gridYStart; y <= gridYEnd; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                board.Add(coordinates, new Cell(coordinates));
            }
        }
    }
    
    public bool PlaceBuilding(Building buildingPrefab, Vector2Int cellIndex)
    {
        Vector2Int size = buildingPrefab.Size;
        Vector3 coordinates = grid.GetCellCenterWorld((Vector3Int)cellIndex);

        // If a building can be placed, place it
        if (CanPlace(cellIndex, size))
        {
            Instantiate(buildingPrefab, coordinates, Quaternion.identity);

            for (int x = cellIndex.x; x < size.x + cellIndex.x; x++)
            {
                for (int y = cellIndex.y; y > cellIndex.y - size.y; y--)
                {
                    Vector2Int neighborCell = new Vector2Int(x, y);

                    if (board.TryGetValue(neighborCell, out Cell cell))
                    {
                        cell.IsEmpty = false;
                    }        
                }
            }

            return true;
        }

        return false;   
    }

    // Converts mouse coordinates to cell index
    public Vector2Int GetCellIndex(Vector3 mouseCoordinates)
    {
        Vector2Int cellIndex = (Vector2Int)grid.WorldToCell(mouseCoordinates);
        return cellIndex;
    }

    // Converts the cell index to tile coordinates
    public Vector3 GetCellPosition(Vector2Int cellIndex)
    {
        Vector3 tileCoordinates = grid.GetCellCenterWorld((Vector3Int)cellIndex);
        return tileCoordinates;
    }

    // If the area is suitable for building's size, it can be placed
    public bool CanPlace(Vector2Int cellIndex, Vector2Int size)
    {
        for (int x = cellIndex.x; x < size.x + cellIndex.x; x++)
        {
            for (int y = cellIndex.y; y > cellIndex.y - size.y; y--)
            {
                Vector2Int neighborCell = new Vector2Int(x, y);

                if (board.TryGetValue(neighborCell, out Cell cell))
                {
                    if (!cell.IsEmpty)
                    {
                        return false;
                    }          
                }
                else
                {
                    return false;
                }  
            }
        }
        return true;
    }
    
    // For pathfinding
    public List<Cell> GetNeighbours(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = cell.index.x + x;
                int checkY = cell.index.y + y;

                if (checkX >= gridXStart && checkX <= gridXEnd && checkY >= gridYStart && checkY <= gridYEnd)
                {
                    // Add the cell with this index to the neighbours 
                    neighbours.Add(board[(new Vector2Int(checkX, checkY))]);
                }
            }
        }
        return neighbours;
    }
}
