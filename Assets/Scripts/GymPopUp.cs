using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 운동완료 팝업을 일정 시간동안 띄우는 스크립트
 */
public class GymPopUp : MonoBehaviour {

    /*현재 시간*/
    float currentTime = 0;
    /*지속 시간*/
    float duraition = 3;
    /*현재 캐릭터*/
    Character currentChar;

	// Use this for initialization
	void Start () {
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
        //현재 순서의 캐릭터 팝업으로 바꿔줌
        transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Gym/" + currentChar.name + "_gym_popup");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale == GameData.open)
        {
            currentTime += Time.deltaTime;

            //화면 터치 시 바로 넘어감
            if (Input.GetMouseButtonDown(0))
            {
                transform.localScale = GameData.close;
                Gym.isWorkDone = true;
            }
        }

        if (currentTime > duraition)
        {
            transform.localScale = GameData.close;
            Gym.isWorkDone = true;
        }
	}
}
