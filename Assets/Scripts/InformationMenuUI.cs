using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationMenuUI : MonoBehaviour
{

    public Board board;

    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }

    // When a unit is selected, and it can spawn a unit, spawn it
    public void OnUnitSelected(UnitUI unitUI)
    {
        GameObject selectedUnit = UnitSelections.Instance.unitSelected[0];

        if (selectedUnit.GetComponent<Barracks>().CanSpawnUnit)
        {
            Vector2Int cellIndex = board.GetCellIndex(selectedUnit.transform.position);
            Vector3 tileCoordinates = board.GetCellPosition(cellIndex);

            Vector3 spawnPosition = tileCoordinates + new Vector3(1, -4, 0);

            Instantiate(unitUI.soldierPrefab, spawnPosition, Quaternion.identity);
        }

  
    }
}
