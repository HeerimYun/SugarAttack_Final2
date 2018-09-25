using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InsulinInput : MonoBehaviour {
    /*다음 버튼*/
    Button nextBtn;

    /*현재 플레이어 객체*/
    Character currentChar;
    /*플레이어 이미지*/
    Image playerImg;
    /*플레이어 텍스트*/
    Text playerName;

    /*인슐린 버튼*/
    Toggle[] insulin = new Toggle[6];
    /*선택한 인슐린*/
    int selectInsulin = 0;

    /*모든 플레이어의 인슐린을 받았는가?*/
    bool isInputDone = false;

	// Use this for initialization
	void Start () {
        //맨 처음에는 다음 버튼 비활성화
        nextBtn = GameObject.Find("InputDoneBtn").GetComponent<Button>();
        nextBtn.interactable = false;

        //현재 플레이어 객체
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
        //플레이어 이미지
        playerImg = GameObject.Find("playerImage").GetComponent<Image>();
        //플레이어 이름
        playerName = GameObject.Find("TargetName").GetComponent<Text>();

        //인슐린 버튼 6개
        for (int i=0; i<insulin.Length; i++)
        {
            insulin[i] = GameObject.Find("InsulinToggle" + (i + 1)).GetComponent<Toggle>();
        }

        DisplayChar();
	}

    /**
     * 현재 인슐린 입력받을 캐릭터 세팅
     */
    public void DisplayChar()
    {
        //이름 텍스트 설정
        playerName.text = currentChar.kName;

        //이미지 설정
        playerImg.sprite = Resources.Load<Sprite>("1_Char/" + currentChar.name);
        playerImg.SetNativeSize();
    }

    /**
     * 다음 버튼을 누를 때 호출
     */
    public void OnClickNextBtn()
    {
        //토글버튼에서 값 받아 저장
        SetInsulin();


        //다 받았으면 다음 씬으로, 다 못 받았으면 다시 현재씬으로!
        if (!isEveryoneDone()) //모든 플레이어의 인슐린이 아직 입력이 안됐다면,
        {
            //다시 이 씬으로 이동
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else //다 받은 경우
        {
            //다음 씬(룰렛)으로 이동
            PageMove.MoveToRoulette();
        }
        GameData.TurnChange(); //다음 턴으로 넘김
        //Debug.Log("현재 순서 : " + GameData.currentOrder);
    }

    /**
     * 모든 플레이어의 인슐린이 받아졌는 지 확인
     */
    public bool isEveryoneDone()
    {
        int count = 0;
        for (int i = 0; i < GameData.character.Length; i++)
        {
            if (GameData.character[i].inputInsulin != 0) //인슐린이 입력되어있다면,
            {
                count++;
            }
        }

        if (count == GameData.character.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
     * 버튼에서 값 받아 데이터에 인슐린 저장
     */
    public void SetInsulin()
    {
        currentChar.inputInsulin = selectInsulin;
        //Debug.Log("캐릭터: " + currentChar.kName + "의 인슐린 총량은 " + currentChar.inputInsulin);
    }

    /**
     * 인슐린 용량버튼 누를 때 마다 동작
     */
    public void OnClickInsulinToggle()
    {
        //버튼 활성화
        if (!nextBtn.IsInteractable())
        {
            nextBtn.interactable = true;
        }

        //켜져있는 버튼에 대해 인슐린 값 받기
        for (int i = 0; i < insulin.Length; i++)
        {
            if (insulin[i].isOn)
            {
                selectInsulin = 20 + (i * 5);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
