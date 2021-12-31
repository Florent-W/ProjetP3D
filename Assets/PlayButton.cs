using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public string sceneToLoad;
    public InputField champNomJoueur;
    [SerializeField]
    private PersonnageSelectionScript personnageSelectionScript;
    public static ClassLibrary.Dresseur Joueur = new ClassLibrary.Dresseur();
    private string nomGameObjectWithResources;
    [SerializeField]
    private GameObject toggleChoixVoixGameObject;

    public void passerALaSceneChargement()
    {
        this.changeScene();
    }

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
        /*
        else if(this.gameObject.GetComponent<ActiverBoutonNouvellePartie>().MenuActuel == "MenuChargerPartie")
        {
            Joueur = this.gameObject.GetComponent<SauvegardeXml>().Joueur;
        }
        */
        DonneesChargement.nomSceneSuivante = sceneToLoad;

        if (personnageSelectionScript.choixClassePersonnage.value == 0) {
            string nomGameObjectWithResources = "Models/Personnages/Pokemon/" + personnageSelectionScript.listePersonnagesDresseurs[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name + "/" + personnageSelectionScript.listePersonnagesDresseurs[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name;
            DonneesChargement.dimensionPersonnage = "3D";
            DonneesChargement.nomGameObjectJoueur = nomGameObjectWithResources;
            DonneesChargement.nomGameObjectModel = personnageSelectionScript.listePersonnagesDresseurs[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name;
            DonneesChargement.musiquePersonnage = personnageSelectionScript.listePersonnagesDresseurs[personnageSelectionScript.numeroActuelListePersonnages].personnageMusique;
            DonneesChargement.listePersonnageDialogues = personnageSelectionScript.listePersonnagesDresseurs[personnageSelectionScript.numeroActuelListePersonnages].listePersonnageDialogues;
        }
        else if (personnageSelectionScript.choixClassePersonnage.value == 1)
        {
            string nomGameObjectWithResources = "Models/Personnages/" + personnageSelectionScript.listePersonnagesHeros[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name + "/" + personnageSelectionScript.listePersonnagesHeros[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name;
            DonneesChargement.dimensionPersonnage = "3D";
            DonneesChargement.nomGameObjectJoueur = nomGameObjectWithResources;
            DonneesChargement.nomGameObjectModel = personnageSelectionScript.listePersonnagesHeros[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name;
            DonneesChargement.musiquePersonnage = personnageSelectionScript.listePersonnagesHeros[personnageSelectionScript.numeroActuelListePersonnages].personnageMusique;
            DonneesChargement.listePersonnageDialogues = personnageSelectionScript.listePersonnagesHeros[personnageSelectionScript.numeroActuelListePersonnages].listePersonnageDialogues;
        }
        else if (personnageSelectionScript.choixClassePersonnage.value == 2)
        {
            string nomGameObjectWithResources = "Images/Personnages/" + personnageSelectionScript.listePersonnagesSprites[personnageSelectionScript.numeroActuelListePersonnages].personnageSprite.name;
            DonneesChargement.dimensionPersonnage = "2D";
            DonneesChargement.nomGameObjectJoueur = nomGameObjectWithResources;
            DonneesChargement.nomGameObjectModel = personnageSelectionScript.listePersonnagesSprites[personnageSelectionScript.numeroActuelListePersonnages].personnageSprite.name;
            DonneesChargement.nomSpriteHaut = personnageSelectionScript.listePersonnagesSprites[personnageSelectionScript.numeroActuelListePersonnages].personnageSpriteHaut.name;
            DonneesChargement.nomSprite = personnageSelectionScript.listePersonnagesSprites[personnageSelectionScript.numeroActuelListePersonnages].personnageSprite.name;
            DonneesChargement.nomSpriteDroite = personnageSelectionScript.listePersonnagesSprites[personnageSelectionScript.numeroActuelListePersonnages].personnageSpriteDroite.name;
            DonneesChargement.nomSpriteGauche = personnageSelectionScript.listePersonnagesSprites[personnageSelectionScript.numeroActuelListePersonnages].personnageSpriteGauche.name;
            DonneesChargement.musiquePersonnage = personnageSelectionScript.listePersonnagesSprites[personnageSelectionScript.numeroActuelListePersonnages].personnageMusique;
            DonneesChargement.listePersonnageDialogues = personnageSelectionScript.listePersonnagesSprites[personnageSelectionScript.numeroActuelListePersonnages].listePersonnageDialogues;
        }
        else if (personnageSelectionScript.choixClassePersonnage.value == 3)
        {
            string nomGameObjectWithResources = "Models/Personnages/" + personnageSelectionScript.listePersonnagesNintendo[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name + "/" + personnageSelectionScript.listePersonnagesNintendo[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name;
            DonneesChargement.dimensionPersonnage = "3D";
            DonneesChargement.nomGameObjectJoueur = nomGameObjectWithResources;
            DonneesChargement.nomGameObjectModel = personnageSelectionScript.listePersonnagesNintendo[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name;
            DonneesChargement.musiquePersonnage = personnageSelectionScript.listePersonnagesNintendo[personnageSelectionScript.numeroActuelListePersonnages].personnageMusique;
            DonneesChargement.listePersonnageDialogues = personnageSelectionScript.listePersonnagesNintendo[personnageSelectionScript.numeroActuelListePersonnages].listePersonnageDialogues;
        }
        else if (personnageSelectionScript.choixClassePersonnage.value == 4)
        {
            string nomGameObjectWithResources = "Models/Personnages/" + personnageSelectionScript.listePersonnagesFinalFantasy[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name + "/" + personnageSelectionScript.listePersonnagesFinalFantasy[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name;
            DonneesChargement.dimensionPersonnage = "3D";
            DonneesChargement.nomGameObjectJoueur = nomGameObjectWithResources;
            DonneesChargement.nomGameObjectModel = personnageSelectionScript.listePersonnagesFinalFantasy[personnageSelectionScript.numeroActuelListePersonnages].personnageGameObject.name;
            DonneesChargement.musiquePersonnage = personnageSelectionScript.listePersonnagesFinalFantasy[personnageSelectionScript.numeroActuelListePersonnages].personnageMusique;
            DonneesChargement.listePersonnageDialogues = personnageSelectionScript.listePersonnagesFinalFantasy[personnageSelectionScript.numeroActuelListePersonnages].listePersonnageDialogues;
        }

        DonneesChargement.voixAleatoireActive = toggleChoixVoixGameObject.GetComponent<Toggle>().isOn;

        // string nomGameObjectWithResources = UnityEditor.AssetDatabase.GetAssetPath(personnageSelectionScript.listePersonnages[personnageSelectionScript.numeroActuelListePersonnages]);
        // nomGameObjectWithResources = nomGameObjectWithResources.Replace(".FBX", "");
        // nomGameObjectWithResources = nomGameObjectWithResources.Replace(".fbx", "");
        //  nomGameObjectWithResources = nomGameObjectWithResources.Replace(".dae", "");
        //  nomGameObjectWithResources = nomGameObjectWithResources.Replace(".prefab", "");

        // load a new scene       
        SceneManager.LoadScene("SceneChargement");
    }
}