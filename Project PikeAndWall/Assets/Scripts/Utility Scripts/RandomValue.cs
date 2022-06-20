using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomValue : MonoBehaviour
{
    // Start is called before the first frame update
    public float max = 5f;
    public float min = 2.5f;
    Light light;
    bool running;
    void Start()
    {
        light = GetComponent<Light>();
        running = false;
    }

    private void Update()
    {
        if (!running)
        {
            StartCoroutine(RandomLight());
        }
    }

    IEnumerator RandomLight()
    {
        running = true;
        yield return new WaitForSeconds(0.1f);

        light.intensity = Random.Range(min, max);
        running = false;
    }
    
    
}
