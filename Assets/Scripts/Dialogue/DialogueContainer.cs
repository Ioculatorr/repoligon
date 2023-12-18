using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Container", menuName = "Dialogue System/Dialogue Container")]
public class DialogueContainer : ScriptableObject
{
    public DialogueData[] dialogues;
}
