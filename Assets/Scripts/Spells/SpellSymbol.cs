using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSymbol : MonoBehaviour
{
    public int index;
    public SpellSelector s;
    public GameObject selectedDisplay;

    private bool selected;

    private void Start()
    {
        selected = false;
        selectedDisplay.SetActive(selected);
    }

    public void mouseEnter()
    {
        Debug.Log($"Mouse Over {index}");
        if(selected == false)
        {
            selected = true;
        }

        selectedDisplay.SetActive(selected);

        s.addToSequence(index);
    }

    public void deselect()
    {
        selected = false;
        selectedDisplay.SetActive(selected);
    }
}
