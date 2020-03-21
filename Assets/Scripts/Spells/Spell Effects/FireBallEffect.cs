using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallEffect : MonoBehaviour
{
    public float damage;
    public float radius;
    public float lifeTime;

    private float lifeSpan;
    private float currentScale;

    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = Vector3.zero;
        currentScale = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = Vector3.one * currentScale;

        if(lifeSpan < lifeTime/2)
        {
            currentScale += (radius * Time.fixedDeltaTime) / (lifeTime / 2);
        }
        else
        {
            currentScale -= (radius * Time.fixedDeltaTime) / (lifeTime / 2);

            if (lifeSpan >= lifeTime)
            {
                Destroy(gameObject);
            }
        }

        lifeSpan += Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");

        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(damage);
        }
    }
}
