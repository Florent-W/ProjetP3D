using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarreVieScript : MonoBehaviour
{
    private ClassLibrary.Pokemon pokemon;
    private GameObject barViePokemon;
    private Image barViePokemonImage;

    // Start is called before the first frame update
    void Start()
    {
        // Initialisation des variables
        pokemon = this.gameObject.transform.parent.gameObject.GetComponent<StatistiquesPokemon>().GetPokemon();
        barViePokemon = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject;
        barViePokemonImage = barViePokemon.GetComponent<Image>();
        InvokeRepeating("rafraichirBarreViePokemon2", 0f, 5f); // Lancement methode
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Rafraichir barre de vie d'un pokémon dans le monde
    public void rafraichirBarreViePokemon2()
    {
        //  barViePokemonAdversaireImage.color = new Color32(81, 209, 39, 255);
        barViePokemonImage.fillAmount = (float)pokemon.getPvRestant() / (float)pokemon.getPv();
    }
}
