using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDresseurScript : MonoBehaviour
{
    ProjetP3DScene1.main Main;

    private void OnCollisionEnter(Collision collision)
    {
        if (Main.EnCombat == false)
        {
            Main.EnCombatContre = "PokemonDresseur";
            Main.dresseurAdverse = Main.jeu.setChercherPersonnage(this.gameObject.transform.parent.name);

           // GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().pokemonAdverseGameObject = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().jeu.setChercherPersonnage(this.gameObject.transform.parent.name).getPokemonEquipe()[0];
            Main.DeclenchementCombat();
        }
    }

    void Start()
    {
        Main = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>();
    }
}
