using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class GameStatesL4 : MonoBehaviour
{
    bool executed14 = true;
    bool executed = true;
    bool executed2 = true;
    bool executed3 = true;
    public Flowchart flowchart;
    public GameObject eventSystem;
    int wallsbuilt=0;

    //private void Start()
    //{
    //    flowchart.StopAllBlocks();
    //    flowchart.ExecuteBlock("Level start");
    //}
    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("LumberjackBuild(Clone)") != null && executed14)
        {
            flowchart.ExecuteBlock("Hütte gebaut");
            executed14 = false;
        }

       

        if (GameEnviroment.Singleton.Units.Count == 0)
        {
            eventSystem.GetComponent<GameStates>().Lose();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Unit" && executed)
        {
            flowchart.ExecuteBlock("Armee in Ruine");
            executed = false;
            
        }
        if(other.gameObject.name == "MIneStonePlaced(Clone)" && executed2)
        {
            flowchart.ExecuteBlock("Build Walls");
            executed2 = false;
        }
        if(other.gameObject.name== "WallBuildingPlaced(Clone)" && executed3)
        {
            wallsbuilt++;
            Debug.Log(wallsbuilt);
            if (wallsbuilt == 7)
            {
                executed3 = false;
                flowchart.ExecuteBlock("New Block");
            }
        }
    }
}
