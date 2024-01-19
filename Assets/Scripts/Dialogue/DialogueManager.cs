using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private CharacterController playerMovement;
    
    
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI CharacterName;
    public Image dialogueImage;
    public CanvasGroup dialogueGroup;
    //public float textAnimationSpeed = 0.05f;

    private int currentLine = 0;
    private DialogueData currentDialogue;
    private DialogueContainer receiveddialogueContainer;
    private bool isAnimatingText = false;
    public bool isDialogueActive = false;

    [SerializeField] private AudioSource audioSource;

    private float instantAnimSpeed = 0f;
    bool useSkip = false;


    private void Start()
    {
        dialogueText.text = "";
        dialogueGroup.alpha = 0f;

        //foreach (var item in dialogueContainer.dialogues)
        //{

        //}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isDialogueActive == true)
        {
            if (!isAnimatingText)
            {
                ShowNextLine();

            }
            else if (currentLine >= currentDialogue.dialogueLines.Length)
            {
                // If the text animation is in progress and all lines are shown, end the dialogue
                EndDialogue();
            }
        }
        if (currentDialogue != null && currentDialogue.instantTransition == true && !isAnimatingText && isDialogueActive == true)
        {
            ShowNextLine();
        }
        SkipText();

        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    StartDialogueContainer();
        //}
    }

    public void StartDialogueContainer(DialogueContainer dialogueContainer)
    {
        receiveddialogueContainer = dialogueContainer;

        currentLine = 0;
        StartDialogue(dialogueContainer.dialogues[currentLine]);
    }

    public void StartDialogue(DialogueData dialogue)
    {
        dialogueText.text = ""; // Clear the text before starting a new dialogue

        currentDialogue = dialogue;

        // Set the font of dialogueText using the font from the DialogueData
        dialogueText.font = currentDialogue.font;
        dialogueText.fontSize = currentDialogue.fontSize;
        dialogueImage.sprite = currentDialogue.characterSprite;

        CharacterName.text = currentDialogue.characterName;
        CharacterName.fontSize = currentDialogue.fontNameSize;
        CharacterName.font = currentDialogue.fontName;

        audioSource.pitch = currentDialogue.typingPitch;

        dialogueGroup.DOFade(1f, 1f);

        isDialogueActive = true;
        StopAllCoroutines();

        // Check if the coroutine is already running before starting a new one
        if (!isAnimatingText)
        {
            StartCoroutine(AnimateText());
        }

        playerMovement.enabled = false;
    }

    public void ShowNextLine()
    {
        useSkip = false;

        if (!isAnimatingText && currentLine < receiveddialogueContainer.dialogues.Length - 1)
        {
            currentLine++;
            StartDialogue(receiveddialogueContainer.dialogues[currentLine]);
        }
        else if (!isAnimatingText && currentLine >= receiveddialogueContainer.dialogues.Length - 1)
        {
            EndDialogue();
        }
    }

    IEnumerator AnimateText()
    {
        isAnimatingText = true;
        dialogueText.text = "";
        string line = currentDialogue.dialogueLines;

        for (int i = 0; i < line.Length; i++)
        {
            //dialogueText.text += line[i];

            char currentChar = line[i];

            dialogueText.text += currentChar;


            if (currentChar != ' ' && currentDialogue.typingSound != null && currentDialogue.typingTalk == true && useSkip == false)
            {
                audioSource.PlayOneShot(currentDialogue.typingSound); // Play typing sound
                audioSource.pitch = Random.Range(0.9f, 1.2f);
            }

            if (currentDialogue.fontShake == true)
            {
                dialogueText.transform.DOShakePosition(0.2f, currentDialogue.fontShakePower, 15, 10);
            }


            float waitTime = useSkip ? instantAnimSpeed : currentDialogue.fontAnimSpeed;

            yield return new WaitForSeconds(waitTime);
            
            //useSkip = false;

        }

        audioSource.pitch = 1.0f;

        isAnimatingText = false;
    }


    public void EndDialogue()
    {
        isAnimatingText = false;
        currentLine = 0;

        dialogueGroup.DOFade(0f, 1f);

        StopAllCoroutines();

        isDialogueActive = false;

        playerMovement.enabled = true;

    }

    private void SkipText()
    {
        if (Input.GetKey(KeyCode.R))
        {
            useSkip = true;
        }
    }
}
