using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttaqueMouvementCombatScript : MonoBehaviour
{
    GameObject SceneBuilder, pnjADeplacer, pnjDestination;
    Vector3 positionInitialPnjADeplacer;
    NavMeshAgent navMeshAgentPnjADeplacer;

    public string animationStatutAttaqueContrePokemonAdversaire;

    // Start is called before the first frame update
    void Start()
    {
        SceneBuilder = GameObject.Find("SceneBuilder");
    }

    IEnumerator PNJAvanceRevientPositionInitiale()
    {
        while (navMeshAgentPnjADeplacer.remainingDistance > 3)
        {
            yield return new WaitForSeconds(0.1f);
        }

        animationStatutAttaqueContrePokemonAdversaire = "RevientPositionInitiale";
        navMeshAgentPnjADeplacer.angularSpeed = 0;
        navMeshAgentPnjADeplacer.destination = positionInitialPnjADeplacer;


        while (navMeshAgentPnjADeplacer.remainingDistance > 1)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (navMeshAgentPnjADeplacer.gameObject.name == "PokemonJoueur")
        {
            Destroy(navMeshAgentPnjADeplacer.gameObject.transform.GetChild(navMeshAgentPnjADeplacer.gameObject.transform.childCount - 1).gameObject); // On enlève l'animation qui est au dernier index du pokémon
        }
        // navMeshAgentPnjADeplacer.angularSpeed = 120;
        animationStatutAttaqueContrePokemonAdversaire = "Fini";
        navMeshAgentPnjADeplacer.isStopped = true;
    }

    public void GoToNextPNJ(GameObject pnjADeplacer, GameObject pnjDestination)
    {
       // Debug.Log(SceneBuilder.GetComponent<ProjetP3DScene1.main>().attaqueLancerPokemon.getId());

        if (pnjADeplacer.name == "PokemonJoueur")
        {
           GameObject particulesAttaqueGameObject = Instantiate(SceneBuilder.transform.GetChild(0).gameObject.transform.GetChild(SceneBuilder.GetComponent<ProjetP3DScene1.main>().attaqueLancerPokemon.getId() - 1).gameObject, pnjADeplacer.transform); // On active l'animation de l'attaque en l'instantiant sur le pokémon
            particulesAttaqueGameObject.SetActive(true); // On active l'animation
            // SceneBuilder.GetComponent<ProjetP3DScene1.main>().transform.GetChild(0).gameObject.GetComponent<ActiverAttaqueParticule>().activerParticuleAttaque(SceneBuilder.GetComponent<ProjetP3DScene1.main>().attaqueLancerPokemon.getId()); // On lance l'attaque avec son id
        }

        this.pnjADeplacer = pnjADeplacer;
        this.pnjDestination = pnjDestination;
        this.positionInitialPnjADeplacer = pnjADeplacer.transform.position;

        this.navMeshAgentPnjADeplacer = pnjADeplacer.GetComponent<NavMeshAgent>();

        navMeshAgentPnjADeplacer.isStopped = false;
        animationStatutAttaqueContrePokemonAdversaire = "PremierDeplacement";
        navMeshAgentPnjADeplacer.destination = pnjDestination.transform.position;

        StartCoroutine("PNJAvanceRevientPositionInitiale");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
