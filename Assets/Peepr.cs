using UnityEngine;
using System.Collections.Generic;

public class Peepr : MonoBehaviour {
	public Peep peepObject;
	public List<Peep> peepList;

	// Use this for initialization

	public void CreatePeep(string name,string messege){
		Peep p = (Peep)Instantiate(peepObject.gameObject).GetComponent<Peep>();
		p.nameText.text = name;
		p.contentText.text = messege;
	    p.profileImage.sprite = Resources.Load<Sprite>("Profiles/" + name);
		peepList.Add(p);
		p.transform.SetParent(gameObject.transform,false);
		p.transform.SetAsFirstSibling();

	}

}
