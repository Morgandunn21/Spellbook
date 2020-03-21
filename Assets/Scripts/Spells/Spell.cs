using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private GameObject spellPrefab;
    public SpellDescription spellDesc;
    private GameObject spellUI;
    private GameObject spellEffectInstance;
    private SpellGUI spellGUI;
    private bool continuousCast;
    private bool canCast;

    public virtual void init(GameObject spellUI, SpellDescription sD)
    {
        canCast = true;

        spellDesc = sD;

        this.spellUI = spellUI;
        spellUI.SetActive(true);

        spellPrefab = spellDesc.spellPrefab;

        spellGUI = spellUI.GetComponent<SpellGUI>();
        spellGUI.Init(spellDesc.spellSymbol, spellDesc.ammoColor, spellDesc.ammoCount, this);
        
        continuousCast = false;
    }

    public virtual void cast(Transform castPoint)
    {
        if (canCast)
        {
            if (spellDesc.continuous)
            {
                spellEffectInstance = Instantiate(spellPrefab, castPoint);
                continuousCast = true;
            }
            else
            {
                spellEffectInstance = Instantiate(spellPrefab, castPoint.position, castPoint.rotation);
                spellGUI.cast(1);
            }
        }
    }

    public virtual void stopCast()
    {
        Debug.Log(spellDesc);

        if (spellDesc.continuous)
        {
            continuousCast = false;
            Destroy(spellEffectInstance);
        }
    }

    public virtual void deactivateSpell()
    {
        spellUI.SetActive(false);

        canCast = false;
    }

    public void FixedUpdate()
    {
        if (spellDesc != null && spellDesc.continuous)
        {
            if (continuousCast)
            {
                spellGUI.cast(Time.fixedDeltaTime);
            }
        }
    }

    public override string ToString()
    {
        return $"Name: {spellDesc.spellName}\nSequence: {spellDesc.sequence}";
    }

    public int getCastPoint()
    {
        return (int)spellDesc.castingPoint;
    }
}
