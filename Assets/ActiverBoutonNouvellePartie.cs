using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActiverBoutonNouvellePartie : MonoBehaviour
{
    public GameObject MenuPrincipal, MenuNouvellePartie, MenuChargerPartie, MenuOptions, Menu;
    public string MenuActuel = "MenuPrincipal";
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip bruitageBouton;
    [SerializeField]
    private GameObject Sauvegardes;

    // Enleve le menu selectionner après que l'animation soit fini
    private IEnumerator enleverMenu(string menuAEnlever)
    {
        AnimatorStateInfo stateActuelAnimationMenu = Menu.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        
        if ((stateActuelAnimationMenu.IsName("MenuVersMenuChargerPartie") == true || stateActuelAnimationMenu.IsName("MenuVersMenuOptions") == true) && Menu.GetComponent<Animator>().GetAnimatorTransitionInfo(0).normalizedTime < 1)
        {
            yield return new WaitForSeconds(stateActuelAnimationMenu.length); // On attend la durée de la transition vers le menu principal pour enlever le menu actuel
        }
        else if(stateActuelAnimationMenu.IsName("MenuVersMenuNouvellePartie") == true) {
            yield return new WaitForSeconds(stateActuelAnimationMenu.length + 0.5f);
        }

        if (menuAEnlever == "MenuNouvellePartie") // Menu a enlever
        {
            MenuNouvellePartie.SetActive(false);
        }
        else if(menuAEnlever == "MenuChargerPartie")
        {
            MenuChargerPartie.SetActive(false);

            foreach(Transform child in Sauvegardes.transform) // Supprime les emplacements de sauvegardes pour ne pas qu'ils se dupliquent après avoir retourner plusieurs dans le menu
            {
                if(child.gameObject.name != "1")
                {
                    Destroy(child.gameObject);
                }
            }
        }
        else if(menuAEnlever == "MenuOptions")
        {
            MenuOptions.SetActive(false);
        }
    }

    public void ChoixNom() // Menu de nouvelle partie
    {
        StartCoroutine(choixNomCharger());
    }

    private IEnumerator choixNomCharger()
    {
        audioSource.PlayOneShot(bruitageBouton);
        MenuNouvellePartie.SetActive(true);
        Menu.GetComponent<Animator>().SetInteger("EstDansMenu", 1);
        MenuActuel = "MenuNouvellePartie";
        // MenuNouvellePartie.transform.GetChild(2).gameObject.GetComponent<InputField>().Select();
        yield return new WaitForSeconds(0.01f);
        MenuNouvellePartie.transform.GetChild(4).gameObject.transform.GetChild(4).gameObject.transform.GetChild(0).GetComponent<Button>().Select();
    }

        public void ChoixChargerPartie() // Menu de chargement de partie
    {
        audioSource.PlayOneShot(bruitageBouton);
        MenuChargerPartie.SetActive(true);
        Menu.GetComponent<Animator>().SetInteger("EstDansMenu", 2);
        MenuActuel = "MenuChargerPartie";
        MenuChargerPartie.transform.GetChild(2).gameObject.GetComponent<Button>().Select();
    }

    public void ChoixOptions()
    {
        audioSource.PlayOneShot(bruitageBouton);
        MenuOptions.SetActive(true);
        Menu.GetComponent<Animator>().SetInteger("EstDansMenu", 3);
        MenuActuel = "MenuOptions";
        MenuOptions.transform.GetChild(2).gameObject.GetComponent<Dropdown>().Select();
    }

    public void QuitterJeu()
    {
        audioSource.PlayOneShot(bruitageBouton);
        Application.Quit();
    }

    public void RetourMenuPrincipalApresMenuNouvellePartie()
    {
        Menu.GetComponent<Animator>().SetInteger("EstDansMenu", 0);
        MenuActuel = "MenuPrincipal";
        MenuPrincipal.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Button>().Select();
        StartCoroutine(enleverMenu("MenuNouvellePartie"));
    }

    public void RetourMenuPrincipalApresMenuChargerPartie()
    {
        Menu.GetComponent<Animator>().SetInteger("EstDansMenu", 0);
        MenuActuel = "MenuPrincipal";
        MenuPrincipal.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Button>().Select();
        StartCoroutine(enleverMenu("MenuChargerPartie"));
    }

    public void RetourMenuPrincipalApresMenuOption()
    {
        Menu.GetComponent<Animator>().SetInteger("EstDansMenu", 0);
        MenuActuel = "MenuPrincipal";
        MenuPrincipal.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Button>().Select();
        StartCoroutine(enleverMenu("MenuOptions"));
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuPrincipal.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
