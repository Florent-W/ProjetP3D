using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class PatrouilleMouvement : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
     //   agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        if (GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().EnCombat == true && agent.isStopped == false)
        {
            agent.isStopped = true;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        if (GameObject.Find("SceneBuilder").GetComponent<ProjetP3DScene1.main>().EnCombat == false && agent.isStopped == true)
        {
            agent.isStopped = false;
        }

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        { 
              GotoNextPoint();
        }
    }

}
