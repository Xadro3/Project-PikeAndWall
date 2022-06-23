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
    private bool animate;

    void Start()
    {
        unit = GetComponentInParent<UnitClass>();
        agent = GetComponentInParent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {  
        //targetHitbox = unit.targetHitbox;
        //targetHealth = unit.targetHealth;
        if (isAttacking && unit.enemyInRange && unit.targetHitbox != null)
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

    public void StopAttack()
    {
        StopCoroutine(Attack());
    }

    private void RotateFaceToEnemy()
    {
        lookDirection = (unit.targetHitbox.transform.position - unit.transform.position).normalized;
        lookRotation = Quaternion.LookRotation(lookDirection);
        unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, lookRotation, Time.deltaTime * unit.turnRate);
    }
    private IEnumerator Attack()
    {
        isAttacking = true;
        while(unit.enemyInRange)
        {
            unit.PlayAnimation("TrAttack");
            yield return new WaitForSeconds(unit.fireRate);
            if((gameObject.name == "Bow" || gameObject.name == "Musket" || gameObject.name == "Pistol") && unit.enemyInRange && unit.targetHitbox != null)
            {
                Transform projectileTransform = Instantiate(projectile, new Vector3(unit.weapon.transform.position.x, unit.weapon.transform.position.y, unit.weapon.transform.position.z), Quaternion.identity);
                if (projectile.name == "BulletRed" || projectile.name == "BulletBlue"){
                    Vector3 shootDirection = new Vector3(unit.targetHitbox.transform.position.x - transform.position.x, unit.targetHitbox.transform.position.y - transform.position.y, unit.targetHitbox.transform.position.z - transform.position.z);
                    projectileTransform.GetComponent<ProjectileBullet>().Setup(shootDirection);
                    projectileTransform.GetComponent<ProjectileBullet>().SetDamage(unit.damageValue);
                }
                if (projectile.name == "ArrowRed" || projectile.name == "ArrowBlue")
                {
                    projectileTransform.GetComponent<ProjectileArch>().SetProjectileArch(unit.weapon.transform, unit.targetHitbox.transform);
                    projectileTransform.GetComponent<ProjectileArch>().SetDamage(unit.damageValue);
                    
                }
            }
            if((gameObject.name == "Spear" || gameObject.name == "Sword" || gameObject.name == "HeavySpear") && unit.enemyInRange && unit.targetHitbox != null)
            {
                unit.targetHealth.TakeDamage(unit.damageValue);
            }
            audio.PlayOneShot(audio.clip, 1f);
            if (unit.targetHealth.hitPoints <= 0f)
            {
                isAttacking = false;
                unit.enemyInRange = false;
                yield break;
            }
        }
        //if (unit.targetHealth.hitPoints <= 0f)
        //{
        //    isAttacking = false;
        //    unit.enemyInRange = false;
        //    yield break;
        //}
        isAttacking = false;
    }
}
