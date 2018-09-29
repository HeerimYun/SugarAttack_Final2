using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharAndChart : MonoBehaviour {

    /*현재 캐릭터*/
    Image charImg;
    /*현재 캐릭터 객체*/
    Character currentChar;

    // Use this for initialization
    void Start () {
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);

        charImg = GameObject.Find("CharacterImage").GetComponent<Image>();
        charImg.sprite = Resources.Load<Sprite>("Characters/" + currentChar.name + "/idle/" + currentChar.name.ToLower() + "_idle_01");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
