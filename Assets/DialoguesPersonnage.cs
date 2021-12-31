using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script pour que le personnage parle avec des lignes de dialogue al�atoire selon le personnage
public class DialoguesPersonnage : MonoBehaviour
{
    private float tempsDialogue;

    private bool coroutineDialogueLancer = false;

    // Start is called before the first frame update
    void Start()
    {
        if (DonneesChargement.voixAleatoireActive == true && (this.gameObject.transform.parent.gameObject.name == "vThirdPersonCamera" || this.gameObject.transform.parent.gameObject.name == "vThirdPersonCamera2")) // On lance la coroutine si on a activ� les voix
        {
            StartCoroutine(lanceRandomDialogue());
        } 
    }

    /*
    // Lit un dialogue random du personnage
    private void lanceDialogueAleatoire()
    {
        if (DonneesChargement.voixAleatoireActive == true)
        { // On regarde si on a autoris� de mettre des voix
            if (DonneesChargement.listePersonnageDialogues != null)
            {
                if (DonneesChargement.listePersonnageDialogues.Count > 0) // On regarde si il y a au moins un dialogue pour le personnage
                {
                    int numeroMusique = Random.Range(0, DonneesChargement.listePersonnageDialogues.Count); // On prend un dialogue al�atoire
                    this.gameObject.GetComponent<AudioSource>().clip = DonneesChargement.listePersonnageDialogues[numeroMusique]; // On met le dialogue
                    this.gameObject.GetComponent<AudioSource>().Play(); // On le joue
                }
            }
        }
    } */

    // Lit un dialogue random du personnage
    private IEnumerator lanceRandomDialogue()
    {
        coroutineDialogueLancer = true; // On dit que la coroutine se lance
        while (DonneesChargement.voixAleatoireActive == true && DonneesChargement.listePersonnageDialogues != null && (this.gameObject.transform.parent.gameObject.name == "vThirdPersonCamera" || this.gameObject.transform.parent.gameObject.name == "vThirdPersonCamera2")) // On regarde si on a autoris� de mettre des voix
        {
            if (DonneesChargement.listePersonnageDialogues.Count > 0)
            { 
              // On regarde si il y a au moins un dialogue pour le personnage
                int numeroMusique = Random.Range(0, DonneesChargement.listePersonnageDialogues.Count); // On prend un dialogue al�atoire
                this.gameObject.GetComponent<AudioSource>().clip = DonneesChargement.listePersonnageDialogues[numeroMusique]; // On met le dialogue
                this.gameObject.GetComponent<AudioSource>().Play(); // On le joue

                yield return new WaitForSeconds(3f + this.gameObject.GetComponent<AudioSource>().clip.length); // On attend quelques secondes apr�s que le dialogue se dit pour le relancer
            }
            else
            {
                yield return new WaitForSeconds(0.1f); // On attend un peu pour pas que la fonction se relance tout de suite
            }
        }
        yield return new WaitForSeconds(0.1f);  // On attend un peu pour pas que la coroutine se relance tout de suite

        coroutineDialogueLancer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineDialogueLancer == false && DonneesChargement.voixAleatoireActive == true) // Si on est pas dans la coroutine (pas de dialogue lanc�) et qu'on a activ� les voix, on peut activ� la coroutine permettant de lancer les dialogues
        {
            StartCoroutine(lanceRandomDialogue());
        }
    }
}
