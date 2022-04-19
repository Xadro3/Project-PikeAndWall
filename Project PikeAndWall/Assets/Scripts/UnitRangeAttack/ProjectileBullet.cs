using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileBullet : MonoBehaviour
{
    private Vector3 shootDirection;
    private UnitClassRange unit;
    private float speed = 5f;

    void Start(){
        unit = gameObject.GetComponentInParent<UnitClassRange>();
    }


    public static float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }


    private void OnTriggerEnter(Collider collision){
        if (collision.TryGetComponent<Hitbox>(out Hitbox hitbox)) {
            Health hit = hitbox.GetComponentInParent<Health>();
            hit.TakeDamage(unit.damageValue);
            Destroy(gameObject,0f);
        }
    }


    public void Setup(Vector3 shootDirection){
        this.shootDirection = shootDirection;
        transform.eulerAngles = new Vector3(GetAngleFromVectorFloat(shootDirection),GetAngleFromVectorFloat(shootDirection), GetAngleFromVectorFloat(shootDirection));
        Destroy(gameObject, 2f);

    }

    private void Update(){
        transform.position += shootDirection * speed * Time.deltaTime;
    }

}
