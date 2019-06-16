using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class SkateBoardStats : MonoBehaviour {

    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        disableAgent();
    }

    public void setDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
        Debug.Log("Skateboard destination was set");
    }

    public void disableAgent()
    {
        agent.enabled = false;
    }

    public void enableAgent()
    {
        agent.enabled = true;
    }
}
