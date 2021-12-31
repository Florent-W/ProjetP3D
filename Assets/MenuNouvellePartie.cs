using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNouvellePartie : MonoBehaviour
{
    private Button boutonConfirmer;
    private InputField inputNom;

    // Start is called before the first frame update
    void Start()
    {
        boutonConfirmer = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
        inputNom = this.gameObject.transform.GetChild(2).gameObject.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputNom.text != "" && boutonConfirmer.interactable == false) // Si l'input du nom n'est pas vide, on active le bouton pour confirmer la création de la partie
        {
            boutonConfirmer.interactable = true;
        }
        else if(inputNom.text == "" && boutonConfirmer.interactable == true)
        {
            boutonConfirmer.interactable = false;
        }
    }
}
