using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI CharacterName;
    public Image dialogueImage;
    public CanvasGroup dialogueGroup;
    //public float textAnimationSpeed = 0.05f;

    private int currentLine = 0;
    private DialogueData currentDialogue;
    //private DialogueContainer dialogueContainer;
    private bool isAnimatingText = false;
    public bool isDialogueActive = false;

    [SerializeField] private AudioSource audioSource;

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
    }

    public void StartDialogue(DialogueData dialogue)
    {
        dialogueText.text = ""; // Clear the text before starting a new dialogue

        currentDialogue = dialogue;
        currentLine = 0;

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
    }

    public void ShowNextLine()
    {
        if (!isAnimatingText && currentLine < currentDialogue.dialogueLines.Length)
        {
            StartCoroutine(AnimateText());
        }
        else if (!isAnimatingText && currentLine == currentDialogue.dialogueLines.Length)
        {
            EndDialogue();
        }
    }

    IEnumerator AnimateText()
    {
        isAnimatingText = true;
        dialogueText.text = "";
        string line = currentDialogue.dialogueLines[currentLine];

        for (int i = 0; i < line.Length; i++)
        {
            dialogueText.text += line[i];
            if (currentDialogue.typingSound != null && currentDialogue.typingTalk == true)
            {
                audioSource.PlayOneShot(currentDialogue.typingSound); // Play typing sound
                //currentDialogue.typingPitch = Random.Range(0.7f, 1.5f);
            }

            if (currentDialogue.fontShake == true)
            {
                dialogueText.transform.DOShakePosition(0.2f, currentDialogue.fontShakePower, 15, 10);
            }

            yield return new WaitForSeconds(currentDialogue.fontAnimSpeed);
        }

        isAnimatingText = false;

        // Increment currentLine after the animation is complete
        currentLine++;
    }


    public void EndDialogue()
    {
        isAnimatingText = false;
        currentLine = 0;

        dialogueGroup.DOFade(0f, 1f);

        StopAllCoroutines();

        isDialogueActive = false;

    }
}
