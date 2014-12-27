using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    //info bar
	public Text moneyDisplay;
    public Text timeDisplay;

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

    //Main Game
    public Text earnedDisplay;

    public Slider priceSlider;
    public Text priceDisplay;






    //Singleton crap
    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();

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

    

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
        //Cocoa and sugar slider
        if (cocoaAmtSlider != null && sugarAmtSlider != null)
        {
            GameManager.instance.cocoaAmt=(int)Mathf.Round(cocoaAmtSlider.value);
            cocoaDisplay.text= cocoaAmtSlider.value.ToString();
            GameManager.instance.sugarAmt = (int)Mathf.Round(cocoaAmtSlider.value);

            sugarDisplay.text= sugarAmtSlider.value.ToString();

        }

        if (moneyDisplay != null)
        {
            moneyDisplay.text = " $ "+ GameManager.instance.money;        }

        if (milkInventoryDisplay != null && cocoaInventoryDisplay != null && sugarInventoryDisplay)
        {
            milkInventoryDisplay.text = "Milk: "+GameManager.instance.milkInventory;
            cocoaInventoryDisplay.text = "Cocoa Powder: " + GameManager.instance.cocoaInventory;
            sugarInventoryDisplay.text = "Sugar: " + GameManager.instance.sugarInventory;

        }
        if (milkPriceDisplay != null && cocoaPriceDisplay != null && sugarPriceDisplay != null)
        {
            milkPriceDisplay.text = "$" + GameManager.instance.milkPrice;
            sugarPriceDisplay.text = "$" + GameManager.instance.sugarPrice;
            cocoaPriceDisplay.text = "$" + GameManager.instance.cocoaPrice;

        }

        if (inventoryDisplay != null)
        {
            inventoryDisplay.text = "S: " + GameManager.instance.sugarInventory + " C: " + GameManager.instance.cocoaInventory + " M: " + GameManager.instance.milkInventory;
        }

        if (timeDisplay != null)
        {
            timeDisplay.text = GameManager.instance.GetTime();
        }
        if (priceDisplay != null && priceSlider != null)
        {
            GameManager.instance.salePrice = priceSlider.value;
            priceDisplay.text = "Price: S" + priceSlider.value;
        }


        if (recipeMenu.activeSelf == true)
        {
            GameManager.instance.paused = true;
            timeDisplay.text = "Day "+ GameManager.instance.day;
        }

	}

    public void OpenCloseRecipeMenu()
    {
        if (recipeMenu.activeSelf==true){
            recipeMenu.SetActive(false);
        }
        else
        {
            recipeMenu.SetActive(true);
        }
    }

    // message box stuff below

    public void OpenMessageBox(string title, string text,UnityEngine.Events.UnityAction clickAction)
    {
        messageBox.SetActive(true);
        messageBoxTitle.text = title;
        messageBoxText.text = text;
        GameManager.instance.paused = true;
        if (clickAction != null)
        {
            messageBoxButton.onClick.AddListener(()=>clickAction());
            messageBoxButton.onClick.AddListener(()=>CloseMessageBox());
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
            GameManager.instance.paused = false;
        }


    }

    public void OpenCloseInGameBox()
    {
        if(inGameBoxAnim.GetBool("Open")==true){
            inGameBoxAnim.SetBool("Open",false);

        }else{
            inGameBoxAnim.SetBool("Open",true);

        }
    }
}

