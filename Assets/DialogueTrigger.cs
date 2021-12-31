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

    public void ajouterDialogue(Dialogue dialogue)
    {
        for(int i = 0; i < dialogue.GetSentences().Count; i++)
        {
            this.m_dialogue.AddSentence(dialogue.GetSentences()[i]);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(m_dialogue);
      //  m_dialogue.GetSentences().Clear();
    }

    public void TriggerDialoguePnj(DialogueManager dialogueManager)
    {
        dialogueManager.StartDialogue(m_dialogue);
        //  m_dialogue.GetSentences().Clear();
    }
}
