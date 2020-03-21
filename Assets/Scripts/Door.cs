using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [Header("Door Variables")]
    public EnemyGrouping enemyGroup;
    public GameObject openPrefab;
    public bool locked;

    // Start is called before the first frame update
    void Start()
    {
        if(locked)
        {
            interactionPromptText = "F: Use Key to Unlock Door";
        }
        else
        {
            interactionPromptText = "";
        }
    }

    public override void Interact()
    {
        Debug.Log("Door Interact");
        if(locked && player.GetComponent<InventoryManager>().hasItem("Key"))
        {
            player.GetComponent<InventoryManager>().useItem("Key");

            locked = false;

            interactionPromptText = "";

            MenuNavigator.showInteractPrompt(interactionPromptText);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!locked && collision.gameObject.CompareTag("Player"))
        {
            if(enemyGroup != null)
            {
                enemyGroup.setTarget(collision.gameObject);
            }
            Instantiate(openPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
