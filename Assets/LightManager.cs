using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {
    GameManager gm;

    public Color morningColor;
    public Color afternoonColor;
    public Color eveningColor;
    public Color nightColor;


    public Light[] lampLights;

    int startingHour;


	// Use this for initialization
	void Start () {
        gm = gameObject.GetComponent<GameManager>();
        startingHour = gm.startingHour;
        
	}
	
	// Update is called once per frame
	void Update () {
        
        int hour = gm.GetHour();
        if (hour < 6 || hour > 16)
        {
            foreach (Light light in lampLights)
            {
                light.gameObject.SetActive(true);
            }
        }else{
            foreach (Light light in lampLights)
            {
                light.gameObject.SetActive(false);
            }
            }
        
        if (hour == startingHour)
        {
            
            RenderSettings.ambientLight = morningColor;
            
        }
        if (hour == 11)
        {
            if (RenderSettings.ambientLight != afternoonColor)
            {
                RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, afternoonColor, 0.01f * gm.tickTimer);
            }
        }
        else if (hour == 16)
        {
            if (RenderSettings.ambientLight != eveningColor)
            {
                RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, eveningColor, 0.01f * gm.tickTimer);
            }
        }
        else if (hour >= 18)
        {
            if (RenderSettings.ambientLight != nightColor)
            {
                RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, nightColor, 0.01f * gm.tickTimer);
            }
        }


        

	
	}
}
