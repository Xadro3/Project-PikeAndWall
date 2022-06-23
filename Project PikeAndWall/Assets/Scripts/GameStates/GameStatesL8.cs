using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class GameStatesL8 : MonoBehaviour
{
    public GameObject eventSystem;
    bool executed=true;
    public Flowchart flowchart;
    public GameObject objective;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objective==null)
        {
            eventSystem.GetComponent<GameStates>().Lose();
        }
        if(GameEnviroment.Singleton.Enemies.Count == 0 && executed)
        {
            flowchart.ExecuteBlock("Level ende");
            executed = false;
        }
    }
}
