using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour
{
    public string name;
    public float rent;
    public float spawnRate;
    public enum Climate { cold,cool,warm,hot}

    public Climate climateTemps;

    public bool secret;

    private GameObject graphics;

    private GameObject waypoints;

    private GameObject stand;

    private int temperature;

    private string condition;

    private WeatherManager wm;

	



	// Use this for initialization
	void Awake ()
	{
		graphics = transform.FindChild("Graphics").gameObject;
        waypoints = transform.FindChild("Waypoints").gameObject;
        stand = transform.FindChild("Stand").gameObject;
        wm = GameObject.FindObjectOfType<WeatherManager>();
        NewDay();
        DeactivateLocation();

	}

    void Start()
    {

    }

    public void NewDay()
    {
        wm.GenerateWeather(climateTemps.ToString());
        temperature = wm.GetTemperature();
        condition = wm.GetCondition();
    }

    public int GetTemperature()
    {
        return temperature;
    }

    public string GetCondition()
    {
        return condition.ToString();
    }
    public void ActivateLocation()
    {

        graphics.SetActive(true);
        waypoints.SetActive(true);
        stand.SetActive(true);
        

    }

    public void DeactivateLocation()
    {
        graphics.SetActive(false);
        waypoints.SetActive(false);
        stand.SetActive(false);

    }
	
}
