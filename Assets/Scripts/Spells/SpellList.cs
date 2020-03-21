using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    public List<SpellDescription> spells;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void addSpell()
    {

    }

    public void removeSpell()
    {

    }

    public SpellDescription getSpell(int[] seq)
    {
        //go through each spell in the spellbook
        foreach(SpellDescription s in spells)
        {
            //get the sequence for this spell
            int[] sequence = s.sequence;
            //if its length matches the given sequence's length
            if(sequence.Length == seq.Length)
            {
                //flag to see if they match
                bool match = true;

                //loop through each value in the sequence
                for(int i = 0; i < seq.Length; i++)
                {
                    //if any dont match
                    if(sequence[i] != seq[i])
                    {
                        //set match to false and exit the loop
                        match = false;
                        break;
                    }
                }

                //if they do match
                if(match)
                {
                    //set the spell to return equal to this spell
                    return s;
                }
            }
        }

        //return spell
        return null;
    }

    public SpellDescription getSpell(string name)
    {
        //go through each spell in the spellbook
        foreach (SpellDescription s in spells)
        {
            if (s.spellName.Equals(name))
            {
                Debug.Log(s.spellName);
                return s;
            }
        }

        //return spell
        return null;
    }

    public SpellDescription getSpell(int index)
    {
        if(index >=0 && index < spells.Count)
        {
            return spells[index];
        }
        else
        {
            return null;
        }
    }

    public int getSpellCount()
    {
        return spells.Count;
    }
}
