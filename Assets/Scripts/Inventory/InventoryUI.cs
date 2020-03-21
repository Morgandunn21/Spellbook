using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public const int itemCapacity = 4;
    public Image[] itemImages;
    public TextMeshProUGUI[] itemCountTexts;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < itemCapacity; i++)
        {
            Debug.Log(i);
            itemImages[i].sprite = null;
            itemCountTexts[i].text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateItemCount(int index, string count)
    {
        itemCountTexts[index].text = count;
    }

    public void updateItemImage(int index, Sprite img)
    {
        itemImages[index].sprite = img;
    }
}
