using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 판넬 오브젝트에 붙어서 동작하는 팝업
 * 퀴즈 결과에 따라 등장하는 팝업
 */
public class QuizResultPopUp : MonoBehaviour {

    /*시간*/
    float currentTime = 0;
    /*노출 시간*/
    float duraition = 3;
    /*연 상태, 닫은 상태*/
    Vector3 open, close;

	// Use this for initialization
	void Start () {
        GetUI();
        SetUI();
	}
	
    public void GetUI()
    {
        open = new Vector3(1, 1, 1);
        close = new Vector3(0, 1, 1);
    }

    public void SetUI()
    {
        transform.localScale = close;
    }

	// Update is called once per frame
	void Update () {
        //currentTime += Time.deltaTime;

        if (transform.localScale == open)
        {
            currentTime += Time.deltaTime;
            
        }

        if (currentTime > duraition)
        {
            //transform.localScale = close;
            //화면 넘어가기

            if (transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name.Equals("correct_" + GameData.GetCharByOrder(GameData.currentOrder).name))
            {
                Debug.Log("?");
                //맞았으면 보상화면
                PageMove.MoveToQuizReward();
            }
            else
            {
                //틀렸으면 도서관으로 보내기
                PageMove.MoveToLibrary();
            }
        }
    }
}
