using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LabelPrincipaleMenuScript : MonoBehaviour
{
    public bool boutonEstSelectionner = false;

    public void OnSelect(BaseEventData eventData)
    {
        this.gameObject.GetComponent<Text>().color = Color.white;
        this.gameObject.transform.parent.GetComponent<Image>().enabled = true;
        this.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        boutonEstSelectionner = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.gameObject.GetComponent<Text>().color = Color.black;
        this.gameObject.transform.parent.GetComponent<Image>().enabled = false;
        boutonEstSelectionner = false;
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        this.gameObject.GetComponent<Text>().fontSize = 80;
        this.gameObject.transform.parent.GetComponent<Image>().enabled = true;
        this.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        this.gameObject.GetComponent<Text>().fontSize = 61;

        if (boutonEstSelectionner == false)
        {
            this.gameObject.transform.parent.GetComponent<Image>().enabled = false;
        }

        this.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
