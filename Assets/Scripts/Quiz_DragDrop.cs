using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 드래그 드롭 퀴즈 스크립트
 * AnswerDropArea에 있는 객체를 정답으로 인정한다.
 * 1. 문제 보기 세팅 - 문제 세팅, 보기 세팅
 * 2. 답 판정 - 영역에 들어와있으면 답으로 치고, 해당 객체들의 text 값을 정답과 비교
 */
public class Quiz_DragDrop : MonoBehaviour {

    /*문제 텍스트*/
    Text question;
    /*포스트잇에 나타낼 텍스트*/
    Text[] postit = new Text[6];
    /*현재 퀴즈*/
    DragQuiz quiz;
    /*정답 놓는 영역*/
    GameObject answerDropArea;
    /*정답확인 버튼*/
    Button checkAnswerBtn;
    /*입력받은 정답 리스트*/
    string[] choosedAnswer;
    /*정답 여부*/
    bool isAnswer = false;

    /*결과 팝업*/
    GameObject quizResult;
    /*팝업 상태*/
    Vector3 open = new Vector3(1, 1, 1);
    Vector3 close = new Vector3(0, 1, 1);
    /*팝업 이미지*/
    SpriteRenderer popUpImg;

    /*correct_particle*/
    ParticleSystem correctParticle;
    /*Popup pop anim*/
    Animator popAnim;
    /*result sound*/
    AudioSource resultSound;
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    // Use this for initialization
    void Start () {
        RandomQuiz();
        GetUI();
        SetUI();
	}

    /**
     * choose quiz randomly
     */
    public void RandomQuiz()
    {
        quiz = GameData.dragQuizzes[Random.Range(0, GameData.dragQuizzes.Length)];
        quiz.appear++;
    }

    /**
     * UI Loading
     */
    public void GetUI()
    {
        question = GameObject.Find("QuizText").GetComponent<Text>();
        Debug.Log(question);

        for (int i=0; i<postit.Length; i++)
        {
            postit[i] = GameObject.Find("ColorCard" + (i + 1) + "/Text").GetComponent<Text>();
        }

        answerDropArea = GameObject.Find("AnswerDropArea");
        checkAnswerBtn = GameObject.Find("CheckAnswerBtn").GetComponent<Button>();

        quizResult = GameObject.Find("QuizResult");
        quizResult.transform.localScale = close;

        popUpImg = GameObject.Find("QuizResult/Image").GetComponent<SpriteRenderer>();

        correctParticle = GameObject.Find("QuizResult/correct_panpare").GetComponent<ParticleSystem>();
        correctParticle.Pause();

        popAnim = GameObject.Find("QuizResult").GetComponent<Animator>();

        resultSound = GameObject.Find("QuizResult").GetComponent<AudioSource>();
    }

    /**
     * UI setting
     */
    public void SetUI()
    {
        question.text = quiz.question; //quiz question text setting

        for(int i=0; i<postit.Length; i++) //6 postit text setting
        {
            postit[i].text = quiz.postit[i];
        }
    }
	
    /**
     * 정답확인 버튼 활성화/비활성화
     */
    public void SetCheckAnswerBtn()
    {
        //정답 영역에 객체가 하나도 없으면 비활성화
        if (answerDropArea.transform.childCount < 1)
        {
            checkAnswerBtn.interactable = false;
        }
        else
        {
            checkAnswerBtn.interactable = true;
        }
    }

    /**
     * 정답확인 버튼 누를 시 동작
     */
    public void OnClickCheckAnswerBtn()
    {
        isAnswer = false;

        int choosedAnswerNum = answerDropArea.transform.childCount;
        
        choosedAnswer = new string[choosedAnswerNum];
        //현재 정답 area에 있는 포스트잇의 텍스트들을 배열로 저장
        for(int i=0; i<choosedAnswerNum; i++)
        {
            Debug.Log(answerDropArea.transform.GetChild(i));
            Debug.Log(answerDropArea.transform.GetChild(i).gameObject);
            choosedAnswer[i] = GameObject.Find(answerDropArea.transform.GetChild(i).gameObject.name + "/Text").GetComponent<Text>().text;
        }

        if (choosedAnswerNum == quiz.answer.Length) //개수가 같으면,
        {
            //내용까지 같은 지 확인한다.
            if (IsAllAnswer())
            {
                isAnswer = true;
            }
        }
        else
        {
            isAnswer = false;
        }

        SetResultPopup();
    }

    /**
     * 답이 있는 지 확인
     */
    public bool IsAllAnswer()
    {
        int count = 0;
        bool result = false;

        //정답 배열을 돌면서 텍스트와 같은지 찾는다. 찾으면 count++, 카운트 수가 정답 개수와 같으면 맞는 것
        for (int i=0; i<choosedAnswer.Length; i++) //고른 정답
        {
            for(int k=0; k < quiz.answer.Length; k++) //정답 배열
            {
                if (choosedAnswer[i].Equals(quiz.answer[k])) //정답이 있으면
                {
                    count++;
                }
            }
        }

        if (count == choosedAnswer.Length)
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
     * 결과 팝업 띄우기
     */
    public void SetResultPopup()
    {
        string correctness = "";

        if (isAnswer)
        {
            correctness = "correct";
            /*particle view*/
            correctParticle.Play();
            /*resultSound*/
            resultSound.PlayOneShot(correctSound);
        }
        else
        {
            correctness = "incorrect";
            /*particle view*/
            correctParticle.Stop();
            /*resultSound*/
            resultSound.PlayOneShot(incorrectSound);
        }
        /*popup pop anim*/
        popAnim.SetTrigger("ResultPop");
        popUpImg.sprite = Resources.Load<Sprite>("quiz/" + correctness + "_" + GameData.GetCharByOrder(GameData.currentOrder).name);
        quizResult.transform.localScale = open;
    }

	// Update is called once per frame
	void Update () {
        SetCheckAnswerBtn();
	}
}
