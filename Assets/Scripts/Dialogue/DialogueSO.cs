using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] dialogueLines;

    [Header("Font")]

    public TMP_FontAsset font; // Add a reference to the TextMeshPro font asset

    public float fontSize = 36f;
    public float fontAnimSpeed = 0.05f;

    public float fontShakePower = 3f;
    public bool fontShake = false;

    [Header("Other")]

    public bool typingTalk = true;
    public AudioClip typingSound; // Reference to the typing sound
    [Range(0.5f, 1.5f), Tooltip("Typing pitch with 2 decimal places")] public float typingPitch = 1f;
    public Sprite characterSprite;

}
