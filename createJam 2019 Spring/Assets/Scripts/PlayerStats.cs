using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int startCurrency;
    public static int currentCurrency;

    private void Start()
    {
        currentCurrency = startCurrency;
    }
}
