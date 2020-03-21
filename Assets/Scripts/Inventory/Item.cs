using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public new string name;
    public Sprite itemImage;

    public Item(string n, Sprite image)
    {
        name = n;
        itemImage = image;
    }

    public virtual void use(GameObject player)
    {

    }
}
