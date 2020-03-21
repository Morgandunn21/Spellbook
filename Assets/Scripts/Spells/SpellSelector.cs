using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSelector : MonoBehaviour
{
    public GameObject[] symbols;
    public Image selectedSpellImage;
    public SpellList spellList;
    public CastingController castControl;
    public float activationDelay;

    private bool primary;
    private List<int> sequence;
    private SpellDescription selectedSpell;

    public void Start()
    {
        sequence = new List<int>();
        selectedSpellImage.gameObject.SetActive(false);
    }

    public void addToSequence(int index)
    {
        Debug.Log($"Added {index} to sequence");
        if (selectedSpell == null)
        {
            if (sequence.Count > 2 && sequence[0] == index)
            {
                Debug.Log("Done");
                selectedSpell = spellList.getSpell(formattedArray(false));

                if (selectedSpell != null)
                {
                    Debug.Log("Success");
                    selectedSpellImage.gameObject.SetActive(true);
                    selectedSpellImage.sprite = selectedSpell.spellSymbol;
                    Invoke("endSelection", activationDelay);
                }
                else
                {
                    selectedSpell = spellList.getSpell(formattedArray(true));

                    if (selectedSpell != null)
                    {
                        Debug.Log("Success");
                        selectedSpellImage.gameObject.SetActive(true);
                        selectedSpellImage.sprite = selectedSpell.spellSymbol;
                        Invoke("endSelection", activationDelay);
                    }
                    else
                    {
                        Debug.Log("Fail");
                        clearAllSelected();
                    }
                }
            }
            else
            {
                Debug.Log(sequence);
                sequence.Add(index);
            }
        }
    }

    private int[] formattedArray(bool reverse)
    {
        int[] temp = new int[sequence.Count];
        int minValue = 6;
        int shiftAmount = 0;

        for (int i = 0; i < sequence.Count; i++)
        {
            if(sequence[i] < minValue)
            {
                minValue = sequence[i];
                shiftAmount = i;
            }
        }
        
        for (int i = 0; i < sequence.Count; i++)
        {
            temp[i] = sequence[(i + shiftAmount) % sequence.Count];
        }

        if (reverse)
        {
            for (int i = 1; i < (sequence.Count+1) / 2; i++)
            {
                int tempIndex = temp[i];

                temp[i] = temp[sequence.Count - (i)];

                temp[sequence.Count - (i)] = tempIndex;
            }
        }

        return temp;
    }

    private void clearAllSelected()
    {
        Debug.Log("Clear");
        for(int i = 0; i < 6; i++)
        {
            symbols[i].GetComponent<SpellSymbol>().deselect();
        }

        sequence.Clear();
    }

    private void endSelection()
    {
        Debug.Log("Spell Selected");
        selectedSpellImage.gameObject.SetActive(false);
        Time.timeScale = 1;
        castControl.setActiveSpell(primary, selectedSpell);
        clearAllSelected();
        selectedSpell = null;
        gameObject.SetActive(false);
    }

    public void setPrimary(bool p)
    {
        Debug.Log($"Primary: {p}");
        primary = p;
    }
}
