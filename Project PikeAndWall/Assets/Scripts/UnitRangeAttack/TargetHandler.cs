using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    
    private UnitClassRange unit;

    //List <GameObject> currentCollisions = new List <GameObject> ();

    private void Awake() {
        unit = GetComponent<UnitClassRange>();
    }
    
    private void OnTriggerEnter(Collider collision){
        if (collision.TryGetComponent<Hitbox>(out Hitbox hitbox)) {
            unit.enemyInRange = true;
            unit.SetTarget(hitbox);
            Debug.Log("initial target");
        }
    }



    private void OnTriggerExit(Collider collision){
        if (collision.TryGetComponent<Hitbox>(out Hitbox hitbox)) {
            unit.SetTarget(null);
            unit.enemyInRange = false;
            Debug.Log("Target lost!");
        }
    }


}
