using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeEffect : MonoBehaviour
{
    public float DPS;
    public float slowAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().changeSpeed(slowAmount, 0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(DPS * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().changeSpeed(1, 0);
        }
    }
}
