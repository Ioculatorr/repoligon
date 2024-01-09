using System;
using UnityEngine;
using DG.Tweening;

public class DialogueNPC : MonoBehaviour
{
    public DialogueContainer initialDialogue;
    private DialogueContainer dialogueToSent;
    private bool inTalkingRange = false;
    private bool alreadyTalking = false;
    //private bool canTalk = true;
    [SerializeField] private CanvasGroup npcClickTo;

    DialogueManager isDialogueActive;

    public void Start()
    {
        dialogueToSent = initialDialogue;
        npcClickTo.alpha = 0f;
    }

    public void ReplaceDialogue(DialogueContainer newDialogue)
    {
        // Replace the initial dialogue with the new one
        dialogueToSent = newDialogue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inTalkingRange == true && alreadyTalking == false)
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.StartDialogueContainer(dialogueToSent);
            alreadyTalking = true;
            npcClickTo.DOFade(0f, 1f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcClickTo.DOFade(1f, 1f);
            inTalkingRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //canTalk = true;
            inTalkingRange = false;
            npcClickTo.DOFade(0f, 1f);
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.EndDialogue();
            alreadyTalking = false;
        }
    }
}
