using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPokemonScript : MonoBehaviour
{
    private ClassLibrary.Pokemon pokemon;
    private GameObject barViePokemon;
    private Image barViePokemonImage;
    private Text LabelNomPokemon, LabelNiveauPokemon, LabelSexePokemon, LabelPvPokemon;

    // Start is called before the first frame update
    void Start()
    {
        // Initialisation des variables
        pokemon = this.gameObject.transform.parent.gameObject.GetComponent<StatistiquesPokemon>().GetPokemon();

        LabelNomPokemon = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
        LabelNiveauPokemon = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();
        LabelSexePokemon = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<Text>();
        LabelPvPokemon = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        barViePokemon = this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject;
        barViePokemonImage = barViePokemon.GetComponent<Image>();

        LabelNomPokemon.text = pokemon.getNom();
        LabelNiveauPokemon.text = "N. " + pokemon.getNiveau();
        if (pokemon.getSexe() == "Feminin")
        {
            LabelSexePokemon.text = "♀";
            LabelSexePokemon.color = new Color32(255, 130, 192, 255);
        }
        else if (pokemon.getSexe() == "Masculin")
        {
            LabelSexePokemon.text = "♂";
            LabelSexePokemon.color = new Color32(0, 128, 255, 255);
        }

        InvokeRepeating("rafraichirUIPokemon2", 0f, 5f); // Lancement methode
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Rafraichir l'ui d'un pokémon dans le monde
    public void rafraichirUIPokemon2()
    {
        //  barViePokemonAdversaireImage.color = new Color32(81, 209, 39, 255);
        LabelPvPokemon.text = pokemon.getPvRestant().ToString() + " / " + pokemon.getPv().ToString() + " PV";
        barViePokemonImage.fillAmount = (float)pokemon.getPvRestant() / (float)pokemon.getPv();
    }
}
