using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public float money;
    public float salePrice;



    private int sales;


	//stats stuff
    private float moneyEarned;
	private float moneySpent;

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
    private int hour;
    public float timeMult=2;
    public int day=1;
    public float tickTimer;

    public int startingHour;
    public int endHour;

    //recipe stuff
    public int cocoaAmt;
    public int sugarAmt;


	public bool nearEndOfDay;
    public bool endOfDay=false;
	public bool paused=true;

    
	//Peeps
	private Peepr peepR;

	//feats
	public bool milkGoesBad;
    public bool cow = false;

    //Upgrades
    public int maxMilkSaved;


    //Weather Stuff
    public WeatherManager weather;
    public int temperature;



    //Singleton crap

	private UIManager uiManager;
    private Settings settings;

	private CustomerManager cusManager;
	//Current Location
	private LocationManager locManager;
	public Location currentLocation;




    void Awake()
    {
		peepR = GameObject.FindObjectOfType<Peepr> ().GetComponent<Peepr> ();
		settings = gameObject.GetComponent<Settings>();
		uiManager = gameObject.GetComponent<UIManager> ();
		locManager = GameObject.FindObjectOfType<LocationManager> ().GetComponent<LocationManager>();
		cusManager=GameObject.FindObjectOfType<CustomerManager> ().GetComponent<CustomerManager>();
    }
	void Start () {

        hour = startingHour;
		locManager.SetLocation ("City");
		currentLocation = locManager.GetLocation ();
		cusManager.ActivateCustomers ();
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
		//TODO Fix time display
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

    public int GetHour()
    {
        return hour;
    }




    //for buttons to buy stuff. Checks how much money has and evulates if it should add more to the inventory
    public void BuyMilk()
    {
        if (money >= milkPrice)
        {
            milkInventory += 10;
            money -= milkPrice;
			moneySpent+=milkPrice;

        }
    }
    public void BuySugar()
    {
        if (money >= sugarPrice)
        {
            sugarInventory += 50;
            money -= sugarPrice;
			moneySpent+=sugarPrice;
        }
    }
    public void BuyCocoa()
    {
        if (money >= cocoaPrice)
        {
            cocoaInventory += 50;
            money -= cocoaPrice;
			moneySpent+=cocoaPrice;
        }
    }

	public void StartDay(){
        if (milkInventory > 0 && cocoaInventory >= cocoaAmt && sugarInventory >= sugarAmt)
        {
            uiManager.OpenCloseRecipeMenu();
			paused=false;
            



        }
        else
        {
            if (money > cocoaPrice || money > sugarPrice || money > milkPrice)
            {
                uiManager.OpenMessageBox("WARNING", "You don't have enough items to start the day with the current recipe!", null);
            }
            else
            {
                uiManager.OpenMessageBox("GAME OVER", "You don't have enough money to buy supplies to coutinue. It's time to pack up and go home.",()=> Application.LoadLevel(0));
            }
        }
		}
    public void EndDay()
    {
        

        string additionalMsg = "";
        if (!canMakeMilk())
        {
            additionalMsg = "You ran out of some supplies";
        }

		//Milk goes bad and must be thrown out unless you have fridge
		if (milkGoesBad) {
            if (milkInventory > maxMilkSaved)
            {
                milkInventory = maxMilkSaved;
            }
			
				}
        if (cow)
        {
            milkInventory += 10;
        }
        



        uiManager.OpenMessageBox("End of Day " + day, "Money Earned: \t\t" + moneyEarned.ToString("C2")+
		                                  "\n\nMoney Spent: \t\t"+moneySpent.ToString("C2")+
		                                  "\n\nTotal Earned: \t\t"+(moneyEarned-moneySpent).ToString("C2")+
                                          "\n\n"+additionalMsg
		                                  ,PrepareForDay);

		//Resets positions of all customers
		foreach (CustomerScript customer in (CustomerScript[])GameObject.FindObjectsOfType<CustomerScript>()) {
			customer.EndDay();
				}
        

    }
	void PrepareForDay(){
		hour = startingHour;
		minute = 0;
		day += 1;
		endOfDay = false;
		nearEndOfDay = false;
		moneyEarned = 0;
		moneySpent = 0;
		sales = 0;
		uiManager.OpenCloseRecipeMenu();
        ChangeTemp();

	}

    public string GetTemp(){
        if (settings.celsius == true)
        {
            return ((temperature - 32) * 5 / 9).ToString() + " °C";
        }
        else
        {
            return temperature.ToString() + " °F";
        }
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

    public int getChocoMilk()
    {
        return Mathf.Min(milkInventory, Mathf.Min(Mathf.FloorToInt(cocoaInventory/cocoaAmt),Mathf.FloorToInt(sugarInventory/sugarAmt)));
    }
    void ChangeTemp()
    {

            temperature += Random.Range(-10, 10);
            temperature = Mathf.Clamp(temperature, 10, 90);

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
        uiManager.OpenMessageBox("PAUSED", "Hit ok to coutinue", null);
    }


	// Update is called once per frame
	void Update () {
        if (paused == false)
        {
			currentLocation=locManager.GetLocation();
            
            tickTimer+= Time.deltaTime*timeMult;
           
            if (tickTimer >= 0.9)
            {
                TimeCycle();
                tickTimer = 0;
            }

            if (hour >= endHour)
            {

                EndDay();
					endOfDay=true;
            }
            if (hour >= endHour-1)
            {
                nearEndOfDay = true;
            }


        }
        
	}
}
