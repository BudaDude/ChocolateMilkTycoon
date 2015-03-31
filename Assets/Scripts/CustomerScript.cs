using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class CustomerScript : MonoBehaviour {
    public float speed;
    private bool walking=true;
    public float yPosition;

    public bool deployed;
    public bool alerted;
	private Animator anim;
   
	//
    public int destination = 0;

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

    //walk to stabd
    private bool walkToStand=false;

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
    #region Reset
    private void ResetSelf(){
        gameManager.popularity += happiness;

		walking = true;
		decidedToBuy = false;
		happiness = 0;
        emotionImage.sprite = null;
        deployed = false;
        alerted = false;

	    walkToStand = (Random.value < 0.5);
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
		
		destination = 2;
		SetPersonality();

		xStandOffset = waypoints[3].transform.position.x + Random.Range(-0.5f, 0.5f);
		}
    #endregion


    public void EndDay(){
		ResetSelf ();
		}

    void SetPersonality()
    {
        int temp= gameManager.temperature;
        
        
        sugarDesired= Mathf.Clamp(Mathf.RoundToInt((temp/10)),1,10);
        cocoaDesired = Mathf.Clamp((10 - Mathf.RoundToInt((temp / 10))),1,10); 
    }

    private IEnumerator DecideToBuy()
    {
        walking = false;
        yield return new WaitForSeconds(2.0f);
        if (gameManager.canMakeMilk())
        {
            if (gameManager.salePrice <= maxPriceWilling)
            {
                StartCoroutine(BuyMilk());
            }
            else
            {

                destination = GetExit();
                walking = true;
                StartCoroutine(DisplayFeeling("expensive"));
            }
        }
        else
        {
            destination = GetExit();
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

    IEnumerator DisplayFeeling(string feeling)
    {
        Debug.Log(feeling);
       
        try
        {
            emotionImage.gameObject.SetActive(true);
            emotionImage.sprite = Resources.Load<Sprite>("Emotions/" + feeling);
            
            
        }
        catch
        {
            Debug.LogError("No file named "+feeling);
            emotionImage.gameObject.SetActive(false);
        }
        if (feeling == "alert")
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
        }
        emotionImage.gameObject.SetActive(false);

    }

     public IEnumerator BuyMilk()
     {
        
        if (gameManager.canMakeMilk() == true)
        {
            gameManager.CustomerBuy();
            Feelings();
        }
		anim.SetTrigger("Drinking");
        yield return new WaitForSeconds(1);
        destination = GetExit();
        walking = true;
    }

     private IEnumerator Alert()
    {
        walking = false;
        StartCoroutine(DisplayFeeling("alert"));
        yield return new WaitForSeconds(0.5f);
        
        walking = true;
    }


	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("Walking", walking && !gameManager.paused);
		//layer is equal to y postion, good for overlaping
        gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -Mathf.RoundToInt(transform.position.y * 10);


        if (destination != 3)
        {
            target = new Vector2(waypoints[destination].transform.position.x, yPosition);
        }
        else
        {
          target   = new Vector2(xStandOffset,waypoints[destination].transform.position.y);
        }
        #region walkingStuff
        if (walking == true && gameManager.paused==false && deployed==true)
        {

            float distance = Vector2.Distance(transform.position, target);
            
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (distance < 0.3)
            {
                switch(destination){
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
                        if (walkToStand == true||alerted==true)
                        {
                            destination = 3;
                        }
                        else
                        {
                            destination = GetExit();
                        }
                        break;
                    case 3:
                        
                        Debug.Log("IM BUYING SOME STUFF YO");
                        StartCoroutine(DecideToBuy());
                        
                        break;
                }
            }


        }
        #endregion

    }
    void OnMouseDown()
    {
        Debug.Log("clicked");
        if (alerted == false && destination!= 3)
        { 
            StartCoroutine(Alert());
            destination = 3;
            alerted = true;
            
        }
    }
}
