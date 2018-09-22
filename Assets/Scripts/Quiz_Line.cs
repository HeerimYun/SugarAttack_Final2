using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * 선잇기 퀴즈 스크립트
 */
public class Quiz_Line : MonoBehaviour {

    /*문제 텍스트*/
    Text quizText;
    /*왼쪽 부분 버튼*/
    Toggle[] leftBtn = new Toggle[2];
    /*왼쪽 부분 텍스트*/
    Text[] leftText = new Text[2];
    /*오른쪽 부분 버튼*/
    Toggle[] rightBtn = new Toggle[2];
    /*오른쪽 부분 텍스트*/
    Text[] rightText = new Text[2];
    /*퀴즈 객체*/
    LineQuiz quiz;
    /*정답확인 버튼*/
    Button checkAnswerBtn;
    /*입력받은 정답*/
    string[] inputAnswer = new string[2];
    /*클릭순서*/
    string[] clickedObject = new string[4];
    /*현재 클릭한 개수*/
    int clickCount = 0;

    // Use this for initialization
    void Start () {
        RandomQuiz();
        GetUI();
        SetUI();
	}

    public void GetUI()
    {
        quizText = GameObject.Find("QuizText").GetComponent<Text>();

        for (int i=0; i<2; i++)
        {
            leftBtn[i] = GameObject.Find("Q" + (i + 1) + "_Toggle").GetComponent<Toggle>();
            rightBtn[i] = GameObject.Find("A" + (i + 1) + "_Toggle").GetComponent<Toggle>();
            leftText[i] = GameObject.Find("Q" + (i + 1) + "_Text").GetComponent<Text>();
            rightText[i] = GameObject.Find("A" + (i + 1) + "_Text").GetComponent<Text>();
        }

        checkAnswerBtn = GameObject.Find("CheckAnswerBtn").GetComponent<Button>();
        checkAnswerBtn.interactable = false;
    }

    public void SetUI()
    {
        for(int i=0; i<2; i++)
        {
            leftBtn[i].isOn = false;
            rightBtn[i].isOn = false;
            leftText[i].text = quiz.choice_left[i];
            rightText[i].text = quiz.choice_right[i];
        }
    }
	
    public void RandomQuiz()
    {
        quiz = GameData.lineQuizzes[Random.Range(0, GameData.lineQuizzes.Length + 1)];
    }

    /**
     * 선잇기 버튼 누를 시 동작
     */
    public void OnClickChoice()
    {
        //선은 시각적으로 이어지도록 나중에 추가

        //클릭순서
        clickedObject[clickCount] = EventSystem.current.currentSelectedGameObject.name;
        clickCount++;

        CheckClick();

        //모든 토글이 켜져야 정답 활성화
        if (CheckAllToggleOn())
        {
            checkAnswerBtn.interactable = true;
        }
    }

    /**
     * 클릭 시 같은 편 버튼을 체크하지 않도록 체크
     */
    public void CheckClick()
    {

    }

    /**
     * 클릭 순서가 배열 됐는지 보기
     */
    public bool CheckAllToggleOn()
    {
        int count = 0;
        bool result = false;

        for (int i=0; i<2; i++)
        {
            if (leftBtn[i].isOn)
            {
                count++;
            }

            if (rightBtn[i].isOn)
            {
                count++;
            }
        }

        if (count == 4)
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    /**
     * 정답 확인 버튼
     */
    public void OnClickCheckAnswerBtn()
    {
        //정답 입력 받고, 정답인지 체크
    }

	// Update is called once per frame
	void Update () {
		
	}
}
