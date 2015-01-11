using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MarketManager : MonoBehaviour
{
	public bool fridge;

    public Text displayName;
    public Text displayDesc;
    public Text displayCost;
    public Image displayImage;

    public UpgradeItem upgradeItemSlot;
    public GameObject parentObject;

    public UpgradeItem selected;

    public List<UpgradeItem> upgradeItems;

	public void BuyFridge(){


		}
	// Use this for initialization
	void Start () {
        NewItem("Small Freezer", "This fridge is your basic 2 bedroom apartment fridge. We had to remove the compressor to cut costs but it keeps your milk cold with the power hopes and dreams", 700);
        NewItem("Large Fridge", "This isn't your grandma's fridge. This fridge runs on pure liquid nitrogen. It can fit up 5 human bodies. Not that we recommend that!", 1500);

        NewItem("Cow", "Butt", 5200);
        NewItem("Cocoa Plant", "Butt", 500);
        NewItem("Mixer", "Butt", 3500);
        NewItem("Null", "Butt", 1500);
        NewItem("None", "Butt", 22500);

        selected = upgradeItems[0];

        
	}

    public void NewItem(string name, string desc, float cost)
    {
        UpgradeItem itemScript = (UpgradeItem)Instantiate(upgradeItemSlot);
        itemScript.itemName = name;
        itemScript.itemDesc = desc;
        itemScript.itemCost = cost;
        itemScript.transform.SetParent(parentObject.transform);
        upgradeItems.Add(itemScript);
    }

    public void RefreshDisplay(UpgradeItem item)
    {
        selected = item;
    }
	
	// Update is called once per frame
	void Update () {

       

        if (selected != null)
        {
            displayName.text = selected.itemName;
            displayDesc.text = selected.itemDesc;
            displayCost.text = "Cost: "+selected.itemCost;
            displayImage.sprite = selected.itemIcon;
            
        }

	}
}
