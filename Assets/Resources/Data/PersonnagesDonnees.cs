using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe ou se trouvent les données des personnages dans le menu de selection
[CreateAssetMenu(fileName = "PersonnagesDonnees", menuName = "ScriptableObjects/PersonnagesDonnees", order = 1)]
public class PersonnagesDonnees : ScriptableObject
{
    public List<Personnage> listePersonnagesDresseurs = new List<Personnage>();
    public List<Personnage> listePersonnagesHeros = new List<Personnage>();
    public List<Personnage> listePersonnagesSprites = new List<Personnage>();
    public List<Personnage> listePersonnagesNintendo = new List<Personnage>();
    public List<Personnage> listePersonnagesFinalFantasy = new List<Personnage>();
}
