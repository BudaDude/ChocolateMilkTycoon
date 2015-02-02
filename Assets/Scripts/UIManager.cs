using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    //info bar
	public Text moneyDisplay;
    public Text centerDisplay;
    public Text tempDisplay;

    public Text milkInventoryDisplay;
    public Text milkPriceDisplay;


    public Text sugarInventoryDisplay;
    public Text sugarPriceDisplay;

    public Text cocoaInventoryDisplay;
    public Text cocoaPriceDisplay;
    //

    public GameObject recipeMenu;

    public Slider cocoaAmtSlider;
    public Slider sugarAmtSlider;
    public Text cocoaDisplay;
    public Text sugarDisplay;

    //Message box
    public GameObject messageBox;
    public Text messageBoxTitle;
    public Text messageBoxText;
    public Button messageBoxButton;

    //InGameBox
    public Animator inGameBoxAnim;
    public Text inventoryDisplay;
    public Text popDisplay;

    //Main Game
    public Text earnedDisplay;

    public Slider priceSlider;
    public Text priceDisplay;



	private GameManager gameManager;


    //Settings
    public GameObject settingsMenu;

    //bools
    bool recipeMenuOpened = true;
    void Awake()
    {

    }

    

	// Use this for initialization
	void Start () {
		gameManager = gameObject.GetComponent<GameManager> ();

	}
	
	// Update is called once per frame
	void Update () {
        //Cocoa and sugar slider
        if (gameManager != null)
        {
            if (cocoaAmtSlider != null && sugarAmtSlider != null)
            {
                gameManager.cocoaAmt = (int)Mathf.Round(cocoaAmtSlider.value);
                cocoaDisplay.text = cocoaAmtSlider.value.ToString();
                gameManager.sugarAmt = (int)Mathf.Round(sugarAmtSlider.value);

                sugarDisplay.text = sugarAmtSlider.value.ToString();

            }

            if (moneyDisplay != null)
            {
                moneyDisplay.text =gameManager.money.ToString("C2");
            }

            if (milkInventoryDisplay != null && cocoaInventoryDisplay != null && sugarInventoryDisplay)
            {
                milkInventoryDisplay.text = gameManager.milkInventory.ToString();
                cocoaInventoryDisplay.text = gameManager.cocoaInventory.ToString();
                sugarInventoryDisplay.text = gameManager.sugarInventory.ToString();

            }
            if (milkPriceDisplay != null && cocoaPriceDisplay != null && sugarPriceDisplay != null)
            {
                milkPriceDisplay.text =  gameManager.milkPrice.ToString("C2");
                sugarPriceDisplay.text = gameManager.sugarPrice.ToString("C2");
                cocoaPriceDisplay.text = gameManager.cocoaPrice.ToString("C2");

            }

            if (inventoryDisplay != null)
            {
                inventoryDisplay.text = "S: " + gameManager.sugarInventory + " C: " + gameManager.cocoaInventory + " M: " + gameManager.milkInventory;
            }

            if (centerDisplay != null)
            {
                if (recipeMenuOpened==true)
                {
                    centerDisplay.text = "Day "+gameManager.day+" - Prep";
                }
                else
                {
                    centerDisplay.text = gameManager.GetTime();
                }
                
            }
            if (tempDisplay != null)
            {

                tempDisplay.text = gameManager.GetTemp();
            }

            if (priceDisplay != null && priceSlider != null)
            {
                gameManager.salePrice = priceSlider.value*0.25f;

                   
                priceDisplay.text = "Price: " +  (priceSlider.value * 0.25f).ToString("C2");

                
            }

            if (popDisplay != null) {
                popDisplay.text = "Popularity: " + gameManager.popularity;
            }



        }
	}

    public void OpenCloseRecipeMenu()
    {
        Animator anim=recipeMenu.GetComponent<Animator>();;
        if (anim.GetBool("Opened")==true){
            anim.SetBool("Opened",false);
            recipeMenuOpened = false;
        }
        else
        {
            anim.SetBool("Opened",true);
            recipeMenuOpened = true;
        }
    }

    // message box stuff below

    public void OpenMessageBox(string title, string text,UnityEngine.Events.UnityAction clickAction)
    {
        messageBox.SetActive(true);
        messageBoxTitle.text = title;
        messageBoxText.text = text;
       gameManager.paused = true;
        if (clickAction != null)
        {
            
           
            messageBoxButton.onClick.AddListener(() => clickAction());
            messageBoxButton.onClick.AddListener(() => CloseMessageBox());
            Debug.Log("Opened with 2 commands");
        }
        else
        {
            messageBoxButton.onClick.AddListener(()=>CloseMessageBox());
            Debug.Log("Opened with 1 command");
        }

    }


    public void CloseMessageBox()
    {
        messageBoxButton.onClick.RemoveAllListeners();
        messageBox.SetActive(false);
        if (recipeMenu.activeSelf == false)
        {
            gameManager.paused = false;
        }


    }

    public void OpenCloseInGameBox()
    {
        if(inGameBoxAnim.GetBool("isHidden")==true){
            inGameBoxAnim.SetBool("isHidden",false);

        }else{
            inGameBoxAnim.SetBool("isHidden",true);

        }
    }


    public void OpenCloseSettings()
    {
        if (settingsMenu.activeSelf == true)
        {
            settingsMenu.SetActive(false);
        }
        else
        {
            settingsMenu.SetActive(true);
        }
    }
}

