using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class SkateBoardStats : MonoBehaviour {

    NavMeshAgent agent;

    public List<skaterBehaviour> waitingSkaters;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        waitingSkaters = new List<skaterBehaviour>();
    }

    private void Start()
    {
        disableAgent();
    }

    public void setDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
        //Debug.Log("Skateboard destination was set");
    }

    public void disableAgent()
    {
        agent.enabled = false;
    }

    public void enableAgent()
    {
        agent.enabled = true;
    }

    public void addSkaterToQue(skaterBehaviour objToAdd)
    {
        if (waitingSkaters.Contains(objToAdd))
        {
            Debug.Log("Object " + objToAdd.name + " is already in the que and was therefore not added");
            return;
        }
            
        waitingSkaters.Add(objToAdd);
    }

    public void removeSkaterFromQue(skaterBehaviour objToRemove)
    {

        if (waitingSkaters.Contains(objToRemove))
        {
            waitingSkaters.Remove(objToRemove);
        }
    }
    public void removeSkaterFromQue()
    {
        waitingSkaters.RemoveAt(0);
    }


}
