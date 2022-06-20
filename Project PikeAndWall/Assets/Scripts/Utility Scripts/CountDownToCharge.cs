using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownToCharge : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > time)
        {
            foreach(GameObject unit in GameEnviroment.Singleton.Enemies)
            {
                unit.GetComponent<Ai>().charge = true;
            }
        }
    }
}
