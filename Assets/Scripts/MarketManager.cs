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

	public void BuyFridge(){


		}
	// Use this for initialization
	void Start () {
        NewItem("Small Fridge",
            "Preserves 10 milk at the end of the day",
            700,false,()=>GameManager.instance.maxMilkSaved=10);
        NewItem("Midsize Fridge", "Preserves 25 milk at the end of the day ", 1500,false,()=>GameManager.instance.maxMilkSaved=25);

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

    public void NewItem(string name, string desc, float cost,bool stacking,Action purchaseAction)
    {
        UpgradeItem itemScript = (UpgradeItem)Instantiate(upgradeItemSlot);
        itemScript.itemName = name;
        itemScript.itemDesc = desc;
        itemScript.itemCost = cost;
        itemScript.stackable = stacking;
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
        if (GameManager.instance.money >= selected.itemCost)
        {
            selected.purchased = true;
            GameManager.instance.money -= selected.itemCost;
            selected.purchaseAction();


            selected.purchased = true;

        }
        else
        {
            UIManager.instance.OpenMessageBox("Not Enough Money!", "You don't have enough money to purchase this upgrade.", null);
        }
    }


	
	// Update is called once per frame
	void Update () {

       

        if (selected != null)
        {
            displayName.text = selected.itemName;
            displayDesc.text = selected.itemDesc;
            displayCost.text = "Cost: "+selected.itemCost;
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
