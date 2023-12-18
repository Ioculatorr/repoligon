using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueReaderTest : MonoBehaviour
{
    public DialogueContainer dialogueContainer;

    private void Update()
    {
        if(dialogueContainer != null)
        {
            foreach (var item in dialogueContainer.dialogues)
            {
                Debug.Log(item.fontSize);
                Debug.Log(item.fontAnimSpeed);
                Debug.Log(item.fontShake);
            }
        }
        else
        {
            Debug.LogWarning("DialogueContainer is not assigned!");
        }
    }
}
