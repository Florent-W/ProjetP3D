// Donnees à conserver pendant les temps de chargement
using System.Collections.Generic;
using UnityEngine;

public static class DonneesChargement
{
    public static string nomSceneSuivante = "Scene1Pokeball";
    public static string nomGameObjectJoueur = "Models/Personnages/Pokemon/Red/Red";
    public static string nomGameObjectModel = "Red";

    public static string nomSprite;
    public static string nomSpriteHaut;
    public static string nomSpriteBas;
    public static string nomSpriteDroite;
    public static string nomSpriteGauche;

    public static string dimensionPersonnage = "3D";

    public static bool voixAleatoireActive = false;

    public static AudioClip musiquePersonnage;
    public static List<AudioClip> listePersonnageDialogues;
}
