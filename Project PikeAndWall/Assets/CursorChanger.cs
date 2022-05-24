using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public GameObject eventsystem;
    public SelectedUnitsDictionary selected;
    public Texture2D attackCursor;
    public Texture2D selectCursor;
    public Texture2D moveCursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    Ray ray;
    RaycastHit[] raycastHits;

    // Start is called before the first frame update
    void Start()
    {
        eventsystem = GameObject.FindGameObjectWithTag("EventSystem");
        selected = eventsystem.GetComponent<SelectedUnitsDictionary>();

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        raycastHits = Physics.RaycastAll(ray, 5000f);
        foreach (RaycastHit hit in raycastHits)
        {
            if (hit.collider.gameObject.tag == "Unit" || hit.collider.gameObject.tag == "Buildings")
            {
                Cursor.SetCursor(selectCursor, hotSpot, cursorMode);
                break;
            }
            if ((hit.collider.gameObject.tag == "Enemy") && (selected.selectedUnits.Count != 0))
            {
                Cursor.SetCursor(attackCursor, hotSpot, cursorMode);
                break;
            }
            if ((selected.selectedUnits.Count != 0) && !(hit.collider.gameObject.tag == "Enemy"))
            {
                Cursor.SetCursor(moveCursor, hotSpot, cursorMode);
            }

        }
    }
}
