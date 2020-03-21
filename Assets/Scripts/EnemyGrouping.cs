using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrouping : MonoBehaviour
{
    private Enemy[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new Enemy[transform.childCount];

        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = transform.GetChild(i).GetComponent<Enemy>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTarget(GameObject target)
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.setTarget(target);
        }
    }
}
