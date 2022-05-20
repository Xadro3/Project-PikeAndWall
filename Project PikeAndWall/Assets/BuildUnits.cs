using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUnits : MonoBehaviour
{
    public float timer;
    List<GameObject> buildQueue;
    public bool subroutineRunning;
    Vector3 spawnpoint;
    public GameObject buildable;
    

    private void Start()
    {
        buildQueue = new List<GameObject>();
        spawnpoint = this.transform.GetChild(1).position;
        subroutineRunning = false;
    }

    private void Update()
    {
        if (buildQueue.Count >=1 && !subroutineRunning)
        {

            Debug.Log("Starting coroutine");
            subroutineRunning = true;
            StartCoroutine(BuildUnit(buildQueue[0]));
            
        }
    }
    public void QueueUnit ()
    {
        if (buildQueue.Count >= 15)
        {

        }
        else
        {
            buildQueue.Add(buildable);
        }


    }

    IEnumerator BuildUnit(GameObject unitToBuild)
    {
        float buildTime= unitToBuild.gameObject.GetComponent<UnitClass>().buildTime;
        yield return new WaitForSeconds(buildTime);
      
        
            Instantiate(unitToBuild, spawnpoint, transform.rotation);
            buildQueue.Remove(unitToBuild);
            Debug.Log("Weird shit");
            subroutineRunning = false;
           // yield return new WaitForSeconds(buildTime);
            

        

    }


    
    
    
}
