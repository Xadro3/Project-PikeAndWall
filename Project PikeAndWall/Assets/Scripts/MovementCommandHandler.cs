using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovementCommandHandler : MonoBehaviour
{
    // Start is called before the first frame update

    NavMeshAgent agent;
    RaycastHit destinationHit;
    Vector3 destination;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButton(1)&&(gameObject.GetComponent<UnitHighlighter>() != null)))
        {
            Ray destinationRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(destinationRay, out destinationHit, 50000f))
            {
                destination = destinationHit.point;
               // MoveToDestiantionSingle();
            }
        }

    }

   // void MoveToDestiantionSingle()
  //  {
   //     agent.SetDestination(destination);
    //}
    public void MoveToDestinationMultiple(Vector3 dest)
    {
        Debug.Log("Destination is: " + dest);
        agent.SetDestination(dest);
    }



}
