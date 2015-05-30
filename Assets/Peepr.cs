using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Peepr : MonoBehaviour {
	public Peep peepObject;
	public List<Peep> peepList;


    List<Dictionary<string, object>> data;
    
	// Use this for initialization

    void Start()
    {
        ParseCSV();
        CreatePeep("Test", "Rain");
    }
    void ParseCSV()
    {
            data = CSVReader.Read("peeps");

           
    }

	private void AddPeep(string name,string messege){
		Peep p = (Peep)Instantiate(peepObject.gameObject).GetComponent<Peep>();
		p.nameText.text = name;
		p.contentText.text = messege;
	    p.profileImage.sprite = Resources.Load<Sprite>("Profiles/" + name);
        if (p.profileImage.sprite == null)
        {
            p.profileImage.sprite = Resources.Load<Sprite>("Profiles/none");
        }
		peepList.Add(p);
		p.transform.SetParent(gameObject.transform,false);
		p.transform.SetAsFirstSibling();

	}

    public void CreatePeep(string name, string type)
    {
        string msg="";
        while(msg==""){
            msg=data[Random.Range(0, data.Count)][type].ToString();
        }
        AddPeep(name,msg );
    }

}
