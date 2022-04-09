using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    // Start is called before the first frame update
    
    private RangeUnit playerDummy;

    private void Awake() {
        playerDummy = GetComponent<RangeUnit>();
    }
    
    private void OnTriggerEnter(Collider collision){
        if (collision.TryGetComponent<Enemy>(out Enemy enemy)) {
            playerDummy.enemyInRange = true;
            playerDummy.SetTarget(enemy);
            Debug.Log("initial target");
        }
    }



    private void OnTriggerExit(Collider collision){
        if (collision.TryGetComponent<Enemy>(out Enemy enemy)) {
            playerDummy.SetTarget(null);
            playerDummy.enemyInRange = false;
            Debug.Log("Target lost!");
        }
    }


}
