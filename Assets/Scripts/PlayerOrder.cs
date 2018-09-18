using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 플레이어 순서 나타내기
 */
public class PlayerOrder : MonoBehaviour {
    /*플레이어 수*/
    int playerNum = GameData.playerCount;

    /*순서 나타내는 이름 4개*/
    GameObject[] turn = new GameObject[4];

    /*Arrow 3개*/
    GameObject[] arrow = new GameObject[3];

    /*썸네일 이미지*/
    GameObject playerImg;

	// Use this for initialization
	void Start () {
        GetUI(); //플레이어 수 많큼 UI 활성화
        SetUI();
    }
	
    /**
     * UI 가져오기
     */
    public void GetUI()
    {
        //순서 텍스트 4개
        for (int i=0; i<turn.Length; i++)
        {
            turn[i] = GameObject.Find("Player" + (i + 1));
        }

        //화살표 3개
        for (int i=0; i<arrow.Length; i++)
        {
            arrow[i] = GameObject.Find("Arrow" + (i + 1));
        }

        playerImg = GameObject.Find("CurrentPlayerImage");
    }

    /**
     * 텍스트, 위치 설정
     */
    public void SetUI()
    {
        //텍스트에 이름넣기
        for (int i=0; i<playerNum; i++)
        {
            turn[i].GetComponent<Text>().text = GameData.GetCharByOrder(i + 1).kName;
        }
        
        //현재 순서인 플레이어 이미지
        playerImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UiFrame/" + GameData.GetCharByOrder(GameData.currentOrder).name + "_turn");

        //현재 플레이어 순서에 따른 위치 배열
        switch (GameData.currentOrder) //현재 플레이어
        {
            case 1:
                playerImg.transform.localPosition = new Vector3(6.5f, 296.5f, 0);
                for (int i=0; i<4; i++)
                {
                    turn[i].transform.localPosition = new Vector3(0, 83 - (i * 200), 0);
                }
                for (int i=0; i<3; i++)
                {
                    arrow[i].transform.localPosition = new Vector3(0,  -17 - (i * 200), 0);
                }
                break;
            case 2:
                //현재 플레이어 썸네일 ( 순서 text 위로 213.5에 위치)
                playerImg.transform.localPosition = new Vector3(6.5f, turn[1].transform.localPosition.y + 213.5f, 0);

                turn[0].transform.localPosition = new Vector3(0, 408, 0);
                arrow[0].transform.localPosition = new Vector3(0, turn[0].transform.localPosition.y - 100, 0);
                break;

            case 3:
                //현재 플레이어 이미지
                playerImg.transform.localPosition = new Vector3(6.5f, turn[2].transform.localPosition.y + 213.5f, 0);
                turn[0].transform.localPosition = new Vector3(0, 408, 0);
                arrow[0].transform.localPosition = new Vector3(0, turn[0].transform.localPosition.y - 100, 0);
                turn[1].transform.localPosition = new Vector3(0, arrow[0].transform.localPosition.y - 100, 0);
                arrow[1].transform.localPosition = new Vector3(0, turn[1].transform.localPosition.y - 100, 0);
                break;

            case 4:
                //현재 플레이어 이미지
                playerImg.transform.localPosition = new Vector3(6.5f, turn[3].transform.localPosition.y + 213.5f, 0);
                turn[0].transform.localPosition = new Vector3(0, 408, 0);
                arrow[0].transform.localPosition = new Vector3(0, turn[0].transform.localPosition.y - 100, 0);
                turn[1].transform.localPosition = new Vector3(0, arrow[0].transform.localPosition.y - 100, 0);
                arrow[1].transform.localPosition = new Vector3(0, turn[1].transform.localPosition.y - 100, 0);
                turn[2].transform.localPosition = new Vector3(0, arrow[1].transform.localPosition.y - 100, 0);
                arrow[2].transform.localPosition = new Vector3(0, turn[2].transform.localPosition.y - 100, 0);
                break;
        }

        //플레이어 수 이외 turn과 arrow 비활성화
        switch (playerNum)
        {
            case 3:
                turn[3].SetActive(false);
                arrow[2].SetActive(false);
                break;
            case 2:
                turn[3].SetActive(false);
                turn[2].SetActive(false);
                arrow[2].SetActive(false);
                arrow[1].SetActive(false);
                break;
        }

        //텍스트 컬러 설정
        SetTextColor();
    }

    /**
     * 플레이어 별 컬러 결정
     */
    private void SetTextColor()
    {
        //현재 순서가 아닌 플레이어에 대한 컬러
        for (int i = 0; i < playerNum; i++)
        {
            if (turn[i].GetComponent<Text>().text.Equals("마리"))
            {
                turn[i].GetComponent<Text>().color = new Color32(141, 81, 237, 178);
            }
            else if (turn[i].GetComponent<Text>().text.Equals("매트"))
            {
                turn[i].GetComponent<Text>().color = new Color32(95, 190, 33, 178);
            }
            else if (turn[i].GetComponent<Text>().text.Equals("빅터"))
            {
                turn[i].GetComponent<Text>().color = new Color32(27, 123, 236, 178);
            }
            else if (turn[i].GetComponent<Text>().text.Equals("루시"))
            {
                turn[i].GetComponent<Text>().color = new Color32(243, 49, 68, 178);
            }
        }

        //현재 순서인 플레이어에 대한 컬러
        Text currentPlayer = turn[GameData.currentOrder - 1].GetComponent<Text>();

        if (currentPlayer.text.Equals("마리"))
        {
            currentPlayer.color = new Color32(141, 81, 237, 255);
        }
        else if (currentPlayer.text.Equals("매트"))
        {
            currentPlayer.color = new Color32(95, 190, 33, 255);
        }
        else if (currentPlayer.text.Equals("빅터"))
        {
            currentPlayer.color = new Color32(27, 123, 236, 255);
        }
        else if (currentPlayer.text.Equals("루시"))
        {
            currentPlayer.color = new Color32(243, 49, 68, 255);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
