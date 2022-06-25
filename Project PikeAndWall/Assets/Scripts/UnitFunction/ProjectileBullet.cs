using UnityEngine;
using System.Collections;


public class ProjectileBullet : MonoBehaviour
{
    private Vector3 shootDirection;
    private UnitClass unit;
    private float speed = 5f;
    private int damageValue;
    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        StartCoroutine(TimeToLive());

    }

    private IEnumerator TimeToLive()
    {
        Destroy(gameObject, 2f);
        yield break;
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    public void SetDamage(int damageValue)
    {
        this.damageValue = damageValue;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Hitbox hitbox))
        {
            if (collision.CompareTag("Enemy") && gameObject.CompareTag("Player") || collision.CompareTag("Player") && gameObject.CompareTag("Enemy"))
            {
                Health hit = hitbox.GetComponentInParent<Health>();
                hit.TakeDamage(damageValue);
                Destroy(gameObject,0f);
            }
        }


        //if (collision.TryGetComponent<Hitbox>(out Hitbox hitbox))
        //{
        //    Health hit = hitbox.GetComponentInParent<Health>();
        //    hit.TakeDamage(damageValue);
        //    Destroy(gameObject, 0f);
        //}
    }


    public void Setup(Vector3 shootDirection)
    {
        this.shootDirection = shootDirection;
        transform.eulerAngles = new Vector3(GetAngleFromVectorFloat(shootDirection), GetAngleFromVectorFloat(shootDirection), GetAngleFromVectorFloat(shootDirection));
        Destroy(gameObject, 2f);

    }

    private void Update()
    {
        //transform.position += shootDirection * speed * Time.deltaTime;
        body.MovePosition(transform.position + shootDirection * speed * Time.deltaTime);
    }

}
