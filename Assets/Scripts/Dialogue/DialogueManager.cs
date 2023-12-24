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
    private DialogueData currentSentence;
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
            else if (currentLine >= currentSentence.dialogueLine.Length)
            {
                // If the text animation is in progress and all lines are shown, end the dialogue
                EndDialogue();
            }
        }
    }

    public void StartDialogue(DialogueData dialogue)
    {
        dialogueText.text = ""; // Clear the text before starting a new dialogue

        currentSentence = dialogue;
        currentLine = 0;

        // Set the font of dialogueText using the font from the DialogueData
        dialogueText.font = currentSentence.font;
        dialogueText.fontSize = currentSentence.fontSize;
        dialogueImage.sprite = currentSentence.characterSprite;

        CharacterName.text = currentSentence.characterName;
        CharacterName.fontSize = currentSentence.fontNameSize;
        CharacterName.font = currentSentence.fontName;

        audioSource.pitch = currentSentence.typingPitch;

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
        if (!isAnimatingText && currentLine < currentSentence.dialogueLine.Length)
        {
            StartCoroutine(AnimateText());
        }
        else if (!isAnimatingText && currentLine == currentSentence.dialogueLine.Length)
        {
            EndDialogue();
        }
    }

    IEnumerator AnimateText()
    {
        isAnimatingText = true;
        dialogueText.text = "";
        string line = currentSentence.dialogueLine[currentLine].ToString();

        for (int i = 0; i < line.Length; i++)
        {
            dialogueText.text += line[i];
            if (currentSentence.typingSound != null && currentSentence.typingTalk == true)
            {
                audioSource.PlayOneShot(currentSentence.typingSound); // Play typing sound
                //currentSentence.typingPitch = Random.Range(0.7f, 1.5f);
            }

            if (currentSentence.fontShake == true)
            {
                dialogueText.transform.DOShakePosition(0.2f, currentSentence.fontShakePower, 15, 10);
            }

            yield return new WaitForSeconds(currentSentence.fontAnimSpeed);
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
