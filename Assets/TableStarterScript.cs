using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableStarterScript : MonoBehaviour
{
    [SerializeField]
    private ProjetP3DScene1.main Main;
    [SerializeField]
    private GameObject JoueurGameObjectVThirdCamera;
    private GameObject Joueur2GameObjectVThirdCamera;

    Text[] LabelObjetProche = new Text[2];
    private GameObject[] MessageObjetProcheGameObject = new GameObject[2];
    bool collisionJoueur = false;
    GameObject PokeballBulbizarre, PokeballSalameche, PokeballCarapuce;
    ClassLibrary.Pokemon pokemonStarter = new ClassLibrary.Pokemon();
    float[] DistancePokeballBulbizarreJoueur = new float[2];
    float[] DistancePokeballSalamecheJoueur = new float[2];
    float[] DistancePokeballCarapuceJoueur = new float[2];

    private void OnCollisionEnter(Collision collision)
    {
        int positionJoueur = 0;

        if (collision.gameObject.name == "vThirdPersonCamera")
        {
            positionJoueur = 0;
        }
        else if (collision.gameObject.name == "vThirdPersonCameraJoueur2")
        {
            positionJoueur = 1;
        }

        if (Main.JoueurManager.Joueurs[positionJoueur].starterChoisi == false)
        {
            collisionJoueur = true;

            if (MessageObjetProcheGameObject[positionJoueur].activeSelf == false)
            {
                MessageObjetProcheGameObject[positionJoueur].SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        int positionJoueur = 0;

        if (collision.gameObject.name == "vThirdPersonCamera")
        {
            positionJoueur = 0;
        }
        else if (collision.gameObject.name == "vThirdPersonCameraJoueur2")
        {
            positionJoueur = 1;
        }

        if (Main.JoueurManager.Joueurs[positionJoueur].starterChoisi == false)
        {
            collisionJoueur = false;

            if (MessageObjetProcheGameObject[positionJoueur].activeSelf == true)
            {
                MessageObjetProcheGameObject[positionJoueur].SetActive(false);
            }
        }
    }

    private void AffichageTexteStarter(GameObject joueurGameObject, int positionJoueur, GameObject canvasGameObject)
    {
        if (collisionJoueur == true && Main.JoueurManager.Joueurs[positionJoueur].starterChoisi == false)
        {
            DistancePokeballBulbizarreJoueur[positionJoueur] = (PokeballBulbizarre.transform.position - joueurGameObject.transform.position).sqrMagnitude;
            DistancePokeballSalamecheJoueur[positionJoueur] = (PokeballSalameche.transform.position - joueurGameObject.transform.position).sqrMagnitude;
            DistancePokeballCarapuceJoueur[positionJoueur] = (PokeballCarapuce.transform.position - joueurGameObject.transform.position).sqrMagnitude;

            if (DistancePokeballBulbizarreJoueur[positionJoueur] <= DistancePokeballSalamecheJoueur[positionJoueur] && DistancePokeballBulbizarreJoueur[positionJoueur] <= DistancePokeballCarapuceJoueur[positionJoueur] && LabelObjetProche[positionJoueur].text != "Choisir Bulbizarre" && Main.JoueurManager.Joueurs[positionJoueur].starterChoisi == false)
            {
                LabelObjetProche[positionJoueur].text = "Choisir Bulbizarre";
            }
            else if (DistancePokeballSalamecheJoueur[positionJoueur] < DistancePokeballBulbizarreJoueur[positionJoueur] && DistancePokeballSalamecheJoueur[positionJoueur] <= DistancePokeballCarapuceJoueur[positionJoueur] && LabelObjetProche[positionJoueur].text != "Choisir Salamèche" && Main.JoueurManager.Joueurs[positionJoueur].starterChoisi == false)
            {
                LabelObjetProche[positionJoueur].text = "Choisir Salamèche";
            }
            else if (DistancePokeballCarapuceJoueur[positionJoueur] < DistancePokeballBulbizarreJoueur[positionJoueur] && DistancePokeballCarapuceJoueur[positionJoueur] < DistancePokeballSalamecheJoueur[positionJoueur] && LabelObjetProche[positionJoueur].text != "Choisir Carapuce" && Main.JoueurManager.Joueurs[positionJoueur].starterChoisi == false)
            {
                LabelObjetProche[positionJoueur].text = "Choisir Carapuce";
            }

        }
    }

    public void choisirPokemon(GameObject joueurGameObject, int positionJoueur)
    {
        if (DistancePokeballBulbizarreJoueur[positionJoueur] <= DistancePokeballSalamecheJoueur[positionJoueur] && DistancePokeballBulbizarreJoueur[positionJoueur] <= DistancePokeballCarapuceJoueur[positionJoueur] && MessageObjetProcheGameObject[positionJoueur].activeSelf == true && Main.JoueurManager.Joueurs[positionJoueur].starterChoisi == false)
        {            
                pokemonStarter = pokemonStarter.setChercherPokemonStarter("Bulbizarre", Main.jeu);

                Main.JoueurManager.Joueurs[positionJoueur].ajouterPokemonEquipe(pokemonStarter);
                Main.JoueurManager.Joueurs[positionJoueur].pokemonSelectionner = Main.JoueurManager.Joueurs[positionJoueur].getPokemonEquipe()[0];
                Main.rafraichirBarreViePokemonJoueur2(positionJoueur);

                Main.JoueurManager.Joueurs[positionJoueur].starterChoisi = true;
              // Destroy(PokeballBulbizarre);

                Main.canvasGameObject[positionJoueur].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);

                LabelObjetProche[positionJoueur].text = "Bulbizarre a été choisi !";

                StartCoroutine(Main.WaitBeforeMessageDisapear());
        }
        else if (DistancePokeballSalamecheJoueur[positionJoueur] < DistancePokeballBulbizarreJoueur[positionJoueur] && DistancePokeballSalamecheJoueur[positionJoueur] <= DistancePokeballCarapuceJoueur[positionJoueur] && MessageObjetProcheGameObject[positionJoueur].activeSelf == true && Main.JoueurManager.Joueurs[positionJoueur].starterChoisi == false)
        {
                pokemonStarter = pokemonStarter.setChercherPokemonStarter("Salamèche", Main.jeu);

                Main.JoueurManager.Joueurs[positionJoueur].ajouterPokemonEquipe(pokemonStarter);
                Main.JoueurManager.Joueurs[positionJoueur].pokemonSelectionner = Main.JoueurManager.Joueurs[positionJoueur].getPokemonEquipe()[0];
                Main.rafraichirBarreViePokemonJoueur2(positionJoueur);

                Main.JoueurManager.Joueurs[positionJoueur].starterChoisi = true;
            // Destroy(PokeballSalameche);

                Main.canvasGameObject[positionJoueur].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);

                LabelObjetProche[positionJoueur].text = "Salamèche a été choisi !";

                StartCoroutine(Main.WaitBeforeMessageDisapear());
        }
        else if (DistancePokeballCarapuceJoueur[positionJoueur] < DistancePokeballBulbizarreJoueur[positionJoueur] && DistancePokeballCarapuceJoueur[positionJoueur] < DistancePokeballSalamecheJoueur[positionJoueur] && MessageObjetProcheGameObject[positionJoueur].activeSelf == true && Main.JoueurManager.Joueurs[positionJoueur].starterChoisi == false)
        {
                pokemonStarter = pokemonStarter.setChercherPokemonStarter("Carapuce", Main.jeu);

                Main.JoueurManager.Joueurs[positionJoueur].ajouterPokemonEquipe(pokemonStarter);
                Main.JoueurManager.Joueurs[positionJoueur].pokemonSelectionner = Main.JoueurManager.Joueurs[positionJoueur].getPokemonEquipe()[positionJoueur];
                Main.rafraichirBarreViePokemonJoueur2(positionJoueur);

                Main.JoueurManager.Joueurs[positionJoueur].starterChoisi = true;
            // Destroy(PokeballCarapuce);

                Main.canvasGameObject[positionJoueur].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);

                LabelObjetProche[positionJoueur].text = "Carapuce a été choisi !";

                StartCoroutine(Main.WaitBeforeMessageDisapear());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PokeballBulbizarre = this.gameObject.transform.GetChild(0).gameObject;
        PokeballSalameche = this.gameObject.transform.GetChild(1).gameObject;
        PokeballCarapuce = this.gameObject.transform.GetChild(2).gameObject;

        LabelObjetProche[0] = Main.canvasGameObject[0].transform.GetChild(2).gameObject.GetComponent<Text>();
        MessageObjetProcheGameObject[0] = Main.canvasGameObject[0].transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        AffichageTexteStarter(JoueurGameObjectVThirdCamera, 0, Main.canvasGameObject[0]);

        if (Main.JoueurManager.Joueurs.Count == 2)
        {
            if (Joueur2GameObjectVThirdCamera == null)
            {
                Joueur2GameObjectVThirdCamera = GameObject.Find("vThirdPersonCameraJoueur2");
                Main.canvasGameObject[1] = GameObject.Find("CanvasJoueur2");
                LabelObjetProche[1] = Main.canvasGameObject[1].transform.GetChild(2).gameObject.GetComponent<Text>();
                MessageObjetProcheGameObject[1] = Main.canvasGameObject[1].transform.GetChild(2).gameObject;
            }

            AffichageTexteStarter(Joueur2GameObjectVThirdCamera, 1, Main.canvasGameObject[1]);
        }
    }
}
