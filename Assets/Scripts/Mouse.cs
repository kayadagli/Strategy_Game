using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public GameObject sprite;

    public Building buildingPrefab;

    public Board board;

    private bool selected;

    private static Mouse instance;

    public static Mouse Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Mouse>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<Mouse>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        board = FindObjectOfType<Board>();   
    }

    private void Update()
    {
        if (selected)
        {
            Vector3 mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            sprite.transform.position = new Vector3 (mouseCoordinates.x, mouseCoordinates.y, 1);

            Vector2Int cellIndex = board.GetCellIndex(mouseCoordinates);

            // If the area is appropriate to place the building
            if (board.CanPlace(cellIndex, buildingPrefab.Size))
            {
                sprite.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }
            // If the area is inappropriate to place the building
            else
            {
                sprite.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (board.PlaceBuilding(buildingPrefab, cellIndex))
                {
                    sprite.SetActive(false);
                    selected = false;
                }
            }
        }
        
    }

    public void SelectBuilding(Building buildingPrefab)
    {
        this.buildingPrefab = buildingPrefab;
    }

    public void Activate(GameObject sprite)
    {
        if (this.sprite != null)
        {
            Destroy(this.sprite);
        }

        this.sprite = Object.Instantiate(sprite);
        selected = true;
    }
}
