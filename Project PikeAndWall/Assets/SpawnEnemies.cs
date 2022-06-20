using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public float countDown;
    public int archers;
    public int pikes;
    public int cavalry;
    public GameObject archersFab;
    public GameObject pikeFab;
    public GameObject cavalFab;
    bool executed = true;

    // Update is called once per frame
    public void Spawn()
    {
        
            
            for(int i = 0; i <= archers;i++)
            {
                Instantiate(archersFab, transform.position, transform.rotation);
            }
            
            for (int x = 0; x <= pikes; x++)
            {
                Instantiate(pikeFab, transform.position, transform.rotation);
            }
            for (int y = 0; y <= cavalry; y++)
            {
                Instantiate(cavalFab, transform.position, transform.rotation);
            }
           gameObject.GetComponent<GameStatesL6>().spawned = true;
        }
    }


