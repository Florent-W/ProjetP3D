using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Script pour déclencher animation dancer des personnages
public class Dancer : MonoBehaviour
{
    [SerializeField]
    private PersonnagesSelection personnageSelection;
    private GameObject vThirdPersonControllerGameObject;
    private Animator animatorPersonnage;

    // Start is called before the first frame update
    void Start()
    {
        vThirdPersonControllerGameObject = this.gameObject; // Initialisation
        animatorPersonnage = vThirdPersonControllerGameObject.GetComponent<Animator>();
    }

    // Quand on appuie sur la touche pour dancer
    void OnDancer()
    {
        float boutonDancer = vThirdPersonControllerGameObject.GetComponent<PlayerInput>().actions["Dancer"].ReadValue<float>(); // Appuie sur la touche pour dancer

        if (boutonDancer == 1)
        {
            if (animatorPersonnage.GetBool("isDancing") == false)
            {
                animatorPersonnage.SetBool("isDancing", true);
                animatorPersonnage.SetInteger("Dance", 1); // On active l'animation de conga si on appuie sur la touche

                // La même chose pour les allier pour qu'ils dancent aussi
                for (int i = 0; i < personnageSelection.listePersonnagesAllier.Count; i++)
                {
                    personnageSelection.listePersonnagesAllier[i].GetComponent<Animator>().SetBool("isDancing", true);
                    personnageSelection.listePersonnagesAllier[i].GetComponent<Animator>().SetInteger("Dance", 1); // On active l'animation de conga si on appuie sur la touche
                }
            }
            else // On enlève l'animation de vol si l'utilisateur le veut et qu'elle en train d'etre faite
            {
                animatorPersonnage.SetBool("isDancing", false);

                // On enlève pour les alliés
                for (int i = 0; i < personnageSelection.listePersonnagesAllier.Count; i++)
                {
                    personnageSelection.listePersonnagesAllier[i].GetComponent<Animator>().SetBool("isDancing", false);
                }
            }
        }
    }

    // Quand on appuie sur la touche pour dancer manrobotics
    void OnDancer9()
    {
        float boutonDancer = vThirdPersonControllerGameObject.GetComponent<PlayerInput>().actions["Dancer9"].ReadValue<float>(); // Appuie sur la touche pour dancer

        if (boutonDancer == 1)
        {
            if (animatorPersonnage.GetBool("isDancing") == false)
            {
                animatorPersonnage.SetBool("isDancing", true);
                animatorPersonnage.SetInteger("Dance", 2); // On active l'animation de manrobotics si on appuie sur la touche

                // La même chose pour les allier pour qu'ils dancent aussi
                for (int i = 0; i < personnageSelection.listePersonnagesAllier.Count; i++)
                {
                    personnageSelection.listePersonnagesAllier[i].GetComponent<Animator>().SetBool("isDancing", true);
                    personnageSelection.listePersonnagesAllier[i].GetComponent<Animator>().SetInteger("Dance", 2); // On active l'animation de manrobotics si on appuie sur la touche
                }
            }
            else // On enlève l'animation de vol si l'utilisateur le veut et qu'elle en train d'etre faite
            {
                animatorPersonnage.SetBool("isDancing", false);

                for (int i = 0; i < personnageSelection.listePersonnagesAllier.Count; i++)
                {
                    personnageSelection.listePersonnagesAllier[i].GetComponent<Animator>().SetBool("isDancing", false);
                }
            }
        }
    }

    // Quand on appuie sur la touche pour dancer kazotzky
    void OnDancer0()
    {
        float boutonDancer = vThirdPersonControllerGameObject.GetComponent<PlayerInput>().actions["Dancer0"].ReadValue<float>(); // Appuie sur la touche pour dancer

        if (boutonDancer == 1)
        {
            if (animatorPersonnage.GetBool("isDancing") == false)
            {
                animatorPersonnage.SetBool("isDancing", true);
                animatorPersonnage.SetInteger("Dance", 3); // On active l'animation de kazotzky si on appuie sur la touche

                // La même chose pour les allier pour qu'ils dancent aussi
                for (int i = 0; i < personnageSelection.listePersonnagesAllier.Count; i++)
                {
                    personnageSelection.listePersonnagesAllier[i].GetComponent<Animator>().SetBool("isDancing", true);
                    personnageSelection.listePersonnagesAllier[i].GetComponent<Animator>().SetInteger("Dance", 3); // On active l'animation de kazotzky si on appuie sur la touche
                }
            }
            else // On enlève l'animation de vol si l'utilisateur le veut et qu'elle en train d'etre faite
            {
                animatorPersonnage.SetBool("isDancing", false);

                for (int i = 0; i < personnageSelection.listePersonnagesAllier.Count; i++)
                {
                    personnageSelection.listePersonnagesAllier[i].GetComponent<Animator>().SetBool("isDancing", false);
                }
            }
        }
    }
}
