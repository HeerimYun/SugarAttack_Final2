using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/**
 * 룰렛에 붙이는 스크립트
 * 룰렛 돌리기: 내가 돌리는 방향, 드래그 한 만큼 돌아감
 * 손을 뗀 이후에는 다시 돌릴 수 없고 자동으로 멈출 때까지 기다리기
 */
public class RouletteSpin : MonoBehaviour {
    //회전 속도
    public float spinSpeed = 0;
    //멈춘 후 각도
    float rouletteAngle = 0;
    //버튼 눌린 상태
    bool spinBtnPress = false;
    //돌리기 버튼 객체
    Button spinBtn;
    //결과 텍스트
    Text rouletteResult;
    //숫자 결과 하이라이트 이미지
    GameObject hightlight;
    //결과 숫자 이미지
    GameObject resultNum;
    //sound
    AudioSource source;
    //result sound audio clip
    public AudioClip resultAppear;

	// Use this for initialization
	void Start () {
        spinBtn = GameObject.Find("roulette_btn").GetComponent<Button>();
        rouletteResult = GameObject.Find("roulette_result").GetComponent<Text>();
        rouletteResult.enabled = false; //처음엔 감출 것

        //하이라이트 이미지
        hightlight = GameObject.Find("roulette_hightlight");
        hightlight.GetComponent<Image>().enabled = false;

        resultNum = GameObject.Find("Number_Img");
        resultNum.GetComponent<Image>().enabled = false;

        //sound
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		//마우스로 드래그를 하면 그만큼의 힘 설정
        //왼쪽 버튼 누르면 판 움직임
        if (spinBtnPress)
        {
            spinSpeed = Random.Range(15.0f, 28.0f);
            spinBtnPress = false;
        }
        transform.Rotate(0, 0, spinSpeed); //원판 z축 rotating
        spinSpeed *= 0.987f; //점점 속도 느리게 하기 -> 7초 정도 걸리도록 수정
        //spinSpeed *= Random.Range(0.99f, 0.999f); //점점 속도 느리게 하기 -> 17초

        if (spinSpeed < 0.1 && spinSpeed > 0)
        {
            
            spinSpeed = 0; //룰렛을 멈추고

            rouletteAngle = transform.localEulerAngles.z; //현재 각도를 알아낸 뒤,

            if (transform.rotation.z % 10 == 0) // 딱 경계선인 경우
            {
                transform.Rotate(0, 0, (transform.rotation.z + 0.5f)); //조금만 움직여주기
                rouletteAngle = transform.localEulerAngles.z;
            }

            //숫자가 얼마 나왔는지 나타내기
            //먼저 돌리기 글자를 없애고,
            //GameObject.Find("roulette_spin_text").GetComponent<Image>().enabled = false;

            rouletteResult.enabled = true;
            rouletteResult.text = RouletteResult() + "";

            //중앙 돌리기 버튼 led 애니메이션 변경
            Animator btnAnim = spinBtn.GetComponent<Animator>();
            btnAnim.SetTrigger("RouletteBtnStop");

            //result appear sound
            source.PlayOneShot(resultAppear);

            //하이라이트 이미지 각도 조정하고, 보이기
            hightlight.transform.Rotate(0, 0, (rouletteAngle % 30) - 15);
            hightlight.GetComponent<Image>().enabled = true;

            
            resultNum.GetComponent<Image>().enabled = true;
            resultNum.GetComponent<Image>().sprite = Resources.Load<Sprite>("roulette/number_" + RouletteResult());

            if (RouletteResult() == 1) //1이면 position의 x값을 -12
            {
                //Debug.Log("1나옴");
                resultNum.transform.localPosition -= new Vector3(12, 0, 0);
                
            }

            resultNum.GetComponent<Image>().SetNativeSize();

            //숫자를 현재 캐릭터 이동에 반영
            GameData.GetCharByOrder(GameData.currentOrder).position += RouletteResult();

            //해당 칸에 따른 Scene으로 이동 하는 함수 호출
            //PageMove.OnClickDice();
        }

    }

    /**
     * "돌리기" 버튼을 누르는 순간 동작
     */
    public void OnClickSpinBtn()
    {
        spinBtnPress = true;

        //한번 룰렛을 돌린 이후로는 또 돌릴 수 없도록 버튼 비활성화
        spinBtn.interactable = false;

        //룰렛 가운데 글자를 물음표로 변경
        rouletteResult.enabled = true; //글자를 활성화 하고,
        rouletteResult.text = "?";

        //sound + .8sec delay
        //source.Play(35280);
        source.Play();
    }

    /**
     *  룰렛 숫자 판정
     */
     public int RouletteResult()
    {
        int result = 0;

        rouletteAngle = transform.localEulerAngles.z;
        //Debug.Log("현재 룰렛 Z 값: " + rouletteAngle);

        //각도 계산을 위해 180 넘으면 빼줌
        if (rouletteAngle > 180)
        {
            rouletteAngle -= 180;
        }

        result = (int)(rouletteAngle / 30) + 1;

        return result;
    }
}
