using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    private string m_name;
    private string m_statut_dialogue;

    private List<string> m_sentences = new List<string>();

    public string GetName()
    {
        return this.m_name;
    }

    public void clearDialogue()
    {
        this.m_sentences.Clear();
    }

    public string GetStatutDialogue()
    {
        return this.m_statut_dialogue;
    }

    public List<string> GetSentences()
    {
        return this.m_sentences;
    }

    public void SetStatutDialogue(string statut)
    {
        this.m_statut_dialogue = statut;
    }

    public void AddSentence(string texte)
    {
        this.m_sentences.Add(texte);
    }
}
