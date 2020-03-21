using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBlastEffect : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lifeTime > 0)
        {
            transform.Translate(0, 0, moveSpeed * Time.fixedDeltaTime);
            lifeTime -= Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit");

        if(other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(damage);
        }

        Destroy(gameObject);
    }
}
