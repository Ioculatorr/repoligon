using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField] private DialogueContainer initialDialogue;
    [SerializeField] private DialogueContainer currentContainer;
    [SerializeField] private bool inTalkingRange = false;
    private bool alreadyTalking = false;

    [SerializeField] private bool talkInstant = false;
    [SerializeField] private bool uniqueDialogue; 

    DialogueManager isDialogueActive;
    
    [SerializeField] private UnityEvent inTrigger;
    [SerializeField] private UnityEvent outTrigger;
    

    public void Start()
    {
        currentContainer = initialDialogue;
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
            outTrigger.Invoke();
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
        if (other.CompareTag("Player"))
        {
            inTalkingRange = true;
            inTrigger.Invoke();
        }
    }
    
    // Move text handling on interaction to Dialogue Manager

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //canTalk = true;
            inTalkingRange = false;
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.EndDialogue();
            alreadyTalking = false;
            outTrigger.Invoke();
        }
    }
}
