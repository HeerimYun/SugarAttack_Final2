using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 4지 선다형 문제를 틀릴 시 해설
 */
public class LibraryChoice : MonoBehaviour
{

    /*넘겨받은 퀴즈의 번호*/
    int index;
    /*퀴즈 객체*/
    ChoiceQuiz quiz;
    /*퀴즈 선택지 넘버*/
    int number;

    /*퀴즈 문제 텍스트*/
    Text question;
    /*정답 부분 이미지*/
    Image answer;
    /*정답 텍스트*/
    Text answerTxt;
    /*해설 부분 텍스트*/
    Text library;

    GameObject answerSection;

    // Use this for initialization
    void Start()
    {
        LoadQuiz();
        GetUI();
        SetUI();
    }

    private void LoadQuiz()
    {
        index = Quiz_Choice.index;
        quiz = GameData.choiceQuizzes[index];
        GetNumber();
    }

    private void GetUI()
    {
        question = GameObject.Find("Question").GetComponent<Text>();
        answer = GameObject.Find("Choice_Answer").GetComponent<Image>();
        answerTxt = GameObject.Find("Choice_Answer/Text").GetComponent<Text>();
        library = GameObject.Find("LibraryText").GetComponent<Text>();
        answerSection = GameObject.Find("AnswerSection");

    }

    private void SetUI()
    {
        question.text = quiz.question;
        answer.sprite = Resources.Load<Sprite>("quiz/btn_" + number + "_selected");
        answerTxt.text = quiz.answer;
        library.text = quiz.library;

        answerSection.GetComponent<RectTransform>().sizeDelta = new Vector2((quiz.answer.Length) * 48 + 270, 82.3f);
        if (answerSection.GetComponent<RectTransform>().rect.width < 450)
        {
            answerSection.transform.localPosition += new Vector3(200, 0, 0);
        }
        else if (answerSection.GetComponent<RectTransform>().rect.width < 550)
        {
            answerSection.transform.localPosition += new Vector3(160, 0, 0);
        }
        //GameObject.Find("Choice_Answer/Text").GetComponent<RectTransform>().sizeDelta = new Vector2((quiz.answer.Length) * 48, 82.3f);
        //Debug.Log("길이 " + answerTxt.text.Length); //한 글자당 width 48
    }

    private void GetNumber()
    {
        for (int i = 0; i < quiz.choice.Length; i++)
        {
            if (quiz.choice[i].Equals(quiz.answer))
            {
                number = i + 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
