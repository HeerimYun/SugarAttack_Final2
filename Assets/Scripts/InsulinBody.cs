using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * 세 번 깜빡이고 자동으로 넘어갈것
 */
public class InsulinBody : MonoBehaviour {

    /*현재 캐릭터*/
    Character currentChar;
    /*가이드 텍스트*/
    GameObject guideText;
    /*신체부위 이미지*/
    Image bodyImg;
    /*가이드 텍스트 사이즈*/
    float width;

    float currentTime = 0;
    float duraition = 3;

	// Use this for initialization
	void Start () {
        GetUI();
        SetUI();
	}

    private void GetUI()
    {
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
        guideText = GameObject.Find("GuideText");
        bodyImg = GameObject.Find("BodyImage").GetComponent<Image>();
    }

    private void SetUI()
    {
        string content = "";
        string fileName = "";

        switch (currentChar.bodyOrder)
        {
            case GameData.ARM:
                width = 976.7f;
                content = GameData.body[GameData.ARM].kName;
                fileName = GameData.body[GameData.ARM].name;
                break;
            case GameData.STOMACH:
                width = 976.7f;
                content = GameData.body[GameData.STOMACH].kName;
                fileName = GameData.body[GameData.STOMACH].name;
                break;
            case GameData.THIGH:
                width = 1139.9f;
                content = GameData.body[GameData.THIGH].kName;
                fileName = GameData.body[GameData.THIGH].name;
                break;
        }

        //가이드 텍스트 설정
        guideText.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 182); //가이드 텍스트 사이즈 설정
        guideText.transform.GetChild(0).GetComponent<Text>().text = content + "에 주사를 맞아요!";

        //신체 이미지 설정
        bodyImg.sprite = Resources.Load<Sprite>("InsulinNeedle/" + fileName);

        //현재 캐릭터의 다음에 맞을 주사 부위 변경
        currentChar.bodyOrder++;

        if (currentChar.bodyOrder > 2) //인덱스 0,1,2 유지
        {
            currentChar.bodyOrder = 0;
        }
    }
 
    // Update is called once per frame
    void Update () {
        currentTime += Time.deltaTime;

        if (currentTime > duraition)
        {
            //각도 페이지로 이동
            PageMove.MoveToInsulinAngle();
        }
	}
}
