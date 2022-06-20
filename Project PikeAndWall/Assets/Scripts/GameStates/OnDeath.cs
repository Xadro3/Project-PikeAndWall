using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class OnDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public Flowchart flowchart;
    private void OnDestroy()
    {
        flowchart.ExecuteBlock("Level ende");
    }
}
