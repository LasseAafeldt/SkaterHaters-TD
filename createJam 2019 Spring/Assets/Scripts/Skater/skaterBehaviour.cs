using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class skaterBehaviour : MonoBehaviour {
    public Transform skaterFeet;
    public float goalDistanceThreshold;
    [SerializeField] private SkaterStats unitStats;

    [Header("Debug ONLY")]
    [SerializeField] private float currentHealth;
    private float moveSpeed;
    private float despawnTime;
    private bool isDead = false;

    private Vector3 start;
    private Transform sBoard;
    private SkateBoardStats sBoardStats;
    private NavMeshAgent nav;

    private static bool gameOver = false;
    private bool isOnSkateboard;


    void Awake() {
        nav = GetComponent<NavMeshAgent>();
        if(unitStats == null)
            Debug.LogError("Missing Skater Scriptable objects");
    }

    private void Start()
    {
        sBoard = GameManager.singleton.sboard;
        isOnSkateboard = false;
        //aSkaterHasSkateboard = false;
        start = transform.position;

        currentHealth = unitStats.getMaxHealth();
        moveSpeed = unitStats.getMoveSpeed();
        despawnTime = unitStats.getDeathDespawnTime();
        sBoardStats = sBoard.GetComponent<SkateBoardStats>();
        nav.speed = moveSpeed;
    }

    void Update() {
        //this is temp
        UpdateMovementGoal();
    }

    void UpdateMovementGoal() {
        if (gameOver || isDead)
        {
            return;
        }
        if (!isOnSkateboard)
        {
            Vector3 skateboardXY = new Vector3(sBoard.position.x, transform.position.y, sBoard.position.z);
            nav.SetDestination(skateboardXY);
        }
        else
        {
            Vector3 spawnXY = new Vector3(start.x, transform.position.y, start.z);
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
    }

    //check if skater runs into skateboard
    private void OnTriggerEnter(Collider other)
    {
        //if (aSkaterHasSkateboard || other.gameObject.layer != LayerMask.NameToLayer("Skateboard"))
            //return;
        //Debug.Log("collision with: " + other.name);

        //skater picks up skateboard
        OnSkateboardPickup();
    }

    void OnSkateboardPickup()
    {
        isOnSkateboard = true;
        sBoard.position = skaterFeet.position;
        sBoard.rotation = transform.rotation;
        sBoard.Rotate(skaterFeet.up, 90f);
        transform.SetParent(sBoard);

        if(sBoardStats != null)
        {
            //disable skater nav agent
            nav.enabled = false;
            //enable the nav agent for skateboard
            sBoardStats.enableAgent();
            sBoardStats.setDestination(start);
            
        }
        else
        {
            Debug.LogError("Skateboard object has not SkateBoardStats component!");
        }
    }

    void GameOver()
    {
        Death();
        Debug.Log("Player has lost the game... The skaters got their skateboard back!");
        //gameOver = true;
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        stopMovement();

        //Change layer to dead or similar
        gameObject.layer = LayerMask.NameToLayer("Death");
        //Do death animation?

        StartCoroutine(DeSpawnDead(despawnTime));
    }

    IEnumerator DeSpawnDead(float timeForRemoval)
    {
        yield return new WaitForSeconds(timeForRemoval);
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);//if we don't use a dictionary then use destroy here instead
        Debug.LogWarning("A skater was set inactive instead of detroyed... Remember to create a dictionary");
    }

    void stopMovement()
    {
        if (isOnSkateboard)
        {
            sBoardStats.setDestination(transform.position);
            sBoardStats.disableAgent();
            return;
        }
        //nav.SetDestination(transform.position);
        nav.isStopped = true;
        //nav.ResetPath();
    }

    public bool getIsOnSkateboard()
    {
        return isOnSkateboard;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Skater")))
        {
            //Debug.Log("I have hit another skater!" + collision.gameObject.name);
            skaterBehaviour otherSkater = collision.gameObject.GetComponent<skaterBehaviour>();
            if (otherSkater.getIsOnSkateboard())
            {
                //do some que system
                Debug.Log("I will que up now");
                
            }
        }
    }
}