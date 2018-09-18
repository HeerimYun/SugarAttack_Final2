using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 캐릭터 고르는 화면
 * 
 */
public class CharSelect : MonoBehaviour {

    /*GameData 객체*/
    GameData gameData = new GameData();

    /*캐릭터 4명의 토글 버튼*/
    Toggle[] characters = new Toggle[4];

    /*선택완료 버튼*/
    Button selectDoneBtn;

    /*캐릭터 선택 수*/
    public int onCount = 0;

    // Use this for initialization
    void Start()
    {
        //4명의 캐릭터 토글 버튼 연결
        characters[0] = GameObject.Find("Marie").GetComponent<Toggle>();
        characters[1] = GameObject.Find("Matt").GetComponent<Toggle>();
        characters[2] = GameObject.Find("Lucy").GetComponent<Toggle>();
        characters[3] = GameObject.Find("Victor").GetComponent<Toggle>();

        //선택완료 버튼
        selectDoneBtn = GameObject.Find("SelectDoneBtn").GetComponent<Button>();

        //처음에는 선택완료 버튼 비활성화
        selectDoneBtn.interactable = false;
    }

    /**
     * 캐릭터 버튼을 누를 때 마다 동작하는 함수
     */
    public void OnClickCharacter()
    {
        //켜진 캐릭터 수 초기화
        onCount = 0;

        //명 수 체크
        for (int i=0; i<characters.Length; i++)
        {
            if (characters[i].isOn)
            {
                onCount++;
            }
        }

        //선택 인원 수가 둘 이상이면 선택완료 버튼 활성화
        if (onCount >= 2)
        {
            selectDoneBtn.interactable = true;
        }
        else
        {
            selectDoneBtn.interactable = false;
        }
    }

    /**
     * 선택완료 버튼을 누를 시 동작
     * 선택 사항들로 캐릭터 객체 생성
     */
	public void OnClickSelectDoneBtn()
    {
        //플레이어 수
        GameData.playerCount = onCount;

        //플레이어 리스트
        string[] players = new string[onCount];

        //플레이어 리스트의 인덱스
        int k = 0;

        //플레이어 리스트 만들기
        for (int i=0; i<4; i++)
        {
            if (characters[i].isOn) //토글이 켜져있으면 플레이어 리스트에 넣을 것
            {
                players[k] = characters[i].name; //해당 이름 넘김
                k++;
            }
        }

        //완성된 플레이어 리스트로 생성
        GameData.playerList = new string[onCount];
        
        for (int i=0; i<onCount; i++)
        {
            GameData.playerList[i] = players[i];
        }

        //캐릭터 생성
        gameData.SetCharacters();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
