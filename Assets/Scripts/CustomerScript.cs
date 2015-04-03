using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CustomerScript : MonoBehaviour
{
	public float speed;
	private bool walking = true;
	public bool deployed;
	public bool alerted;
	private Animator anim;
	bool decidedToBuy;
	private Vector2 spawn1;
	private Vector2 spawn2;

	//Shops location
	private Vector2 standPoint;
	private Vector2 spawnPoint;
	private Vector2 exitPoint;
	Vector2 target;

	//Emotions
	int happiness = 0;
	public float maxPriceWilling;
	public int cocoaDesired;
	public int sugarDesired;
	public Image emotionImage;
	private GameManager gameManager;
	private Location location;

	//walk to stabd
	private bool walkToStand = false;

	// Use this for initialization
	void Start ()
	{
		gameManager = GameManager.FindObjectOfType<GameManager> ();
		location = gameManager.currentLocation;
		anim = gameObject.GetComponentInChildren<Animator> ();
		emotionImage = gameObject.GetComponentInChildren<Image> ();
        
		ResetSelf ();
	}
    #region Reset
	private void ResetSelf ()
	{
		if (location) {
			Debug.Log(location.transform.Find("Waypoints").transform.FindChild("Spawn1").name);
			spawn1 = location.transform.Find("Waypoints").transform.FindChild("Spawn1").position;
			spawn2 = location.transform.Find("Waypoints").transform.FindChild("Spawn2").position;
			standPoint = location.gameObject.transform.FindChild ("Stand").transform.position;
		} else {
			ResetSelf();
			return;
		}


		gameManager.popularity += happiness;

		walking = true;
		decidedToBuy = false;
		happiness = 0;
		emotionImage.sprite = null;
		deployed = false;
		alerted = false;

		walkToStand = (Random.value < 0.5);
		emotionImage.gameObject.SetActive (false);
		maxPriceWilling = Random.Range (1.0f, 5.25f);

		if (gameManager.popularity >= 1000) {
			maxPriceWilling += 2;
		} else if (gameManager.popularity >= 100) {
			maxPriceWilling += 1;
		}

		
		
		if (Random.value > 0.5) {
			spawnPoint = spawn1;
			exitPoint=spawn2;
		} else {
			spawnPoint = spawn2;
			exitPoint=spawn1;
		}

		float yPosition = spawnPoint.y + Random.Range (-1, 1);

		transform.position = new Vector2 (spawnPoint.x, yPosition);

		//Rotates the image to face the correct direction
		if (spawnPoint == spawn1) {
			gameObject.GetComponentInChildren<SpriteRenderer> ().gameObject.transform.localEulerAngles = new Vector3 (0, 0, 0);
			
			
		} else if (spawnPoint == spawn2) {
			gameObject.GetComponentInChildren<SpriteRenderer> ().gameObject.transform.localEulerAngles = new Vector3 (0, 180, 0);
			
		}
		target = exitPoint;
		SetPersonality ();

	}
    #endregion


	public void EndDay ()
	{
		ResetSelf ();
	}

	void SetPersonality ()
	{
		int temp = gameManager.temperature;
        
        
		sugarDesired = Mathf.Clamp (Mathf.RoundToInt ((temp / 10)), 1, 10);
		cocoaDesired = Mathf.Clamp ((10 - Mathf.RoundToInt ((temp / 10))), 1, 10); 
	}

	private IEnumerator DecideToBuy ()
	{
		walking = false;
		yield return new WaitForSeconds (2.0f);
		if (gameManager.canMakeMilk ()) {
			if (gameManager.salePrice <= maxPriceWilling) {
				StartCoroutine (BuyMilk ());
			} else {

				target = exitPoint;
				walking = true;
				StartCoroutine (DisplayFeeling ("expensive"));
			}
		} else {
			target = exitPoint;
		}
	}

	void Feelings ()
	{
		float cocoa = gameManager.cocoaAmt;
		float sugar = gameManager.sugarAmt;

		if (sugar == sugarDesired) {
			happiness += 3;

            
		} else if (sugar == (sugarDesired - 1) || sugar == (sugarDesired + 1)) { //Check if amount is close and rewards the player
			happiness += 1;

           

		} else if (sugar <= (sugarDesired - 5) || sugarDesired >= (sugarDesired + 5)) {
			happiness -= 1;
            
		}

		if (cocoa == cocoaDesired) {
			happiness += 3;
            

		} else if (cocoa == (cocoaDesired - 1) || cocoa == (cocoaDesired + 1)) {//Check if amount is close and rewards the player
			happiness += 1;
            

		} else if (cocoa <= (cocoaDesired - 5) || cocoa >= (cocoaDesired + 5)) {
			happiness -= 1;
            

		}

        
        

	}

	IEnumerator DisplayFeeling (string feeling)
	{
		Debug.Log (feeling);
       
		try {
			emotionImage.gameObject.SetActive (true);
			emotionImage.sprite = Resources.Load<Sprite> ("Emotions/" + feeling);
            
            
		} catch {
			Debug.LogError ("No file named " + feeling);
			emotionImage.gameObject.SetActive (false);
		}
		if (feeling == "alert") {
			yield return new WaitForSeconds (0.5f);
		} else {
			yield return new WaitForSeconds (1.5f);
		}
		emotionImage.gameObject.SetActive (false);

	}

	public IEnumerator BuyMilk ()
	{
        
		if (gameManager.canMakeMilk () == true) {
			gameManager.CustomerBuy ();
			Feelings ();
		}
		anim.SetTrigger ("Drinking");
		yield return new WaitForSeconds (1);
		target = exitPoint;
		walking = true;
	}

	private IEnumerator Alert ()
	{
		walking = false;
		StartCoroutine (DisplayFeeling ("alert"));
		yield return new WaitForSeconds (0.5f);
        
		walking = true;
	}


	
	// Update is called once per frame
	void Update ()
	{
		anim.SetBool ("Walking", walking && !gameManager.paused);
		//layer is equal to y postion, good for overlaping
		gameObject.GetComponentInChildren<SpriteRenderer> ().sortingOrder = -Mathf.RoundToInt (transform.position.y * 10);


		#region walkingStuff
		if (walking == true && gameManager.paused == false && deployed == true) {

			float distance = Vector2.Distance (transform.position, target);
            
			transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime);
			if (distance < 0.5) {
               
				if (target == exitPoint) {
					Debug.Log ("exited at"+target.ToString());
					if (gameManager.nearEndOfDay == false) {
						ResetSelf ();
					} else {
						EndDay ();
					}
				} else if (target == standPoint) {
                        
					Debug.Log ("IM BUYING SOME STUFF YO");
					StartCoroutine (DecideToBuy ());


				}
			}


		}
		#endregion

	}

	void OnMouseDown ()
	{
		Debug.Log ("clicked");
		if (alerted == false && target != standPoint) { 
			StartCoroutine (Alert ());
			target = standPoint;
			alerted = true;
            
		}
	}
}
