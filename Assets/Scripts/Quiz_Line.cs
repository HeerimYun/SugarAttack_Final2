using System;
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
    /*왼쪽 부분 텍스트*/
    Text[] text = new Text[4];
    /*4개 토글 버튼*/
    Toggle[] toggleBtn = new Toggle[4];
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
    /*선*/
    GameObject lines;
    /*리셋 버튼 (칠판에 투명하게 있음)*/
    GameObject resetBtn;

    /*결과 팝업*/
    GameObject quizResult;
    /*팝업 이미지*/
    Image popUpImg;

    /*퀴즈 인덱스 넘버*/
    public static int index;

    // Use this for initialization
    void Start () {
        RandomQuiz();
        GetUI();
        SetUI();
	}

    public void GetUI()
    {
        quizText = GameObject.Find("QuizText").GetComponent<Text>();

        for (int i=0; i<text.Length; i++)
        {
            text[i] = GameObject.Find("Text"+ (i + 1)).GetComponent<Text>();
            //Debug.Log(text[i].text);
        }

        for (int i=0; i<toggleBtn.Length; i++)
        {
            toggleBtn[i] = GameObject.Find("Toggle" + (i + 1)).GetComponent<Toggle>();
        }

        checkAnswerBtn = GameObject.Find("CheckAnswerBtn").GetComponent<Button>();
        checkAnswerBtn.interactable = false;

        resetBtn = GameObject.Find("ResetButton");
        resetBtn.transform.localScale = GameData.close;
        lines = GameObject.Find("Lines");

        quizResult = GameObject.Find("QuizResult");
        quizResult.transform.localScale = GameData.close;

        popUpImg = GameObject.Find("QuizResult/Image").GetComponent<Image>();
    }

    public void SetUI()
    {
        for(int i=0; i<2; i++)
        {
            text[i].text = quiz.choice_left[i];
            text[i+2].text = quiz.choice_right[i];
        }

        for(int i=0; i<toggleBtn.Length; i++)
        {
            toggleBtn[i].isOn = false;
        }

        for(int i=0; i<inputAnswer.Length; i++)
        {
            inputAnswer[i] = "";
        }

        clickedObject[0] = "";

        for (int i=0; i< lines.transform.childCount; i++) //선 모두 안보이게 하기
        {
            lines.transform.GetChild(i).transform.localScale = GameData.close;
        }
    }
	
    public void RandomQuiz()
    {
        index = UnityEngine.Random.Range(0, GameData.lineQuizzes.Length);
        quiz = GameData.lineQuizzes[index];
    }

    /**
     * 선잇기 버튼 누를 시 동작
     */
    public void OnClickChoice()
    {
        if (CheckToggleOn() == 4)
        {
            if (lines.transform.GetChild(0).transform.localScale == GameData.open)
            {
                lines.transform.GetChild(1).transform.localScale = GameData.open;
            }
            else if(lines.transform.GetChild(1).transform.localScale == GameData.open)
            {
                lines.transform.GetChild(0).transform.localScale = GameData.open;
            }
            else if (lines.transform.GetChild(2).transform.localScale == GameData.open)
            {
                lines.transform.GetChild(3).transform.localScale = GameData.open;
            }
            else if (lines.transform.GetChild(3).transform.localScale == GameData.open)
            {
                lines.transform.GetChild(2).transform.localScale = GameData.open;
            }
        }
    }

    /**
     * 클릭 시 같은 편 버튼을 체크하지 않도록 체크
     */
    public void CheckClick()
    {
        checkAnswerBtn.interactable = false;

        if (CheckToggleOn() == 1) //토글 하나만 켜진 경우
        {
            if (toggleBtn[0].isOn)
            {
                toggleBtn[1].interactable = false;
            }
            else if (toggleBtn[1].isOn)
            {
                toggleBtn[0].interactable = false;
            }
            else if (toggleBtn[2].isOn)
            {
                toggleBtn[3].interactable = false;
            }
            else if (toggleBtn[3].isOn)
            {
                toggleBtn[2].interactable = false;
            }

            toggleBtn[2].interactable = true;
            toggleBtn[3].interactable = true;
        }

        if (CheckToggleOn() == 0)
        {
            for (int i=0; i<2; i++)
            {
                toggleBtn[i].interactable = true;
                toggleBtn[i+2].interactable = false;
            }
        }

        if (CheckToggleOn() == 2) //2개가 켜진 경우
        {
            if (toggleBtn[0].isOn) //왼쪽 위 문제에 대한 답 입력
            {
                toggleBtn[0].interactable = false;
                if (toggleBtn[2].isOn) //오른쪽의 위를 선택한 경우
                {
                    inputAnswer[0] = text[2].text;
                    //선그리기
                    lines.transform.GetChild(2).transform.localScale = GameData.open;
                    toggleBtn[2].interactable = false;
                    toggleBtn[3].interactable = true;
                }
                else if (toggleBtn[3].isOn) //오른쪽 아래를 선택한 경우
                {
                    inputAnswer[0] = text[3].text;
                    //선그리기
                    lines.transform.GetChild(1).transform.localScale = GameData.open;
                    toggleBtn[3].interactable = false;
                    toggleBtn[2].interactable = true;
                }
                toggleBtn[1].interactable = true;
            }
            else if (toggleBtn[1].isOn)
            {
                if (toggleBtn[2].isOn) //오른쪽의 위를 선택한 경우
                {
                    inputAnswer[1] = text[2].text;
                    //선그리기
                    lines.transform.GetChild(0).transform.localScale = GameData.open;
                    toggleBtn[2].interactable = false;
                    toggleBtn[3].interactable = true;
                }
                else if (toggleBtn[3].isOn) //오른쪽 아래를 선택한 경우
                {
                    inputAnswer[1] = text[3].text;
                    //선그리기
                    lines.transform.GetChild(3).transform.localScale = GameData.open;
                    toggleBtn[3].interactable = false;
                    toggleBtn[2].interactable = true;
                }

                toggleBtn[0].interactable = true;
            }
        }

        if (CheckToggleOn() == 3) //3개가 켜진 경우
        {
            if (inputAnswer[0].Equals("")) //첫번째가 비어있으면,
            {
                if (toggleBtn[2].isOn)
                {
                    inputAnswer[0] = text[3].text;
                }
                else if (toggleBtn[3].isOn)
                {
                    inputAnswer[0] = text[2].text;
                }
            }
            else if (inputAnswer[1].Equals(""))
            {
                if (toggleBtn[2].isOn)
                {
                    inputAnswer[1] = text[3].text;
                }
                else if (toggleBtn[3].isOn)
                {
                    inputAnswer[1] = text[2].text;
                }
            }
        }

        if (CheckToggleOn() == 4) //4개 다 켜지면,
        {
            for (int i=0; i<toggleBtn.Length; i++)
            {
                toggleBtn[i].interactable = false;
            }

            checkAnswerBtn.interactable = true;

            
        }
    }

    

    /**
     * 켜진 토글 개수 알려주기
     */
    public int CheckToggleOn()
    {
        int count = 0;

        for (int i=0; i<toggleBtn.Length; i++)
        {
            if (toggleBtn[i].isOn)
            {
                count++;
            }
        }

        return count;
    }

    /**
     * 정답 확인 버튼
     */
    public void OnClickCheckAnswerBtn()
    {
        bool isAnswer = false;

        //정답 입력 받고, 정답인지 체크
        if (inputAnswer[0].Equals(quiz.answers[0]) && inputAnswer[1].Equals(quiz.answers[1]))
        {
            isAnswer = true;
        }
        else
        {
            isAnswer = false;
        }

        DisplayResult(isAnswer);
    }

    /**
     * 활성화 된 버튼 개수
     */
    public int CheckBtnsInteractable()
    {
        int count = 0;
        for (int i = 0; i < toggleBtn.Length; i++)
        {
            if (toggleBtn[i].interactable) //활성화가 되면 추가
            {
                count++;
            }
        }

        return count;
    }

    public void ResetButtons()
    {
        if (CheckBtnsInteractable() == 0)
        {
            //모든 체크 사항 및 답 리셋
            for (int i = 0; i < inputAnswer.Length; i++)
            {
                inputAnswer[i] = "";
            }

            for (int i = 0; i < toggleBtn.Length; i++)
            {
                toggleBtn[i].interactable = true;
                toggleBtn[i].isOn = false;
                lines.transform.GetChild(i).transform.localScale = GameData.close;
            }
        }
    }

    /**
     * 퀴즈 맞춤 여부에 대한 결과 팝업 띄우기
     */
    public void DisplayResult(bool isAnswer)
    {
        string correctness = "";
        if (isAnswer)
        {
            correctness = "correct";
        }
        else
        {
            correctness = "incorrect";
        }

        popUpImg.sprite = Resources.Load<Sprite>("quiz/" + correctness + "_" + GameData.GetCharByOrder(GameData.currentOrder).name);
        quizResult.transform.localScale = GameData.open;
    }

    // Update is called once per frame
    void Update () {
        CheckClick();

        if (CheckBtnsInteractable() == 0)
        {
            resetBtn.transform.localScale = GameData.open;
        }
        else
        {
            resetBtn.transform.localScale = GameData.close;
        }
	}
}
