using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 4.7 혈당그래프 변화하는 씬
 */
public class InsulinResult : MonoBehaviour {

    /*가이드 텍스트*/
    GameObject guideText;
    /*주사기 이미지*/
    GameObject needle;
    /*주사기의 이름, 용량*/
    Text nName, nVolume;

    /*혈당 내려감 or 내려가지 않는 여부*/
    bool changeBloodSugar;

    Character currentChar;

	// Use this for initialization
	void Start () {
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
        GetResult();
        GetUI();
        SetUI();
	}

    private void GetResult()
    {
        if (InsulinAngle.success) //각도를 성공한 경우,
        {
            //각도 성공 시 주입여부와 상관없이 변화는 있음
            changeBloodSugar = true;

        }
        else //각도를 실패할 경우
        {
            //혈당 변화 없음
            changeBloodSugar = false;
        }
    }

    private void GetUI()
    {
        guideText = GameObject.Find("GuideText");
        needle = GameObject.Find("Needle");
        nName = GameObject.Find("needleName").GetComponent<Text>();
        nVolume = GameObject.Find("needleVolume").GetComponent<Text>();
    }

    private void SetUI()
    {
        float width = 0;
        string content = "";

        if (changeBloodSugar) //혈당이 변화하는 경우
        {
            width = 959.2f;
            content = "혈당이 내려갔어요!";

            //임시 코드: 혈당 낮춰지는지 확인
            ChangeBloodSugar();
        }
        else
        {
            width = 1068.2f;
            content = "혈당에 변화가 없어요!";
        }

        guideText.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 182);
        guideText.transform.GetChild(0).GetComponent<Text>().text = content;
        nName.text = currentChar.kName;
        nVolume.text = InsulinVolume.finalValue + "";
    }

    /**
     * 혈당 내리는 함수 호출
     */
    public void ChangeBloodSugar()
    {
        //일단 임시로 foodGI가 43인 음식을 먹었다고 가정하고 계산
        GameData.GetInsulin(InsulinVolume.finalValue, InsulinInject.injectedTime, 43, currentChar); //혈당 변화 업데이트
    }

    // Update is called once per frame
    void Update () {
        //일단 임시로 시간 대신 화면 터치시 다음 화면으로 넘어가도록 함
		if (Input.GetMouseButtonDown(0))
        {
            if (InsulinInject.success && InsulinAngle.success) //둘다 성공한 경우,
            {
                //턴 넘기고 룰렛으로 이동
                GameData.TurnChange();
                PageMove.MoveToRoulette();
            }
            else //하나라도 틀린 경우, 도서관으로 이동
            {
                //아직 도서관 내용이 없어 임시로 턴넘기고 룰렛 이동
                GameData.TurnChange();
                PageMove.MoveToRoulette();
            }
        }
	}
}
