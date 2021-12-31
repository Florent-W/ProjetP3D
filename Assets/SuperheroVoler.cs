using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Script pour faire voler un superhéro
public class SuperheroVoler : MonoBehaviour
{
    private GameObject vThirdPersonControllerGameObject;
    private Animator animatorPersonnage;
    [SerializeField]
    private GameObject trailVent;

    // Start is called before the first frame update
    void Start()
    {
        vThirdPersonControllerGameObject = this.gameObject; // Initialisation
        animatorPersonnage = vThirdPersonControllerGameObject.GetComponent<Animator>();
     /*   GameObject vent = Instantiate(trailVent, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(-180, 90, 0)), vThirdPersonControllerGameObject.transform); // On ajoute des trainés de vent
        vent.transform.position = new Vector3(vThirdPersonControllerGameObject.transform.position.x, vThirdPersonControllerGameObject.transform.position.y + 1.2f, vThirdPersonControllerGameObject.transform.position.z - 4.1f); */
    }

    // Quand on appuie sur la touche pour voler
    void OnVoler()
    {
        float boutonVoler = vThirdPersonControllerGameObject.GetComponent<PlayerInput>().actions["Voler"].ReadValue<float>(); // Appuie sur la touche pour voler

        if (boutonVoler == 1)
        {
            if (animatorPersonnage.GetBool("enTrainDeVoler") == false)
            {
                animatorPersonnage.SetBool("enTrainDeVoler", true); // On active l'animation de vol si on appuie sur la touche et que le personnage n'est pas déjà en train de voler
                vThirdPersonControllerGameObject.GetComponent<Rigidbody>().useGravity = false; // On désactive la gravité quand le personnage vole
                vThirdPersonControllerGameObject.GetComponent<Rigidbody>().drag = 30;
            }
            else // On enlève l'animation de vol si l'utilisateur le veut et qu'elle en train d'etre faite
            {
                animatorPersonnage.SetBool("enTrainDeVoler", false);
                vThirdPersonControllerGameObject.GetComponent<Rigidbody>().useGravity = true; // On remet la gravité quand le personnage est à terre
                vThirdPersonControllerGameObject.GetComponent<Rigidbody>().drag = 0;
            }
        }
    }

    // Quand on appuie sur la touche pour monter en hauteur en volant
    void OnVolerMonter()
    {
        float boutonVolerMonter = vThirdPersonControllerGameObject.GetComponent<PlayerInput>().actions["VolerMonter"].ReadValue<float>(); // Appuie sur la touche pour s'envoler plus haut

        if (boutonVolerMonter == 1)
        {
            if (animatorPersonnage.GetBool("enTrainDeVoler") == true) // On regarde si le personnage est en train de voler
            {
                vThirdPersonControllerGameObject.transform.position = new Vector3(vThirdPersonControllerGameObject.transform.position.x, vThirdPersonControllerGameObject.transform.position.y + 1, vThirdPersonControllerGameObject.transform.position.z); // On ajoute de la hauteur au personnage
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
