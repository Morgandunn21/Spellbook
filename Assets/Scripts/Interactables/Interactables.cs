using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public static List<bool> interactions = new List<bool>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static int addInteraction()
    {
        interactions.Add(false);

        return interactions.Count - 1;
    }

    public static void setInteraction(int index, bool check)
    {
        interactions[index] = check;
    }
}
