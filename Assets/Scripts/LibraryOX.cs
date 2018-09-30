using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * OX 유형 도서관
 * 도서관에서 해설 펼치기
 * 틀린 문제의 유형을 안 뒤, 해당 템플릿을 펼친다.
 */
public class LibraryOX : MonoBehaviour {

    /*넘겨받은 퀴즈의 번호*/
    int index;
    /*퀴즈 객체*/
    OXQuiz quiz;

    /*퀴즈 문제 텍스트*/
    Text question;
    /*정답 부분 이미지*/
    Image answer;
    /*해설 부분 텍스트*/
    Text library;

	// Use this for initialization
	void Start () {
        LoadQuiz();
        GetUI();
        SetUI();
	}

    private void SetUI()
    {
        question.text = quiz.question;
        answer.sprite = Resources.Load<Sprite>("quiz/" + quiz.answer.ToLower() + "_btn_active");
        library.text = quiz.library;
    }

    private void GetUI()
    {
        question = GameObject.Find("Question").GetComponent<Text>();
        answer = GameObject.Find("OX_Answer").GetComponent<Image>();
        library = GameObject.Find("LibraryText").GetComponent<Text>();
    }

    public void LoadQuiz()
    {
        index = Quiz_OX.index;
        quiz = GameData.oxQuizzes[index];
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
