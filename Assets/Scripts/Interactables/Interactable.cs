using UnityEngine;
/*Author: Akram Taghavi-Burris
 * Created: 10-20-19
 * Modified: 10-20-19
 * Description: defines interactables*/
public class Interactable : MonoBehaviour
{
    /****VARIABLES****/
    [Header("Interactable Variables")]
    public float radius = 3f; //how close to interact
    public Transform interactionTransform; //tranform of Interactable (does not have to be the object, but an empty object defines the exact location of where the inateraction takes place)
    public bool isFocused = false; //is interactable selected
    public GameObject player; //the player
    public bool hasInteracted = false;//check if interacted with
    public bool interactOnContact;
    public string interactionPromptText = "Press E to interact";

    private bool playerInCollider;

    private void Awake()
    {
        var col = gameObject.AddComponent<SphereCollider>();
        col.radius = radius;
        col.isTrigger = true;
        col.enabled = true;
    }

    private void Update()
    {
        if(playerInCollider && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    public virtual void Interact()
    {//virtual methods are parent methods that are called and can be added to
        Debug.Log("Interact");
    }

    private void OnDrawGizmosSelected()
    { //gizmo to display object

        if (interactionTransform == null)
        {//if we have no interaction Transform, set the transform to the interatable own transform
            interactionTransform = transform;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            Debug.Log("Player Enter");
            player = other.gameObject;

            if(interactOnContact)
            {
                Interact();
            }
            else
            {
                MenuNavigator.showInteractPrompt(interactionPromptText);
                playerInCollider = true;
                Debug.Log(playerInCollider);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            Debug.Log("Player Exit");
            player = null;

            MenuNavigator.hideInteractPrompt();
            playerInCollider = false;
        }
    }

}//end class
