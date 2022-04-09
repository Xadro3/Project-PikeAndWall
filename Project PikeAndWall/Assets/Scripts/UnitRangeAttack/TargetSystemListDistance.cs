using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystemListDistance : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float range;

    private RangeUnit playerDummy;

    private void Awake() {
        playerDummy = GetComponent<RangeUnit>();
    } 
    
    private void Update(){
        foreach (Enemy enemy in Enemy.GetEnemyList()){
            if (Vector3.Distance(transform.position, enemy.transform.position) < range) {
                playerDummy.SetTarget(enemy);
                Debug.Log("Target!");
            }
            playerDummy.SetTarget(null);
            Debug.Log("Target lost!");
        }
    }
}
