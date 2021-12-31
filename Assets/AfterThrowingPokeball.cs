using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.AI;

public class AfterThrowingPokeball : MonoBehaviour
{
    Animator animator;
    GameObject Pokeball;
    GameObject terrainGameObject;

    public int NumeroPokedexPokemon, NumeroPokedexPokemonAdverse;
    public GameObject pokemonJoueurGameObject = null, pokemonAdverseGameObject = null;
    public int positionJoueur;
    ProjetP3DScene1.main Main;

    public void AfterThrowPokeball()
    {
        Pokeball = this.gameObject;

        animator = Pokeball.GetComponent<Animator>();
        //   animator.applyRootMotion = true;

        Transform positionJoueurGameObject = GameObject.Find("vThirdPersonCamera").transform;

        Vector3 positionPokeball = Pokeball.transform.position;

        Main.gameObject.GetComponent<gererCameraScript>().CMVCam1.SetActive(true);
        Main.gameObject.GetComponent<gererCameraScript>().CMVCam2.SetActive(true);
        Main.gameObject.GetComponent<gererCameraScript>().CMVCam3.SetActive(true);
        Main.gameObject.GetComponent<gererCameraScript>().CMVCam4.SetActive(true);

        // this.gameObject.transform.SetParent(null, true);
        //  Pokeball.transform.position = new Vector3(1, 0, 1);

        //  Debug.Log(positionPokeball);

        //   GameObject PokeballGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokeballs/Poke ball"), Pokeball.transform.position, Pokeball.transform.rotation);
        /*
        CapsuleCollider gameObjectCapsuleCollider = Pokeball.GetComponent<CapsuleCollider>();
        gameObjectCapsuleCollider.enabled = true;
        gameObjectCapsuleCollider.radius = 0;
        gameObjectCapsuleCollider.height = 0.2f;
        /*
        Pokeball.gameObject.AddComponent<Rigidbody>();
        Rigidbody gameObjectRigibody = Pokeball.gameObject.GetComponent<Rigidbody>();
        gameObjectRigibody.mass = 5; 
        */



        // GameObject pokemonJoueurGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + animator.GetInteger("NumeroPokedexPokemon")), animator.gameObject.transform.position, animator.gameObject.transform.rotation);
        //  pokemonJoueurGameObject.name = "PokemonJoueur";
    }

    public void LancerPokeballChangementCamera()
    {
        /*
        Pokeball = this.gameObject;

        Main.gameObject.GetComponent<gererCameraScript>().CMVCam4.GetComponent<CinemachineVirtualCamera>().LookAt = Pokeball.transform;
        Main.gameObject.GetComponent<gererCameraScript>().CMVCam4.GetComponent<CinemachineVirtualCamera>().Follow = Pokeball.transform;
        */
    }

