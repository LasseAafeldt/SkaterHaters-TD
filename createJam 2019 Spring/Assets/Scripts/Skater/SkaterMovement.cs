using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class SkaterMovement : MonoBehaviour {

    public Transform sBoard;
    public Transform skaterFeet;
    public Transform start;
    public float goalDistanceThreshold;
    UnityEngine.AI.NavMeshAgent nav;

    bool skateboard;
    public static bool aSkaterHasSkateboard;
    public delegate void SkateboardPickupDelegate();
    public event SkateboardPickupDelegate skateboardPickupEvent;


    void Awake() {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Start()
    {
        skateboard = false;
        aSkaterHasSkateboard = false;
    }

    void Update() {
        //this is temp
        UpdateMovementGoal();
    }

    void UpdateMovementGoal() {
        if (!skateboard)
        {
            Vector3 skateboardXY = new Vector3(sBoard.position.x, transform.position.y, sBoard.position.z);
            nav.SetDestination(skateboardXY);
        }
        else
        {
            Vector3 spawnXY = new Vector3(start.position.x, transform.position.y, start.position.z);
            //        nav.SetDestination(start.position);
            //check if goal is reached

            if (Vector3.Distance(nav.transform.position, spawnXY) < goalDistanceThreshold)
            {

                //skateboard = false;
                transform.SetParent(null);
                //nav.enabled = true;

                GameOver();
            }
        }
        /*if (Vector3.Distance(nav.transform.position, sBoard.position) < goalDistanceThreshold) {
            skateboard = true;
        }*/
    }

    //check if skater runs into skateboard
    private void OnTriggerEnter(Collider other)
    {
        if (aSkaterHasSkateboard || other.gameObject.layer != LayerMask.NameToLayer("Skateboard"))
            return;
        //Debug.Log("collision with: " + other.name);

        //skater picks up skateboard
        OnSkateboardPickup();
    }

    void OnSkateboardPickup()
    {
        skateboard = true;
        // calleing out that the skateboard has been picked up
        if (skateboardPickupEvent != null)
            skateboardPickupEvent();

        //attach the skatebaord to the skater who picked it up
        sBoard.position = skaterFeet.position;
        sBoard.rotation = transform.rotation;
        sBoard.Rotate(skaterFeet.up, 90f);
        transform.SetParent(sBoard);

        //a skater has picked up the skateboard
        aSkaterHasSkateboard = true;

        // set destination for skateboard
        SkateBoardStats sBoardStats = sBoard.GetComponent<SkateBoardStats>();
        if(sBoardStats != null)
        {
            //disable skater nav agent
            nav.enabled = false;
            //enable the nav agent for skateboard
            sBoardStats.enableAgent();
            sBoardStats.setDestination(start.position);//new Vector3(start.position.x, transform.position.y, start.position.z));
        }
        else
        {
            Debug.LogError("Skateboard object has not SkateBoardStats component!");
        }
    }

    void GameOver()
    {
        Debug.Log("Player has lost the game... The skaters got their skateboard back!");
        //make delegate and handle in gameManager script or something
    }
}