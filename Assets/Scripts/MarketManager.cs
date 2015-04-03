using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MarketManager : MonoBehaviour
{
	public bool fridge;

    public Text displayName;
    public Text displayDesc;
    public Text displayCost;
    public Image displayImage;
    public Button purchaseButton;

    public UpgradeItem upgradeItemSlot;
    public GameObject parentObject;

    public GameObject upgradeObject;

    public UpgradeItem selected;

    public List<UpgradeItem> upgradeItems;



	private GameManager gameManager;
	private CustomerManager cusManager;
	private UIManager uiManager;

	void Awake(){
		gameManager = GameObject.FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
		uiManager = GameObject.FindObjectOfType<UIManager> ().GetComponent<UIManager> ();
		}
	// Use this for initialization
	void Start () {


        //NewItem("Cow", "Butt", 5200);
        //NewItem("Cocoa Plant", "Butt", 500);


        selected = upgradeItems[0];

        
	}

    public void OpenCloseUpgradeMenu()
    {
        if (upgradeObject.activeSelf == true)
        {
            upgradeObject.SetActive(false);
        }
        else
        {
            upgradeObject.SetActive(true);
        }
    }

    public void NewItem(string name, string desc, float cost,Action purchaseAction)
    {
        UpgradeItem itemScript = (UpgradeItem)Instantiate(upgradeItemSlot);
        itemScript.itemName = name;
        itemScript.itemDesc = desc;
        itemScript.itemCost = cost;
        
        itemScript.purchaseAction = purchaseAction;
        itemScript.transform.SetParent(parentObject.transform);
        upgradeItems.Add(itemScript);
    }

    public void RefreshDisplay(UpgradeItem item)
    {
        selected = item;
    }

    public void Purchase()
    {
		if (gameManager.money >= selected.itemCost)
        {
            selected.purchased = true;
			gameManager.money -= selected.itemCost;
            selected.purchaseAction();


            selected.purchased = true;

        }
        else
        {
            uiManager.OpenMessageBox("Not Enough Money!", "You don't have enough money to purchase this upgrade.", null);
        }
    }


	
	// Update is called once per frame
	void Update () {

       

        if (selected != null)
        {
            displayName.text = selected.itemName;
            displayDesc.text = selected.itemDesc;
            displayCost.text = "Cost: "+selected.itemCost.ToString("C2");
            displayImage.sprite = selected.itemIcon;
            if (selected.purchased == true)
            {
                purchaseButton.interactable = false;
            }
            else
            {
                purchaseButton.interactable = true;

            }
            
        }

	}
}
