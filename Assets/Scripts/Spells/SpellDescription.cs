using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Spell Description", order = 1)]
public class SpellDescription : ScriptableObject
{
    public enum CastingPoints
    {
        HAND = 0,
        SELF = 1,
        RANGED = 2
    }

    public CastingPoints castingPoint;

    public string spellName;

    public string spellDesc;

    public Sprite spellSymbol;

    public Color ammoColor;

    public int[] sequence;

    public bool continuous;

    public float ammoCount;

    public GameObject spellPrefab;
}
