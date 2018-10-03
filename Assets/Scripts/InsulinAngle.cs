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

    bool active = false;
	// Use this for initialization
	void Start () {
        rangeLever = GameObject.Find("Lever");
        angleSetBtn = GameObject.Find("AngleSetButton").GetComponent<Button>();
        angleSetBtn.interactable = false;
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
            finalAngle = finalAngle - 360;
        }
        Debug.Log(finalAngle);
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
        Debug.Log("실시간 현재 각도 : " + rangeLever.transform.localEulerAngles.z);
        angleSetBtn.interactable = active; //버튼의 활성화 여부

        if (Input.GetMouseButton(0)) //클릭 (한 번 이라도 레버를 조작하려 시도할 시) 시 활성화
        {
            active = true;
        }
    }
}
