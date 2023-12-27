using UnityEngine;
using DG.Tweening;

public class DialogueNPC : MonoBehaviour
{
    public DialogueContainer initialDialogue;
    private DialogueContainer dialogue;
    public bool inTalkingRange;
    //private bool canTalk = true;
    [SerializeField] private CanvasGroup npcClickTo;

    DialogueManager isDialogueActive;

    public void Start()
    {
        dialogue = initialDialogue;
        npcClickTo.alpha = 0f;
    }

    public void ReplaceDialogue(DialogueContainer newDialogue)
    {
        // Replace the initial dialogue with the new one
        dialogue = newDialogue;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //npcClickTo.alpha = 1f;
            //npcClickTo.DOFade(1f, 1f);
            inTalkingRange = true;

            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.StartDialogueContainer(dialogue);
            //canTalk = false;
            //if (Input.GetKeyDown(KeyCode.T) && canTalk == true && isDialogueActive == false)
            //{
            //    npcClickTo.alpha = 0f;
            //    canTalk = false;
            //    dialogueManager.StartDialogue(dialogue);
            //    //npcClickTo.DOFade(0f, 1f);
            //}
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //canTalk = true;
            inTalkingRange = false;
            //npcClickTo.DOFade(0f, 1f);
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.EndDialogue();
        }
    }
}
