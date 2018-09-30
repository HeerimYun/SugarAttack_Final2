using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 체육관 스크립트
 * 맨 처음 책을 누르면 책이 펼쳐짐
 */
public class Gym : MonoBehaviour {

    /*운동*/
    Exercise exercise;
    /*현재 캐릭터*/
    Character currentChar;

    /*가이드 텍스트*/
    GameObject guideText;
    /*책 이미지*/
    GameObject book;
    /*운동 이미지*/
    Image workImg;
    /*운동 이름*/
    Text workName;
    /*어디에 좋을까? 설명*/
    Text advantage;
    /*주의할 점! 설명*/
    Text caution;
    /*운동하기 버튼*/
    Button workBtn;
    /*운동 완료 팝업*/
    GameObject workDone;
    /*캐릭터&차트*/
    GameObject charAndChart;

    public static bool isWorkDone = false; //팝업이 종료되었는가?
    float currentTime = 0;
    float duraition = 4;

    /*책 상태*/
    const int BOOK_CLOSE_FRONT = 0;
    const int BOOK_OPEN = 1;
    const int BOOK_CLOSE_BACK = 2;

	// Use this for initialization
	void Start () {
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
        RandomExercise();
        GetUI();
        SetUI();
	}

    private void RandomExercise()
    {
        exercise = GameData.exercise[UnityEngine.Random.Range(0, GameData.exercise.Length)]; //운동 하나를 무작위로 뽑기 
    }

    private void SetUI()
    {
        workBtn.interactable = false;
        workBtn.transform.localScale = GameData.open;
        workDone.transform.localScale = GameData.close;
        SetBook(BOOK_CLOSE_FRONT);
        charAndChart.transform.localScale = GameData.close;

        //책 내용 세팅
        //운동 이미지
        SetWorkImg();
        //운동 이름
        workName.text = exercise.name + exercise.type;
        //어디에 좋을까?
        advantage.text = exercise.advantage;
        //주의사항
        caution.text = exercise.caution;
    }

    /**
     * 운동 이미지 및 글자 위치 지정
     */
    private void SetWorkImg()
    {
        string imgFileName = "";
        switch (exercise.name)
        {
            case "걷기":
                imgFileName = "walking";
                break;
            case "테니스":
                imgFileName = "tenis";
                break;
            case "자전거타기":
                imgFileName = "bike";
                break;
            case "배드민턴":
                imgFileName = "badminton";
                break;
            case "달리기":
                imgFileName = "walking";
                break;
            case "계단 오르내리기":
                imgFileName = "stairs";
                break;
            case "체조":
                imgFileName = "gymnastics";
                break;
            case "수영":
                imgFileName = "swimming";
                break;
        }
        workImg.sprite = Resources.Load<Sprite>("Gym/" + imgFileName);
        workImg.SetNativeSize();
    }

    private void GetUI()
    {
        guideText = GameObject.Find("GuideText");
        book = GameObject.Find("Book");
        workImg = GameObject.Find("WorkElement").GetComponent<Image>();
        workName = GameObject.Find("WorkName").GetComponent<Text>();
        advantage = GameObject.Find("Title1/Content").GetComponent<Text>();
        caution = GameObject.Find("Title2/Content").GetComponent<Text>();
        workBtn = GameObject.Find("WorkButton").GetComponent<Button>();
        workDone = GameObject.Find("WorkDonePopup");
        charAndChart = GameObject.Find("CharAndChart_small");
    }

    /**
     * 맨 처음 책 표지를 누르면 동작
     * 책이 열리고, 
     */
    public void OnClickBook()
    {
        SetBook(BOOK_OPEN);
        workBtn.interactable = true;
    }
	
    public void SetBook(int state)
    {
        switch (state)
        {
            case 0:
                book.transform.GetChild(BOOK_CLOSE_FRONT).transform.localScale = GameData.open;
                book.transform.GetChild(BOOK_OPEN).transform.localScale = GameData.close;
                book.transform.GetChild(BOOK_CLOSE_BACK).transform.localScale = GameData.close;
                break;
            case 1:
                book.transform.GetChild(BOOK_CLOSE_FRONT).transform.localScale = GameData.close;
                book.transform.GetChild(BOOK_OPEN).transform.localScale = GameData.open;
                book.transform.GetChild(BOOK_CLOSE_BACK).transform.localScale = GameData.close;
                break;
            case 2:
                book.transform.GetChild(BOOK_CLOSE_FRONT).transform.localScale = GameData.close;
                book.transform.GetChild(BOOK_OPEN).transform.localScale = GameData.close;
                book.transform.GetChild(BOOK_CLOSE_BACK).transform.localScale = GameData.open;
                break;
        }
    }

    /**
     * 운동하기 버튼을 누를 경우 동작하는 함수
     */
    public void OnClickWorkBtn()
    {
        //운동완료 팝업
        workDone.transform.localScale = GameData.open;
        //혈당 내리기 (내릴 때는 -값으로 전달)
        GameData.UpdateBloodSugar(currentChar, -1 * exercise.GI);
        //currentChar.bloodSugar.Add(currentChar.bloodSugar[currentChar.bloodSugar.Count - 1] - exercise.GI);
    }
    
	// Update is called once per frame
	void Update () {
		if(isWorkDone)
        {
            SetBook(BOOK_CLOSE_BACK);
            workBtn.transform.localScale = GameData.close;
            charAndChart.transform.localScale = GameData.open;
            guideText.GetComponent<RectTransform>().sizeDelta = new Vector2(945.4f, 182);
            guideText.transform.GetChild(0).GetComponent<Text>().text = "혈당이 내려갔어요!";

            currentTime += Time.deltaTime;
        }

        if (currentTime > duraition) //책 닫히고 2초 후 다음턴 룰렛으로 이동
        {
            isWorkDone = false;
            GameData.TurnChange();
            PageMove.MoveToRoulette();
        }
	}
}
