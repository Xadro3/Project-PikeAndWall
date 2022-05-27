using UnityEngine;

public class UnitReach : MonoBehaviour
{

    private SphereCollider unitReach;
    private float unitRange;

    void Start()
    {
        unitRange = GetComponentInParent<UnitClass>().range;
        unitReach = GetComponent<SphereCollider>();
        unitReach.radius *= unitRange;
    }
    
    void Update()
    {
    }

    public float SetUnitReach(float unitRange)
    {
        unitReach.radius = 1;
        unitReach.radius *= unitRange;
        return unitRange;
    }
    
}
