using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoutonMenuPokemonScript : MonoBehaviour
{

    public void OnSelect(BaseEventData eventData)
    {
        this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Menu/Background_menu_start_bouton");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Menu/Background_menu_start_bouton_non_selection");
    }
}
