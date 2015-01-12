using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


    public void BeginGame()
    {
        Application.LoadLevel("MainGame");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
