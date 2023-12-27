using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChanger : MonoBehaviour
{
    public DialogueContainer newDialogue;
    [SerializeField] private GameObject changedNPC;

    private DialogueNPC npcInteraction;

    void Start()
    {
        // Get the NPCInteraction component from the NPC GameObject
        npcInteraction = changedNPC.GetComponent<DialogueNPC>();
    }

    // Update is called once per frame
    public void ChangeDialogue()
    {
        // Check if the NPCInteraction component is not null
        if (npcInteraction != null)
        {
            // Change the dialogue when the script starts
            npcInteraction.ReplaceDialogue(newDialogue);
        }
    }
}
