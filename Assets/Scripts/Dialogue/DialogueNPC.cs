using System;
using UnityEngine;
using DG.Tweening;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField] private DialogueContainer initialDialogue;
    [SerializeField] private DialogueContainer currentContainer;
    private bool inTalkingRange = false;
    private bool alreadyTalking = false;
    private bool afterUnique = false;
    [SerializeField] private CanvasGroup npcClickTo;
    
    [SerializeField] private bool talkInstant = false;
    [SerializeField] private bool uniqueDialogue; 

    DialogueManager isDialogueActive;

    public void Start()
    {
        currentContainer = initialDialogue;
        npcClickTo.alpha = 0f;
    }

    public void ReplaceDialogue(DialogueContainer newDialogue)
    {
        // Replace the initial dialogue with the new one
        currentContainer = newDialogue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inTalkingRange == true && alreadyTalking == false && talkInstant == false)
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.StartDialogueContainer(currentContainer);
            alreadyTalking = true;
            npcClickTo.DOFade(0f, 1f);
        }
        else if (inTalkingRange == true && alreadyTalking == false && talkInstant == true)
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.StartDialogueContainer(currentContainer);
            alreadyTalking = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !afterUnique)
        {
            npcClickTo.DOFade(1f, 1f);
            inTalkingRange = true;
        }
    }
    
    // Move text handling on interaction to Dialogue Manager

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
        if (other.CompareTag("Player") && uniqueDialogue)
        {
            //canTalk = true;
            inTalkingRange = false;
            npcClickTo.DOFade(0f, 1f);
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.EndDialogue();
            alreadyTalking = false;
            currentContainer = null;

            afterUnique = true;
        }
    }
}
