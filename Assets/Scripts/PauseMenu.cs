using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    /**
     * 일시정지 메뉴 펼치기
     */
    public void OpenPauseMenu()
    {
        if (gameObject.transform.localScale.x < 1)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    /**
     * 일시정지 메뉴 닫기
     */
     public void ClosePauseMenu()
    {
        if (gameObject.transform.localScale.x > 0)
        {
            gameObject.transform.localScale = new Vector3(0, 1, 1);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
