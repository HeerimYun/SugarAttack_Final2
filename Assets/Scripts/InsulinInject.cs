using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 인슐린 주입 Scene 스크립트
 */
public class InsulinInject : MonoBehaviour {

    /*주입 성공 여부 (다음씬에서 사용하기 위함)*/
    public static bool success = false;
    /*주입한 시간 저장*/
    public static int injectedTime = 0;
    /*버튼 눌림 여부*/
    bool pressed = false;

    /*현재 시간, 주입 하고 있는 시간*/
    float currentTime = 0;
    float currentTime2 = 0;
    int injectionTime = 10; // 10초
    int timerTime = 10;

    /*Inject Button image*/
    Image injectBtn;

    /*particle control*/
    ParticleSystem sucessParticle;

    /*인슐린 주사*/
    GameObject needle;
    /*주사기에 쓰인 이름*/
    Text needleName;
    /*주사기 용량*/
    Text needleVolume;

    /*타이머*/
    GameObject timer;
    /*타이머 시간*/
    Text timeText;

    /*현재 캐릭터*/
    Character currentChar;

    /*성공 혹은 실패 이미지*/
    GameObject resultImg;

    /*코치마크*/
    GameObject coachMark;

    /*인슐린 버튼 확대 그림자*/
    GameObject shadow;

    /*인슐린 용액 줄어드는 애니메이션*/
    Animator waterAnim;

    /*인슐린 용액*/
    GameObject waterObj;
    /*위치, 크기*/
    public static Vector3 waterPos = new Vector3(0, 0, 0);
    public static Vector2 waterSize = new Vector2(0, 0);

    /*공기 방울*/
    GameObject bubbleObj;
    /*위치, 크기*/
    public static Vector3 bubblePos = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start () {
        injectedTime = 0;
        GetUI();
        SetUI();
	}
    
    private void GetUI()
    {
        needle = GameObject.Find("Needle");
        needleName = GameObject.Find("needleName").GetComponent<Text>();
        needleVolume = GameObject.Find("needleVolume").GetComponent<Text>();
        timer = GameObject.Find("Timer");
        timeText = GameObject.Find("timeText").GetComponent<Text>();
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
        resultImg = GameObject.Find("ResultImg");
        coachMark = GameObject.Find("CoachMark");
        shadow = GameObject.Find("shadow");
        waterAnim = GameObject.Find("Needle/water").GetComponent<Animator>();
        waterObj = GameObject.Find("Needle/water");
        bubbleObj = GameObject.Find("Needle/bubbles");
        injectBtn = GameObject.Find("ButtonImage").GetComponent<Image>();
        sucessParticle = GameObject.Find("ResultImg/correct_panpare").GetComponent<ParticleSystem>();
    }

    private void SetUI()
    {
        needle.transform.Rotate(0, 0, InsulinAngle.angle); //이전 씬에서 설정한 각도로 세팅
        needleName.text = currentChar.kName;
        needleVolume.text = InsulinVolume.finalValue + "";
        timeText.text = timerTime + "";
        waterAnim.speed = 0; //처음에는 물 애니메이션 재생하지 않음
        bubbleObj.GetComponent<Animator>().speed = 0; //공기 방울 애니메이션도 재생 안 함

        if (InsulinAngle.success) //각도를 맞췄을 경우
        {
            shadow.transform.localScale = GameData.open; //버튼의 확대 shadow를 보인다.
        }
        else
        {
            shadow.transform.localScale = GameData.close; //버튼의 확대 shadow를 숨긴다.
        }

        /*timer sound*/
        timer.GetComponent<AudioSource>().enabled = false;


        if (GameData.iInjectCM) //코치마크가 처음이면
        {
            coachMark.transform.localScale = GameData.open; //코치마크 열기
            /*coachAnim*/
            coachMark.GetComponent<Animator>().SetTrigger("CoachMove");
            GameData.iInjectCM = false; //다음부터는 코치마크 열지 않음
        }

        /*particle controll*/
        sucessParticle.Pause();
    }

