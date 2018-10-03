using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryTurnOff : MonoBehaviour {

    float currentTime = 0;
    float duraition = 2;

	// Use this for initialization
	void Start () {
        //현재 순서의 캐릭터 팝업으로 이미지 세팅
        transform.GetChild(0).transform.GetComponent<Image>().sprite = 
            Resources.Load<Sprite>("Library/Library_turnOff_" + GameData.GetCharByOrder(GameData.currentOrder).name);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale == GameData.open) //펼쳐지면,
        {
            currentTime += Time.deltaTime;
        }

        if (currentTime > duraition) //시간 지나면,
        {
            //transform.localScale = GameData.close; //팝업 닫고,
            //다음턴으로 넘기고 룰렛 페이지 새로 로드
            GameData.TurnChange();
            PageMove.MoveToRoulette();
        }
	}
}
