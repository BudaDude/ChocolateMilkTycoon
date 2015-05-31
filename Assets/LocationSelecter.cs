using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocationSelecter : MonoBehaviour {
    private LocationManager locoMan;
    public GameObject locationSlot;

    bool loadedLocations=false;

	// Use this for initialization
	void Start () {
        locoMan = GameObject.FindObjectOfType<LocationManager>().GetComponent<LocationManager>();


	}

    private void AddLocationSlot(Location loc)
    {
        GameObject locoSlot = Instantiate(locationSlot);

        locoSlot.transform.Find("NameLabel").GetComponent<Text>().text=loc.name;
        locoSlot.transform.Find("PriceLabel").GetComponent<Text>().text = loc.rent.ToString("C2")+" / day";



        if (loc.name == locoMan.GetCurrentLocation().name)
        {
            locoSlot.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            locoSlot.GetComponent<Toggle>().isOn = false;
        }

        locoSlot.GetComponent<Toggle>().group = gameObject.GetComponent<ToggleGroup>();
        locoSlot.transform.SetParent(gameObject.transform, false);
        locoSlot.transform.SetAsFirstSibling();
    }

    void LateUpdate()
    {
        if (loadedLocations==false){
            foreach (Location location in locoMan.GetLocations())
            {
                Debug.Log(locoMan.GetCurrentLocation().name);
                AddLocationSlot(location);
            }
            loadedLocations = true;
        }
    }
	

}
