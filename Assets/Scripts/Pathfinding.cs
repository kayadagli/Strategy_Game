using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Board board;

    void Awake()
    {
        board = FindObjectOfType<Board>();
    }

    // A* pathfinding algorithm
    public List<Cell> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector2Int startCellIndex = board.GetCellIndex(startPos);
        Vector2Int targetCellIndex = board.GetCellIndex(targetPos);

        Cell startCell = board.board[(startCellIndex)];
        Cell targetCell = board.board[(targetCellIndex)];

        List<Cell> openSet = new List<Cell>();
        HashSet<Cell> closedSet = new HashSet<Cell>();
        openSet.Add(startCell);

        while (openSet.Count > 0)
        {
            Cell currentCell = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentCell.fCost || openSet[i].fCost == currentCell.fCost && openSet[i].hCost < currentCell.hCost)
                {
                    currentCell = openSet[i];
                }
            }

            openSet.Remove(currentCell);
            closedSet.Add(currentCell);

            if (currentCell == targetCell)
            {

                return RetracePath(startCell, targetCell);
            }

            foreach (Cell neighbour in board.GetNeighbours(currentCell))
            {
                if (!neighbour.IsEmpty || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentCell.gCost + GetDistance(currentCell, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetCell);
                    neighbour.parent = currentCell;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
        return new List<Cell>();
    }

    List<Cell> RetracePath(Cell beginCell, Cell endCell)
    {
        List<Cell> path = new List<Cell>();
        Cell currentCell = endCell;
        

        while (currentCell != beginCell)
        {
            path.Add(currentCell);
            //if (currentCell.parent == null) break;
            currentCell = currentCell.parent;
        }
        path.Add(beginCell);
        path.Reverse();
        
        return path;

    }

    int GetDistance(Cell cellA, Cell cellB)
    {
        int distX = Mathf.Abs(cellA.index.x - cellB.index.x);
        int distY = Mathf.Abs(cellA.index.y - cellB.index.y);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }

}
