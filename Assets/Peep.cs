using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Peep : MonoBehaviour {

	public Text nameText;
	public Text contentText;
	public Image profileImage;

	public string authorName;
	public string messege;

	
	// Use this for initialization
	void Awake () {
		nameText=transform.Find("Name").GetComponent<Text>();
		contentText=transform.Find("Content").GetComponent<Text>();
		profileImage=transform.Find("Profile").GetComponent<Image>();
		
	}

	
	// Update is called once per frame
	void Update () {

	}
}
