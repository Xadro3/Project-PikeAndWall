using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private Vector3 shootDirection;

    public static float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    public void Setup(Vector3 shootDirection){
        this.shootDirection = shootDirection;
        transform.eulerAngles = new Vector3(GetAngleFromVectorFloat(shootDirection),GetAngleFromVectorFloat(shootDirection), GetAngleFromVectorFloat(shootDirection));
        Destroy(gameObject, 1f);

    }

    private void Update(){
        float moveSpeed = 5f;
        transform.position += shootDirection * moveSpeed* Time.deltaTime;
    }

}
