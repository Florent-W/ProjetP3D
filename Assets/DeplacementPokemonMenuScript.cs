using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeplacementPokemonMenuScript : MonoBehaviour
{
    [SerializeField]
    private ProjetP3DScene1.main Main;
    [SerializeField]
    private EventSystem eventSystem;
    public GameObject boutonConfirmerChangementPositionGameObject;
    private int positionPokemonMenuPokemon = 0, positionPokemonMenuActuel = 0;
    public bool EnTrainDeDeplacerPokemon = false;

    public void DeplacementPokemon_click()
    {
        eventSystem.SetSelectedGameObject(null);

        Main.btn_retour_menu_pokemon_apres_menu_pokemon_options_click();

        positionPokemonMenuPokemon = Main.positionPokemonMenuPokemon;
        positionPokemonMenuActuel = positionPokemonMenuPokemon;

        EnTrainDeDeplacerPokemon = true;
        boutonConfirmerChangementPositionGameObject.SetActive(true);
    }

    public void btn_confirmer_changement_position_click()
    {
        EnTrainDeDeplacerPokemon = false;
        this.gameObject.transform.GetChild(positionPokemonMenuActuel).gameObject.GetComponent<Button>().Select();
        boutonConfirmerChangementPositionGameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnTrainDeDeplacerPokemon == true)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && positionPokemonMenuActuel < Main.JoueurManager.Joueurs[0].getPokemonEquipe().Count - 1)
            {
                ClassLibrary.Pokemon pokemonADeplacer = new ClassLibrary.Pokemon();

                pokemonADeplacer = Main.JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuActuel];

                Main.JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuActuel] = Main.JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuActuel + 1];
                Main.JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuActuel + 1] = pokemonADeplacer;

                positionPokemonMenuActuel = positionPokemonMenuActuel + 1;

                Main.rafraichirEquipe(0, Main.canvasGameObject[0]);

                /*
                Vector3 positionAvant = this.gameObject.transform.GetChild(0).gameObject.transform.position;
                this.gameObject.transform.GetChild(0).gameObject.transform.position = this.gameObject.transform.GetChild(1).gameObject.transform.position;
                this.gameObject.transform.GetChild(1).gameObject.transform.position = positionAvant; */
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && positionPokemonMenuActuel > 0)
            {
                ClassLibrary.Pokemon pokemonADeplacer = new ClassLibrary.Pokemon();

                pokemonADeplacer = Main.JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuActuel];

                Main.JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuActuel] = Main.JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuActuel - 1];
                Main.JoueurManager.Joueurs[0].getPokemonEquipe()[positionPokemonMenuActuel - 1] = pokemonADeplacer;

                positionPokemonMenuActuel = positionPokemonMenuActuel - 1;

                Main.rafraichirEquipe(0, Main.canvasGameObject[0]);
            }
        }
    }
}
