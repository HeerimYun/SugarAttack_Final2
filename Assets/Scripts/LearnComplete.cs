using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnComplete : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    /**
     * 학습완료 버튼 누를 경우
     * 턴을 넘기고, 룰렛으로 보냄
     */
    public void OnClickLearnComplete()
    {
        GameData.TurnChange();
        PageMove.MoveToRoulette();
        //도서관 한 턴 쉬는 것 적용
        //GameData.GetCharByOrder(GameData.currentOrder).libraryTurn = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
