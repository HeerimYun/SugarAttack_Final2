using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 랜덤하게 캐릭터 순서 배열 설정
 */
public class CharOrder : MonoBehaviour {

    /*플레이어 수*/
    int players = GameData.playerCount;

    /*순위를 배정할 배열*/
    int[] order;

    Vector3 open = new Vector3(1, 1, 1);
    Vector3 close = new Vector3(0, 1, 1);

    //화살표
    GameObject[] arrow = new GameObject[3];

	// Use this for initialization
	void Start () {
        GetUI();
        MakeRandomOrder(); //랜덤 순위 생성
        SetOrder(); //GameData에 순위 저장
        DisplayOrder();
	}
    
    public void GetUI()
    {
        for (int i=0; i<3; i++)
        {
            arrow[i] = GameObject.Find("Arrow" + (i + 1));
        }
    }

    /**
     * 순위 만들기
     */
    public void MakeRandomOrder()
    {
        order = new int[players]; //배열 생성
        int temp = 0;

        for (int i=0; i<players; i++)
        {
            temp = Random.Range(1, players + 1);
            bool flag = false;

            if (i>0 && i<players)
            {
                for (int k=0; k<=i; k++)
                {
                    if (order[k] == temp)
                    {
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                --i;
            }
            else
            {
                order[i] = temp;
            }
        }

        /*for (int i=0; i<players; i++)
        {
            Debug.Log(order[i]);
        }*/
    }

    /**
     * 순위 저장하기
     */
     public void SetOrder()
    {
        for (int i=0; i<players; i++)
        {
            GameData.character[i].order = order[i];

            if (GameData.character[i].order == 1)
            {
                GameData.character[i].isCurrent = true;
            }
        }
        GameData.currentOrder = 1;
    }

    /**
     * 순위대로 배열
     */
     public void DisplayOrder()
    {
        //처음에 전부 안보이게 하기
        GameObject.Find("Lucy").transform.localScale = close;
        GameObject.Find("Marie").transform.localScale = close;
        GameObject.Find("Matt").transform.localScale = close;
        GameObject.Find("Victor").transform.localScale = close;

        switch (players)
        {
            case 2:
                GameObject.Find(GameData.GetCharByOrder(1).name).transform.localPosition = new Vector3(-380, -15, 0);
                GameObject.Find(GameData.GetCharByOrder(2).name).transform.localPosition = new Vector3(413, -15, 0);
                GameObject.Find(GameData.GetCharByOrder(1).name).transform.localScale = new Vector3(1, 1, 1);
                GameObject.Find(GameData.GetCharByOrder(2).name).transform.localScale = new Vector3(1, 1, 1);

                arrow[0].transform.localScale = close;
                arrow[1].transform.localPosition = new Vector3(28.69995f, -185.5f, 0);
                arrow[2].transform.localScale = close;

                break;
            case 3:
                GameObject.Find(GameData.GetCharByOrder(1).name).transform.localPosition = new Vector3(-506, -15, 0);
                GameObject.Find(GameData.GetCharByOrder(2).name).transform.localPosition = new Vector3(33, -15, 0);
                GameObject.Find(GameData.GetCharByOrder(3).name).transform.localPosition = new Vector3(576, -15, 0);
                GameObject.Find(GameData.GetCharByOrder(1).name).transform.localScale = open;
                GameObject.Find(GameData.GetCharByOrder(2).name).transform.localScale = open;
                GameObject.Find(GameData.GetCharByOrder(3).name).transform.localScale = open;

                arrow[0].transform.localPosition = new Vector3(-232, -185.5f, 0);
                arrow[1].transform.localPosition = new Vector3(311.69995f, -185.5f, 0);
                arrow[2].transform.localScale = close;

                break;
            case 4:
                GameObject.Find(GameData.GetCharByOrder(1).name).transform.localPosition = new Vector3(-789, -15, 0);
                GameObject.Find(GameData.GetCharByOrder(2).name).transform.localPosition = new Vector3(-250, -15, 0);
                GameObject.Find(GameData.GetCharByOrder(3).name).transform.localPosition = new Vector3(293, -15, 0);
                GameObject.Find(GameData.GetCharByOrder(4).name).transform.localPosition = new Vector3(829, -15, 0);
                GameObject.Find(GameData.GetCharByOrder(1).name).transform.localScale = open;
                GameObject.Find(GameData.GetCharByOrder(2).name).transform.localScale = open;
                GameObject.Find(GameData.GetCharByOrder(3).name).transform.localScale = open;
                GameObject.Find(GameData.GetCharByOrder(4).name).transform.localScale = open;

                arrow[0].transform.localPosition = new Vector3(-515, -185.5f, 0);
                arrow[1].transform.localPosition = new Vector3(28.69995f, -185.5f, 0);
                arrow[2].transform.localPosition = new Vector3(570, -185.5f, 0);
                break;
        }
    }


	// Update is called once per frame
	void Update () {
		
	}
}
