using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorDestroy;

    private void Awake()
    {
        ChangeCourser(cursor);
        Cursor.lockState = CursorLockMode.Confined;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void ChangeCourser(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType, Vector2.zero ,CursorMode.Auto);  
    }

}
