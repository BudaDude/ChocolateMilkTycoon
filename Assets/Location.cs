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
    private CustomerManager CM;



	// Use this for initialization
	void Start ()
	{
	    graphics = transform.Find("Graphics").gameObject;
	    CM = gameObject.GetComponentInChildren<CustomerManager>();
        DeactivateLocation();

	}

    public void ActivateLocation()
    {
        graphics.SetActive(true);
        CM.gameObject.SetActive(true);

    }

    public void DeactivateLocation()
    {
        graphics.SetActive(false);
        CM.gameObject.SetActive(false);
    }

    public CustomerManager GetCustomerManager()
    {
        return CM;
    }
}
