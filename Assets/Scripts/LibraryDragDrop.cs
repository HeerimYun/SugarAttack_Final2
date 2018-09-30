using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryDragDrop : MonoBehaviour {

    /*넘겨받은 퀴즈의 번호*/
    int index;
    /*퀴즈 객체*/
    DragQuiz quiz;

    /*퀴즈 문제 텍스트*/
    Text question;
    /*정답 텍스트*/
    Text answerTxt;
    /*해설 부분 텍스트*/
    Text library;

    /*정답 리스트*/
    string answers;

    // Use this for initialization
    void Start () {
        LoadQuiz();
        GetUI();
        SetUI();
    }

    private void LoadQuiz()
    {
        index = Quiz_DragDrop.index;
        quiz = GameData.dragQuizzes[index];
        MakeAnswerStr();
    }

    private void GetUI()
    {
        question = GameObject.Find("Question").GetComponent<Text>();
        answerTxt = GameObject.Find("AnswerText").GetComponent<Text>();
        library = GameObject.Find("LibraryText").GetComponent<Text>();
    }

    private void SetUI()
    {
        question.text = quiz.question;
        answerTxt.text = "정답 :   " + answers;
        library.text = quiz.library;
    }

    public void MakeAnswerStr()
    {
        for (int i=0; i < quiz.answer.Length; i++)
        {
            answers += quiz.answer[i];
            if(i != quiz.answer.Length - 1)
            {
                answers += ", ";
            }
        }

        answers = answers.Replace("\n", " ");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
