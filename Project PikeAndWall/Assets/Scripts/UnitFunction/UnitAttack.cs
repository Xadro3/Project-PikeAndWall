using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform projectile;
    private UnitClass unit;
    public Hitbox targetHitbox;
    public Health targetHealth;
    private bool isAttacking = false;
    private NavMeshAgent agent;
    private Quaternion lookRotation;
    private Vector3 lookDirection;
    private AudioSource audio;

    void Start()
    {
        unit = GetComponentInParent<UnitClass>();
        agent = GetComponentInParent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {  
        targetHitbox = unit.targetHitbox;
        targetHealth = unit.targetHealth;
        if (isAttacking && unit.enemyInRange && targetHitbox != null)
        {
            RotateFaceToEnemy();
        }
        

    }
    public void StartAttack()
    {
        if (isAttacking == false)
            {
                StartCoroutine(Attack());
            }
    }

    private void RotateFaceToEnemy()
    {
        lookDirection = (targetHitbox.transform.position - unit.transform.position).normalized;
        lookRotation = Quaternion.LookRotation(lookDirection);
        unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit.turnRate);
    }
    private IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(unit.fireRate);
        while (unit.enemyInRange == true)
        {
            yield return new WaitForSeconds(unit.fireRate);
            if((gameObject.name == "Bow" || gameObject.name == "Musket") && unit.enemyInRange == true)
            {
                Transform projectileTransform = Instantiate(projectile, new Vector3(unit.weapon.transform.position.x, unit.weapon.transform.position.y, unit.weapon.transform.position.z), Quaternion.identity);
                Vector3 shootDirection = new Vector3(targetHitbox.transform.position.x - transform.position.x, targetHitbox.transform.position.y - transform.position.y, targetHitbox.transform.position.z - transform.position.z);
                if (transform.root.CompareTag("Unit"))
                {
                    projectileTransform.tag = "Player";
                }
                else
                {
                    projectileTransform.tag = "Enemy";
                }
                if (projectile.name == "pfBullet"){
                    projectileTransform.GetComponent<ProjectileBullet>().SetDamage(unit.damageValue);
                    projectileTransform.GetComponent<ProjectileBullet>().Setup(shootDirection);
                }
                if (projectile.name == "pfArch")
                {
                    projectileTransform.GetComponent<ProjectileArch>().SetDamage(unit.damageValue);
                }
            }
            if((gameObject.name == "Spear" || gameObject.name == "Sword") && unit.enemyInRange == true)
            {
                targetHealth.TakeDamage(unit.damageValue);
            }
            audio.PlayOneShot(audio.clip, 1f);
        }
        if (targetHealth.hitPoints <= 0f)
        {
            isAttacking = false;
            unit.enemyInRange = false;
            targetHealth = null;
            targetHitbox = null;
            unit.targetHandler.targetsInRange.Remove(targetHitbox);
            //unit.ClearTarget(targetHitbox);
            yield break;
        }
        isAttacking = false;
    }
}
