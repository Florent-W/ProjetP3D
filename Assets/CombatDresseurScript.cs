using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDresseurScript : MonoBehaviour
{
    ProjetP3DScene1.main Main;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "vThirdPersonCamera" || collision.gameObject.name == "vThirdPersonCameraJoueur2")
        {
            int positionJoueur = Main.JoueurManager.ChercherJoueurGameObjectPosition(collision.gameObject);

            if (Main.JoueurManager.Joueurs[positionJoueur].enCombat == false)
            {
                Main.EnCombatContre = "PokemonDresseur";
                Main.dresseurAdverse = Main.jeu.setChercherDresseur(this.gameObject.name);
                // Debug.Log(Main.dresseurAdverse.getPokemonEquipe().Count);

                // GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().pokemonAdverseGameObject = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().jeu.setChercherPersonnage(this.gameObject.transform.parent.name).getPokemonEquipe()[0];
                Main.DeclenchementCombat(positionJoueur);
            }
        }
    }

    void Start()
    {
        Main = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>();
    }
}
