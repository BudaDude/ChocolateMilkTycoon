using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CustomerScript : MonoBehaviour {
    private float speed;
    private bool walkingBy=true;
    public float yPosition;
   
	//
    public int waypointNumber = 0;

    bool decidedToBuy;

    public List<GameObject> waypoints;
    private int spawnPoint;
    Vector2 target;

    //Emotions
    int happiness = 0;
    public int maxPriceWilling;
    public int cocoaDesired;
    public int sugarDesired;

    float xStandOffset;

    void Awake()
    {
        waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        waypoints.Reverse();
    }

	// Use this for initialization
	void Start () {

        walkingBy = true;
        decidedToBuy = false;
		happiness = 0;

        maxPriceWilling = Random.Range(4, 10);

        yPosition = -Random.Range(1.1f, 2f);
        
        spawnPoint = Random.Range(0,2);
        transform.position = new Vector2(waypoints[spawnPoint].transform.position.x,yPosition);
        waypointNumber = 2;
        SetPersonality();
        

        
        
        
        xStandOffset = waypoints[3].transform.position.x + Random.Range(-0.5f, 0.5f);
	}

	public void EndDay(){
		GameManager.instance.popularity += happiness;
		Start ();
		}

    void SetPersonality()
    {
        int temp= GameManager.instance.temperature;
        Debug.Log(temp);
        speed = Random.Range(0, 1.0f)+1.5f;
        sugarDesired= Mathf.RoundToInt((temp/10));
        cocoaDesired = 10 - Mathf.RoundToInt((temp / 10)); 


    }

    private bool DecideToBuy()
    {
        if (GameManager.instance.canMakeMilk())
        {
            if (GameManager.instance.salePrice <= maxPriceWilling)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        else
        {
            return false;
        }
    }
    private int GetExit()
    {
        Debug.Log("IM PEACING OUT");
        //walk away toward
        if (spawnPoint == 0) 
        {
            return 1;
           
        }else if (spawnPoint == 1)
        {
            return 0;
        }
        else
        {
            Debug.LogError("YO WHERE DID I SPAWN");
            return 0;
        }

    }

    void EvaluateDecision()
    {
        if (decidedToBuy == true && GameManager.instance.nearEndOfDay==false)
        {
            waypointNumber = 3;
        }
        else
        {
            waypointNumber = GetExit();
        }
    }

    void Feelings()
    {
        float cocoa = GameManager.instance.cocoaAmt;
        float sugar = GameManager.instance.sugarAmt;

        if (sugar == sugarDesired) {
						happiness += 2;
				} else if (sugar == sugarDesired - 1 || sugar == sugarDesired + 1) { //Check if amount is close and rewards the player
						happiness += 1;
				} else if (sugar < sugarDesired - 5 || sugarDesired > sugarDesired + 5) {
			happiness-=1;
				}

		if (cocoa ==cocoaDesired)
		{
			happiness += 2;
		}
		else if(cocoa==cocoaDesired-1 || cocoa ==cocoaDesired+1)//Check if amount is close and rewards the player
		{
			happiness += 1;
		} else if (cocoa < cocoaDesired - 5 || cocoa > cocoaDesired+ 5) {
			happiness-=1;
		}

    }

     public IEnumerator BuyMilk()
    {
        yield return new WaitForSeconds(2);
        if (GameManager.instance.canMakeMilk() == true)
        {
            GameManager.instance.CustomerBuy();
            Feelings();
        }
        yield return new WaitForSeconds(.5f);
        Debug.Log("YUM");
        waypointNumber = GetExit();
        walkingBy = true;
    }
	
	// Update is called once per frame
	void Update () {
		//layer is equal to y postion, good for overlaping
        gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(transform.position.y * 10);


        if (waypointNumber != 3)
        {
            target = new Vector2(waypoints[waypointNumber].transform.position.x, yPosition);
        }
        else
        {
          target   = new Vector2(xStandOffset,waypoints[waypointNumber].transform.position.y);
        }
        if (walkingBy == true && GameManager.instance.paused==false)
        {
            float distance = Vector2.Distance(transform.position, target);
            
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (distance < 0.3)
            {
                switch(waypointNumber){
                    case 0:
                        if (GetExit() == 0)
                        {
                            if (GameManager.instance.nearEndOfDay == false)
                            {
                                Start();
                            }else{
							EndDay();
						}
                        }

                        break;
                    case 1:
                        if (GetExit() == 1)
                        {
                            if (GameManager.instance.nearEndOfDay == false)
                            {
                                Start();
                            }else{
							EndDay();
						}
                        }
                        break;
                    case 2:
                        decidedToBuy = DecideToBuy();
                        EvaluateDecision();
                        break;
                    case 3:
                        walkingBy = false;
                        Debug.Log("IM BUYING SOME STUFF YO");
                        StartCoroutine(BuyMilk());
                        break;
                }
            }
            
        }


	}
}
