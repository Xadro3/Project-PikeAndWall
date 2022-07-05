using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLife : MonoBehaviour
{
    
    [Range(0,20)]
    [SerializeField] public float timeToLife;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IWillDie());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator IWillDie()
    {
        Health health = GetComponent<Health>();
        yield return new WaitForSeconds(timeToLife);
        health.hitPoints = 0;

    }
}
