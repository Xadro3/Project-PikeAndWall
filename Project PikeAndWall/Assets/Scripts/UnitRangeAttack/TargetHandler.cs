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
        if(collision.TryGetComponent<Hitbox>(out Hitbox hitbox)){
            if(unit.tag == "Unit"){
                if(collision.tag == "Enemy"){
                    unit.enemyInRange = true;
                    unit.SetTarget(hitbox);
                }
            }
            if(unit.tag == "Enemy"){
                if(collision.tag == "Player"){
                    unit.enemyInRange = true;
                    unit.SetTarget(hitbox);
                }
            }
        }
    }



    private void OnTriggerExit(Collider collision){
        if(collision.TryGetComponent<Hitbox>(out Hitbox hitbox)){
            if(unit.tag == "Unit"){
                if(collision.tag == "Enemy"){
                    unit.SetTarget(null);
                    unit.enemyInRange = false;
                }
            }
            if(unit.tag == "Enemy"){
                if(collision.tag == "Player"){
                    unit.SetTarget(null);
                    unit.enemyInRange = false;
                }
            }
        }
    }


}
