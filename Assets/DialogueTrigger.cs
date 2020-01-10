using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private Dialogue m_dialogue = new Dialogue();

    public Dialogue getDialogue()
    {
        return this.m_dialogue;
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(m_dialogue);
      //  m_dialogue.GetSentences().Clear();
    }
}
