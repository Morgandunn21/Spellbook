using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellGUI : MonoBehaviour
{
    [Header("Display")]
    public Slider ammoDisplay;
    public Image sliderFill;
    public Image ammoImage;

    private Spell spell;
    private Sprite spellImage;
    private Color ammoColor;
    private float ammoCount;
    private float ammoRemaining;

    // Start is called before the first frame update
    public void Init(Sprite image, Color color, float ammo, Spell s)
    {
        ammoColor = color;
        spellImage = image;
        ammoCount = ammo;
        spell = s;

        ammoRemaining = ammoCount;
        initAmmoImages();
    }

    private void initAmmoImages()
    {
        ammoImage.sprite = spellImage;

        ammoDisplay.minValue = 0;
        ammoDisplay.maxValue = ammoCount;
        ammoDisplay.value = ammoRemaining;

        sliderFill.color = ammoColor;
    }

    //Called when spell is cast
    public void cast(float amountDec)
    {
        ammoRemaining -= amountDec;

        ammoDisplay.value = ammoRemaining;

        if(ammoRemaining <= 0)
        {
            spell.deactivateSpell();
        }
    }
}
