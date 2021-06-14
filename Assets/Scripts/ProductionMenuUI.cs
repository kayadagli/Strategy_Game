using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionMenuUI : MonoBehaviour
{
    public void OnBuildingSelected(BuildingItemUI buildingItemUI)
    {
        Mouse.Instance.SelectBuilding(buildingItemUI.buildingPrefab);
        Mouse.Instance.Activate(buildingItemUI.sprite);
    }
     
}
