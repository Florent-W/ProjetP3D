using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Si le pokémon n'est pas dans le navmesh autour du joueur, on détruit le pokémon
public class NavMeshPokemonDestroy : MonoBehaviour
{
    public GameObject mapGenerator;

    private void Start()
    {
        mapGenerator = GameObject.Find("Map Generator");
        StartCoroutine(coroutineAgentOnNavMesh());
    }

    // Si c'est elevé, le jeu sera lent
    float onMeshThreshold = 3;

    public bool IsAgentOnNavMesh(GameObject agentObject)
    {
        Vector3 agentPosition = agentObject.transform.position;
        NavMeshHit hit;

        // Regarde le point le plus proche sur le navmesh vers l'agent, dans le meshThreshold Check for nearest point on navmesh to agent
        if (NavMesh.SamplePosition(agentPosition, out hit, onMeshThreshold, NavMesh.AllAreas))
        {
            // On regarde la longueur et la profondeur
            if (Mathf.Approximately(agentPosition.x, hit.position.x)
                && Mathf.Approximately(agentPosition.z, hit.position.z))
            {
                // On regarde la hauteur
                return agentPosition.y >= hit.position.y;
            }
        }

        return false;
    }

    IEnumerator coroutineAgentOnNavMesh()
    {
        if (!IsAgentOnNavMesh(this.gameObject))
        {
            Destroy(this.gameObject);
        }

        yield return new WaitForSeconds(1f);
    }
}
