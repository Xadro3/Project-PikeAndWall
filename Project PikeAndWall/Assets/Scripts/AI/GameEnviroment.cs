using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnviroment 
{

    private static GameEnviroment instance;
    private List<GameObject> perimeter = new List<GameObject>();
    public List<GameObject> Perimeter
    {
        get { return perimeter; }
    }

    public static GameEnviroment Singleton
    {
        get
        {
            if(instance== null)
            {
                instance = new GameEnviroment();
                instance.Perimeter.AddRange(GameObject.FindGameObjectsWithTag("Perimeter"));
            }
            return instance;
        }
    }

}
