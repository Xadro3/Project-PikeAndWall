using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnviroment 
{

    private static GameEnviroment instance;
    private List<GameObject> perimeter = new List<GameObject>();
    private List<GameObject> units = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> Perimeter
    {
        get { return perimeter; }
    }
    public List<GameObject> Units
    {
        get { return units; }
    }
    public List<GameObject> Enemies
    {
        get { return enemies; }
    }



    public static GameEnviroment Singleton
    {
        get
        {
            if(instance== null)
            {
                instance = new GameEnviroment();
                instance.Perimeter.AddRange(GameObject.FindGameObjectsWithTag("Perimeter"));
                //instance.Units.AddRange(GameObject.FindGameObjectsWithTag("Unit"));
                //instance.enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
                
            }
            return instance;
        }
    }

}
