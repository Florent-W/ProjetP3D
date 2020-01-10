using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SauvegardeDonnees
{
    public ClassLibrary.Personnage Joueur = new ClassLibrary.Personnage();

    public void setJoueurDonnees(ClassLibrary.Personnage Joueur)
    {
        this.Joueur = Joueur;
    }
}
