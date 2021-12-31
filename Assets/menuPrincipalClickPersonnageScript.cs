using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class menuPrincipalClickPersonnageScript : MonoBehaviour
{
    // Va permettre de changer le personnage indiquer dans le menu principal
    public void changerDresseurMenu()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponent<positionPersonnageMenu>().positionPersonnage);
    }
}
