using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeadWizardInteractable : Interactable
{
    [Header("Head Wizard Variables")]
    public TextMeshProUGUI words;
    public Item key;

    private string[] sayings = {"Thank The Lords you survived! Hurry over here, there is much to discuss...",
                                "The Underworld is invading, I have a skeleton captured in that room to your left as we speak",
                                "Here, add this spell to your Spellbook. It will help you destroy that monster",
                                "Now go! Bring me the head of the Skeleton so I know it has been slain",
                                "You need to bring me back the Skull! You must prove you can fight",
                                "Great Job! Now that I know you can fight I need you to clear the rest of the building",
                                "Here is the key to the lower levels, there should be more spells down there so keep an eye out!",
                                "I believe in you! We shall push back this evil no matter what!",
                                "Why are you still here? Go rid the world of that evil!",
                                "Are you scared? Thats understandable but you must be brave now young warrior!",
                                "Are you serious? You need to leave man...",
                                "Im not going to chat all day with you.",
                                "Alright, we'll start over then"
                                };

    private string[] playerResponses = {"F: Whats going on?",
                                        "F: Why would you do that?",
                                        "F: What? No way that thing will kill me!",
                                        "F: You aren't listening are you...",
                                        "F: I'll go get it geez...",
                                        "F: I guess I have no choice",
                                        "F: More spells huh?.. I'll have to get them all",
                                        "F: Yes! I can do this!",
                                        "F: OK maybe I cant do this...",
                                        "F: Look skeletons are scary!",
                                        "F: Just explain it again?",
                                        "F: Sounds great"
                                        };
    private int interactionCount;

    private void Start()
    {
        interactionCount = 0;
        words.text = sayings[0];
        interactionPromptText = playerResponses[0];
    }

    public override void Interact()
    {
        Debug.Log(interactionCount);
        switch(interactionCount)
        {
            case 3:
                if(!player.GetComponent<InventoryManager>().hasItem("Skull"))
                {
                    Debug.Log("Check1");
                }
                else
                {
                    Debug.Log("Check2");
                    interactionCount++;
                    player.GetComponent<InventoryManager>().useItem("Skull");
                }
                break;
            case 4:
                if (!player.GetComponent<InventoryManager>().hasItem("Skull"))
                {
                    Debug.Log("Check3");
                    interactionCount--; ;
                }
                else
                {
                    player.GetComponent<InventoryManager>().useItem("Skull");
                }
                break;
            case 6:
                player.GetComponent<InventoryManager>().pickupItem(key);
                break;
            case 13:
                interactionCount = 6;
                break;
            default:
                break;
        }

        interactionCount++;
        interactionPromptText = playerResponses[interactionCount];
        MenuNavigator.showInteractPrompt(interactionPromptText);
        words.text = sayings[interactionCount];
    }
}
