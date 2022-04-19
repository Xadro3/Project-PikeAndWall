using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClassRange : MonoBehaviour
{
    [SerializeField] private Transform projectile;
    [SerializeField] private float unitFirerate;
    [SerializeField] public float unitRange;

    public Transform weapon;
    public Hitbox targetHitbox;

    public bool enemyInRange;

    void Start(){
        weapon = gameObject.transform.Find("UnitAttack").transform.Find("Weapon");
    }

    void Update(){ 
    }
    public void SetTarget(Hitbox targetHitbox){
        this.targetHitbox = targetHitbox;
        StartAttack();
    }

    public void StartAttack(){
        StartCoroutine(RangeAttack());
    }

    private IEnumerator RangeAttack(){
        yield return new WaitForSeconds(unitFirerate);
        if(enemyInRange == true){
            Transform projectileTransform = Instantiate(projectile, new Vector3(weapon.transform.position.x, weapon.transform.position.y, weapon.transform.position.z),Quaternion.identity);
            projectileTransform.transform.parent = weapon.transform;
            Vector3 shootDirection = new Vector3(targetHitbox.transform.position.x - transform.position.x, targetHitbox.transform.position.y - transform.position.y, targetHitbox.transform.position.z - transform.position.z );
            if(projectile.name == "pfBullet"){
                projectileTransform.GetComponent<ProjectileBullet>().Setup(shootDirection);
            }            
        
            StartCoroutine(RangeAttack());
        }     
    }

}
