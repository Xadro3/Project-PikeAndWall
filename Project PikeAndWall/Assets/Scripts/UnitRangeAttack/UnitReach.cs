using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitReach : MonoBehaviour
{

    private SphereCollider unitReach;
    private float unitRange;
    
    void Start()
    {
        unitRange = GetComponentInParent<UnitClassRange>().unitRange;
        unitReach = GetComponent<SphereCollider>();
        unitReach.radius *= unitRange;
    }

    void Update()
    {
        
    }
}
