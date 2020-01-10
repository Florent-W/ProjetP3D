using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActiverBoutonNouvellePartie : MonoBehaviour
{
    public GameObject MenuPrincipal, MenuNouvellePartie, MenuChargerPartie, MenuOptions;
    public string MenuActuel = "MenuPrincipal";

    public void ChoixNom()
    {
        //   MenuPrincipal.SetActive(false);
        //   MenuNouvellePartie.SetActive(true);
        MenuPrincipal.GetComponent<Animator>().SetBool("EstDansMenuPrincipal", false);
        MenuNouvellePartie.GetComponent<Animator>().SetBool("EstDansMenuNouvellePartie", true);
        MenuActuel = "MenuNouvellePartie";
    }

    public void ChoixChargerPartie()
    {
        // MenuPrincipal.SetActive(false);
        // MenuChargerPartie.SetActive(true);
        MenuPrincipal.GetComponent<Animator>().SetBool("EstDansMenuPrincipal", false);
        MenuChargerPartie.GetComponent<Animator>().SetBool("EstDansMenuChargerPartie", true);
        MenuActuel = "MenuChargerPartie";
    }

    public void ChoixOptions()
    {
        // MenuPrincipal.SetActive(false);
        // MenuOptions.SetActive(true);
        MenuPrincipal.GetComponent<Animator>().SetBool("EstDansMenuPrincipal", false);
        MenuOptions.GetComponent<Animator>().SetBool("EstDansMenuOptions", true);
        MenuActuel = "MenuOptions";
    }

    public void QuitterJeu()
    {
        Application.Quit();
    }

    public void RetourMenuPrincipalApresMenuNouvellePartie()
    {
        // MenuNouvellePartie.SetActive(false);
        //  MenuPrincipal.SetActive(true);
        MenuNouvellePartie.GetComponent<Animator>().SetBool("EstDansMenuNouvellePartie", false);
        MenuPrincipal.GetComponent<Animator>().SetBool("EstDansMenuPrincipal", true);
        MenuActuel = "MenuPrincipal";
        MenuPrincipal.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Button>().Select();
    }

    public void RetourMenuPrincipalApresMenuChargerPartie()
    {
        // MenuChargerPartie.SetActive(false);
        // MenuPrincipal.SetActive(true);
        MenuChargerPartie.GetComponent<Animator>().SetBool("EstDansMenuChargerPartie", false);
        MenuPrincipal.GetComponent<Animator>().SetBool("EstDansMenuPrincipal", true);
        MenuActuel = "MenuPrincipal";
        MenuPrincipal.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Button>().Select();
    }

    public void RetourMenuPrincipalApresMenuOption()
    {
        // MenuOptions.SetActive(false);
        //  MenuPrincipal.SetActive(true);
        MenuOptions.GetComponent<Animator>().SetBool("EstDansMenuOptions", false);
        MenuPrincipal.GetComponent<Animator>().SetBool("EstDansMenuPrincipal", true);
        MenuActuel = "MenuPrincipal";
        MenuPrincipal.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Button>().Select();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
