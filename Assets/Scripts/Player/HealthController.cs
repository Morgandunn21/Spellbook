using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Slider healthBar;
    public float baseHealth;

    private float health;
    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;

        healthBar.maxValue = baseHealth;
        healthBar.value = health;
    }

    public void takeDamage(float damage)
    {
        //add hurt animation

        health -= damage;
        healthBar.value = health;

        if(health <= 0)
        {
            die();
        }
    }

    public void takeDOT(float dps, float duration)
    {
        StartCoroutine(dotDamage(dps, duration));
    }

    private IEnumerator dotDamage(float dps, float duration)
    {
        float timeRemaining = duration;

        while (timeRemaining > 0)
        {
            takeDamage(dps * Time.fixedDeltaTime);
            yield return null;
            timeRemaining -= Time.fixedDeltaTime;
        }
    }

    private void die()
    {
        MenuNavigator.showEndScreen();
        //Add death animation;
        Destroy(gameObject);
    }
}
