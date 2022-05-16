using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomImpulse : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    Vector3 randomForce;
    public float randomCeil;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        randomForce = new Vector3(Random.Range(0, randomCeil), Random.Range(0, randomCeil), Random.Range(0, randomCeil));
        rigidbody.AddForce(randomForce, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
