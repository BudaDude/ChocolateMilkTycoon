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

    public int GetTemperature()
    {
        return temperature;
    }

    public string GetCondition()
    {
        return condition;
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
            StopPrecipitation();
        }else if (randomVal >= 40)
        {
            condition = "partly cloudy";
            StopPrecipitation();

        }else if (randomVal >= 20)
        {
            condition = "cloudy";
            StopPrecipitation();

        }
        else
        {
            if (temperature > 32)
            {
                condition = "rain";
                BeginRain();
            }
            else
            {
                condition = "snow";
                BeginSnow();
            }
        }

    }

    void BeginRain()
    {
        rainSystem.gameObject.SetActive(true);
        snowSystem.gameObject.SetActive(false);
    }

    void BeginSnow()
    {
        snowSystem.gameObject.SetActive(true);
        rainSystem.gameObject.SetActive(false);
    }

    void StopPrecipitation()
    {
        snowSystem.gameObject.SetActive(false);
        rainSystem.gameObject.SetActive(false);
    }
	
}
