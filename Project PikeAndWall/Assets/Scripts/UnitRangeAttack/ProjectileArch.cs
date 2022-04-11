using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArch : MonoBehaviour
{
    private RangeUnit unit;
    
    private Vector3 shootDirection;
    
    private float speed = 1f;

    public Transform sunrise;
    public Transform sunset;
    public Vector3 center;
    public Vector3 riseRelCenter;
    public Vector3 setRelCenter;

    public float journeyTime = 3f;

    private float startTime;
    
    void Start(){

        
        unit = gameObject.GetComponentInParent<RangeUnit>();
        startTime = Time.time;
        sunrise = unit.transform;
        sunset = unit.targetEnemy.transform;
        center = (sunrise.position + sunset.position) * 0.5f;
        center -= new Vector3(0,1,0);
        riseRelCenter = sunrise.position - center;
        setRelCenter = sunset.position - center;

    }

    private void Update(){

        float fracComplete = (Time.time - startTime) / journeyTime * speed;

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete * speed);
        transform.position += center;

        //transform.position += shootDirection * speed* Time.deltaTime;
    }
    public static float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    public void Setup(Vector3 shootDirection){
        this.shootDirection = shootDirection;
        transform.eulerAngles = new Vector3(GetAngleFromVectorFloat(shootDirection),GetAngleFromVectorFloat(shootDirection), GetAngleFromVectorFloat(shootDirection));
        Destroy(gameObject, 4f);

    }
}
