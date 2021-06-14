using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size;

    public Vector2Int Size => size;

    [SerializeField]
    private Vector2Int index;

    public Vector2Int Index => index;

    [SerializeField]
    private string buildingName;

    public string BuildingName => buildingName;

    [SerializeField]
    private Sprite buildingImage;

    public Sprite BuildingImage => buildingImage;

    [SerializeField]
    private bool canSpawnUnit;

    public bool CanSpawnUnit => canSpawnUnit;
}
