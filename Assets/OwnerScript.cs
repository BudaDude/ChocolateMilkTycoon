using UnityEngine;
using System.Collections;

public class OwnerScript : MonoBehaviour {
    public float bobRate;

    private float timer;
    private bool bobUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Vector3 pos=transform.position;
        if (timer > bobRate)
        {
            if (bobUp)
            {
                pos.y += .05f;
                bobUp = false;
            }
            else
            {
                pos.y -= .05f;
                bobUp = true;
            }
            transform.position = pos;
            timer = 0;
        }
	}
}