    public void SortiePokemonPokeball()
    {
        Pokeball = this.gameObject;

        Main = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>();

        pokemonJoueurGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + Pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemon), Pokeball.transform.position, Pokeball.transform.rotation);
        pokemonJoueurGameObject.name = "PokemonJoueur";
        pokemonJoueurGameObject.tag = "PokemonJoueur";

        Main.pokemonJoueurGameObject = pokemonJoueurGameObject;

        GameObject cameraCombatGameObject = Main.cameraCombatJoueurs[positionJoueur].transform.GetChild(0).gameObject;
        GameObject textPokemon3D = Main.gameObject.GetComponent<CreerComposantScript>().CreateText3D(pokemonJoueurGameObject.transform, "NomPokemon3D", 250, 250, 0, 2, Main.JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getNom(), 12, Color.cyan, cameraCombatGameObject);

        Main.gameObject.GetComponent<gererCameraScript>().CMVCam1.GetComponent<CinemachineVirtualCamera>().LookAt = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;
        Main.gameObject.GetComponent<gererCameraScript>().CMVCam1.GetComponent<CinemachineVirtualCamera>().Follow = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;

        Main.gameObject.GetComponent<gererCameraScript>().CMVCam3.GetComponent<CinemachineVirtualCamera>().LookAt = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;
        Main.gameObject.GetComponent<gererCameraScript>().CMVCam3.GetComponent<CinemachineVirtualCamera>().Follow = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;

      //  Destroy(Pokeball, 1);
    }

    public void SortiePokemonPokeballAccompagne()
    {
        Pokeball = this.gameObject;

        Main = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>();

        pokemonJoueurGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + Pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemon), Pokeball.transform.position, Pokeball.transform.rotation);
        pokemonJoueurGameObject.name = "PokemonJoueur";
        pokemonJoueurGameObject.tag = "PokemonJoueur";

        NavMeshAgent navMeshAgent = pokemonJoueurGameObject.AddComponent<NavMeshAgent>(); // On ajoute l'agent
        PatrouilleMouvement patrouilleMouvementScript = pokemonJoueurGameObject.AddComponent<PatrouilleMouvement>(); // On ajoute le script permettant au pokemon de savoir ou aller

        Transform point = pokemonJoueurGameObject.transform.Find("Point");
        Transform point2 = pokemonJoueurGameObject.transform.Find("Point2");

        patrouilleMouvementScript.points.Add(point);
        patrouilleMouvementScript.points.Add(point2);

        patrouilleMouvementScript.pokemonAllier = true;
        Destroy(pokemonJoueurGameObject.transform.GetChild(0).gameObject.GetComponent<PokemonScript>());

        Main.pokemonJoueurGameObject = pokemonJoueurGameObject;

        GameObject cameraCombatGameObject = Main.cameraCombatJoueurs[positionJoueur].transform.GetChild(0).gameObject;
        GameObject textPokemon3D = Main.gameObject.GetComponent<CreerComposantScript>().CreateText3D(pokemonJoueurGameObject.transform, "NomPokemon3D", 250, 250, 0, 2, Main.JoueurManager.Joueurs[positionJoueur].pokemonSelectionner.getNom(), 12, Color.cyan, cameraCombatGameObject);

        Main.gameObject.GetComponent<gererCameraScript>().CMVCam1.GetComponent<CinemachineVirtualCamera>().LookAt = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;
        Main.gameObject.GetComponent<gererCameraScript>().CMVCam1.GetComponent<CinemachineVirtualCamera>().Follow = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;

        Main.gameObject.GetComponent<gererCameraScript>().CMVCam3.GetComponent<CinemachineVirtualCamera>().LookAt = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;
        Main.gameObject.GetComponent<gererCameraScript>().CMVCam3.GetComponent<CinemachineVirtualCamera>().Follow = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;

        //  Destroy(Pokeball, 1);
    }

    public void SpawnPokemonAdverse()
    {
        terrainGameObject = GameObject.Find("Terrain");

        Vector3 positionPokemonAdverse = pokemonJoueurGameObject.transform.position + pokemonJoueurGameObject.transform.forward * 8;
        float positionYPokemonAdverse = terrainGameObject.GetComponent<Terrain>().SampleHeight(positionPokemonAdverse);

        if (Main.EnCombatContre == "PokemonDresseur")
        {
            Pokeball = this.gameObject;

            // Vector3 positionPokemonAdverse = new Vector3(pokemonJoueurGameObject.transform.position.x, pokemonJoueurGameObject.transform.position.y, pokemonJoueurGameObject.transform.position.z - 8);       

            pokemonAdverseGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + Pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemonAdverse), new Vector3(positionPokemonAdverse.x, positionYPokemonAdverse, positionPokemonAdverse.z), pokemonJoueurGameObject.transform.rotation);
            pokemonAdverseGameObject.name = "PokemonAdverse";
            pokemonAdverseGameObject.tag = "PokemonAdverse";

            pokemonAdverseGameObject.transform.Rotate(0f, 180f, 0f);
        }
        else if(Main.EnCombatContre == "PokemonSauvage")
        {
            pokemonAdverseGameObject = Main.pokemonAdverseGameObject;

            pokemonAdverseGameObject.transform.position = new Vector3(positionPokemonAdverse.x, positionYPokemonAdverse, positionPokemonAdverse.z);
            pokemonAdverseGameObject.transform.rotation = pokemonJoueurGameObject.transform.rotation;

            pokemonAdverseGameObject.transform.Rotate(0f, 180f, 0f);
        }

        Main.pokemonAdverseGameObject = pokemonAdverseGameObject;

        GameObject cameraCombatGameObject = GameObject.Find("CameraCombat");
        GameObject textPokemon3D = Main.gameObject.GetComponent<CreerComposantScript>().CreateText3D(pokemonAdverseGameObject.transform, "NomPokemon3D", 250, 250, 0, 2, Main.pokemon.getNom(), 12, new Color32(25, 109, 231, 255), cameraCombatGameObject);

        Main.gameObject.GetComponent<gererCameraScript>().CMVCam2.GetComponent<CinemachineVirtualCamera>().LookAt = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonAdverseGameObject.transform;
        Main.gameObject.GetComponent<gererCameraScript>().CMVCam2.GetComponent<CinemachineVirtualCamera>().Follow = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonAdverseGameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
