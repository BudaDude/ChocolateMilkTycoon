using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour
{
    public string name;
    public float rent;
    public enum Climate { cold,cool,warm,hot}

    public Climate climateTemps;

    public bool secret;

    private GameObject graphics;

    private GameObject waypoints;

    private GameObject stand;

	



	// Use this for initialization
	void Awake ()
	{
		graphics = transform.FindChild("Graphics").gameObject;
        waypoints = transform.FindChild("Waypoints").gameObject;
        stand = transform.FindChild("Stand").gameObject;

        DeactivateLocation();

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
