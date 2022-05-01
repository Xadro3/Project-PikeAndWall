using UnityEngine;

public class UnitHighlighter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<HealthBarHandler>().ShowHealthBar();
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        GetComponentInChildren<HealthBarHandler>().HideHealthBar();
    }
}
