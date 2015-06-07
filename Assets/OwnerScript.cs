using UnityEngine;
using System.Collections;

public class OwnerScript : MonoBehaviour {
    public float bobRate;

    private float timer;
    private bool bobUp;

	// Use this for initialization
	void Start () {
	
	}

    private void Flip()
    {
        if (transform.localEulerAngles.y == 0)
        {
            FaceRight();
        }
        else
        {
            FaceLeft();
        }

    }

    private void FaceLeft()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    private void FaceRight()
    {
        transform.localEulerAngles = new Vector3(0, 180, 0);
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

            if (Random.value > 0.9f)
            {
                Flip();
            }
        }



	}
}
