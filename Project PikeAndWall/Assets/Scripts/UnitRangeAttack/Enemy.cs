using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public static List<Enemy> enemyList = new List<Enemy>();

    public static List<Enemy> GetEnemyList()
    {
        return enemyList;
    }

    private void Awake()
    {
        enemyList.Add(this);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
