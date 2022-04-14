using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArch : MonoBehaviour
{
    private UnitClassRange unit;
    private float speed = 1f;
    public Transform sunrise;
    public Transform sunset;
    public Vector3 center;
    public Vector3 riseRelCenter;
    public Vector3 setRelCenter;
    public float journeyTime = 2f;
    private float startTime;
    
    void Start(){

        unit = gameObject.GetComponentInParent<UnitClassRange>();
        startTime = Time.time;
        sunrise = unit.weapon.transform;
        sunset = unit.targetHitbox.transform;
        center = (sunrise.position + sunset.position) * 0.5f;
        center -= new Vector3(0,1,0);
        riseRelCenter = sunrise.position - center;
        setRelCenter = sunset.position - center;
        Destroy(gameObject, 4f);

    }

    private void OnTriggerEnter(Collider collision){
        if (collision.TryGetComponent<Hitbox>(out Hitbox hitbox)) {
            Destroy(gameObject,0f);
        }
    }

    private void Update(){

        float fracComplete = (Time.time - startTime) / journeyTime * speed;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete * speed);
        transform.position += center;

    }
    public static float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
