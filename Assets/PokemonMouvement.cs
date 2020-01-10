using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PokemonMouvement : MonoBehaviour
{
   // [SerializeField]
   // Transform _destination;

    NavMeshAgent _navmeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        _navmeshAgent = this.GetComponent<NavMeshAgent>();

        if(_navmeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached");
        }
        else
        {
            SetDestination();
        }
    }

    private void SetDestination()
    {
       // if(_destination != null)
       // {
            Vector3 destination = new Vector3(this.transform.position.x - 14, this.transform.position.y, this.transform.position.z);
            Vector3 destination2 = new Vector3(this.transform.position.x + 14, this.transform.position.y, this.transform.position.z);

            Vector3 targetVector = destination;
            _navmeshAgent.SetDestination(targetVector);
            _navmeshAgent.SetDestination(destination2);
        // }
    }
}
