using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedExpiration : MonoBehaviour
{
    // Start is called before the first frame update

    public float timeToDie;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
            Destroy(gameObject,timeToDie);
        
    }
}
