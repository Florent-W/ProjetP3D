using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputJoueurScript : MonoBehaviour
{
    [SerializeField]
    private ProjetP3DScene1.main Main;
    [SerializeField]
    private TableStarterScript tableStarterScript;
    [SerializeField]
    private PersonnageControleScript personnageControleScript;
    [SerializeField]
    private MenuStartScript menuStartScript;

    /*
    private void OnOuvrirMenu()
    {
        // Met le jeu en pause si le jeu n'est pas en cours de combat, si le jeu est déjà en pause, cela permet de reprendre la partie
        if (Main.EnCombat == false)
        {
            if (Main.jeuEnPause == false)
            {
                menuStartScript.Pause();
                Main.jeuEnPause = true;
            }
            else
            {
                if (Main.MenuPauseActuel == "MenuStart")
                {
                    menuStartScript.Resume();
                    Main.MenuPauseActuel = "";
                    Main.jeuEnPause = false;
                }
            }
        }
    }
    */

    private void SelectionnerPokemonStarter()
    {
        if (Main != null)
        {
            if (this.gameObject.name == "vThirdPersonCamera" && this.GetComponent<PlayerInput>().actions["Action1"].ReadValue<float>() == 1 && Main.JoueurManager.Joueurs[0].starterChoisi == false)
            {
                int positionJoueur = 0;
                tableStarterScript.choisirPokemon(this.gameObject, positionJoueur);
            }

            else if (this.gameObject.name == "vThirdPersonCameraJoueur2" && this.GetComponent<PlayerInput>().actions["Action1"].ReadValue<float>() == 1 && Main.JoueurManager.Joueurs[1].starterChoisi == false)
            {
                int positionJoueur = 1;
                tableStarterScript.choisirPokemon(this.gameObject, positionJoueur);
            }
        }
    }

    private void OnAction1()
    {

    }  

    private void OnBougerVersHautBas()
    {
        // Debug.Log(personnageControleScript.moveAxis);
    }

    private void Update()
    {
        SelectionnerPokemonStarter();
    }
}
