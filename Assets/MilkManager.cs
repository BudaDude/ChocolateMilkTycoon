using UnityEngine;
using System.Collections;

public class MilkManager : MonoBehaviour {
    public int cocoaAmount = 10;
    public int sugarAmount = 5;

    //Singleton crap
    private static MilkManager _instance;
    public static MilkManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MilkManager>();

                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        //More Singleton crap
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}

    public void makemilk(int cocoaAmt,int sugarAmt)
    {
        cocoaAmount = cocoaAmt;
        sugarAmount = sugarAmt;

        Debug.Log(cocoaAmount + "--" + sugarAmount);
    }

   
	
	// Update is called once per frame
	void Update () {
	
	}
}
