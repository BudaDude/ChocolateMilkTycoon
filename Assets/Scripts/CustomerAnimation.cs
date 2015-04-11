using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class CustomerAnimation : MonoBehaviour
{
    public int fps;
    private CustomerScript customerScript;
    public SpriteRenderer CustomerSpriteRenderer;

    private float timeElapsed;

    private Sprite[] spriteSheet;
    private Sprite[] drinkingAnim = new Sprite[4];
    private Sprite[] walkingAnim = new Sprite[4];
    private Sprite idleAnim;

    public bool walking;
    public bool drinking;

    private bool isWalking;
    private bool isDrinking;



    void Awake()
    {
        
        CustomerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        customerScript = gameObject.GetComponentInParent<CustomerScript>();
        spriteSheet = (Resources.LoadAll<Sprite>("Customers/"+customerScript.customerName) as Sprite[]);

        if (spriteSheet.Length > 1)
        {
            for (int i = 0; i < spriteSheet.Length; i++)
            {


                if (i <= 3)
                {
                    walkingAnim[i] = spriteSheet[i];
                }
                else if (i > 3 && i <= 7)
                {
                    drinkingAnim[i - 4] = spriteSheet[i];
                }
            }
            idleAnim = spriteSheet[8];

        }
    }


    private IEnumerator walk()
    {
        foreach (Sprite spr in walkingAnim){

            CustomerSpriteRenderer.sprite = spr;
            yield return new WaitForSeconds(1.0f/fps);

        }
        isWalking = false;
    }
    private IEnumerator drink()
    {
        foreach (Sprite spr in drinkingAnim)
        {

            CustomerSpriteRenderer.sprite = spr;
            yield return new WaitForSeconds(1.0f / fps);

        }
        drinking = false;
        isDrinking = false;
    }
	
	// Update is called once per frame
	void LateUpdate ()
	{
	    if (customerScript.deployed == true)
	    {
	        timeElapsed += Time.deltaTime;


	        
	        if (walking == true && isWalking==false)
	        {
	            isWalking = true;
	            StartCoroutine(walk());
	        }
            else if (drinking == true && isDrinking == false)
	        {
	            isDrinking = true;
	            StartCoroutine(drink());
	        }else if (walking==false && drinking==false)
	        {
                CustomerSpriteRenderer.sprite = idleAnim;
	        }
	    }

	}
}
