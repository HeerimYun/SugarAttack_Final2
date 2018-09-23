using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * OX 퀴즈 스크립트
 */
public class Quiz_OX : MonoBehaviour {

    const int O_BTN = 0;
    const int X_BTN = 1;

    /*퀴즈*/
    OXQuiz quiz;
    /*퀴즈 문제*/
    Text quizText;
    /*OX 버튼*/
    Toggle[] btns = new Toggle[2];
    /*정답확인*/
    Button checkAnswerBtn;
    /*고른 답*/
    string choosedAnswer;

    /*결과 팝업*/
    GameObject quizResult;
    /*팝업 상태*/
    Vector3 open = new Vector3(1, 1, 1);
    Vector3 close = new Vector3(0, 1, 1);
    /*팝업 이미지*/
    Image popUpImg;

    // Use this for initialization
    void Start () {
        RandomQuiz();
        GetUI();
        SetUI();
	}
	
    /**
     * 랜덤하게 퀴즈 선택
     */
    public void RandomQuiz()
    {
        quiz = GameData.oxQuizzes[Random.Range(0, GameData.oxQuizzes.Length + 1)];
        quiz.appeard++;
    }

    /**
     * UI 가져오기
     */
    public void GetUI()
    {
        quizText = GameObject.Find("QuizText").GetComponent<Text>();
        btns[O_BTN] = GameObject.Find("O_Toggle").GetComponent<Toggle>();
        btns[X_BTN] = GameObject.Find("X_Toggle").GetComponent<Toggle>();

        for (int i=0; i<btns.Length; i++)
        {
            btns[i].isOn = false;
        }

        checkAnswerBtn = GameObject.Find("CheckAnswerBtn").GetComponent<Button>();
        checkAnswerBtn.interactable = false;

        quizResult = GameObject.Find("QuizResult");
        quizResult.transform.localScale = close;

        popUpImg = GameObject.Find("QuizResult/Image").GetComponent<Image>();
    }

    /**
     * UI 세팅
     */
    public void SetUI()
    {
        quizText.text = quiz.question;
    }

    /**
     * OX 버튼 누를 시 호출
     */
    public void OnClickToggles()
    {
        checkAnswerBtn.interactable = true;

        if (btns[O_BTN].isOn)
        {
            choosedAnswer = "O";
        }
        else
        {
            choosedAnswer = "X";
        }
    }

    /**
     * 정답확인 버튼
     */
    public void OnClickCheckAnswerBtn()
    {
        string correctness = "";

        if (choosedAnswer.Equals(quiz.answer))
        {
            correctness = "correct";
        }
        else
        {
            correctness = "incorrect";
        }

        popUpImg.sprite = Resources.Load<Sprite>("quiz/" + correctness + "_" + GameData.GetCharByOrder(GameData.currentOrder).name);
        quizResult.transform.localScale = open;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
