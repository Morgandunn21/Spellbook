using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingController : MonoBehaviour
{
    [Header("Spell GUIs")]
    public GameObject leftGUI;
    public GameObject rightGUI;

    [Header("Cast Points")]
    public GameObject[] castingPoints;

    [Header("Spell Selector")]
    public SpellSelector spellSelector;
    
    private SpellList spellList;
    private bool canCast;

    // Start is called before the first frame update
    void Start()
    {
        canCast = true;

        spellList = GetComponent<SpellList>();

        //setActiveSpell(true, spellList.getSpell("Fire Ball"));
        //setActiveSpell(false, spellList.getSpell("Earthquake"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1 && canCast)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    canCast = false;
                    Time.timeScale = 0.25f;
                    spellSelector.gameObject.SetActive(true);
                    spellSelector.setPrimary(true);
                }
                else
                {
                    Spell activeSpell1 = gameObject.GetComponents<Spell>()[0];
                    Transform t;

                    if (activeSpell1.getCastPoint() == (int)SpellDescription.CastingPoints.RANGED)
                    {
                        t = getRangedCastPoint();
                    }
                    else
                    {
                        t = castingPoints[activeSpell1.getCastPoint()].transform;
                    }

                    if (activeSpell1 == null)
                    {
                        Debug.Log("No Active Spell");
                    }
                    else
                    {
                        activeSpell1.cast(t);
                    }
                }                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Spell activeSpell1 = gameObject.GetComponents<Spell>()[0];

                if (activeSpell1 != null)
                {
                    activeSpell1.stopCast();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    canCast = false;
                    Time.timeScale = 0.25f;
                    spellSelector.gameObject.SetActive(true);
                    spellSelector.setPrimary(false);
                }
                else
                {
                    Spell activeSpell2 = gameObject.GetComponents<Spell>()[1];
                    Transform t;

                    if (activeSpell2.getCastPoint() == (int)SpellDescription.CastingPoints.RANGED)
                    {
                        t = getRangedCastPoint();
                    }
                    else
                    {
                        t = castingPoints[activeSpell2.getCastPoint()].transform;
                    }

                    if (activeSpell2 == null)
                    {
                        Debug.Log("No Active Spell");
                    }
                    else
                    {
                        activeSpell2.cast(t);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                Spell activeSpell2 = gameObject.GetComponents<Spell>()[1];

                if (activeSpell2 != null)
                {
                    activeSpell2.stopCast();
                }
            }
        }
    }

    private Transform getRangedCastPoint()
    {
        Transform t = castingPoints[(int)SpellDescription.CastingPoints.RANGED].transform;

        Vector3 camPos = Camera.main.transform.position;
        Vector3 aimPoint = castingPoints[castingPoints.Length - 1].transform.position;

        Ray ray = new Ray(camPos, aimPoint-camPos);
        RaycastHit hit;

        Debug.DrawRay(camPos, (aimPoint - camPos).normalized * 20f, Color.blue, 5f);

        if(Physics.Raycast(ray, out hit))
        {
            t.position = hit.point;
        }
        else
        {
            t.position = camPos + (aimPoint - camPos).normalized * 20f;
        }

        return t;
    }

    public void setActiveSpell(bool leftSpell, SpellDescription spell)
    {
        if (leftSpell)
        {
            gameObject.GetComponents<Spell>()[0].init(leftGUI, spell);
        }
        else
        {
            gameObject.GetComponents<Spell>()[1].init(rightGUI, spell);
        }

        canCast = true;
    }
}
