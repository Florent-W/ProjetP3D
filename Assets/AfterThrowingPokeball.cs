using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class AfterThrowingPokeball : MonoBehaviour
{
    Animator animator;
    GameObject Pokeball;
    public int NumeroPokedexPokemon, NumeroPokedexPokemonAdverse;
    public GameObject pokemonJoueurGameObject = null, pokemonAdverseGameObject = null;

    public void AfterThrowPokeball()
    {
        Pokeball = this.gameObject;

        animator = Pokeball.GetComponent<Animator>();
     //   animator.applyRootMotion = true;

        Vector3 positionPokeball = Pokeball.transform.position;
        Pokeball.transform.SetParent(null, true);
      //  Debug.Log(positionPokeball);

        //   GameObject PokeballGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokeballs/Poke ball"), Pokeball.transform.position, Pokeball.transform.rotation);
        CapsuleCollider gameObjectCapsuleCollider = Pokeball.GetComponent<CapsuleCollider>();
        gameObjectCapsuleCollider.enabled = true;
        gameObjectCapsuleCollider.radius = 0;
        gameObjectCapsuleCollider.height = 0.2f;
        
        Pokeball.gameObject.AddComponent<Rigidbody>();
        Rigidbody gameObjectRigibody = Pokeball.gameObject.GetComponent<Rigidbody>();
        gameObjectRigibody.mass = 5; 
        
        


       // GameObject pokemonJoueurGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + animator.GetInteger("NumeroPokedexPokemon")), animator.gameObject.transform.position, animator.gameObject.transform.rotation);
      //  pokemonJoueurGameObject.name = "PokemonJoueur";
    }

    public void SortiePokemonPokeball()
    {
        Pokeball = this.gameObject;

        pokemonJoueurGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + Pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemon), Pokeball.transform.position, Pokeball.transform.rotation);
        pokemonJoueurGameObject.name = "PokemonJoueur";
        pokemonJoueurGameObject.tag = "PokemonJoueur";

        GameObject CMVCam1 = GameObject.Find("CM vcam1");
        GameObject CMVCam3 = GameObject.Find("CM vcam3");

        CMVCam1.GetComponent<CinemachineVirtualCamera>().LookAt = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;
        CMVCam1.GetComponent<CinemachineVirtualCamera>().Follow = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;

        CMVCam3.GetComponent<CinemachineVirtualCamera>().LookAt = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;
        CMVCam3.GetComponent<CinemachineVirtualCamera>().Follow = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonJoueurGameObject.transform;
    }

    public void SpawnPokemonAdverse()
    {
        if (GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().EnCombatContre == "PokemonDresseur")
        {
            Pokeball = this.gameObject;

            Vector3 positionPokemonAdverse = new Vector3(pokemonJoueurGameObject.transform.position.x, pokemonJoueurGameObject.transform.position.y, pokemonJoueurGameObject.transform.position.z - 8);

            pokemonAdverseGameObject = (GameObject)Instantiate(Resources.Load("Models/Pokemon/" + Pokeball.GetComponent<AfterThrowingPokeball>().NumeroPokedexPokemonAdverse), positionPokemonAdverse, Pokeball.transform.rotation);
            pokemonAdverseGameObject.name = "PokemonAdverse";
            pokemonAdverseGameObject.tag = "PokemonAdverse";

            pokemonAdverseGameObject.transform.Rotate(0f, 180f, 0f);
        }
        else if(GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().EnCombatContre == "PokemonSauvage")
        {
            pokemonAdverseGameObject = GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().pokemonAdverseGameObject;
            pokemonAdverseGameObject.transform.position = new Vector3(pokemonAdverseGameObject.transform.position.x, pokemonAdverseGameObject.transform.position.y, pokemonAdverseGameObject.transform.position.z - 8);
        }

        GameObject CMVCam2 = GameObject.Find("CM vcam2");

        CMVCam2.GetComponent<CinemachineVirtualCamera>().LookAt = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonAdverseGameObject.transform;
        CMVCam2.GetComponent<CinemachineVirtualCamera>().Follow = Pokeball.GetComponent<AfterThrowingPokeball>().pokemonAdverseGameObject.transform;

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
