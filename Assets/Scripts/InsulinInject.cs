using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 인슐린 주입 Scene 스크립트
 */
public class InsulinInject : MonoBehaviour {

    /*주입 성공 여부 (다음씬에서 사용하기 위함)*/
    public static bool success = false;
    /*주입한 시간 저장*/
    public static int injectedTime = 0;
    /*버튼 눌림 여부*/
    bool pressed = false;

    /*현재 시간, 주입 하고 있는 시간*/
    float currentTime = 0;
    float currentTime2 = 0;
    int injectionTime = 10; // 10초
    int timerTime = 10;

    /*인슐린 주사*/
    GameObject needle;
    /*주사기에 쓰인 이름*/
    Text needleName;
    /*주사기 용량*/
    Text needleVolume;

    /*타이머*/
    GameObject timer;
    /*타이머 시간*/
    Text timeText;

    /*현재 캐릭터*/
    Character currentChar;

    /*코치마크*/
    GameObject coachMark;

	// Use this for initialization
	void Start () {
        GetUI();
        SetUI();
	}
    
    private void GetUI()
    {
        needle = GameObject.Find("Needle");
        needleName = GameObject.Find("needleName").GetComponent<Text>();
        needleVolume = GameObject.Find("needleVolume").GetComponent<Text>();
        timer = GameObject.Find("Timer");
        timeText = GameObject.Find("timeText").GetComponent<Text>();
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
        coachMark = GameObject.Find("CoachMark");
    }

    private void SetUI()
    {
        needle.transform.Rotate(0, 0, InsulinAngle.angle); //이전 씬에서 설정한 각도로 세팅
        needleName.text = currentChar.kName;
        needleVolume.text = InsulinVolume.finalValue + "";
        timeText.text = timerTime + "";
        
    }

    // Update is called once per frame
    void Update () {

		if (pressed)
        {
            currentTime += Time.deltaTime;
        }

        if (timerTime - (int)currentTime >= 0)
        {
            timeText.text = timerTime - (int)currentTime + "";
        }

        if (currentTime >= injectionTime) //10초가 지나면
        {
            injectedTime = 10; //10초 성공
            success = true;
            Debug.Log("주입 성공 여부 : " + success);
            DisplayCoachMark(); //코치마크 펼치기
        }

        if (coachMark.transform.localScale == GameData.open)
        {
            currentTime2 += Time.deltaTime;

            if (currentTime2 > 2)
            {
                PageMove.MoveToInsulinResult();
            }
        }
	}

    /**
     * 인슐린 주입 버튼에서 손을 놓을 시 동작
     */
    public void OnPointerUpButton()
    {
        //Debug.Log("버튼 떼짐");
        injectedTime = (int)currentTime; //실패한 경우의 주입한 시간 저장

        pressed = false;
        if (currentTime < injectionTime)
        {
            //Debug.Log("실패!");
            success = false;
            currentTime = 0;
            Debug.Log("주입 성공 여부 : " + success);
            DisplayCoachMark(); //코치마크 펼치기
        }
    }

    /**
     * 인슐린 주입 버튼 누를 시 동작
     */
    public void OnPressButton()
    {
        Debug.Log("버튼 눌림");
        pressed = true;
    }

    /**
     * 코치마크 보이기
     */
    private void DisplayCoachMark()
    {
        coachMark.transform.localScale = GameData.open;
        
        if (success && InsulinAngle.success) //각도, 주입 둘 다 성공하면,
        {
            //성공했어요 코치마크
            coachMark.transform.GetChild(0).GetComponent<Image>().sprite
                = Resources.Load<Sprite>("Insulin_inject/" + currentChar.name + "_sucess");
        }
        else //둘 다 혹은 하나라도 성공 못 한 경우
        {
            //실패했어요 코치마크
            coachMark.transform.GetChild(0).GetComponent<Image>().sprite
                = Resources.Load<Sprite>("Insulin_inject/" + currentChar.name + "_fail");
        }

    }
}
