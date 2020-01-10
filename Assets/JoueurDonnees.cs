using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JoueurDonnees
{
    public string nom; 

    public ClassLibrary.Personnage Joueur = new ClassLibrary.Personnage();

    public void setJoueurDonnees(ClassLibrary.Personnage Joueur)
    {
        this.Joueur = Joueur;
    }

    public JoueurDonnees(ClassLibrary.Personnage Joueur)
    {
        this.nom = Joueur.getNom();
    }
}
