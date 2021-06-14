using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();

    public Text buildingText;
    public Image buildingImage;
    public Button unitButton;

    public Soldier selectedSoldier;
    public Board board;
    

    private static UnitSelections instance;
    public static UnitSelections Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        
        board = FindObjectOfType<Board>();

        // If an instance of this already exists and it isn't this one
        if (instance != null && instance != this)
        {
            // Destroy this instance
            Destroy(this.gameObject);
        }
        else
        {
            // Make this the instance
            instance = this;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && unitSelected.Count != 0)
        {
            if (unitSelected[0].TryGetComponent(out Soldier soldier))
            {
                selectedSoldier = soldier;
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedSoldier != null)
        {
            Vector2Int mouseIndex = board.GetCellIndex(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector3 mousePosition = board.GetCellPosition(mouseIndex);

            selectedSoldier.Move(mousePosition);
            
        }
        
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitSelected.Add(unitToAdd);
        unitToAdd.transform.GetChild(1).gameObject.SetActive(true);
        if (unitToAdd.TryGetComponent(out Building building))
        {
            buildingText.transform.parent.gameObject.SetActive(true);
            buildingText.text = building.BuildingName;
            buildingImage.sprite = building.BuildingImage;
            unitButton.gameObject.SetActive(building is Barracks);
        }
        else
        {
            buildingText.transform.parent.gameObject.SetActive(false);
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitSelected)
        {
            unit.transform.GetChild(1).gameObject.SetActive(false);
        }
        unitSelected.Clear();
        
    }

    public void Deselect(GameObject unitToDeselect)
    {
        
    }
}
