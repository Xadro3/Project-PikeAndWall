using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDestruction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Baum"))
        {
            other.gameObject.SetActive(false);
        }
    }
}


