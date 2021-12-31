using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "vThirdPersonCamera" || collision.gameObject.name == "vThirdPersonCameraJoueur2")
        {
            int positionJoueur = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().JoueurManager.ChercherJoueurGameObjectPosition(collision.gameObject);

            if (GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[positionJoueur].enCombat == false && GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().modeCombat == "Tour par tour") // Si on est pas en combat et que le mode de combat est en tour par tour
            {
                GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().EnCombatContre = "PokemonSauvage";

                GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().pokemonAdverseGameObject = this.gameObject.transform.parent.gameObject;
                GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().DeclenchementCombat(positionJoueur);
            }
        }
    }
}
