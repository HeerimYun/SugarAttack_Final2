using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 인슐린 주사기 스크립트
 */
public class InsulinNeedle : MonoBehaviour {

    /*인슐린 주사기에 새겨진 이름*/
    Text name;
    /*인슐린 주사기에 나타날 용량*/
    Text volume;
    /*인슐린 주사기 용량 값*/
    int volumeVal;
    /*용량설정 완료 버튼*/
    Button volumeSetBtn;
    /*GuideText*/
    GameObject guideText;
    /*코치마크*/
    GameObject coachMark;

    /*현재 순서 캐릭터*/
    Character currentChar;
    /*시간*/
    float currentTime = 0;
    float duraition = 3;

	// Use this for initialization
	void Start () {
        GetCurrentChar();
        GetUI();
        SetUI();
	}

    /**
     * UI setting
     */
    private void SetUI()
    {
        volumeVal = 0;
        name.text = currentChar.kName;
        volume.name = volumeVal + "";

        transform.localPosition = new Vector3(110, 85, 0); //맨 처음 주사기 위치
        transform.localScale = GameData.open;

        volumeSetBtn.transform.localScale = GameData.close;
        volumeSetBtn.interactable = false;

        guideText.GetComponent<RectTransform>().sizeDelta = new Vector2(1259, 182);

        coachMark.transform.localScale = GameData.close;
    }

    /**
     * 현재순서 캐릭터 할당
     */
    private void GetCurrentChar()
    {
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
    }

    /**
     * UI 가져오기
     */
    private void GetUI()
    {
        name = GameObject.Find("body/name").GetComponent<Text>();
        volume = GameObject.Find("body/volume").GetComponent<Text>();
        volumeSetBtn = GameObject.Find("VolumeSetButton").GetComponent<Button>();
        guideText = GameObject.Find("GuideText");
        coachMark = GameObject.Find("CoachMark");
    }

    public void ZoomInsulinNeedle()
    {
        //코치마크를 켠다.
        DisplayCoachMark();
        //주사기가 커진다
        transform.localScale = new Vector3(1.73f, 1.73f, 1);
        //주사기 위치를 바꾼다.
        transform.localPosition = new Vector3(-851, 134.4f, 0);
        //guideText를 크기와 텍스트 내용을 변경한다.
        guideText.GetComponent<RectTransform>().sizeDelta = new Vector2(1320, 182);
        guideText.transform.GetChild(0).GetComponent<Text>().text = "인슐린 용량을 설정해주세요!";
        //설정완료 버튼을 보이게 한다.
        volumeSetBtn.transform.localScale = GameData.open;
    }

    /**
     * 코치 마크 보이기
     */
    private void DisplayCoachMark()
    {
        //만약 이번 칸에 도착한 사람이 최초이면 보인다.
        if (GameData.iInjectCM)
        {
            coachMark.transform.localScale = GameData.open;
            GameData.iInjectCM = false; //다음부터는 코치마크를 보이지 않는다.
        }
        
    }
    
    // Update is called once per frame
    void Update () {
        currentTime += Time.deltaTime;

        if (currentTime > duraition && transform.localScale == GameData.open)
        {
            ZoomInsulinNeedle();
        }

	}
}
