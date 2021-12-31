using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NavigationInputMenu : MonoBehaviour
{
    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private Button btn_confirmer;
    [SerializeField]
    private Button btn_retour;
    [SerializeField]
    private GameObject vthirdPersonCamera;
    [SerializeField]
    private ActiverBoutonNouvellePartie activerBoutonNouvellePartieScript;
    [SerializeField]
    private PersonnageSelectionScript personnageSelectionScript;
    private bool navigationMenuLancer = false;
    private float timerDansInput = 0; 

    // Start is called before the first frame update
    void Start()
    {

    }

    private IEnumerator navigationMenu()
    {
            navigationMenuLancer = true;

            if (eventSystem.currentSelectedGameObject == inputField.gameObject)
            {
                Vector2 valeurBouger = vthirdPersonCamera.GetComponent<PlayerInput>().actions["BougerVersHautBas"].ReadValue<Vector2>();
       
            if (timerDansInput < 1f)
            {
                timerDansInput = timerDansInput + Time.deltaTime; // Variable servant à savoir depuis quand l'input est selectioné, permet de ne pas passer direct au bouton précédent
            }

                if (valeurBouger.y > 0 && timerDansInput > 0.3f) // Si appui haut et qu'on laisse pas le haut, on passe à la dernière ligne des personnages
                {
                    inputField.DeactivateInputField();
                    yield return new WaitForSeconds(0.1f);

                if (personnageSelectionScript.listePersonnageActuel == "Dresseurs")
                {
                    eventSystem.SetSelectedGameObject(personnageSelectionScript.gameObject.transform.GetChild(4).gameObject.transform.GetChild(personnageSelectionScript.listePersonnagesDresseurs.Count / 2).gameObject); // Selection d'un des personnages en appuyant sur le haut
                }
                else if (personnageSelectionScript.listePersonnageActuel == "Super Heros")
                {
                    eventSystem.SetSelectedGameObject(personnageSelectionScript.gameObject.transform.GetChild(4).gameObject.transform.GetChild(personnageSelectionScript.listePersonnagesHeros.Count / 2).gameObject); // Selection d'un des personnages en appuyant sur le haut
                }
                timerDansInput = 0;
                }
                else if (valeurBouger.y < 0 && timerDansInput > 0.3f) // Si appui bas, on passe à precedent
                {
                    inputField.DeactivateInputField();
                    yield return new WaitForSeconds(0.1f);
                    eventSystem.SetSelectedGameObject(personnageSelectionScript.gameObject.transform.GetChild(1).gameObject); // Selection bouton precedent en appuyant sur le bas
                    timerDansInput = 0;
                }

            /*
            else if (valeurBouger.y < 0 && btn_confirmer.interactable == true) // Si appui bas et input rempli, on passe à confirmer
            {
                inputField.DeactivateInputField();
                eventSystem.SetSelectedGameObject(btn_confirmer.gameObject);
                yield return new WaitForSeconds(0.3f);
            } 

            else if (valeurBouger.y > 0 || (valeurBouger.y < 0 && btn_confirmer.interactable == false)) // Si appui haut ou appui bas et l'input n'est pas rempli, on passe au bouton retour
            {
                inputField.DeactivateInputField();
                yield return new WaitForSeconds(0.1f);
                eventSystem.SetSelectedGameObject(personnageSelectionScript.gameObject.transform.GetChild(4).gameObject.transform.GetChild(personnageSelectionScript.listePersonnagesDresseurs.Count / 2).gameObject); // Selection d'un des personnages en appuyant sur le haut
            }*/
        }
            /*
            else if (eventSystem.currentSelectedGameObject == btn_retour.gameObject)
            {
                Vector2 valeurBouger = vthirdPersonCamera.GetComponent<PlayerInput>().actions["BougerVersHautBas"].ReadValue<Vector2>();

                if ((valeurBouger.y > 0 && btn_confirmer.interactable == false) || valeurBouger.y < 0)
                {
                    eventSystem.SetSelectedGameObject(inputField.gameObject);
                    yield return new WaitForSeconds(0.3f);
                }
                else if (valeurBouger.y > 0 && btn_confirmer.interactable == true)
                {
                    eventSystem.SetSelectedGameObject(btn_confirmer.gameObject);
                    yield return new WaitForSeconds(0.3f);
                }
            } */
            /*
            else if (eventSystem.currentSelectedGameObject == btn_confirmer.gameObject)
            {
                Vector2 valeurBouger = vthirdPersonCamera.GetComponent<PlayerInput>().actions["BougerVersHautBas"].ReadValue<Vector2>();

                if (valeurBouger.y > 0)
                {
                    eventSystem.SetSelectedGameObject(inputField.gameObject);
                    yield return new WaitForSeconds(0.3f);
                }
                else if (valeurBouger.y < 0)
                {
                    eventSystem.SetSelectedGameObject(btn_retour.gameObject);
                    yield return new WaitForSeconds(0.3f);
                }
            }
            */
        navigationMenuLancer = false;
        StopCoroutine(navigationMenu());
    } 

    // Update is called once per frame
    void Update()
    {
        if(!navigationMenuLancer && activerBoutonNouvellePartieScript.MenuActuel == "MenuNouvellePartie")
        {
            StartCoroutine(navigationMenu());
        }
    }
}
