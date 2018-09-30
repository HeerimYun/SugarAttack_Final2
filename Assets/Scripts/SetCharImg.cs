using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCharImg : MonoBehaviour {

    string charName;

	// Use this for initialization
	void Start () {
        charName = GameData.GetCharByOrder(GameData.currentOrder).name;
        transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Library/" + charName + "_library");
        transform.GetComponent<Image>().SetNativeSize();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
