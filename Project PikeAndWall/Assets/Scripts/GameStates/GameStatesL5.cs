using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameStatesL5 : MonoBehaviour
{
    // Start is called before the first frame update
    bool executed14 = true;
    public Flowchart flowchart;
    public GameObject eventSystem;
    public int unitsToBuild;
    bool executed = true;
    private void Start()
    {
        InvokeRepeating("UpdateState", 0, 1.0f);
    }
    // Update is called once per frame
    void UpdateState()
    {

        //Debug.Log("Pike"+(GameObject.Find("TrainingPikePlaced(Clone)") != null));
        //Debug.Log("Caval"+(GameObject.Find("TainingCavalryPlaceded(Clone)") != null));
        //Debug.Log("Bow"+(GameObject.Find("TrainingBowmanPlaced(Clone)") != null));
        //Debug.Log("Units"+ (GameEnviroment.Singleton.Units.Count >= 8));

        if (GameObject.Find("TrainingPikePlaced(Clone)") != null && executed14 && GameObject.Find("TrainingCavalryPlaced(Clone)") !=null && GameObject.Find("TrainingBowmanPlaced(Clone)") !=null)
        {
            flowchart.ExecuteBlock("Einheitenbau dia");
            executed14 = false;
        }
        if(GameEnviroment.Singleton.Units.Count >= unitsToBuild && executed)
        {
            flowchart.ExecuteBlock("WinText");
            executed = false;
        }
        if (GameEnviroment.Singleton.Units.Count == 0)
        {
            eventSystem.GetComponent<GameStates>().Lose();
        }
    }
}
