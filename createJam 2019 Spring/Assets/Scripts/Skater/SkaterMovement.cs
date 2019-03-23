using UnityEngine;
using System.Collections;

public class SkaterMovement : MonoBehaviour {

    Transform sBoard;               
    Transform start;
    bool skateboard = false;
    public float goalDistanceThreshold;
    UnityEngine.AI.NavMeshAgent nav;               


    void Awake() {
        sBoard = GameObject.FindGameObjectWithTag("Skateboard").transform;
        start = GameObject.FindGameObjectWithTag("Start").transform;

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update() {
        if (skateboard == false) {
            GetSkateboard();
        } else {
            GotSkateboard();
        }
    }

    void GetSkateboard() {
        nav.SetDestination(sBoard.position);
        if (Vector3.Distance(nav.transform.position, sBoard.position) < goalDistanceThreshold) {
            skateboard = true;
        }
    }
    
    void GotSkateboard() {
        nav.SetDestination(start.position);
        
        if (Vector3.Distance(nav.transform.position, start.position) < goalDistanceThreshold) {
            skateboard = false;
        }
        
    }
}