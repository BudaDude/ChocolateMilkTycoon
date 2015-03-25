using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour
{

    private int temperature;
    private string condition;

    public ParticleSystem snowSystem;
    public ParticleSystem rainSystem;


	// Use this for initialization
	void Start () {
	
	}

    public void GenerateWeather(string type)
    {
        switch (type)
        {
            case "cold":
                temperature = Random.Range(0, 32);
                break;
            case "cool":
                temperature = Random.Range(32, 50);
                break;
            case "warm":
                temperature = Random.Range(50, 80);
                break;
            case "hot":
                temperature = Random.Range(80, 110);
                break;




        }
        GenerateCondition();

        
    }

    private void GenerateCondition()
    {
        float randomVal = Random.value*100;

        if (randomVal >= 60)
        {
            condition = "clear";
        }else if (randomVal >= 40)
        {
            condition = "partly cloudy";
        }else if (randomVal >= 20)
        {
            condition = "cloudy";
        }
        else
        {
            if (temperature > 32)
            {
                condition = "rain";
            }
            else
            {
                condition = "snow";
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
