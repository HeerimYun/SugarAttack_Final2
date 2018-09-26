using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

            if (SceneManager.GetActiveScene().name.Equals("5.0_Quiz_OX"))
            {
                if(transform.GetChild(1).GetComponent<SpriteRenderer>().sprite.name.Equals("correct_" + GameData.GetCharByOrder(GameData.currentOrder).name))
                {
                    //맞았으면 보상화면
                    PageMove.MoveToQuizReward();
                }
                else
                {
                    //틀렸으면 턴 넘기기
                    GameData.TurnChange();
                    PageMove.MoveToRoulette();
                }
            }
            else
            {
                if (transform.GetChild(0).GetComponent<Image>().sprite.name.Equals("correct_" + GameData.GetCharByOrder(GameData.currentOrder).name))
                {
                    //맞았으면 보상화면
                    PageMove.MoveToQuizReward();
                }
                else
                {
                    //틀렸으면 턴 넘기기
                    GameData.TurnChange();
                    PageMove.MoveToRoulette();
                }
            }
        }
    }
}
