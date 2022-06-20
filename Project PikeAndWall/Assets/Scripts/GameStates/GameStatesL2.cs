using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameStatesL2 : MonoBehaviour
{
    // Start is called before the first frame update
    bool executed14 = true;
    public Flowchart flowchart;
    public GameObject eventSystem;

    // Update is called once per frame
    void Update()
    {
        if (GameEnviroment.Singleton.Enemies.Count == 0 && executed14)
        {
            flowchart.ExecuteBlock("nach Kampf");
            executed14 = false;
        }
        if(GameEnviroment.Singleton.Units.Count == 0)
        {
            eventSystem.GetComponent<GameStates>().Lose();
        }
    }
}
