using UnityEngine;
using System.Collections.Generic;


public class CustomerManager : MonoBehaviour {
    public float waitTime;

    private CustomerScript[] customers;

    public List<CustomerScript> readyToDeploy;

    private GameManager gameManager;

    private float timer=0;

    private float additionalTime;

    

	// Use this for initialization
	void Start () {
        gameManager = GameManager.FindObjectOfType<GameManager>()
            ;
        customers = GameObject.FindObjectsOfType<CustomerScript>();
	}

	
	// Update is called once per frame
	void Update () {
        if (gameManager.paused == false)
        {
            timer += Time.deltaTime;
        }
        if (timer >= (waitTime - (gameManager.popularity / 100)))
        {
            if (readyToDeploy.Count > 0)
            {
                readyToDeploy[0].deployed = true;
                readyToDeploy.RemoveAt(0);
                
            }
            timer = 0;
        }

        foreach (CustomerScript cs in customers)
        {
            if (cs.deployed == false && !readyToDeploy.Contains(cs) )
            {
                readyToDeploy.Add(cs);
            }
        }
	}
}
