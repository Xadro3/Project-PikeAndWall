using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCreditsCursor : MonoBehaviour
{
    public Texture2D selectCursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(selectCursor, hotSpot, cursorMode);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