    // Update is called once per frame
    void Update () {

		if (pressed) //용량 주입 버튼이 눌리는 경우
        {
            injectBtn.sprite = Resources.Load<Sprite>("Insulin_inject/inject_button_image_push");
            currentTime += Time.deltaTime; //시간을 흐르게 한다.

            /*timer sound*/
            timer.GetComponent<AudioSource>().enabled = true;
        }

        if (timerTime - (int)currentTime >= 0) //타이머 시간이 0 보다 크면,
        {
            timeText.text = timerTime - (int)currentTime + ""; //타이머에 시간 표시
        }

        if (currentTime >= injectionTime) //10초가 지나면
        {
            injectedTime = 10; //10초 성공
            success = true; //성공여부 true
            //Debug.Log("주입 성공 여부 : " + success);
            DisplayResultPopUp(); //성공여부 결과 팝업 펼치기

            /*timer sound*/
            timer.GetComponent<AudioSource>().enabled = false;
        }

        if (resultImg.transform.localScale == GameData.open)
        {
            currentTime2 += Time.deltaTime;

            if (currentTime2 > 4)
            {
                PageMove.MoveToInsulinResult();
            }
        }

        if (coachMark.transform.localScale == GameData.open) //만약 코치마크가 켜져있다면,
        {
            if (Input.GetMouseButtonDown(0)) //터치할 경우
            {
                /*coachAnim*/
                coachMark.GetComponent<Animator>().SetTrigger("CoachEnd");
                coachMark.transform.localScale = GameData.close; // 코치마크를 닫는다.
            }
        }
	}

    /**
     * 인슐린 주입 버튼에서 손을 놓을 시 동작
     */
    public void OnPointerUpButton()
    {
        //Debug.Log("버튼 떼짐");
        injectedTime = (int)currentTime; //실패한 경우의 주입한 시간 저장

        /*timer sound*/
        timer.GetComponent<AudioSource>().enabled = false;

        injectBtn.sprite = Resources.Load<Sprite>("Insulin_inject/inject_button_image");

        //현재 물 이미지의 양, 위치 저장
        waterPos = waterObj.transform.localPosition;
        waterSize = waterObj.GetComponent<RectTransform>().sizeDelta;

        //방울 위치 저장
        bubblePos = bubbleObj.transform.localPosition;

        pressed = false;
        if (currentTime < injectionTime)
        {
            //Debug.Log("실패!");
            success = false;
            currentTime = 0;
            //Debug.Log("주입 성공 여부 : " + success);
            DisplayResultPopUp(); //성공여부 결과 팝업 펼치기
        }
    }

    /**
     * 인슐린 주입 버튼 누를 시 동작
     */
    public void OnPressButton()
    {
        Debug.Log("버튼 눌림");
        pressed = true;

        //물 줄어드는 애니메이션 시작
        waterAnim.speed = 1;
        //공기 방울 애니메이션 시작
        bubbleObj.GetComponent<Animator>().speed = 1;
    }

    /**
     * 성공 / 실패 결과 팝업 펼치기
     */
    private void DisplayResultPopUp()
    {
        resultImg.transform.localScale = GameData.open;
        
        if (success && InsulinAngle.success) //각도, 주입 둘 다 성공하면,
        {
            //성공했어요 코치마크
            resultImg.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite
                = Resources.Load<Sprite>("Insulin_inject/" + currentChar.name + "_sucess");
            sucessParticle.Play();
        }
        else //둘 다 혹은 하나라도 성공 못 한 경우
        {
            //실패했어요 코치마크
            resultImg.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite
                = Resources.Load<Sprite>("Insulin_inject/" + currentChar.name + "_fail");
            sucessParticle.Stop();
        }
        resultImg.GetComponent<Animator>().SetTrigger("ResultPop");
    }
}
