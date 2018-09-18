using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 첫번째 플레이어가 위치가 0일 때만 동작
 */
public class AllCandyPopUp : MonoBehaviour {
    //시간
    float currentTime = 0;

    //팝업 노출 시간
    public float duration = 3;

	// Use this for initialization
	void Start () {
        if (GameData.GetCharByOrder(1).position == 0) //첫번쨰 플레이어가 처음 위치이면,
        {
            OpenPopUp();
        }
    }
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime; //시간 흐르게 하기

        if (currentTime > duration)
        {
            ClosePopUp();
        }
	}

    public void OpenPopUp()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
    }
}
