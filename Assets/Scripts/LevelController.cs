using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : Interactable
{
    public int levelNum;
    public MenuNavigator mn;

    private void Start()
    {
        interactionPromptText = "F: Continue to Next Level";
    }

    public override void Interact()
    {
        Debug.Log("Level End Interact");
        mn.endLevel(levelNum);
    }
}
