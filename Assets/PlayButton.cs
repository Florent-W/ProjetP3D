using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public string sceneToLoad;
    public InputField champNomJoueur;
    public static ClassLibrary.Personnage Joueur = new ClassLibrary.Personnage();

    public void changeScene()
    {
        if (this.gameObject.GetComponent<ActiverBoutonNouvellePartie>().MenuActuel == "MenuNouvellePartie")
        {
            if (champNomJoueur.text != "")
            {
                Joueur.setNomPersonnage(champNomJoueur.text);
            }
            else
            {
                Joueur.setNomPersonnage("Joueur");
            }
        }
        else if(this.gameObject.GetComponent<ActiverBoutonNouvellePartie>().MenuActuel == "MenuChargerPartie")
        {
            Joueur = this.gameObject.GetComponent<SauvegardeXml>().Joueur;
        }

        // load a new scene
        SceneManager.LoadScene(sceneToLoad);
    }
}