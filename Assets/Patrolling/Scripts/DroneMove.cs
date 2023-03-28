using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMove : MonoBehaviour
{
    [SerializeField]
    Transform destination;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if(navMeshAgent == null)
        {
            Debug.LogError("The NavMeshAgent component is not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }

    void SetDestination()
    {
        if(destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

    
    
}
