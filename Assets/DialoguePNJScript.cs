using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePNJScript : MonoBehaviour
{
    [SerializeField]
    private GameObject BoiteDialoguePNJ;
    [SerializeField]
    private DialogueTrigger dialogueCombat;
    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private ProjetP3DScene1.main Main;
    private bool estEnCollision;

    private void DialoguePerso()
    {
        // sceneBuilder.AddComponent<DialogueTrigger>();
        // DialogueTrigger dialogueCombat = sceneBuilder.GetComponent<DialogueTrigger>(); 

        if (BoiteDialoguePNJ.GetComponent<Animator>().GetBool("IsOpen") == false && estEnCollision == true)
        {
            dialogueCombat.TriggerDialoguePnj(dialogueManager);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (estEnCollision == false)
        {
            ClassLibrary.Personnage personnage = Main.jeu.setChercherPersonnage(this.gameObject.name);

            GameObject BoiteDialoguePNJDescription = BoiteDialoguePNJ.transform.GetChild(3).gameObject;
            BoiteDialoguePNJDescription.transform.GetChild(0).gameObject.GetComponent<Text>().text = personnage.getNom();
            BoiteDialoguePNJDescription.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Personnages/" + personnage.getNom());

            BoiteDialoguePNJ.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            dialogueCombat.getDialogue().clearDialogue();
            dialogueCombat.ajouterDialogue(personnage.getDialogue());

           // dialogueCombat.getDialogue().AddSentence("Selectionne un pokemon");
            estEnCollision = true;
            DialoguePerso();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        estEnCollision = false;
    }

}
