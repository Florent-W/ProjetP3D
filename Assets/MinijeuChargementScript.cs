using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinijeuChargementScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;
    [SerializeField]
    private int nombrePokemon;
    private int point = 0;
    [SerializeField]
    private List<Sprite> liste_images_pokemon = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : " + point;

        GameObject pokemonOriginal = this.gameObject.transform.transform.GetChild(0).gameObject;
        for (int i = 0; i < nombrePokemon; i++) // On instantie les pokemon
        {
            Vector3 positionPokemon = new Vector3(pokemonOriginal.transform.localPosition.x - (100 * i), pokemonOriginal.transform.localPosition.y, pokemonOriginal.transform.localPosition.z);
            GameObject imagePokemonGameObject = Instantiate(pokemonOriginal, this.gameObject.transform);
            imagePokemonGameObject.transform.localPosition = positionPokemon;
            imagePokemonGameObject.GetComponent<Image>().sprite = liste_images_pokemon[Random.Range(0, liste_images_pokemon.Count)]; // On assigne une image de pokemon aléatoire
            imagePokemonGameObject.SetActive(true);
        }
    }

    // Va ajouter un point au score et faire disparaitre le pokémon
    public void AjouterPoint()
    {
        point += 1;
        scoreText.text = "Score : " + point;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
