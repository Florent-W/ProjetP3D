using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class PatrouilleMouvement : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    private int destPoint = 0;
    private NavMeshAgent agent;
    public bool pokemonAllier = false;
    GameObject VthirdPersonCamera, SceneBuilder, PokemonJoueurGameobject, PokemonAdverseGameObject;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        VthirdPersonCamera = GameObject.Find("vThirdPersonCamera");
        SceneBuilder = GameObject.Find("SceneBuilder");

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        //   agent.autoBraking = false;

        if (pokemonAllier == false)
        {
            GotoNextPoint();
        }
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Count == 0)
            return;

        if (SceneBuilder.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0].enCombat == true && agent.isStopped == false)
        {
            agent.isStopped = true;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Count;
    }

    void GotoNextPointJoueur()
    {
        // Returns if no points have been set up
        if (points.Count == 0)
            return;

        if (SceneBuilder.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0].enCombat == true && agent.isStopped == false || SceneBuilder.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0].enCombat == false && pokemonAllier == true && agent.isStopped == false && agent.remainingDistance <= 5)
        {
            agent.isStopped = true;
            if (agent.GetComponent<Invector.CharacterController.vThirdPersonController>() != null)
            { 
                agent.GetComponent<Animator>().SetFloat("InputVertical", 0); // On enlève l'animation de marche
            }
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = VthirdPersonCamera.transform.position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Count;
    }

    void GotoNextPointAttaqueAvanceVersPokemonAdverse()
    {
        /*
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        GameObject gameObjectJoueur = GameObject.Find("PokemonJoueur");
        GameObject gameObjectAdverse = GameObject.Find("PokemonAdverse");

    //  Debug.Log(gameObjectAdverse.transform.position);
    */
        // Set the agent to go to the currently selected destination.
        agent.destination = PokemonAdverseGameObject.transform.position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Count;
    }


    void Update()
    {
       if (!agent.isOnNavMesh) // Si l'agent n'est pas sur le navmesh, on arrete
           return;

       if (SceneBuilder.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0].enCombat == false && agent.isStopped == true || SceneBuilder.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0].enCombat == false && pokemonAllier == true && agent.isStopped == true && agent.remainingDistance > 5)
       {
                agent.isStopped = false;
        }

        if (agent.GetComponent<Invector.CharacterController.vThirdPersonController>() != null) // Si c'est un personnage, on active l'animation de marche
        {
            if (agent.isStopped == false)
            {
                agent.GetComponent<Animator>().SetFloat("InputVertical", 0.8f);
            }
            else
            {

            }
        }

            float DistancePokemonJoueur = (this.gameObject.transform.position - VthirdPersonCamera.transform.position).sqrMagnitude;

            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.5f && pokemonAllier == false)
            {
                GotoNextPoint();
            }
        else if (DistancePokemonJoueur < 25 && SceneBuilder.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0].starterChoisi == true && SceneBuilder.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0].enCombat == false && pokemonAllier == false)
            {
                GotoNextPointJoueur();
            }
            else if (agent.remainingDistance <= 5 && (SceneBuilder.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0].starterChoisi == true || agent.gameObject.tag == "Allier") && SceneBuilder.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0].enCombat == false && pokemonAllier == true)
            {
                GotoNextPointJoueur();
        }
        else if (SceneBuilder.GetComponent<ProjetP3DScene1.main>().JoueurManager.Joueurs[0].enCombat == true)
            {
                //  PokemonJoueurGameobject = SceneBuilder.GetComponent<ProjetP3DScene1.main>().pokemonJoueurGameObject;
                //  PokemonAdverseGameObject = SceneBuilder.GetComponent<ProjetP3DScene1.main>().pokemonJoueurGameObject;
                //  SceneBuilder.GetComponent<AttaqueMouvementCombatScript>().GoToNextPNJ(PokemonJoueurGameobject, PokemonAdverseGameObject);
            }
        }
}
