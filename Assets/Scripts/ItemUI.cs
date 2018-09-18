using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * ItemUI prefab에 붙어서 동작하는 스크립트
 * 현재 순서의 캐릭터의 보건, 사탕, 몬스터 카드 보유개수를 알려준다.
 */
public class ItemUI : MonoBehaviour {

    const int BOGUN = 0;
    const int CANDY = 1;
    const int MONSTER = 2;
    
    /*현재 순서의 캐릭터*/
    Character currentChar;

    // 텍스트 나타나는 부분
    Text[] itemCount = new Text[3];

    // Use this for initialization
    void Start () {
        GetCurrentChar(); //현재 캐릭터 객체 가져오기
        GetItemUI(); //아이템 객체 가져오기
        SetItemUI(); //아이템 숫자 업데이트
    }

    public void GetItemUI()
    {
        itemCount[BOGUN] = GameObject.Find("BogunsilCount").GetComponent<Text>();
        itemCount[CANDY] = GameObject.Find("CandyCount").GetComponent<Text>();
        itemCount[MONSTER] = GameObject.Find("MonsterCardCount").GetComponent<Text>();
    }

    public void SetItemUI()
    {
        itemCount[BOGUN].text = currentChar.bogunCard + "";
        itemCount[CANDY].text = currentChar.candy + "";
        itemCount[MONSTER].text = currentChar.monsterCard.Count + "";
    }

    public void GetCurrentChar()
    {
        currentChar = GameData.GetCharByOrder(GameData.currentOrder);
        //Debug.Log(GameData.GetCharByOrder(GameData.currentOrder));
        //Debug.Log(GameData.currentOrder); -> 문제 발생! 2명밖에 없는데 3이라 뜸
    }

    // Update is called once per frame
    void Update () {
        SetItemUI();
    }
}
