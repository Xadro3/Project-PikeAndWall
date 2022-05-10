using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform projectile;
    private UnitClass unit;
    public Hitbox targetHitbox;
    public Health targetHealth;
    
    void Start()
    {
        unit = GetComponentInParent<UnitClass>();
    }
    // Update is called once per frame
    void Update()
    {  
        targetHitbox = unit.targetHitbox;
        targetHealth = unit.targetHealth;
    }

    public void StartAttack()
    {
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(unit.firerate);
        if (unit.enemyInRange == true)
        {
            if(gameObject.name == "Bow" | gameObject.name == "Musket")
            {
                //audio.Play(0);
                Transform projectileTransform = Instantiate(projectile, new Vector3(unit.weapon.transform.position.x, unit.weapon.transform.position.y, unit.weapon.transform.position.z), Quaternion.identity);
                projectileTransform.transform.parent = unit.weapon.transform;
                Vector3 shootDirection = new Vector3(targetHitbox.transform.position.x - transform.position.x, targetHitbox.transform.position.y - transform.position.y, targetHitbox.transform.position.z - transform.position.z);
                if (projectile.name == "pfBullet"){
                    projectileTransform.GetComponent<ProjectileBullet>().Setup(shootDirection);
                }
            }
            if(gameObject.name == "Spear" | gameObject.name == "Sword")
            {
                targetHealth.TakeDamage(unit.damageValue);
            }
            StartCoroutine(Attack());
        }
    }

}
