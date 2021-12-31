using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatistiquesPokemon : MonoBehaviour
{
    private ClassLibrary.Pokemon m_pokemon;
    private GameObject sceneBuilder;
    private ProjetP3DScene1.main main;

    public ClassLibrary.Pokemon GetPokemon()
    {
        return this.m_pokemon;
    }

    public void SetPokemon(ClassLibrary.Pokemon pokemon)
    {
        this.m_pokemon = pokemon;
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneBuilder = GameObject.Find("SceneBuilder");
        if(sceneBuilder != null)
        {
            main = sceneBuilder.GetComponent<ProjetP3DScene1.main>();
        }
    }

    // Update is called once per frame
    void Update()
    {
            // Si c'est en temps réel, on enlève le pokémon si il atteint zéro
            if(sceneBuilder != null) {
            if (main.modeCombat == "Temps réel")
            {
                if (m_pokemon != null)
                {
                    if (m_pokemon.getPvRestant() <= 0)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
