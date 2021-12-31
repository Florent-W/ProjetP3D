using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoitePcScript : MonoBehaviour
{
    [SerializeField]
    private ProjetP3DScene1.main Main;
    [SerializeField]
    private GameObject MenuPcGameObject;
    [SerializeField]
    private GameObject MessageObjetProcheGameObject;

    bool collisionJoueur = false;

    private void OnCollisionEnter (Collision collision)
    {
        collisionJoueur = true;
        MessageObjetProcheGameObject.GetComponent<Text>().text = "Boîte PC";

        if (MessageObjetProcheGameObject.activeSelf == false)
        {
            MessageObjetProcheGameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionJoueur = false;

        if (MessageObjetProcheGameObject.activeSelf == true)
        {
            MessageObjetProcheGameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && MenuPcGameObject.activeSelf == false && collisionJoueur == true)
        {
            ClassLibrary.Dresseur Joueur = Main.JoueurManager.Joueurs[0];

            if (Joueur.getPokemonPc().Count > 0)
            {
                for (int i = 0; i < Joueur.getPokemonPc().Count; i++)
                {
                    GameObject boutonPokemonGameObject = Main.gameObject.GetComponent<CreerComposantScript>().CreateButton(this.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform, Joueur.getPokemonPc()[i].getNom(), 320, 160, Joueur.getPokemonPc()[i].getNom(), 38);
                    Destroy(boutonPokemonGameObject.transform.GetChild(0).gameObject);
                    Button boutonPokemon = boutonPokemonGameObject.GetComponent<Button>();
                    boutonPokemon.transform.Rotate(0, 180, 0);
                    int idPokedexImage = Joueur.getPokemonPc()[i].getNoIdPokedex();
                    boutonPokemon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/" + idPokedexImage);
                }
            }
            MenuPcGameObject.SetActive(true);
        }
    }
}
