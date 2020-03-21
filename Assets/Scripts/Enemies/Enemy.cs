using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Characteristics")]
    public float health;
    public float baseSpeed;
    public GameObject target;
    public float attackRange;
    public float baseDamage;
    public float attackRate;
    [Header("NavMesh")]
    public NavMeshAgent agent;
    [Header("Loot")]
    public GameObject itemDropPrefab;
    public Item[] itemsToDrop;

    private float speed;
    private float damage;
    private float attackCooldown;

    public void Start()
    {
        speed = baseSpeed;
        damage = baseDamage;
        attackCooldown = 0;
    }

    public void takeDamage(float damage)
    {
        health -= damage;

        Debug.Log($"Health: {health}");

        if(health <= 0)
        {
            die();
        }
    }

    public void takeDOT(float dps, float duration)
    {
        StartCoroutine(dotDamage(dps, duration));
    }

    protected IEnumerator dotDamage(float dps, float duration)
    {
        float timeRemaining = duration;

        while(timeRemaining > 0)
        {
            health -= dps * Time.fixedDeltaTime;
            yield return null;
            timeRemaining -= Time.fixedDeltaTime;
        }
    }

    public void changeSpeed(float multiplier, float duration)
    {
        speed *= multiplier;
        
        if(duration > 0)
        {
            Invoke("resetSpeed", duration);
        }
    }

    protected virtual void resetSpeed()
    {
        speed = baseSpeed;
    }

    public virtual void die()
    {
        Debug.Log("Die");

        foreach(Item i in itemsToDrop)
        {
            Item temp = new Item(i.name, i.itemImage);
            target.GetComponent<InventoryManager>().pickupItem(temp);
        }

        Destroy(gameObject);
    }

    public virtual void attack()
    {
        Debug.Log("Attack");
        //Do an attack animation
        target.GetComponent<HealthController>().takeDamage(damage);
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        bool canAttack = (move() <= attackRange);

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.fixedDeltaTime;
        }
        else
        {
            if(canAttack)
            {
                attackCooldown = attackRate;
                attack();
            }
        }
    }

    protected virtual float move()
    {
        if(target != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime);
            agent.SetDestination(target.transform.position);
            return (transform.position - target.transform.position).magnitude;
        }
        else
        {
            return float.MaxValue;
        }
    }

    public void setTarget(GameObject t)
    {
        target = t;   
    }
}
