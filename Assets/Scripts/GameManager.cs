using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public float money;
    public float salePrice;



    private int sales;
    private float moneyEarned;

    public int inventory;

    public int popularity;


    //inventory stuff
    public int cocoaInventory;
    public float cocoaPrice;

    public int sugarInventory;
    public float sugarPrice;

    public int milkInventory;
    public float milkPrice;

    //Time stuff
    private int minute;
    private int hour=6;
    public float timeMult=2;
    public int day=1;

    //recipe stuff
    public int cocoaAmt;
    public int sugarAmt;

    public bool endOfDay=false;
	public bool paused=true;

    public float tickTimer;


    //Weather Stuff
    public int temperature=60;


    //Singleton crap
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        //More Singleton crap
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void TimeCycle()
    {
        minute += 1;
        if (minute >= 60)
        {
            hour += 1;
            minute = 0;
            //check if you can still sell stuff
            if (!canMakeMilk())
            {
                EndDay();
            }
        }
    }

    public string GetTime()
    {
        if (hour > 12)
        {
            if (minute < 10)
            {
                return hour-12 + ":" + "0" + minute+ " PM";
            }
            else
            {
                return hour - 12 + ":" + minute + " PM";
            }
        }
        else
        {
            if (minute < 10)
            {
                return hour + ":" + "0" + minute + " AM";
            }
            else
            {
                return hour + ":" + minute + " AM";
            }
        }

    }




    //for buttons to buy stuff. Checks how much money has and evulates if it should add more to the inventory
    public void BuyMilk()
    {
        if (money >= milkPrice)
        {
            milkInventory += 10;
            money -= milkPrice;

        }
    }
    public void BuySugar()
    {
        if (money >= sugarPrice)
        {
            sugarInventory += 10;
            money -= sugarPrice;
        }
    }
    public void BuyCocoa()
    {
        if (money >= cocoaPrice)
        {
            cocoaInventory += 10;
            money -= cocoaPrice;
        }
    }

    
    
    
	void Start () {
        
	}

	public void StartDay(){
        if (milkInventory >= 0 && cocoaInventory >= cocoaAmt && sugarInventory >= sugarAmt)
        {
            UIManager.instance.OpenCloseRecipeMenu();
            hour = 10;
            minute = 0;
            paused = false;
            day += 1;
            endOfDay = false;
            moneyEarned = 0;
            sales = 0;
        }
        else
        {
            UIManager.instance.OpenMessageBox("WARNING", "You don't have enough items to start the day with the current recipe!",null);
        }
		}
    public void EndDay()
    {

        UIManager.instance.OpenMessageBox("End of Day " + day, "Money Earned: " + moneyEarned,UIManager.instance.OpenCloseRecipeMenu);
        

    }

    public bool canMakeMilk()
    {
        if (cocoaInventory >= cocoaAmt && sugarInventory >= sugarAmt && milkInventory != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CustomerBuy()
    {
        money += salePrice;
        moneyEarned += salePrice;
        cocoaInventory -= cocoaAmt;
        sugarInventory -= sugarAmt;
        milkInventory -= 1;
        sales += 1;

    }
    public void PauseGame()
    {
        UIManager.instance.OpenMessageBox("PAUSED", "Hit ok to coutinue" + moneyEarned, null);
    }


	// Update is called once per frame
	void Update () {
        if (paused == false)
        {
            
            tickTimer+= Time.deltaTime*timeMult;
           
            if (tickTimer >= 0.9)
            {
                TimeCycle();
                tickTimer = 0;
            }

            if (hour >= 17)
            {

                EndDay();
            }
            if (hour >= 16)
            {
                endOfDay = true;
            }


        }
        
	}
}
