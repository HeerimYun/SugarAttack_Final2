using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Insulin_volume 캔버스에 붙는 스크립트
 */
public class InsulinVolume : MonoBehaviour {

    public static int finalValue = 0;

    /*설정완료 버튼*/
    Button volumeSetBtn;
    /*인슐린 양*/
    int value;

	// Use this for initialization
	void Start () {
        volumeSetBtn = GameObject.Find("VolumeSetButton").GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        value = NeedleHeadController.value; //계속 값 갱신

        //인슐량 용량에 따른 버튼 활성화 / 비활성화
        if (value > 0)
        {
            volumeSetBtn.interactable = true;
        }
        else
        {
            volumeSetBtn.interactable = false;
        }
	}

    /**
     * 설정 확인 버튼 누를 시 동작
     */
    public void OnClickSetVolumeBtn()
    {
        //인슐린 정보 저장
        //-> 아직 미구현, 급식실과 같이 구현..
        //다음페이지로 이동
        finalValue = value;
        PageMove.MoveToInsulinBody();
    }
}
