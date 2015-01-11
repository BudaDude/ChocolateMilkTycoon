using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UpgradeItem : MonoBehaviour {

    public string itemName;
    public string itemDesc;
    public float itemCost;
    public Sprite itemIcon;
    private Text displayName;
    private Button button;

    private bool purchased;

    private MarketManager mManager;
    private Image itemImage;

    public UpgradeItem(string newName, string newDesc, float newCost)
    {
        itemName = newName;
        itemDesc = newDesc;
        itemCost = newCost;
    }

	// Use this for initialization
	void Start () {
        displayName = gameObject.GetComponentInChildren<Text>();
        button = gameObject.GetComponent<Button>();
        mManager = GameObject.FindObjectOfType<MarketManager>();
        itemImage = gameObject.GetComponentInChildren<Image>();
        button.onClick.AddListener(()=>MakeSelected());


        itemIcon = Resources.Load<Sprite>("Upgrades/" + itemName);
        itemImage.sprite = itemIcon;
	}

    void MakeSelected()
    {
        mManager.RefreshDisplay(this);
    }

    
	
	// Update is called once per frame
	void Update () {
        displayName.text = itemName;

        
	}
}
