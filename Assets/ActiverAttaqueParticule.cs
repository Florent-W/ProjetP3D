using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Activer les particules des attaques
public class ActiverAttaqueParticule : MonoBehaviour
{
    private List<GameObject> particuleAttaquesGameObject = new List<GameObject>();
    private ProjetP3DScene1.main main;

    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>();
        GameObject particulesAttaqueGameObject = main.transform.GetChild(0).gameObject;

        for (int i = 0; i < particulesAttaqueGameObject.transform.childCount; i++)
        {
            particuleAttaquesGameObject.Add(particulesAttaqueGameObject.transform.GetChild(i).gameObject); // On ajoute toutes les attaques dans un tableau
        } 
    }

    // Active les particules avec le numéro de l'attaque dans la liste
    public void activerParticuleAttaque(int numeroAttaque)
    {
        // particuleAttaquesGameObject[numeroAttaque].GetComponent<ParticleSystem>().Play();
        // particuleAttaquesGameObject[numeroAttaque].SetActive(true); // On active l'animation attaque
    }

    // Désactive les particules avec le numéro de l'attaque dans la liste
    public void desactiverParticuleAttaque(int numeroAttaque)
    {
        // particuleAttaquesGameObject[numeroAttaque].GetComponent<ParticleSystem>().Play();
        particuleAttaquesGameObject[numeroAttaque].SetActive(false); // On désactiove l'animation attaque
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (main.JoueurManager.JoueursGameObject[0].GetComponent<PlayerInput>().actions["Action1"].ReadValue<float>() == 1)
        {
            particuleAttaquesGameObject[0].GetComponent<ParticleSystem>().Play();
        }
        */
    }
}
