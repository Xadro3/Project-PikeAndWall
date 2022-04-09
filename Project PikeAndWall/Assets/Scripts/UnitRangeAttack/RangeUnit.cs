using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUnit : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;
    private Enemy targetEnemy;
    private float firerate = 2f;

    public bool enemyInRange;

    void Start(){

    }

    void Update(){

    }
    public void SetTarget(Enemy targetEnemy){
        this.targetEnemy = targetEnemy;
        StartAttack();
    }

    public void StartAttack(){
        StartCoroutine(RangeAttack());
    }

    private IEnumerator RangeAttack(){
        if(enemyInRange == true){
            yield return new WaitForSeconds(firerate);
            Transform bulletTransform = Instantiate(pfBullet, new Vector3(transform.position.x, transform.position.y, transform.position.z),Quaternion.identity);
            Vector3 shootDirection = new Vector3(targetEnemy.transform.position.x - transform.position.x, targetEnemy.transform.position.y - transform.position.y, targetEnemy.transform.position.z - transform.position.z );
            bulletTransform.GetComponent<Bullet>().Setup(shootDirection);
        
            StartCoroutine(RangeAttack());
        }     
    }

}
