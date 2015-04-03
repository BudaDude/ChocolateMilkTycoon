using UnityEngine;
using System.Collections;

public class LocationManager : MonoBehaviour
{

    private Location[] locations;
    private Location currentLocation;
    private WeatherManager weatherManager;

	// Use this for initialization
	void Start ()
	{
	    locations = gameObject.GetComponentsInChildren<Location>();
	    weatherManager = GameObject.FindObjectOfType<WeatherManager>();
        SetLocation("city");
	}

    void SetLocation(string name)
    {
        foreach (Location loco in locations)
        {
            if (loco.name.ToLower() == name.ToLower())
            {
                loco.ActivateLocation();
                currentLocation = loco;
                weatherManager.GenerateWeather(loco.climateTemps.ToString());
                Debug.Log(currentLocation.name +"-"+weatherManager.GetCondition());
            }
            else
            {
                loco.DeactivateLocation();
            }
        }   
    }

	public Location GetLocation(){
		return currentLocation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
