using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuStartScript : MonoBehaviour
{
    public ProjetP3DScene1.main Main;

    public void btn_retour_apres_menu_start_click()
    {
        this.Resume();
        Main.jeuEnPause = false;
    }

    public void OuvrirMenu(int positionJoueur)
    {
        // Met le jeu en pause si le jeu n'est pas en cours de combat, si le jeu est déjà en pause, cela permet de reprendre la partie
        if (Main.JoueurManager.Joueurs[positionJoueur].enCombat == false)
        {
            if (Main.jeuEnPause == false)
            {
                this.Pause(positionJoueur);
                Main.jeuEnPause = true;
            }
            else
            {
                if (Main.JoueurManager.Joueurs[positionJoueur].menuPauseActuel == "MenuStart")
                {
                    this.Resume();
                    Main.JoueurManager.Joueurs[positionJoueur].menuPauseActuel = "";
                    Main.jeuEnPause = false;
                }
            }
        }
    }

    /// <summary>
    /// Cette méthode permet d'enlever la pause
    /// </summary>
    public void Resume()
    {
        //   jeu.getUIPopUpMenu().Hide();
        Time.timeScale = 1f;
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;      

        EventSystem eventSystem = GameObject.Find("EventSystem").gameObject.GetComponent<EventSystem>();
        eventSystem.SetSelectedGameObject(null);
        this.gameObject.SetActive(false);
        Main.canvasGameObject[0].transform.GetChild(5).gameObject.SetActive(true); // On remet la minimap
    }

    /// <summary>
    /// Cette méthode permet d'activer la pause et le menu
    /// </summary>
    public void Pause(int positionJoueur)
    {
        // rafraichirEquipe();
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        this.gameObject.SetActive(true);
        EventSystem eventSystem = GameObject.Find("EventSystem").gameObject.GetComponent<EventSystem>();
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(this.gameObject.transform.GetChild(0).gameObject);
        Main.JoueurManager.Joueurs[positionJoueur].menuPauseActuel = "MenuStart";
        Main.canvasGameObject[0].transform.GetChild(5).gameObject.SetActive(false); // On enlève la minimap
    }
}
