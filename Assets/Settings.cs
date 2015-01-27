using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Settings : MonoBehaviour {

    [SerializeField]
    public Toggle celsiusToggle;

    public bool celsius = false;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
        celsius = celsiusToggle.isOn;
	}
}
