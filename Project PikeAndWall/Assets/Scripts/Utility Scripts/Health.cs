using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public int hitPoints;
    public int armor;
    public int maximumHitPoints;
    public int team;
    public int healOverTimeEffect;
    public int healOverTimeTickRate;


    void Start()
    {
        if (gameObject.tag == "Unit")
        {
            GameEnviroment.Singleton.Units.Add(gameObject);
        }
        
        hitPoints = maximumHitPoints;
        StartCoroutine(HealOverTime());
    }

    // Update is called once per frame

    void Update()
    {
        UpdateHealthBar();
        if (hitPoints <= 0)
        {
            if (gameObject.tag == "Unit")
            {
                GameEnviroment.Singleton.Units.Remove(gameObject);
            }
            gameObject.GetComponent<Destructible>().Die();

        }
    }


    void UpdateHealthBar()
    {

        gameObject.GetComponentInChildren<HealthBarHandler>().SetHealth((float)hitPoints / maximumHitPoints);
    }



    public void TakeDamage(int damage)
    {
        hitPoints = hitPoints - damage;

        UpdateHealthBar();
    }
    public void HealDamage(int healing)
    {
        if (hitPoints + healing > maximumHitPoints)
        {
            hitPoints = maximumHitPoints;
        }
        else
        {
            hitPoints += healing;
        }

        UpdateHealthBar();

    }

    IEnumerator HealOverTime()
    {
        if (hitPoints != maximumHitPoints)
        {
            if (maximumHitPoints > hitPoints && hitPoints + healOverTimeEffect <= maximumHitPoints)
            {
                hitPoints += healOverTimeEffect;
            }
            else if (hitPoints + healOverTimeEffect > maximumHitPoints)
            {
                hitPoints = maximumHitPoints;
            }
        }

        UpdateHealthBar();

        yield return new WaitForSeconds(healOverTimeTickRate);

    }

}
