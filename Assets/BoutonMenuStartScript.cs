using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoutonMenuStartScript : MonoBehaviour
{
    public void OnSelect(BaseEventData eventData)
    {
        /*
        if (this.gameObject.name == "BoutonMenuPokemon")
        {
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Menu/Icone_Pokemon_Menu_Start_Selection");
        }
        else if (this.gameObject.name == "BoutonSauvegarde")
        {
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Menu/Icone_Sauvegarde_Menu_Start_Selection");
        }
        else if (this.gameObject.name == "BoutonRetour")
        {
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Menu/Icone_Retour_Menu_Start_Selection");
        }

        this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Menu/Background_menu_start_bouton");
        */
    }

    public void OnDeselect(BaseEventData eventData)
    {
        /*
        if (this.gameObject.name == "BoutonMenuPokemon")
        {
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Menu/Icone_Pokemon_Menu_Start_Non_Selection");
        }
        else if (this.gameObject.name == "BoutonSauvegarde")
        {
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Menu/Icone_Sauvegarde_Menu_Start_Non_Selection");
        }
        else if (this.gameObject.name == "BoutonRetour")
        {
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Menu/Icone_Retour_Menu_Start_Non_Selection");
        }

        this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Menu/Background_menu_start_bouton_non_selection");
        */
    }
}
