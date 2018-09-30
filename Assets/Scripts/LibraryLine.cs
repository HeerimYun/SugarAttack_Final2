using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 선잇기 퀴즈 해설 
 */
public class LibraryLine : MonoBehaviour {

    /*넘겨받은 퀴즈의 번호*/
    int index;
    /*퀴즈 객체*/
    LineQuiz quiz;

    /*퀴즈 문제 텍스트*/
    Text question;
    /*정답 부분 이미지*/
    Image answer;
    /*해설 부분 텍스트*/
    Text library;

    /*퀴즈 인덱스 넘버*/
    int quizIndex;

    // Use this for initialization
    void Start () {
        LoadQuiz();
        GetUI();
        SetUI();
    }

    private void LoadQuiz()
    {
        quizIndex = Quiz_Line.index;
        quiz = GameData.lineQuizzes[quizIndex];
    }

    private void GetUI()
    {
        question = GameObject.Find("Question").GetComponent<Text>();
        answer = GameObject.Find("AnswerImage").GetComponent<Image>();
        library = GameObject.Find("LibraryText").GetComponent<Text>();

    }

    private void SetUI()
    {
        question.text = quiz.question;
        answer.sprite = Resources.Load<Sprite>("Library/line_answer_" + (quizIndex + 1));
        library.text = quiz.library;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
