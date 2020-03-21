using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public const int itemCapacity = 4;
    public InventoryUI invUI;
    public Item[] items;
    public int[] itemCounts;

    public bool hasItem(string itemName)
    {
        Debug.Log("Called hasItem()");
        return (findItem(itemName, false) != null);
    }

    public void pickupItem(Item item)
    {
        Debug.Log("Called pickupItem()");
        for(int i = 0; i < itemCapacity; i++)
        {
            if(items[i] != null && item.name.Equals(items[i].name))
            {
                itemCounts[i]++;
                invUI.updateItemCount(i, $"{itemCounts[i]}");
                return;
            }
        }

        for (int i = 0; i < itemCapacity; i++)
        {
            if (items[i] == null)
            {
                Item temp = new Item(item.name, item.itemImage);
                items[i] = temp;
                itemCounts[i] = 1;

                invUI.updateItemImage(i, items[i].itemImage);
                invUI.updateItemCount(i, $"{itemCounts[i]}");

                Debug.Log($"Added Item {items[i].name} to index {i}");
                return;
            }
        }

        Debug.Log("No space in inventory");
    }

    public void useItem(int index)
    {
        Debug.Log("Called useItem(int)");
        items[index].use(gameObject);
        itemCounts[index]--;
        invUI.updateItemCount(index, $"{itemCounts[index]}");

        if (itemCounts[index] <= 0)
        {
            items[index] = null;
            invUI.updateItemImage(index, null);
            invUI.updateItemCount(index, "");
        }
    }

    public void useItem(string itemName)
    {
        Debug.Log("Called useItem(string)");
        Item temp = findItem(itemName, true);

        temp.use(gameObject);
    }

    private Item findItem(string itemName, bool remove)
    {
        Debug.Log("Called findItem()");
        for(int i = 0; i < itemCapacity; i++)
        {
            if(items[i] != null)
            {
                Debug.Log(items[i].name);
                if (items[i].name.Equals(itemName))
                {
                    Item temp = items[i];
                    if (remove)
                    {
                        itemCounts[i]--;
                        invUI.updateItemCount(i, $"{itemCounts[i]}");

                        if (itemCounts[i] <= 0)
                        {
                            items[i] = null;
                            invUI.updateItemImage(i, null);
                            invUI.updateItemCount(i, "");
                        }
                    }
                    return temp;
                }
            }
            else
            {
                Debug.Log($"Item {i} is null");
            }
        }

        return null;
    }
}
