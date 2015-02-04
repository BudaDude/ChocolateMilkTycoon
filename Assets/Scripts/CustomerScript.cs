using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class CustomerScript : MonoBehaviour {
    public float speed;
    private bool walkingBy=true;
    public float yPosition;

    public bool deployed;
	private Animator anim;
   
	//
    public int waypointNumber = 0;

    bool decidedToBuy;

    public List<GameObject> waypoints;
    private int spawnPoint;
    Vector2 target;

    //Emotions
    int happiness = 0;
    public float maxPriceWilling;
    public int cocoaDesired;
    public int sugarDesired;
    public Image emotionImage;

    float xStandOffset;

	private GameManager gameManager;

    void Awake()
    {
        waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        waypoints.Reverse();

		gameManager = GameObject.FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
        


    }

	// Use this for initialization
	void Start () {

		anim = gameObject.GetComponentInChildren<Animator> ();
        emotionImage = gameObject.GetComponentInChildren<Image>();
        
		ResetSelf ();
	}
	private void ResetSelf(){
        gameManager.popularity += happiness;

		walkingBy = true;
		decidedToBuy = false;
		happiness = 0;
        emotionImage.sprite = null;
        deployed = false;

        emotionImage.gameObject.SetActive(false);
		maxPriceWilling = Random.Range(1.0f, 5.25f);

        if (gameManager.popularity >= 1000)
        {
            maxPriceWilling += 2;
        }
        else if (gameManager.popularity >= 100)
        {
            maxPriceWilling += 1;
        }

		
		
		spawnPoint = Random.Range(0,2);

        yPosition = waypoints[spawnPoint].transform.position.y;

		transform.position = new Vector2(waypoints[spawnPoint].transform.position.x,yPosition);

		//Rotates the image to face the correct direction
		if (spawnPoint == 0) {
			gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localEulerAngles = new Vector3(0,0,0);
			
			
		} else if (spawnPoint == 1) {
            gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localEulerAngles = new Vector3(0, 180, 0);
			
		}
		
		waypointNumber = 2;
		SetPersonality();

		xStandOffset = waypoints[3].transform.position.x + Random.Range(-0.5f, 0.5f);
		}


	public void EndDay(){
		ResetSelf ();
		}

    void SetPersonality()
    {
        int temp= gameManager.temperature;
        
        
        sugarDesired= Mathf.Clamp(Mathf.RoundToInt((temp/10)),1,10);
        cocoaDesired = Mathf.Clamp((10 - Mathf.RoundToInt((temp / 10))),1,10); 


    }

    private bool DecideToBuy()
    {
        if (gameManager.canMakeMilk())
        {
            if (gameManager.salePrice <= maxPriceWilling)
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
        if (decidedToBuy == true && gameManager.nearEndOfDay==false)
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
        float cocoa = gameManager.cocoaAmt;
        float sugar = gameManager.sugarAmt;

        if (sugar == sugarDesired)
        {
            happiness += 3;
            
        }
        else if (sugar == (sugarDesired - 1) || sugar == (sugarDesired + 1))
        { //Check if amount is close and rewards the player
            happiness += 1;
           

        }
        else if (sugar <= (sugarDesired - 5) || sugarDesired >= (sugarDesired + 5))
        {
            happiness -= 1;
            
        }

		if (cocoa == cocoaDesired)
		{
			happiness += 3;
            

		}
		else if(cocoa==(cocoaDesired-1) || cocoa == (cocoaDesired+1))//Check if amount is close and rewards the player
		{
			happiness += 1;
            

		} else if (cocoa <= (cocoaDesired - 5) || cocoa >= (cocoaDesired+ 5)) {
			happiness-=1;
            

		}

        
        

    }

    void DisplayFeelings()
    {
        emotionImage.gameObject.SetActive(true);

        if (happiness >= 3)
        {
            emotionImage.sprite = Resources.Load<Sprite>("Emotions/love");
        }
        else if (happiness == 1 | happiness == 2)
        {
            emotionImage.sprite = Resources.Load<Sprite>("Emotions/happy");

        }
        else if (happiness == 0)
        {
            emotionImage.sprite = Resources.Load<Sprite>("Emotions/okay");

        }
        else if (happiness < 0)
        {
            emotionImage.sprite = Resources.Load<Sprite>("Emotions/sad");

        }
    }

     public IEnumerator BuyMilk()
    {
        yield return new WaitForSeconds(1);
        if (gameManager.canMakeMilk() == true)
        {
            gameManager.CustomerBuy();
            Feelings();
        }
		anim.SetTrigger("Drinking");
        yield return new WaitForSeconds(0.75f);
        DisplayFeelings();
       
        waypointNumber = GetExit();
        walkingBy = true;
    }
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("Walking", walkingBy && !gameManager.paused);
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
        if (walkingBy == true && gameManager.paused==false && deployed==true)
        {

            float distance = Vector2.Distance(transform.position, target);
            
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (distance < 0.3)
            {
                switch(waypointNumber){
                    case 0:
                        if (GetExit() == 0)
                        {
                            if (gameManager.nearEndOfDay == false)
                            {
							ResetSelf();
                            }else{
							EndDay();
						}
                        }

                        break;
                    case 1:
                        if (GetExit() == 1)
                        {
                            if (gameManager.nearEndOfDay == false)
                            {
							ResetSelf();
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
