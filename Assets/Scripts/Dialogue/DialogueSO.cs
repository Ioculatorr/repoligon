using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class DialogueData : ScriptableObject
{
    //[TextArea(3, 10)]
    //public string[] dialogueLines;

    //[Header("Character")]

    //public Sprite characterSprite;
    //public string characterName;
    //public TMP_FontAsset fontName;
    //public float fontNameSize = 60f;

    //[Header("Font")]

    //public TMP_FontAsset font; // Add a reference to the TextMeshPro font asset

    //public float fontSize = 36f;
    //public float fontAnimSpeed = 0.05f;

    //public float fontShakePower = 3f;
    //public bool fontShake = false;

    //[Header("Other")]

    //public bool typingTalk = true;
    //public AudioClip typingSound; // Reference to the typing sound
    //[Range(0.5f, 1.5f), Tooltip("Typing pitch with 2 decimal places")] public float typingPitch = 1f;




    [System.Serializable]
    public struct SentenceData
    {
        [TextArea(3, 10)]
        public string dialogueLine;

        [Header("Character")]
        public Sprite characterSprite;
        public TextMeshProUGUI characterName;

        [Header("Font")]
        public TMP_FontAsset font; // Add a reference to the TextMeshPro font asset
        public float fontSize;
        public float fontAnimSpeed;
        public float fontShakePower;
        public bool fontShake;

        [Header("Other")]
        public bool typingTalk;
        public AudioClip typingSound; // Reference to the typing sound
        [Range(0.5f, 1.5f), Tooltip("Typing pitch with 2 decimal places")]
        public float typingPitch;
    }

    public SentenceData[] sentences;

    // Default values for SentenceData
    public static SentenceData DefaultSentence =>
        new SentenceData
        {
            dialogueLine = "Default Dialogue",
            fontSize = 36f,
            fontAnimSpeed = 0.05f,
            fontShakePower = 3f,
            typingPitch = 1f
            // Add default values for other fields as needed
        };

}
