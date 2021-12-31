using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences;

    public float typyingSpeed;

    private Dialogue dialogueStatut;
    public Animator boxDialogueAnimator;

    string phrase;
    bool CoroutineTypeSentenceIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    IEnumerator EnQueueSentences(Dialogue dialogue)
    {
        foreach (string sentence in dialogue.GetSentences())
        {
            sentences.Enqueue(sentence);
            DisplayNextSentence(dialogue);
            yield return null;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        boxDialogueAnimator.SetBool("IsOpen", true);

        dialogueStatut = dialogue;

        dialogueStatut.SetStatutDialogue("Demarrage");

        boxDialogueAnimator.gameObject.transform.GetChild(2).gameObject.GetComponent<Button>().Select();

        //   dialogue.GetSentences().Clear();

        nameText.text = dialogue.GetName();

       // dialogue.AddSentence("test");
       // dialogue.AddSentence("test 2");

        sentences.Clear();

        dialogueStatut.SetStatutDialogue("Parcours");

        foreach (string sentence in dialogue.GetSentences())
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(dialogue);
        // StartCoroutine(EnQueueSentences(dialogueStatut));
    }

    public void DisplayNextSentenceForButton()
    {
        DisplayNextSentence(dialogueStatut);
    }

    public void DisplayNextSentence(Dialogue dialogue)
    {
        if(sentences.Count == 0)
        {
          //  EndDialogue();
            return;
        }

        if (CoroutineTypeSentenceIsRunning)
        {
            dialogue.GetSentences().Remove(phrase);
        }

        string sentence = sentences.Dequeue();
        phrase = sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, dialogue));
    }

    IEnumerator TypeSentence(string sentence, Dialogue dialogue)
    {
        CoroutineTypeSentenceIsRunning = true;
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typyingSpeed);
        }

        CoroutineTypeSentenceIsRunning = false;

        dialogue.GetSentences().Remove(sentence);

        if (sentences.Count > 0)
        {
            yield return new WaitForSeconds(1f);
            DisplayNextSentence(dialogue);
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        boxDialogueAnimator.SetBool("IsOpen", false);
        dialogueStatut.SetStatutDialogue("Fini");
    }
}
