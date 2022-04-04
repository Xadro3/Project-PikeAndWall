using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float movementSpeed;
    public float movementTime;
    public float rotationSpeed;
    public float zoomMaxHeight;
    public float zoomMinHeight;
    public float zoomSpeed;


    Vector3 newPosition;
    Vector3 rotationPosition1;
    Vector3 rotationPosition2;
    

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {

        HandleMovementInput();
        HandleCameraRotation();
    }

    void HandleMovementInput()
    {
        if (Input.GetAxis("Vertical")>0)
        {
            newPosition += transform.forward * movementSpeed;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            newPosition += transform.forward * -movementSpeed;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            newPosition += transform.right * movementSpeed;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            newPosition += transform.right * -movementSpeed;
        }
       
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {

            Debug.Log("Transform.position "+transform.GetChild(0).position.y);
            Debug.Log("Tranform.Child.transform "+(transform.GetChild(0).transform.forward * -zoomSpeed).y);

            if ((transform.GetChild(0).position.y +zoomSpeed) < zoomMaxHeight)
            {
                transform.GetChild(0).position += transform.GetChild(0).transform.forward * -zoomSpeed;
                
            }
            
        }
       
        

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if ((transform.GetChild(0).position.y-zoomSpeed) > zoomMinHeight)
            {
                transform.GetChild(0).position += transform.GetChild(0).transform.forward * zoomSpeed;
                
            }
           
        }
       

        

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }

    void HandleCameraRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            rotationPosition1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotationPosition2= Input.mousePosition;

            float rotationInX = (rotationPosition2 - rotationPosition1).x * rotationSpeed;
           

            transform.rotation *= Quaternion.Euler(new Vector3(0, rotationInX, 0));
        }


    }
   
    
}
