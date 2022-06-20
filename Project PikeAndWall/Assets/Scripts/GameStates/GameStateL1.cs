using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.AI;
public class GameStateL1 : MonoBehaviour
{

    SelectedUnitsDictionary selected;
    public Flowchart flowchart;
    bool executed12=true;
    bool executed13 = true;
    bool executed14 = true;
    public GameObject pikeman;
    public GameObject eventSystem;

    private void Start()
    {
        selected = GameObject.Find("EventSystem").GetComponent<SelectedUnitsDictionary>();
        eventSystem = GameObject.Find("EventSystem");

    }

    private void Update()
    {
        if (selected.ReportNumberOfSelectedUnits() > 0 && executed12)
        {
            flowchart.ExecuteBlock("Tutorial 1.2");
            executed12 = false;
        }
        if (pikeman.GetComponent<NavMeshAgent>().hasPath && executed13)
        {
            flowchart.ExecuteBlock("Tutorial 1.3");
            executed13 = false;
        }
        if(GameEnviroment.Singleton.Enemies.Count == 0 && executed14) 
        {
            flowchart.ExecuteBlock("Tutorial 1.4");
            executed14 = false;
        }
        if (GameEnviroment.Singleton.Units.Count == 0)
        {
            eventSystem.GetComponent<GameStates>().Lose();
        }
    }

}
