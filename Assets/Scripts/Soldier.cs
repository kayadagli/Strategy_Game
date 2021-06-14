using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size;

    public Vector2Int Size => size;

    [SerializeField]
    private string soldierName;

    public string SoldierName => soldierName;

    [SerializeField]
    private Sprite soldierImage;

    public Sprite SoldierImage => soldierImage;

    Pathfinding pathFinding;
    List<Cell> path;
    Board board;

    public float speed;

    void Awake()
    {
        pathFinding = GetComponent<Pathfinding>();
        board = FindObjectOfType<Board>();
        speed = 5f;   
    }

    // Moving soldier on the path
    public void Move(Vector3 targetLocation)
    {
        path = pathFinding.FindPath(transform.position, targetLocation);

        StartCoroutine(MoveNextCell());
    }

    IEnumerator MoveNextCell()
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(board.GetCellPosition(path[i].index), board.GetCellPosition(path[i+1].index), travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
        }
  
    }
}
