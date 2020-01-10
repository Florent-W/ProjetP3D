using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().EnCombat == false)
        {
            GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().EnCombatContre = "PokemonSauvage";

            GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().pokemonAdverseGameObject = this.gameObject.transform.parent.gameObject;
            GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().DeclenchementCombat();
        }
    }
}
