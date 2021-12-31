using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

// Va charger les informations du pokemon disponible dans l'écran de chargement
public class PokemonInformationChargementScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ClassLibrary.Jeu jeu = new ClassLibrary.Jeu(); // Parametres du jeu (attaque, pokemon)

        jeu.initialisationEspecePokemon();

        int numeroPokemon = Random.Range(1, jeu.getListeEspecePokemon().Count + 1); // On choisit un numero de pokemon au hasard pour choisir sa description et son model
        GameObject pokemonInfo = Instantiate(Resources.Load<GameObject>("Models/Pokemon/Models originaux/" + numeroPokemon), this.gameObject.transform.GetChild(1).gameObject.transform); // Le pokémon apparait
        Animator pokemonInfoAnimator = pokemonInfo.GetComponent<Animator>();
        pokemonInfoAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Controller/" + numeroPokemon + "Controller"); // On ajoute le bon controlleur pour avoir les animations sur le pokémon
        this.gameObject.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = jeu.getListeEspecePokemon()[numeroPokemon - 1].pokedex_pokemon_resume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
