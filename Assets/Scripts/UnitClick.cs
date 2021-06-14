using UnityEngine;

public class UnitClick : MonoBehaviour
{

    public LayerMask clickable;
    public LayerMask ground;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Selecting units by clicking on them
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.zero);
            if (hit.collider != null)
            {
                // If we hit a clickable object
                UnitSelections.Instance.ClickSelect(hit.collider.gameObject);

            }
            else
            {
                // If we didn't
                //UnitSelections.Instance.DeselectAll();

            }

        }
    }
}
