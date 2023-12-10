using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public DialogueData dialogue;
    public bool inTalkingRange;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTalkingRange = true;
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.StartDialogue(dialogue);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTalkingRange = false;
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.EndDialogue();
        }
    }
}
