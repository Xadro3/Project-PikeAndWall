using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateButtonText : MonoBehaviour
{
    // Start is called before the first frame update
    Text upgradeCosts;
    public GameObject gameObject;
    SelectedUnitsDictionary selected;
    void Start()
    {
        selected = gameObject.GetComponent<SelectedUnitsDictionary>();
        upgradeCosts = GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        string updateText = selected.returnAR[0] + "\n \n" + selected.returnAR[1] + "\n \n" + selected.returnAR[2];
        upgradeCosts.text = updateText;
    }
}
