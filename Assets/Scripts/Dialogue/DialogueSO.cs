using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] dialogueLines;

    [Header("Character")]

    public Sprite characterSprite;
    public string characterName = ("Noone");
    public TMP_FontAsset fontName;
    public float fontNameSize = 60f;

    [Header("Font")]

    public TMP_FontAsset font; // Add a reference to the TextMeshPro font asset

    public float fontSize = 36f;
    public float fontAnimSpeed = 0.05f;

    [Header("Font Effects")]

    public bool fontShake = false;
    public float fontShakePower = 3f;

    [Header("Other")]

    public bool typingTalk = true;
    public AudioClip typingSound; // Reference to the typing sound
    [Range(0.5f, 1.5f), Tooltip("Typing pitch with 2 decimal places")] public float typingPitch = 1f;

}
