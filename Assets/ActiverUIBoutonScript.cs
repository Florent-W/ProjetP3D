using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Va activer l'aide des boutons
public class ActiverUIBoutonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject UIBoutons;
    [SerializeField]
    private ProjetP3DScene1.main main;
    private bool dansActiverUIBoutonsCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("ActiverUIBoutons", 0f, 0.1f);
    }

    private IEnumerator ActiverUIBoutons()
    {
        dansActiverUIBoutonsCoroutine = true;
        float valeurOuvrirUIBoutons = main.JoueurManager.JoueursGameObject[0].GetComponent<PlayerInput>().actions["Lb"].ReadValue<float>();

        if (valeurOuvrirUIBoutons == 1 && UIBoutons.activeSelf == false)
        {
            UIBoutons.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }
        else if (valeurOuvrirUIBoutons == 1 && UIBoutons.activeSelf == true)
        {
            UIBoutons.SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
        dansActiverUIBoutonsCoroutine = false;
    }
    /*
    private void ActiverUIBoutons()
    {
        float valeurOuvrirUIBoutons = main.JoueurManager.JoueursGameObject[0].GetComponent<PlayerInput>().actions["Lb"].ReadValue<float>();

        if (valeurOuvrirUIBoutons == 1 && UIBoutons.activeSelf == false)
        {
            UIBoutons.SetActive(true);
        }
        else if (valeurOuvrirUIBoutons == 1 && UIBoutons.activeSelf == true)
        {
            UIBoutons.SetActive(false);
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (dansActiverUIBoutonsCoroutine == false)
        {
            StartCoroutine(ActiverUIBoutons());
        }
    }
}
