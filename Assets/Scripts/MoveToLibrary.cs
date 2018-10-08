using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 도서관으로 이동해주세요 팝업
 */
public class MoveToLibrary : MonoBehaviour {

    /*팝업 이미지*/
    Animator popupImg;

    /*현재 순서 캐릭터*/
    Character current;

	// Use this for initialization
	void Start () {
        current = GameData.GetCharByOrder(GameData.currentOrder); //현재 캐릭터
        popupImg = GameObject.Find("PopUpImage").GetComponent<Animator>(); //팝업 이미지

        popupImg.SetTrigger("lib" + current.name + "Run");
	}
	
    /**
     * 이동완료 버튼 누를 시 동작
     */
    public void OnClickMoveBtn()
    {
        current.isLibrary = true; //도서관 한 턴 쉬기 적용
        PageMove.MoveToLibrary(); //도서관으로 이동
    }

	// Update is called once per frame
	void Update () {
		
	}
}
