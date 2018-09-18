using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * 룰렛 씬에 붙어서 동작하는 스크립트
 * 현재 누구 턴인지 나타냄
 */
public class RouletteScene : MonoBehaviour {
    /*현재 캐릭터 객체*/
    Character currentChar;

    /*룰렛 왼쪽 하단의 캐릭터 나타내는 이미지*/
    Image charImg;

    /*상단의 이름*/
    Text turnName;

    /*캐릭터 애니메이션*/
    Animator rouletteCharacterAnim;

    /*룰렛씬 머무는 시간*/
    public float duraition = 23;

    float currentTime = 30;

	// Use this for initialization
	void Start () {
        GetCurrentChar(); //현재 순서인 캐릭터의 객체
        GetUI(); //이미지 가져오기
        SetUI(); //이미지 씌우기
	}

    public void SetUI()
    {
        //캐릭터에 따른 경로에서 파일 찾아와서 적용하기
        charImg.sprite = Resources.Load<Sprite>("Characters/" + currentChar.name + "/idle/" + currentChar.name.ToLower() + "_idle_01");
        rouletteCharacterAnim.SetTrigger(currentChar.name + "Roulette");
        turnName.text = currentChar.kName;
    }

    public void GetCurrentChar()
    {
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
        Debug.Log("현재 순서는 " + currentChar.kName);
    }

    public void GetUI()
    {
        charImg = GameObject.Find("CharacterImage").GetComponent<Image>();
        rouletteCharacterAnim = GameObject.Find("CharacterImage").GetComponent<Animator>();
        turnName = GameObject.Find("TurnName").GetComponent<Text>();
    }
	
    /**
     * 버튼 누르면 24초 정도 타이머를 센다
     */
    public void StartTimer()
    {
        currentTime = 0;
    }

	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        Debug.Log("현재 시간" + currentTime);
        if (currentTime > duraition && currentTime < duraition + 2)
        {
            GameData.TurnChange();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
