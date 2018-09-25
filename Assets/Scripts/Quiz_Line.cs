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
    int clickIndex = 0;
    /*이전에 클릭한 객체*/
    string previous;

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

        clickedObject[0] = "";
    }
	
    public void RandomQuiz()
    {
        quiz = GameData.lineQuizzes[Random.Range(0, GameData.lineQuizzes.Length)];
    }

    /**
     * 선잇기 버튼 누를 시 동작
     */
    public void OnClickChoice()
    {
        //선은 시각적으로 이어지도록 나중에 추가
        
        
        //CheckClick();
        if (leftBtn[0].isOn)
        {
            if (rightBtn[0])
            {
                inputAnswer[0] = rightText[0].text;
            }
            else if (rightBtn[1])
            {
                inputAnswer[0] = rightText[1].text;
            }

            if (leftBtn[1].isOn)
            {
                if (rightBtn[0])
                {
                    inputAnswer[1] = rightText[0].text;
                }
                else if (rightBtn[1])
                {
                    inputAnswer[1] = rightText[1].text;
                }
            }
        }

        Debug.Log("선택한 답 1 : " + inputAnswer[0]);
        Debug.Log("선택한 답 2 : " + inputAnswer[1]);

        //모든 토글이 켜져야 정답 활성화
        if (CheckAllToggleOn())
        {
            checkAnswerBtn.interactable = true;
        }
        else
        {
            checkAnswerBtn.interactable = false;
        }

        //previous = EventSystem.current.currentSelectedGameObject.name;
    }

    /**
     * 클릭 시 같은 편 버튼을 체크하지 않도록 체크
     */
    public void CheckClick()
    {
        //왼쪽 버튼 하나 켜져 있을 시 다른 쪽은 비활성화
        if (leftBtn[0].isOn) //왼쪽 위 버튼이 켜지면
        {
            if (rightBtn[0].isOn)
            {
                leftBtn[1].interactable = true;
                inputAnswer[0] = rightText[0].text;
            }
            else if (rightBtn[1].isOn)
            {
                leftBtn[1].interactable = true;
                inputAnswer[0] = rightText[1].text;
            }
            else //아직 선잇기를 안한 경우는 같은 쪽 버튼을 누를 수 없음
            {
                leftBtn[1].interactable = false;
            }
        }
        else if (leftBtn[1].isOn)
        {
            if (rightBtn[0].isOn)
            {
                leftBtn[0].interactable = true;
                inputAnswer[1] = rightText[0].text;
            }
            else if (rightBtn[1].isOn)
            {
                leftBtn[0].interactable = true;
                inputAnswer[1] = rightText[1].text;
            }
            else //아직 선잇기를 안한 경우는 같은 쪽 버튼을 누를 수 없음
            {
                leftBtn[1].interactable = false;
            }
        }
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
        if (inputAnswer[0].Equals(quiz.answers[0]) && inputAnswer[1].Equals(quiz.answers[1]))
        {
            Debug.Log("맞았어요");
        }
        else
        {
            Debug.Log("틀렸어요");
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
