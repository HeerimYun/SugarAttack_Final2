using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InsulinAngle : MonoBehaviour {

    /*여기서 놓은 각도 (다음 씬에서 사용하기 위함)*/
    public static float angle = 0;

    /*각도 조절에 성공 실패 여부 (다음 씬에서 사용하기 위함)*/
    public static bool success = false;

    /*각도 조절 버튼 (빨강)*/
    GameObject rangeLever;
    /*설정 확인 버튼*/
    Button angleSetBtn;

    /*마지막 결정 된 각도*/
    float finalAngle = 0;

    /*코치마크*/
    GameObject coachMark;

    /*버튼 활성화 여부*/
    bool active = false;

    /*주사기 이름, 용량*/
    Text nName, nVolume;

    /*Animator*/
    Animator fingerAnim;

	// Use this for initialization
	void Start () {
        GetUI();
        SetUI();
	}

    private void GetUI()
    {
        rangeLever = GameObject.Find("Lever");
        angleSetBtn = GameObject.Find("AngleSetButton").GetComponent<Button>();
        coachMark = GameObject.Find("CoachMark"); //코치마크 객체 찾기
        nName = GameObject.Find("NeedleName").GetComponent<Text>();
        nVolume = GameObject.Find("NeedleValue").GetComponent<Text>();
        fingerAnim = GameObject.Find("GuideFinger").GetComponent<Animator>();
        fingerAnim.SetTrigger("fingerStop");
    }

    private void SetUI()
    {
        angleSetBtn.interactable = false;
        //주사기 이름, 용량 세팅
        nVolume.text = InsulinVolume.finalValue + "";
        nName.text = GameData.GetCharByOrder(GameData.currentOrder).kName;

        if (GameData.iAngleCM) //코치마크가 처음이면 
        {
            fingerAnim.SetTrigger("fingerMove");
            coachMark.transform.localScale = GameData.open; //펼치고,
            GameData.iAngleCM = false; //다음부터는 펼치지 않는다.
        }
    }
    
    /**
     * 설정 완료 버튼 누를 시 동작
     */
    public void OnClickAngleSetBtn()
    {
        finalAngle = rangeLever.transform.localEulerAngles.z;
        //Debug.Log("현재 각도 : " + finalAngle);
        if (rangeLever.transform.localEulerAngles.z > 100)
        {
            finalAngle = finalAngle - 360; //각도 값 수정
        }
        //Debug.Log(finalAngle);

        //정상 범위 내에 들었을 경우
        if (finalAngle < 16 && finalAngle > -16) 
        {
            //맞았음을 기록
            success = true;
        }
        else //정상범위가 아닐 경우
        {
            //틀림을 기록
            success = false;
        }

        Debug.Log("각도 성공 여부 : " + success);
        //angle 저장
        angle = rangeLever.transform.eulerAngles.z;
        //다음 페이지로 이동
        PageMove.MoveToInsulinInject();
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("실시간 현재 각도 : " + rangeLever.transform.localEulerAngles.z);
        angleSetBtn.interactable = active; //버튼의 활성화 여부

        if (Input.GetMouseButton(0)) //클릭 (한 번 이라도 레버를 조작하려 시도할 시) 시 활성화
        {
            active = true;
        }

        if (coachMark.transform.localScale == GameData.open) //만약 코치마크가 열려있다면,
        {
            if (Input.GetMouseButtonDown(0)) // 터치가 있을 경우
            {
                fingerAnim.SetTrigger("fingerStop");
                coachMark.transform.localScale = GameData.close; // 코치마크를 닫는다.
            }
        }
    }
}
