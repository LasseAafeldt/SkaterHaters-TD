using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager singleton;

    private void Awake()
    {
        if(singleton != null)
        {
            Debug.Log("There was more than one GameManager in the scene");
            Destroy(gameObject);
            return;
        }
        singleton = this;
    }
    #endregion

    public Transform sboard;

    private void Start()
    {
        sboard = GameObject.FindGameObjectWithTag("Skateboard").transform;
    }
}
